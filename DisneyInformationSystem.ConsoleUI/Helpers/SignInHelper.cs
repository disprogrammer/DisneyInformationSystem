using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System;
using System.Threading;

namespace DisneyInformationSystem.ConsoleUI.Helpers
{
    /// <summary>
    /// Helper class for registering and signing in.
    /// </summary>
    public class SignInHelper
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of <see cref="IDatabaseReaderGateway"/>.
        /// </summary>
        private readonly IDatabaseReaderGateway _databaseReaderGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInHelper"/> class.
        /// </summary>
        /// <param name="console">Instance of the <see cref="IConsole"/> interface.</param>
        /// <param name="databaseReaderGateway">Instance of the <see cref="IDatabaseReaderGateway"/> interface.</param>
        public SignInHelper(IConsole console, IDatabaseReaderGateway databaseReaderGateway)
        {
            _console = console;
            _databaseReaderGateway = databaseReaderGateway;
        }

        /// <summary>
        /// Allows person to sign in as an admin or user.
        /// </summary>
        /// <returns>Person signed in.</returns>
        public Person SignIn()
        {
            _console.ForegroundColor(DisColors.Cyan);
            _console.WriteLine("===== Sign In =====");

            _console.ForegroundColor(DisColors.DarkGray);
            _console.WriteLine("Please provide your email and password, followed by if you are signing in as an admin or user.");
            _console.WriteLine("Press [enter] to leave the sign in menu.");

            var successfullySignedIn = false;
            var exceptionIsThrown = false;
            Person personSigningIn = null;
            while (!successfullySignedIn)
            {
                try
                {
                    _console.ForegroundColor(DisColors.White);
                    var emailAddress = _console.Prompt("Email Address: ");
                    if (string.IsNullOrWhiteSpace(emailAddress))
                    {
                        return null;
                    }

                    ExceptionHandler.CheckIfEmailContainsAddressSign(emailAddress);

                    var password = _console.Prompt("Password: ");
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        return null;
                    }

                    var userAdmin = _console.Prompt("Admin or User: ").ToLower();
                    switch (userAdmin)
                    {
                        case "admin":
                            var adminSignIn = AdminSignIn(emailAddress, password);
                            personSigningIn = adminSignIn.Item1;
                            successfullySignedIn = adminSignIn.Item2;
                            break;

                        case "user":
                            var userSignIn = UserSignIn(emailAddress, password);
                            personSigningIn = userSignIn.Item1;
                            successfullySignedIn = userSignIn.Item2;
                            break;

                        case "":
                        case "exit":
                            return null;

                        default:
                            throw new InvalidSignInTypeException($"{userAdmin} is not valid. Must be Admin or User.");
                    }
                }
                catch (AddressSignNotFoundException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                catch (EmailNotFoundException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                catch (InvalidPasswordException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                catch (InvalidSignInTypeException exception)
                {
                    exceptionIsThrown = true;
                    ConsoleStringHelper.PrintExceptionMessage(_console, exception);
                }
                finally
                {
                    ChecksIfPersonSuccessfullySignedInOrIfExceptionWasThrown(successfullySignedIn, exceptionIsThrown, personSigningIn);
                }
            }

            return personSigningIn;
        }

        /// <summary>
        /// Signs in the admin.
        /// </summary>
        /// <param name="emailAddress">Email Address.</param>
        /// <param name="password">Password.</param>
        /// <returns>Admin.</returns>
        private Tuple<Admin, bool> AdminSignIn(string emailAddress, string password)
        {
            var admin = _databaseReaderGateway.RetrieveAdminByEmail(emailAddress);
            if (admin == null)
            {
                throw new EmailNotFoundException($"{emailAddress} was not found in our database. Please try again.");
            }

            var passwordsMatch = SecurePasswordHasher.Verify(password, admin.Password);
            if (!passwordsMatch)
            {
                throw new InvalidPasswordException($"{password} is invalid and does not match our records.");
            }

            return new Tuple<Admin, bool>(admin, true);
        }

        /// <summary>
        /// Signs in the user.
        /// </summary>
        /// <param name="emailAddress">Email Address.</param>
        /// <param name="password">Password.</param>
        /// <returns>User.</returns>
        private Tuple<User, bool> UserSignIn(string emailAddress, string password)
        {
            var user = _databaseReaderGateway.RetrieveUserByEmail(emailAddress);
            if (user == null)
            {
                throw new EmailNotFoundException($"{emailAddress} was not found in our database. Please try again.");
            }

            var passwordsMatch = SecurePasswordHasher.Verify(password, user.Password);
            if (!passwordsMatch)
            {
                throw new InvalidPasswordException($"{password} is invalid and does not match our records.");
            }

            return new Tuple<User, bool>(user, true);
        }

        /// <summary>
        /// Checks to see if person successfully logged in or if an exception was thrown.
        /// </summary>
        /// <param name="successfullySignedIn">Successfully signed in boolean.</param>
        /// <param name="exceptionIsThrown">Exception is thrown.</param>
        /// <param name="personSigningIn">Person signed in.</param>
        private void ChecksIfPersonSuccessfullySignedInOrIfExceptionWasThrown(bool successfullySignedIn, bool exceptionIsThrown, Person personSigningIn)
        {
            if (exceptionIsThrown)
            {
                successfullySignedIn = false;
                _console.WriteLine("Please try again.");
            }

            if (successfullySignedIn)
            {
                DisplayPersonSignedInMessage(personSigningIn);

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine("You will be returned to the main menu...");
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// Displays the signed in messaged for an admin or user.
        /// </summary>
        /// <param name="personSigningIn">Person signing in.</param>
        private void DisplayPersonSignedInMessage(Person personSigningIn)
        {
            _console.ForegroundColor(DisColors.Green);
            if (personSigningIn.PIN.StartsWith("A"))
            {
                Admin admin = (Admin)personSigningIn;
                _console.WriteLine($"{admin.GetType().Name}, {admin.FirstName} {admin.LastName}, was successfully signed in.");
            }
            else
            {
                User user = (User)personSigningIn;
                _console.WriteLine($"{user.GetType().Name}, {user.FirstName} {user.LastName}, was successfully signed in.");
            }
        }
    }
}