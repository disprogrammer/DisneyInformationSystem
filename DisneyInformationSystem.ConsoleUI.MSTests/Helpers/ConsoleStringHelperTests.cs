using DisneyInformationSystem.Business.Exceptions.Business;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Helpers
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConsoleStringHelperTests
    {
        /// <summary>
        /// Output string.
        /// </summary>
        private string _outputString;

        /// <summary>
        /// Title string.
        /// </summary>
        private ConsoleColor _foregroundColor;

        /// <summary>
        /// Mock of the console interface.
        /// </summary>
        private Mock<IConsole> _mockConsole;

        [TestInitialize]
        public void Initialize()
        {
            _outputString = null;
            _foregroundColor = ConsoleColor.White;
            _mockConsole = new Mock<IConsole>();
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.ForegroundColor(It.IsAny<ConsoleColor>())).Callback<ConsoleColor>(color => _foregroundColor = color);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ConsoleStringHelper_PrintExceptionMessage_WhenProvidedWithExceptionAndConsole_ShouldDisplayErrorMessage()
        {
            // Arrange
            var exception = new EmailNotFoundException("This is an exception message.");

            // Act
            ConsoleStringHelper.PrintExceptionMessage(_mockConsole.Object, exception);

            // Assert
            Assert.AreEqual(ConsoleColor.Red, _foregroundColor, AssertMessage.ExpectValuesToBeEqual);
            StringAssert.Contains(_outputString, "Exception Type: Email Not Found Exception", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Exception Message: This is an exception message.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Stack Trace", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}