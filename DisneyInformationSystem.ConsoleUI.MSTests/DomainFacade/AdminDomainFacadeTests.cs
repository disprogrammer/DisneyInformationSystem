using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.DomainFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.DomainFacade
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdminDomainFacadeTests
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
            _outputString = null;
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("TAD")]
        [DataRow("TVA")]
        public void AdminDomainFacade_Core_WhenAdminTypesAreDone_ShouldDisplayAppropriateMessages(string adminTypeCode)
        {
            // Arrange
            var listOfResorts = DatabaseMockers.MockSetupListOfResorts();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(listOfResorts);
            var adminDomainFacade = new AdminDomainFacade(_mockConsole.Object, adminTypeCode, _mockDatabaseReaderGateway.Object);

            var input = new[] { "", "y" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            adminDomainFacade.Core();

            // Assert
            StringAssert.Contains(_outputString, "Are you finished? (Y/N)", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Thank you for keeping our system up to date.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Returning to the main menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("TAD")]
        [DataRow("TVA")]
        public void AdminDomainFacade_Core_WhenAdminTypesAreNotDone_ShouldDisplayAppropriateMessages(string adminTypeCode)
        {
            // Arrange
            var listOfResorts = DatabaseMockers.MockSetupListOfResorts();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(listOfResorts);
            var adminDomainFacade = new AdminDomainFacade(_mockConsole.Object, adminTypeCode, _mockDatabaseReaderGateway.Object);

            var input = new[] { "", "n", "", "y" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            adminDomainFacade.Core();

            // Assert
            StringAssert.Contains(_outputString, "Returning you to your admin menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminDomainFacade_Core_WhenResortAdminTypeIsDone_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var listOfResorts = DatabaseMockers.MockSetupListOfResorts();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(listOfResorts);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortByName("Walt Disney")).Returns(listOfResorts[0]);
            var adminDomainFacade = new AdminDomainFacade(_mockConsole.Object, "WDW", _mockDatabaseReaderGateway.Object);

            var input = new[] { "y", "", "y" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            adminDomainFacade.Core();

            // Assert
            StringAssert.Contains(_outputString, "Thank you for keeping our system up to date.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AdminDomainFacade_Core_WhenResortAdminTypeIsNotDone_ShouldDisplayAppropriateMessages()
        {
            // Arrange
            var listOfResorts = DatabaseMockers.MockSetupListOfResorts();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(listOfResorts);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortByName("Walt Disney")).Returns(listOfResorts[0]);
            var adminDomainFacade = new AdminDomainFacade(_mockConsole.Object, "WDW", _mockDatabaseReaderGateway.Object);

            var input = new[] { "y", "", "n", "y", "", "y" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            adminDomainFacade.Core();

            // Assert
            StringAssert.Contains(_outputString, "Returning you to your admin menu...", ConsoleUiTestHelper.ExpectStringInOutput);
        }
    }
}