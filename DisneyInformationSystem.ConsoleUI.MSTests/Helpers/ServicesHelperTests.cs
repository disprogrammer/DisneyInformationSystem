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
    public class ServicesHelperTests
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
        [DataRow("n", false)]
        [DataRow("", false)]
        [DataRow("1", false)]
        [DataRow("%", false)]
        [DataRow("y", true)]
        public void ServicseHelper_RetrieveOperatingValue_WhenProvidingValue_ShouldReturnCorrectBoolean(string input, bool expectedOutput)
        {
            // Arrange
            var consoleInput = new[] { input };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var actualOutput = servicesHelper.RetrieveOperatingValue("resort");

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [ExpectedException(typeof(FormatException))]
        public void ServicesHelper_RetrieveOpeningDate_WhenInputIsInvalid_ShouldThrowFormatException()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.RetrieveOpeningDate();

            // Assert
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicseHelper_RetrieveOpeningDate_WhenInputIsValid_ShouldReturnDate()
        {
            // Arrange
            var input = new[] { "1971-10-01" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var openingDate = servicesHelper.RetrieveOpeningDate();

            // Assert
            Assert.IsNotNull(openingDate, AssertMessage.ExpectValueReturn);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_RetrieveClosingDate_WhenOperating_ShouldReturnMaxDate()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var expectedClosingDate = servicesHelper.RetrieveClosingDate(true);

            // Assert
            Assert.AreEqual(expectedClosingDate, DateTime.MaxValue, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_RetrieveClosingDate_WhenNotOperatingAndValidClosingDate_ShouldReturnDate()
        {
            // Arrange
            var expectedClosingDate = new DateTime(2060, 12, 31);
            var closingDateString = "2060-12-31";
            var input = new[] { closingDateString };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var actualClosingDate = servicesHelper.RetrieveClosingDate(false);

            // Assert
            Assert.AreEqual(expectedClosingDate, actualClosingDate, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [ExpectedException(typeof(FormatException))]
        public void ServicesHelper_RetrieveClosingDate_WhenInputIsInvalid_ShouldThrowFormatException()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.RetrieveClosingDate(false);

            // Assert
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicseHelper_CheckIfFinsihedOrExceptionIsThrown_WhenExceptionIsThrown_ShouldSetFinishedToFalseAndDisplayMessage()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.CheckIfFinsihedOrExceptionIsThrown(true, false);

            // Assert
            StringAssert.Contains(_outputString, "Please try again", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicseHelper_CheckIfFinishedOrExceptionIsThrown_WhenFinsihedIsTrue_ShouldDisplayAppropriateMessage()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.CheckIfFinsihedOrExceptionIsThrown(false, true);

            // Assert
            StringAssert.Contains(_outputString, "Thank you for your contributions to the Disney Information System!", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}