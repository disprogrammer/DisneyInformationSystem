using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Managers.Admins;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Managers.Admins
{
    [ExcludeFromCodeCoverage, TestClass]
    public class DestinationsAndEntertainmentAdminManagerTests
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
            _outputString = null;
            _mockConsole = new Mock<IConsole>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("")]
        [DataRow("exit")]
        public void DestinationsAndEntertainmentAdminManager_UpdateCore_WhenAdminDecidesToExit_ShouldShowCorrectMessage(string input)
        {
            // Arrange
            var consoleInput = new[] { input };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var destinationsAndEntertainmentAdminManager = new DestinationsAndEntertainmentAdminManager(_mockConsole.Object);

            // Act
            destinationsAndEntertainmentAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "Thank you for your contributions to the Disney Information System.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}