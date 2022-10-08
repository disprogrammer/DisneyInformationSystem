using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.ConsoleSetup.Interfaces
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExtensionMethodsForIConsoleTests
    {
        /// <summary>
        /// Output string.
        /// </summary>
        private string _outputString;

        /// <summary>
        /// Mocks the console interface.
        /// </summary>
        private Mock<IConsole> _mockConsole;

        [TestInitialize]
        public void Initialize()
        {
            _outputString = null;
            _mockConsole = new Mock<IConsole>();
            _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ExtensionMethodsForIConsole_Prompt_WhenProvidedWithIConsoleObjectAndString_ShouldReturnUserInput()
        {
            // Arrange
            var consoleInput = new[] { "Explore" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            // Act
            var input = _mockConsole.Object.Prompt("Explore, Book, List, Music, Games, Sports, Admin, or Exit: ");

            // Act
            StringAssert.Contains(_outputString, "Explore, Book, List, Music, Games, Sports, Admin, or Exit:", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.AreEqual("Explore", input, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}