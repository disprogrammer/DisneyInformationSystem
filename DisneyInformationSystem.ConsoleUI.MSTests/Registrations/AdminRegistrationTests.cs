using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.Assessments;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Registrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Registrations
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdminRegistrationTests
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

        /// <summary>
        /// Mock of the database reader gateway interface.
        /// </summary>
        private Mock<IDatabaseWriterGateway> _mockDatabaseWriterGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _mockConsole = new Mock<IConsole>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenNotProvidedWithAdminType_ShouldReturnNull()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object, 
                _mockDatabaseReaderGateway.Object, 
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("ASMR")]
        [DataRow("MK")]
        [DataRow("TA2")]
        [DataRow("ATM")]
        public void AdminRegistration_Register_WhenAdminTypeIsInvalid_ShouldThrowAdminTypeInvalidException(string adminType)
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { adminType };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenFirstNameIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenLastNameIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "James", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenEmailAddressIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "James", "McKinney", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenEmailAddressIsInvalid_ShouldThrowAddressSignNotFoundException()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "James", "McKinney", "magickingdom.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            _ = adminRegistration.Register();

            // Assert
            StringAssert.Contains(_outputString, "magickingdom.com does not contain '@'. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenEmailAddressAlreadyExists_ShouldThrowEmailAlreadyExistsException()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "James", "McKinney", "test@test.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            _ = adminRegistration.Register();

            // Assert
            StringAssert.Contains(_outputString, "Your inputted email address already exists.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenPasswordIsNotProvided_ShouldReturnNull()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "James", "McKinney", "epcot@center.com", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                new AssessmentManager(_mockConsole.Object, "./Assessments/"));

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenEverythingIsProvidedButDoesNotPassAssessment_ShouldReturnNull()
        {
            // Arrange
            SetUpDatabaseReaderGateway();
            var input = new[] { "TAD", "James", "McKinney", "epcot@center.com", "WorldNature", "Orlando, FL", "5" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var assessmentManager = new AssessmentManager(_mockConsole.Object, "./Assessments/");
            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                assessmentManager);

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNull(value, AssertMessage.ExpectNullValue);
            StringAssert.Contains(
                _outputString,
                $"We are sorry. You did not pass the assessment. Your final score was {assessmentManager.AssessmentScore}.",
                ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Please come back and try again anytime.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminRegistration_Register_WhenEverythingIsProvidedAndDoesPassAssessment_ShouldReturnAdmin()
        {
            // Arrange
            SetUpDatabaseReaderGateway();

            var adminTypeCode = "TAD";
            var firstName = "James";
            var lastName = "McKinney";
            var email = "epcot@center.com";
            var password = SecurePasswordHasher.Hash("Password");

            var input = new[] { adminTypeCode, firstName, lastName, email, password, "Orlando, FL", "4" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var admin = new Admin(It.IsAny<string>(), adminTypeCode, firstName, lastName, email, password, It.IsAny<int>());
            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Insert(admin));

            var assessmentManager = new AssessmentManager(_mockConsole.Object, "./Assessments/");
            var adminRegistration = new AdminRegistration(
                _mockConsole.Object,
                _mockDatabaseReaderGateway.Object,
                _mockDatabaseWriterGateway.Object,
                assessmentManager);

            // Act
            var value = adminRegistration.Register();

            // Assert
            Assert.IsNotNull(value, AssertMessage.ExpectNotNullValue);
            StringAssert.Contains(_outputString, "Congratulations!", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        /// <summary>
        /// Sets up the database reader gateway.
        /// </summary>
        private void SetUpDatabaseReaderGateway()
        {
            var listOfAdminTypes = new List<AdminTypes>
            {
                new AdminTypes("TAD", "Top Admin")
            };

            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfAdmins()).Returns(DatabaseMockers.MockSetupListOfAdmins);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfAdminTypes()).Returns(listOfAdminTypes);
        }
    }
}