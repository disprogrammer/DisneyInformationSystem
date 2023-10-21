using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Helpers
{
    [TestClass, ExcludeFromCodeCoverage]
    public class SignInHelperTests
    {
        /// <summary>
        /// Output string.
        /// </summary>
        private string _outputString;

        /// <summary>
        /// Mock of the console interface.
        /// </summary>
        private Mock<IConsole> _mockConsole;

        /// <summary>
        /// Mock of the database reader gateway interface.
        /// </summary>
        private Mock<IDatabaseReaderGateway> _mockDatabaseReaderGateway;

        [TestInitialize]
        public void Initialize()
        {
            _outputString = null;
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenNotProvidedWithEmail_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
            StringAssert.Contains(_outputString, "===== Sign In =====", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Please provide your email and password, followed by if you are signing in as an admin or user.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Press [enter] to leave the sign in menu.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Email Address:", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenNotProvidedWithPassword_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "mckinneyjames1995@gmail.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
            StringAssert.Contains(_outputString, "Password:", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenEmailAddressDoesNotContainAddressSign_ShouldThrowAddressSignNotFoundException()
        {
            // Arrange
            var input = new[] { "MagicKingdomgmail.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
            StringAssert.Contains(_outputString, "MagicKingdomgmail.com does not contain '@'. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenNotProvidedWithAdminOrUserOption_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "mckinneyjames1995@gmail.com", "Waltdisney1995", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
            StringAssert.Contains(_outputString, "Admin or User:", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenInputIsInvalidForAdminOrUser_ShouldThrowInvalidSignInTypeException()
        {
            // Arrange
            var input = new[] { "mckinneyjames1995@gmail.com", "Waltdisney1995", "admin1", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
            StringAssert.Contains(_outputString, "admin1 is not valid. Must be Admin or User.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenAdmin_ShouldReturnThatPerson()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins()[0];
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveAdminByEmail(admin.EmailAddress)).Returns(DatabaseMockers.MockSetupListOfAdmins().First());

            var input = new[] { admin.EmailAddress, "TestPassword", "admin" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNotNull(value, AssertMessage.ExpectNotNullValue);
            Assert.IsTrue(value is Admin, AssertMessage.ExpectTrue);
            StringAssert.Contains(_outputString, $"Admin, {admin.FirstName} {admin.LastName}, was successfully signed in.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "You will be returned to the main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenAdminButNotFoundInDatabase_ShouldThrowEmailNotFoundException()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins()[0];
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveAdminByEmail(admin.EmailAddress)).Returns(DatabaseMockers.MockSetupListOfAdmins().First());

            var input = new[] { "abc@gmail.com", "TestPassword", "admin", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            _ = signInHelper.SignIn();

            // Assert
            StringAssert.Contains(_outputString, "abc@gmail.com was not found in our database. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenAdminAndPasswordIsNotCorrect_ShouldThrowInvalidPasswordException()
        {
            // Arrange
            var admin = DatabaseMockers.MockSetupListOfAdmins()[0];
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveAdminByEmail(admin.EmailAddress)).Returns(DatabaseMockers.MockSetupListOfAdmins().First());

            var input = new[] { admin.EmailAddress, "TestPassword1", "admin", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            _ = signInHelper.SignIn();

            // Assert
            StringAssert.Contains(_outputString, "TestPassword1 is invalid and does not match our records.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenUser_ShouldReturnThatPerson()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers()[0];
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveUserByEmail(user.EmailAddress)).Returns(DatabaseMockers.MockSetupListOfUsers().First());

            var input = new[] { user.EmailAddress, "TestPassword", "user" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            var value = signInHelper.SignIn();

            // Assert
            Assert.IsNotNull(value, AssertMessage.ExpectNotNullValue);
            Assert.IsTrue(value is User, AssertMessage.ExpectTrue);
            StringAssert.Contains(_outputString, $"User, {user.FirstName} {user.LastName}, was successfully signed in.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "You will be returned to the main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenUserButNotFoundInDatabase_ShouldThrowEmailNotFoundException()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers()[0];
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveUserByEmail(user.EmailAddress)).Returns(DatabaseMockers.MockSetupListOfUsers().First());

            var input = new[] { "abc@gmail.com", "TestPassword", "user", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            _ = signInHelper.SignIn();

            // Assert
            StringAssert.Contains(_outputString, "abc@gmail.com was not found in our database. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void SignInHelper_SignIn_WhenUserAndPasswordIsNotCorrect_ShouldThrowInvalidPasswordException()
        {
            // Arrange
            var user = DatabaseMockers.MockSetupListOfUsers()[0];
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveUserByEmail(user.EmailAddress)).Returns(DatabaseMockers.MockSetupListOfUsers().First());

            var input = new[] { user.EmailAddress, "TestPassword1", "user", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var signInHelper = new SignInHelper(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            _ = signInHelper.SignIn();

            // Assert
            StringAssert.Contains(_outputString, "TestPassword1 is invalid and does not match our records.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}