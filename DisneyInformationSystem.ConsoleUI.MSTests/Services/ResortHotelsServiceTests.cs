using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ResortHotelsServiceTests
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
        /// Mock of the database reader gateway interface.
        /// </summary>
        private Mock<IDatabaseReaderGateway> _mockDatabaseReaderGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("", "===== Resort Hotels Service =====")]
        [DataRow("4", "This is not a valid option. Please try again.")]
        public void ResortHotelsService_Options_WhenExitOrInvalid_ShouldDisplayAppropriateMessages(string option, string message)
        {
            // Arrange
            var consoleInput = new[] { option, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelsService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            resortHotelsService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, message, ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortHotelsService_Options_WhenSelectingToAdd_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var consoleInput = new[] { "1", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelsService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);

            // Act
            resortHotelsService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "===== Adding Resort Hotel =====", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortHotelsService_Options_WhenOptionIsUpdateAndNotSelectingValidResortHotel_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "2", "Steelers", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfResortHotels());

            // Act
            resortHotelService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "A valid resort hotel was not selected. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortHotelsService_Options_WhenOptionIsUpdateAndSelectingValidResortHotel_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "2", "Wilderness", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfResortHotels());

            // Act
            resortHotelService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "Select a resort hotel below.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortHotelsService_Options_WhenOptionIsDeleteAndValidResortHotel_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "3", "Wilderness", DateTime.Today.ToString(), "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfResortHotels());

            // Act
            resortHotelService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "Select a resort hotel below.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortHotelsService_Options_WhenOptionIsDeleteAndNotValidResortHotel_ShouldDisplayCorrectMessage()
        {
            // Arrange
            var consoleInput = new[] { "3", "Orlando", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("WDW")).Returns(DatabaseMockers.MockSetupListOfResortHotels());

            // Act
            resortHotelService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, "A valid resort hotel was not selected. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}