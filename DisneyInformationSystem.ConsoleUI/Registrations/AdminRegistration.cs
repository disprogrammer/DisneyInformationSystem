using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.Assessments;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Registrations
{
    /// <summary>
    /// Allows person to register as an admin.
    /// </summary>
    public class AdminRegistration : IRegister<Admin>
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
        /// Use of the <see cref="DatabaseReaderGateway"/> object.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Use of the <see cref="DatabaseWriterGateway"/> object.
        /// </summary>
        private readonly IDatabaseWriterGateway _databaseWriterGateway;

        /// <summary>
        /// Use of the <see cref="AssessmentManager"/> object.
        /// </summary>
        private readonly AssessmentManager _assessmentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRegistration"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        /// <param name="databaseReaderGateway">Instance of the <see cref="IDatabaseReaderGateway"/> interface.</param>
        /// <param name="databaseWriterGateway">Instance of the <see cref="IDatabaseWriterGateway"/> interface.</param>
        /// <param name="assessmentManager">Instance of the <see cref="AssessmentManager"/> object.</param>
        public AdminRegistration(
            IConsole console,
            IDatabaseReaderGateway databaseReaderGateway,
            IDatabaseWriterGateway databaseWriterGateway,
            AssessmentManager assessmentManager)
        {
            _console = console;
            _registrationHelper = new RegistrationHelper(_console);
            _databaseReaderGateway = databaseReaderGateway;
            _databaseWriterGateway = databaseWriterGateway;
            _assessmentManager = assessmentManager;
        }

        /// <inheritdoc />
        public Admin Register()
        {
            Admin admin = null;
            var adminDidNotFinishRegistration = false;
            var exceptionIsThrown = false;
            var listOfAdmins = _databaseReaderGateway.RetrieveListOfAdmins();
            var listOfAdminTypes = _databaseReaderGateway.RetrieveListOfAdminTypes();

            try
            {
                _console.ForegroundColor(DisColors.Cyan);
                _console.WriteLine("===== Admin Registration =====");

                _console.ForegroundColor(DisColors.DarkGray);
                _console.WriteLine("Below, give us your information to begin becoming a Dis Admin!");
                _console.WriteLine("After providing your information, you will be taking an assessment.");
                _console.WriteLine("In order to become the desired admin, you will need a 90% or above.");
                _console.WriteLine("Please allow time to complete the assessment.");
                _console.WriteLine("If no input is provided, you will be exited from registration.");

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("Admin Types:");
                foreach (var adminType in listOfAdminTypes)
                {
                    _console.WriteLine($"{adminType.ID} - {adminType.AdminType}");
                }

                var adminTypeDesired = _console.Prompt("Admin Type (code only): ").ToUpper().Trim();
                if (string.IsNullOrWhiteSpace(adminTypeDesired))
                {
                    adminDidNotFinishRegistration = true;
                    return null;
                }

                ExceptionHandler.CheckIfAdminTypeCodeIsValid(adminTypeDesired, listOfAdminTypes);

                var firstName = _console.Prompt("First Name: ");
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    adminDidNotFinishRegistration = true;
                    return null;
                }

                var lastName = _console.Prompt("Last Name: ");
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    adminDidNotFinishRegistration = true;
                    return null;
                }

                var emailAddress = _console.Prompt("Email Address: ");
                if (string.IsNullOrWhiteSpace(emailAddress))
                {
                    adminDidNotFinishRegistration = true;
                    return null;
                }

                ExceptionHandler.CheckIfEmailContainsAddressSign(emailAddress);
                var listOfEmailAddresses = listOfAdmins.Select(admin => admin.EmailAddress).ToList();
                ExceptionHandler.CheckThatEmailDoesNotAlreadyExist(emailAddress, listOfEmailAddresses);

                var password = _console.Prompt("Password: ");
                if (string.IsNullOrWhiteSpace(password))
                {
                    adminDidNotFinishRegistration = true;
                    return null;
                }

                var hashedPassword = SecurePasswordHasher.Hash(password);
                var listOfPins = listOfAdmins.Select(admin => admin.PIN).ToList();
                var generatedPin = RandomGenerator.RetrieveRandomGeneratedPin(listOfPins);
                var pin = $"A{generatedPin}";

                var adminTypeName = listOfAdminTypes.First(type => type.ID == adminTypeDesired.ToUpper().Trim()).AdminType;
                _assessmentManager.TakeAssessment(adminTypeName);
                var assessmentScore = _assessmentManager.AssessmentScore;

                if (assessmentScore >= 90)
                {
                    _console.ForegroundColor(DisColors.Green);
                    _console.WriteLine($"Congratulations! You passed the assessment with the score of {assessmentScore}.");
                    _console.WriteLine($"You are now a {adminTypeName}! To get started, you can sign in on the main menu.");
                    admin = new Admin(pin, adminTypeDesired, firstName, lastName, emailAddress, hashedPassword, _assessmentManager.AssessmentScore);
                    _databaseWriterGateway.Insert(admin);
                }
                else
                {
                    _console.ForegroundColor(DisColors.Red);
                    _console.WriteLine($"We are sorry. You did not pass the assessment. Your final score was {assessmentScore}.");
                    _console.WriteLine("Please come back and try again anytime.");
                }
            }
            catch (AdminTypeInvalidException exception)
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
                _registrationHelper.CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown(adminDidNotFinishRegistration, exceptionIsThrown);
            }

            return admin;
        }
    }
}