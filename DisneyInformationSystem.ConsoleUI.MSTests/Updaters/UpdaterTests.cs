using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Updaters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Updaters
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UpdaterTests
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
        /// Mock of the database writer gateway interface.
        /// </summary>
        private Mock<IDatabaseWriterGateway> _mockDatabaseWriterGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void Updater_Update_WhenNotProvidingInputForProperty_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var consoleInput = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var updater = new Updater(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseWriterGateway.Object);

            // Act
            updater.Update();

            // Assert
            StringAssert.Contains(_outputString, "You did not provide a field name to update, so no changes were made to the resort.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void Updater_Update_WhenInputIsNotValid_ShouldThrowInvalidPropertyTypeException()
        {
            // Arrange
            var consoleInput = new[] { "Magic Kingdom", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var updater = new Updater(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseWriterGateway.Object);

            // Act
            updater.Update();

            // Assert
            StringAssert.Contains(_outputString, "Inputted field name MagicKingdom is not a field in the Resorts table.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void Updater_Update_WhenInputIsValidButDoesNotProvideNewValue_ShouldExitAndDisplayAppropriateMessage()
        {
            // Arrange
            var consoleInput = new[] { "resort name", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var updater = new Updater(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseWriterGateway.Object);

            // Act
            updater.Update();

            // Assert
            StringAssert.Contains(_outputString, "No input was provided to change the value of ResortName. No changes will be made.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void Updater_Update_WhenProvidingNewValue_ShouldUpdateResort()
        {
            // Arrange
            var consoleInput = new[] { "resort name", "Disney World Resort", "n", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var updater = new Updater(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseWriterGateway.Object);
            _mockDatabaseWriterGateway.Setup(gateway => gateway.Update(DatabaseMockers.MockSetupListOfResorts().First()));

            // Act
            updater.Update();

            // Assert
            StringAssert.Contains(_outputString, "Resort was successfully updated. ResortName new value is: Disney World Resort.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("y")]
        [DataRow("yes")]
        public void Updater_Update_WhenDecidingToUpdateAnotherValue_ShouldDisplayCorrectMessage(string input)
        {
            // Arrange
            var consoleInput = new[] { "resort name", "Disney World Resort", input, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var updater = new Updater(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseWriterGateway.Object);
            _mockDatabaseWriterGateway.Setup(gateway => gateway.Update(DatabaseMockers.MockSetupListOfResorts().First()));

            // Act
            updater.Update();

            // Assert
            StringAssert.Contains(_outputString, "Type the field name that you would like to update for it's value.", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}