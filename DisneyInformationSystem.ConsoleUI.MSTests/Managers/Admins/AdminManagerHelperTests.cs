using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Managers.Admins;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Managers.Admins
{
    [ExcludeFromCodeCoverage, TestClass]
    public class AdminManagerHelperTests
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
        [DataRow(true)]
        [DataRow(false)]
        public void AdminManagerHelper_CheckThatAdminIsFinishedOrExceptionIsThrown_WhenBothAreFalseOrBothAreTrue_ShouldNotDisplayAnyConsoleMessages(bool booleanValue)
        {
            // Arrange
            var adminManagerHelper = new AdminManagerHelper(_mockConsole.Object);

            // Act
            adminManagerHelper.CheckThatAdminIsFinishedOrExceptionIsThrown(booleanValue, booleanValue);

            // Assert
            Assert.IsTrue(_outputString == null, AssertMessage.ExpectTrue);
            Assert.IsNull(_outputString, AssertMessage.ExpectNullValue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminManagerHelper_CheckThatAdminIsFinishedOrExceptionIsThrown_WhenFinished_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var adminManagerHelper = new AdminManagerHelper(_mockConsole.Object);

            // Act
            adminManagerHelper.CheckThatAdminIsFinishedOrExceptionIsThrown(false, true);

            // Assert
            StringAssert.Contains(_outputString, "Thank you for you your contributions to the Disney Information System.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Returning to the Admin menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminManagerHelper_CheckThatAdminIsFinishedOrExceptionIsThrown_WhenExceptionIsThrown_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var adminManagerHelper = new AdminManagerHelper(_mockConsole.Object);

            // Act
            adminManagerHelper.CheckThatAdminIsFinishedOrExceptionIsThrown(true, false);

            // Assert
            StringAssert.Contains(_outputString, "Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}