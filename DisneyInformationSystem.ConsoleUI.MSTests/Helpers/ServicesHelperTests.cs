using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        public void ServicesHelper_RetrieveOperatingValue_WhenProvidingValue_ShouldReturnCorrectBoolean(string input, bool expectedOutput)
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
        public void ServicesHelper_RetrieveOpeningDate_WhenInputIsValid_ShouldReturnDate()
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
        public void ServicesHelper_RetrieveNumber_WhenInputIsNotProvided_ShouldReturnZero()
        {
            // Arrange
            var expectedValue = 0;
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var actualValue = servicesHelper.RetrieveNumber("Provide number: ");

            // Assert
            Assert.AreEqual(expectedValue, actualValue, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_RetrieveNumber_WhenInputIsProvided_ShouldReturnNumber()
        {
            // Arrange
            var expectedValue = 4;
            var input = new[] { "4" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var actualValue = servicesHelper.RetrieveNumber("Provide number: ");

            // Assert
            Assert.AreEqual(expectedValue, actualValue, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [ExpectedException(typeof(FormatException))]
        public void ServicesHelper_RetrieveNumber_WhenInputIsNotNumber_ShouldThrowFormatException()
        {
            // Arrange
            var input = new[] { "WDW" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            _ = servicesHelper.RetrieveNumber("Provide number: ");

            // Assert
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_CheckIfFinishedOrExceptionIsThrown_WhenExceptionIsThrown_ShouldSetFinishedToFalseAndDisplayMessage()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.CheckIfFinishedOrExceptionIsThrown(true, false);

            // Assert
            StringAssert.Contains(_outputString, "Please try again", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_CheckIfFinishedOrExceptionIsThrown_WhenFinishedIsTrue_ShouldDisplayAppropriateMessage()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.CheckIfFinishedOrExceptionIsThrown(false, true);

            // Assert
            StringAssert.Contains(_outputString, "Thank you for your contributions to the Disney Information System!", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_RetrieveServiceDecision_WhenCalled_ShouldDisplayCorrectMessages()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.RetrieveServiceDecision("===== Some Title =====");

            // Assert
            StringAssert.Contains(_outputString, "Select an option below that you would like to do.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_UpdateRecord_WhenCalled_ShouldDisplayRecordPropertiesAndValues()
        {
            // Arrange
            var consoleInput = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);
            var themePark = DatabaseMockers.MockSetupListOfThemeParks().First();
            var propertiesAndValues = RecordHelper<ThemePark>.RetrieveListOfPropertiesAndValues(themePark);
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.UpdateRecord(themePark, propertiesAndValues);

            // Assert
            foreach (var propertyValuePair in propertiesAndValues)
            {
                StringAssert.Contains(_outputString, propertyValuePair, ConsoleUiTestHelper.ExpectStringInOutput);
            }
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_NotValidMessage_WhenCalled_ShouldPrintMessage()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.NotValidMessage("theme park");

            // Assert
            StringAssert.Contains(_outputString, "A valid theme park was not selected. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_InitialMessages_WhenCalled_ShouldPrintMessages()
        {
            // Arrange
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            servicesHelper.InitialMessages("Theme Park");

            // Assert
            StringAssert.Contains(_outputString, "===== Adding Theme Park =====", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Provide the information below to add a theme park to the database.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "If you do not provide any information for the data fields, you will lose your inputs.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_RetrieveTransportation_WhenNoInputIsProvided_ShouldOnlyReturnCar()
        {
            // Arrange
            var expectedReturnValue = "Car";
            var consoleInput = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var actualReturnValue = servicesHelper.RetrieveTransportation();

            // Assert
            Assert.AreEqual(expectedReturnValue, actualReturnValue, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ServicesHelper_RetrieveTransportation_WhenTransportationIsProvided_ShouldReturnAppropriateString()
        {
            // Arrange
            var expectedReturnValue = "Car, Monorail, Boat";
            var consoleInput = new[] { "Car, Monorail, Boat" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);
            var servicesHelper = new ServicesHelper(_mockConsole.Object);

            // Act
            var actualReturnValue = servicesHelper.RetrieveTransportation();

            // Assert
            Assert.AreEqual(expectedReturnValue, actualReturnValue, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}