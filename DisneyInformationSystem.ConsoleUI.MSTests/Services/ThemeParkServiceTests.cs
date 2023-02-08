using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ThemeParkServiceTests
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
        /// Mock of the database writer gateway.
        /// </summary>
        private Mock<IDatabaseWriterGateway> _mockDatabaseWriterGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkService_Options_WhenOptionProvidedIsInvalid_ShouldDispalyMessage()
        {
            // Arrange
            var consoleInput = new[] { "9", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            themeParkService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "This is not a valid option. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkService_Options_WhenOptionProvidedIsBlank_ShouldExitMethod()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            themeParkService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            Assert.IsTrue(!_outputString.Contains("This is not a valid option. Please try again."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkService_Options_WhenOptionIsAdd_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "1", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfThemeParks()).Returns(DatabaseMockers.MockSetupListOfThemeParks());

            // Act
            themeParkService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "===== Adding Theme Park =====", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkService_Options_WhenOptionIsUpdateAndNotSelectingValidPark_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "2", "Vikings", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveThemeParksByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfThemeParks());

            // Act
            themeParkService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "A valid theme park was not selected. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("2", "Magic")]
        [DataRow("3", "Magic")]
        public void ThemeParkService_Options_WhenOptionIsUpdateOrDeleteAndSelectingValidPark_ShouldDisplayCorrectMessage(string option, string park)
        {
            // Arrange
            var consoleInput = new[] { option, park, "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveThemeParksByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfThemeParks());

            // Act
            themeParkService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "Select a theme park below.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkService_Options_WhenOptionIsDeleteAndNotSelectingValidPark_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "3", "Orlando", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveThemeParksByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfThemeParks());

            // Act
            themeParkService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "A valid theme park was not selected. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}