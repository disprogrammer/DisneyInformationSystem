using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Registrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Registrations
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UserRegistrationTests
    {
        /// <summary>
        /// Mock of the console interface.
        /// </summary>
        private Mock<IConsole> _mockConsole;

        /// <summary>
        /// Mock of the database reader gateway interface.
        /// </summary>
        private Mock<IDatabaseReaderGateway> _mockDatabaseReaderGateway;

        /// <summary>
        /// Mock of the database writer gateway interface.
        /// </summary>
        private Mock<IDatabaseWriterGateway> _mockDatabaseWriterGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _mockConsole = new Mock<IConsole>();
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenNotProvidedWithFirstName_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenLastNameIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "James", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenPhoneNumberIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "James", "McKinney", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("123456789")]
        [DataRow("1234fgt789")]
        public void UserRegistration_Register_WhenPhoneNumberIsInvalid_ShouldThrowPhoneNumberInvalidException(string phoneNumber)
        {
            // Arrange
            var input = new[] { "James", "McKinney", phoneNumber };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenEmailAddressIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            var input = new[] { "James", "McKinney", "1234567890", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenEmailAddressIsInvalid_ShouldThrowAddressSignNotFoundException()
        {
            // Arrange
            var input = new[] { "James", "McKinney", "1234567890", "wdwgmail.com" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenEmailAddressAlreadyExists_ShouldThrowEmailAlreadyExistsException()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfUsers()).Returns(DatabaseMockers.MockSetupListOfUsers);

            var input = new[] { "Magic", "Kingdom", "1234567890", "test@test.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenPasswordIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfUsers()).Returns(DatabaseMockers.MockSetupListOfUsers);

            var input = new[] { "Magic", "Kingdom", "1234567890", "epcotcenter@gmail.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenHomeAddressIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfUsers()).Returns(DatabaseMockers.MockSetupListOfUsers);

            var input = new[] { "Magic", "Kingdom", "1234567890", "epcotcenter@gmail.com", "HollywoodStudios", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void UserRegistration_Register_WhenProvidedWithAllCorrectInformation_ShouldReturnUser()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfUsers()).Returns(DatabaseMockers.MockSetupListOfUsers);

            var firstName = "Magic";
            var lastName = "Kingdom";
            var phoneNumber = "1234567890";
            var email = "epcotcenter@gmail.com";
            var password = SecurePasswordHasher.Hash("HollywoodStudios");
            var address = "98 Animal Kingdom Drive, Orlando, FL 12345";

            var input = new[] { firstName, lastName, phoneNumber, email, "HollywoodStudios", address };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var user = new User(It.IsAny<string>(), firstName, lastName, phoneNumber, email, password, address);
            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.InsertNewUser(user));

            var userRegistration = new UserRegistration(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            var value = userRegistration.Register();

            // Assert
            Assert.IsNotNull(value, AssertMessage.ExpectNotNullValue);
        }
    }
}