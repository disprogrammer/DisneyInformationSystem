using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        /// <summary>
        /// Mock of the database writer gateway.
        /// </summary>
        private Mock<IDatabaseWriterGateway> _mockDatabaseWriterGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod]
        [DataRow("", "===== Resort Hotels Service =====")]
        [DataRow("4", "This is not a valid option. Please try again.")]
        public void ResortHotelsService_Options_WhenExitOrInvalid_ShouldDisplayAppropriateMessages(string option, string message)
        {
            // Arrange
            var consoleInput = new[] { option, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);

            var resortHotelsService = new ResortHotelsService(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortHotelsService.Options(DatabaseMockers.MockSetupListOfResorts().First());

            // Assert
            StringAssert.Contains(_outputString, message, ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}