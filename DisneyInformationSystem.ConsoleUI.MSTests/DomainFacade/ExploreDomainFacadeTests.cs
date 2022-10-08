using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.DomainFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.ConsoleUI.MSTests.DomainFacade
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExploreDomainFacadeTests
    {
        /// <summary>
        /// Output string.
        /// </summary>
        private string _outputString;

        /// <summary>
        /// Title string.
        /// </summary>
        private string _titleStrings;

        /// <summary>
        /// Mock of the console interface.
        /// </summary>
        private Mock<IConsole> _mockConsole;

        [TestInitialize]
        public void Initialize()
        {
            _outputString = null;
            _titleStrings = null;
            _mockConsole = new Mock<IConsole>();
            _ = _mockConsole.Setup(console => console.Title(It.IsAny<string>())).Callback<string>(str => _titleStrings += str);
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("")]
        [DataRow("3")]
        [DataRow("exit")]
        public void ExploreDomainFacade_Core_WhenDoesNotMakeDecision_ShouldDisplayAppropriateMessages(string input)
        {
            // Arrange
            var consoleInput = new[] { input };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var exploreDomainFacade = new ExploreDomainFacade(_mockConsole.Object, "No one signed in.");

            // Act
            exploreDomainFacade.Core();

            // Assert
            StringAssert.Contains(_outputString, "===== Explore =====", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Explore from the many destinations to the vast entertainment!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Press [enter] or type 'exit' to return to the main menu.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Make a selection below of what you would like to do!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "We hope you had fun exploring around the Disney Company!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "You are now returning to the main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("1", "Disney Information System - Destinations; No one signed in.")]
        [DataRow("destinations", "Disney Information System - Destinations; No one signed in.")]
        [DataRow("2", "Disney Information System - Entertainment; No one signed in.")]
        [DataRow("entertainment", "Disney Information System - Entertainment; No one signed in.")]
        public void ExploreDomainFacade_Core_WhenChoosingValidOption_ShouldDisplayAppropriateStrings(string input, string titleString)
        {
            // Arrange
            var consoleInput = new[] { input, "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var exploreDomainFacade = new ExploreDomainFacade(_mockConsole.Object, "No one signed in.");

            // Act
            exploreDomainFacade.Core();

            // Assert
            StringAssert.Contains(_titleStrings, titleString, "We were expecting the string to be the title, but it was not.");
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ExploreDomainFacade_Core_WhenInputIsInvalid_ShouldThrowInvalidConsoleExploreMenuStringException()
        {
            // Arrange
            var input = new[] { "magic kingdom", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var exploreDomainFacade = new ExploreDomainFacade(_mockConsole.Object, "No one signed in.");

            // Act
            exploreDomainFacade.Core();

            // Assert
            StringAssert.Contains(_outputString, "Stack Trace:", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Exception Type: Invalid Console Explore Menu String Exception", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Exception Message: 'magic kingdom' is not a valid menu option. Must choose a valid option.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}