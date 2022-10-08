using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DisneyInformationSystemConsoleUiTests
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
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("")]
        [DataRow("exit")]
        public void DisneyInformationSystemConsoleUi_Run_WhenUserDoesNotMakeSelectionFromOptions_ShouldDisplayAppropriateMessages(string input)
        {
            // Arrange
            var consoleInput = new[] { input };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, "========== Welcome to the DIS Application! ==========", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Your home to all things Disney!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Press [enter] at any menu or input to go back to the previous option(s).", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Make a selection below of what you would like to do!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Type 'sign in' to sign into your account or 'register' to create a new account!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Explore, Book, List, Music, Games, Sports, Admin, or Exit:", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Thanks for exploring all things Disney! Come back real soon!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "This application will close in three seconds...", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_titleStrings, "Disney Information System - Home", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenUserSelectsAnInvalidOption_ShouldThrowInvalidConsoleMainMenuStringExceptionAndDisplayMessages()
        {
            // Arrange
            var consoleInput = new[] { "Magic Kingdom", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, "Exception Type: Invalid Console Main Menu String Exception", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, $"Exception Message:", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, $"Stack Trace:", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("explore", "Disney Information System - Explore Home; No one signed in")]
        [DataRow("book", "Disney Information System - Book Home; No one signed in")]
        [DataRow("list", "Disney Information System - List Home; No one signed in")]
        [DataRow("music", "Disney Information System - Music Home; No one signed in")]
        [DataRow("games", "Disney Information System - Games Home; No one signed in")]
        [DataRow("sports", "Disney Information System - Sports Home; No one signed in")]
        public void DisneyInformationSystemConsoleUi_Run_WhenInputGoesToDomainFacades_ShouldDisplayCorrectTitleString(string input, string title)
        {
            // Arrange
            var consoleInput = new[] { input, "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_titleStrings, title, "We were expecting the title to be in the titles, but it was not.");
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenThereIsNoAdminSignedIn_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var consoleInput = new[] { "admin", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, "There is no admin signed in. You can not continue any further.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Returning to the main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenSigningInWithSomeoneAlreadySignedIn_ShouldDisplayAppropriateMessage()
        {
            // Arrange
            var consoleInput = new[] { "sign in", "mckinneyjames1995@gmail.com", "Waltdisney1995", "admin", "sign in", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(
                _outputString,
                "A person is already signed in. Please log out before signing in a different person.",
                ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenSelectingAdminWhenAlreadySignedIn_ShouldDisplayCorrectMessages()
        {
            // Arrange
            var consoleInput = new[] { "sign in", "mckinneyjames1995@gmail.com", "Waltdisney1995", "admin", "admin", "", "y", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_titleStrings, "James McKinney", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("user")]
        [DataRow("admin")]
        public void DisneyInformationSystemConsoleUi_Run_WhenRegisteringButNotCompleting_ShouldDisplayAppropriateMessage(string registerString)
        {
            // Arrange
            var consoleInput = new[] { "register", registerString, "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, "You did not finish your registration. All information will be lost.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Returning to main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenNotProvidingOptionForRegistering_ShouldNotAddPersonToTitle()
        {
            // Arrange
            var consoleInput = new[] { "register", "", "book", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_titleStrings, "Disney Information System - Book Home;", "We were expecting the title in the list of titles, but it was not.");
            Assert.IsTrue(!_titleStrings.Contains('U'), AssertMessage.ExpectTrue);
            Assert.IsTrue(!_titleStrings.Contains('A'), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenRegisterInputIsInvalid_ShouldThrowInvalidRegisterTypeException()
        {
            // Arrange
            var consoleInput = new[] { "register", "admin1", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, "admin1 is not valid. Must be Admin or User.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisneyInformationSystemConsoleUi_Run_WhenLoggingOutWithNoOneSignedIn_ShouldDisplayMessage()
        {
            // Arrange
            var consoleInput = new[] { "log out", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, "No one is currently signed in to be able to log out.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("admin", "mckinneyjames1995@gmail.com", "Waltdisney1995", "James McKinney is being signed out.")]
        [DataRow("user", "mckinneyjames1995@gmail.com", "Waltdisney1995!", "James McKinney is being signed out.")]
        public void DisneyInformationSystemConsoleUi_Run_WhenLoggingOutAsAdminOrUser_ShouldDisplaySignedOutMessage(string personType, string email, string password, string signedOutMessage)
        {
            // Arrange
            var consoleInput = new[] { "sign in", email, password, personType, "log out", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var disneyInformationSystemConsoleUi = new DisneyInformationSystemConsoleUi(_mockConsole.Object);

            // Act
            disneyInformationSystemConsoleUi.Run();

            // Assert
            StringAssert.Contains(_outputString, signedOutMessage, ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}