using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
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

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
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

            var themeParkService = new ThemeParkService(_mockConsole.Object);

            // Act
            themeParkService.Options("WDW");

            // Assert
            StringAssert.Contains(_outputString, "This is not a valid option. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkService_Options_WhenOptionProvidedIsBlank_ShouldExitMethod()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object);

            // Act
            themeParkService.Options("WDW");

            // Assert
            Assert.IsTrue(!_outputString.Contains("This is not a valid option. Please try again."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("1", "===== Adding Theme Park =====")]
        [DataRow("2", "")]
        [DataRow("3", "")]
        public void ThemeParkService_Options_WhenOptionIsAdd_ShouldDisplayCorrectMessage(string input, string substring)
        {
            // Arrange
            var consoleInput = new[] { input, "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var themeParkService = new ThemeParkService(_mockConsole.Object);

            // Act
            themeParkService.Options("WDW");

            // Assert
            StringAssert.Contains(_outputString, substring, ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}