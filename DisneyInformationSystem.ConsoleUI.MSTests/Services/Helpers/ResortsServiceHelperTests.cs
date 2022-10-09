using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Services.Helpers
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ResortsServiceHelperTests
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
        public void ResortsServiceHelper_AdditionalResortInformationOptions_WhenNoOptionIsSelected_ShouldExitTheMethod()
        {
            // Arrange
            var consoleInput = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortsServiceHelper = new ResortsServiceHelper(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First());

            // Act
            resortsServiceHelper.AdditionalResortInformationOptions();

            // Assert
            StringAssert.Contains(_outputString, "11. Guest Services", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsServiceHelper_AdditionalResortInformationOptions_WhenNoOptionIsInvalid_ShouldDisplayErrorMessage()
        {
            // Arrange
            var consoleInput = new[] { "99" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortsServiceHelper = new ResortsServiceHelper(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First());

            // Act
            resortsServiceHelper.AdditionalResortInformationOptions();

            // Assert
            StringAssert.Contains(_outputString, "This is not a valid option. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsServiceHelper_AdditionalResortInformationOptions_WhenThemeParkIsSelected_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var consoleInput = new[] { "1", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortsServiceHelper = new ResortsServiceHelper(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First());

            // Act
            resortsServiceHelper.AdditionalResortInformationOptions();

            // Assert
            StringAssert.Contains(_outputString, "===== Theme Park Service =====", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}