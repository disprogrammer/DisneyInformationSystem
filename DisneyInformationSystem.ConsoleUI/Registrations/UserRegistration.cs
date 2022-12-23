using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using System.Linq;
using System.Text.RegularExpressions;

namespace DisneyInformationSystem.ConsoleUI.Registrations
{
    /// <summary>
    /// Allows person to register as a user.
    /// </summary>
    public partial class UserRegistration : IRegister<User>
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Use of the <see cref="RegistrationHelper"/> object.
        /// </summary>
        private readonly RegistrationHelper _registrationHelper;

        /// <summary>
        /// Use of the <see cref="IDatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="IDatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegistration"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        /// <param name="databaseReaderGateway">Instance of the <see cref="IDatabaseReaderGateway"/> interface.</param>
        /// <param name="databaseWriterGateway">Instance of the <see cref="IDatabaseWriterGateway"/> interface.</param>
        public UserRegistration(IConsole console, IDatabaseReaderGateway databaseReaderGateway, IDatabaseWriterGateway databaseWriterGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
            _registrationHelper = new RegistrationHelper(_console);
        }

        /// <inheritdoc />
        public User Register()
        {
            User user = null;
            var userDidNotFinishRegistration = false;
            var exceptionIsThrown = false;
            var listOfUsers = _databaseReaderGateway.RetrieveListOfUsers();

            try
            {
                _console.ForegroundColor(DisColors.Cyan);
                _console.WriteLine("===== User Registration =====");

                _console.ForegroundColor(DisColors.DarkGray);
                _console.WriteLine("Below, give us your information to create your FREE Disney Information System account!");
                _console.WriteLine("If no input is provided, you will be exited from registration.");

                _console.ForegroundColor(DisColors.White);
                var firstName = _console.Prompt("First Name: ");
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    userDidNotFinishRegistration = true;
                    return null;
                }

                var lastName = _console.Prompt("Last Name: ");
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    userDidNotFinishRegistration = true;
                    return null;
                }

                var phoneNumber = _console.Prompt("Phone Number (without dashes): ");
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    userDidNotFinishRegistration = true;
                    return null;
                }

                ExceptionHandler.CheckIfPhoneNumberIsValid(phoneNumber);
                var formattedPhoneNumber = PlaceDashesIntoPhoneNumber().Replace(phoneNumber, "$1-$2-$3");

                var emailAddress = _console.Prompt("Email Address: ");
                if (string.IsNullOrWhiteSpace(emailAddress))
                {
                    userDidNotFinishRegistration = true;
                    return null;
                }

                ExceptionHandler.CheckIfEmailContainsAddressSign(emailAddress);
                var listOfEmailAddresses = listOfUsers.Select(user => user.EmailAddress).ToList();
                ExceptionHandler.CheckThatEmailDoesNotAlreadyExist(emailAddress, listOfEmailAddresses);

                var password = _console.Prompt("Password: ");
                if (string.IsNullOrWhiteSpace(password))
                {
                    userDidNotFinishRegistration = true;
                    return null;
                }

                var hashedPassword = SecurePasswordHasher.Hash(password);

                var homeAddress = _console.Prompt("Home Address: ");
                if (string.IsNullOrWhiteSpace(homeAddress))
                {
                    userDidNotFinishRegistration = true;
                    return null;
                }

                var listOfPins = listOfUsers.Select(user => user.PIN).ToList();
                var generatedPin = RandomGenerator.RetrieveRandomGeneratedPin(listOfPins);
                var pin = $"U{generatedPin}";
                user = new User(pin, firstName, lastName, formattedPhoneNumber, emailAddress, hashedPassword, homeAddress);
                _databaseWriterGateway.Insert(user);
            }
            catch (PhoneNumberInvalidException exception)
            {
                exceptionIsThrown = true;
                ConsoleStringHelper.PrintExceptionMessage(_console, exception);
            }
            catch (AddressSignNotFoundException exception)
            {
                exceptionIsThrown = true;
                ConsoleStringHelper.PrintExceptionMessage(_console, exception);
            }
            catch (EmailAlreadyExistsException exception)
            {
                exceptionIsThrown = true;
                ConsoleStringHelper.PrintExceptionMessage(_console, exception);
            }
            finally
            {
                _registrationHelper.CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown(userDidNotFinishRegistration, exceptionIsThrown);
            }

            return user;
        }

        [GeneratedRegex("(\\d{3})(\\d{3})(\\d{4})")]
        private static partial Regex PlaceDashesIntoPhoneNumber();
    }
}