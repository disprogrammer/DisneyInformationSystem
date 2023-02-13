using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Managers.Admins;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Managers.Admins
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TopAdminManagerTests
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
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenOptionIsNotValid_ShouldThrowInvalidConsoleTopAdminMainMenuStringException()
        {
            // Arrange
            var input = new[] { "99", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "'99' is not a valid option. The only valid options are 1-9.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("")]
        [DataRow("exit")]
        public void TopAdminManager_UpdateCore_WhenOptionIsToExit_ShouldExitMethodAndClass(string input)
        {
            // Arrange
            var consoleInput = new[] { input };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "Thank you for you your contributions to the Disney Information System.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenOptionIsToAddOrUpdateResortButDoesNotSpecify_ShouldDisplayAppropriateMessage()
        {
            // Arrange
            var input = new[] { "1", "magic kingdom", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "No selection was made to add or update a resort. You will be returned to your admin menu.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenOptionIsToAddOrUpdateResortAndSelectsToAdd_ShouldDisplayAppropriateOutput()
        {
            // Arrange
            var input = new[] { "1", "add", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "===== Adding Resort =====", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenOptionIsToAddOrUpdateResortAndSelectsToUpdateButNotAResort_ShouldDisplayAppropriateOutput()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(DatabaseMockers.MockSetupListOfResorts);
            var input = new[] { "1", "update", "", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "- Walt Disney World Resort", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenOptionIsToAddOrUpdateResortAndSelectsToUpdateAndResort_ShouldDisplayAppropriateOutput()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(DatabaseMockers.MockSetupListOfResorts);
            var input = new[] { "1", "update", "Walt Disney World", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "Updating resort information? (Y/N):", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingAResortThatDoesNotExistToUpdate_ShouldNotGoToResortsService()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(DatabaseMockers.MockSetupListOfResorts);
            var input = new[] { "1", "update", "Disneyland", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(!_outputString.Contains("Updating resort information? (Y/N):"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateOtherDestinations_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "2", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("2. Add or Update other destinations."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateMovieAndShows_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "3", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("3. Add or Update movies and/or shows."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateAssessments_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "4", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("4. Add or Update assessments."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateSports_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "5", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("5. Add or Update sports."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateAdmins_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "6", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("6. Add or Update admins."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateUsers_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "7", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("7. Add or Update users."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToAddOrUpdateMusic_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "8", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("8. Add or Update music."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToDeleteInformationButNotMakingAdditionalSelection_ShouldShowAppropriateMessage()
        {
            // Arrange
            var input = new[] { "9", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("9. Delete information."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToDeleteInformationAndSelectingInvalidOption_ShouldShowErrorMessage()
        {
            // Arrange
            var input = new[] { "9", "5", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(_outputString.Contains("This is not a valid decision. Please try again."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void TopAdminManager_UpdateCore_WhenSelectingToDeleteInformationAndSelectResort_ShouldSetOperatingToFalse()
        {
            // Arrange
            var waltDisneyWorldResort = "walt disney world resort";
            var input = new[] { "9", "1", waltDisneyWorldResort, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var listOfResorts = DatabaseMockers.MockSetupListOfResorts();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(listOfResorts);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveThemeParksByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfThemeParks());
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfResortHotels());
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortByPin("WDW")).Returns(listOfResorts.First());
            var topAdminManager = new TopAdminManager(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            topAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "Resort has successfully been updated. The operating value is now FALSE.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}