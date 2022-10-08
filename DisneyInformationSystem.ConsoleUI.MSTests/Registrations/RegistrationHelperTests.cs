using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Registrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Registrations
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RegistrationHelperTests
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
        /// Class to test.
        /// </summary>
        private RegistrationHelper _registrationHelper;

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _registrationHelper = new RegistrationHelper(_mockConsole.Object);
            _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void RegistrationHelper_CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown_WhenDidNotFinishRegisteringIsTrue_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            _registrationHelper.CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown(true, false);

            // Assert
            StringAssert.Contains(_outputString, "You did not finish your registration. All information will be lost.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Returning to main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void RegistrationHelper_CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown_WhenExceptionIsThrownIsTrue_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            _registrationHelper.CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown(false, true);

            // Assert
            StringAssert.Contains(_outputString, "An error has occurred. All information will be lost.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Returning to main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void RegistrationHelper_CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown_WhenAnExceptionIsNotThrownAndRegisteringIsCompleted_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            _registrationHelper.CheckIfRegistrationDidNotFinishOrIfExceptionWasThrown(false, false);

            // Assert
            StringAssert.Contains(
                _outputString,
                "You are now successfully registered! Thanks for joining! You will be signed in automatically.",
                ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}