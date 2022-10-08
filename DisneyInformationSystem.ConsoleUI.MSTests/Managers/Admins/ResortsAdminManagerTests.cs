using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Managers.Admins;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Managers.Admins
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ResortsAdminManagerTests
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
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("DLR", "Disneyland Resort")]
        [DataRow("DPA", "Paris")]
        [DataRow("HKR", "Hong")]
        [DataRow("SDR", "Shanghai")]
        [DataRow("TDR", "Tokyo")]
        [DataRow("WDW", "Walt Disney")]
        public void ResortsAdminManager_UpdateCore_WhenProvidedWithValidAdminType_ShouldTakeAdminToService(string adminTypeCode, string resortName)
        {
            // Arrange
            var matchingResort = ListOfResorts().First(resort => resort.ResortName.Contains(resortName));
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortByName(resortName)).Returns(matchingResort);
            var resortsAdminManager = new ResortsAdminManager(_mockConsole.Object, adminTypeCode, _mockDatabaseReaderGateway.Object);
            var input = new[] { "y", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            resortsAdminManager.UpdateCore();

            // Assert
            StringAssert.Contains(_outputString, "Updating resort information? (Y/N):", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsAdminManager_UpdateCore_WhenNotProvidedWithValidAdminType_ShouldNotTakeAdminToService()
        {
            // Arrange
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(DatabaseMockers.MockSetupListOfResorts());
            var resortsAdminManager = new ResortsAdminManager(_mockConsole.Object, "ABC", _mockDatabaseReaderGateway.Object);

            // Act
            resortsAdminManager.UpdateCore();

            // Assert
            Assert.IsTrue(!_outputString.Contains("Updating resort information? (Y/N):"), AssertMessage.ExpectTrue);
        }

        /// <summary>
        /// List of resorts for our mock database reader gateway.
        /// </summary>
        /// <returns>List of Resorts.</returns>
        private static List<Resort> ListOfResorts()
        {
            return new List<Resort>
            {
                new Resort("ABC", "Disneyland Resort", "123 Disneyland Way, Anaheim, CA", "123-456-7890", 2, 3, 50, 0, 1, true, new DateTime(), DateTime.MaxValue),
                new Resort("DEF", "Disneyland Paris Resort", "123 Paris Way, Paris, France", "123-456-7890", 2, 3, 50, 0, 1, true, new DateTime(), DateTime.MaxValue),
                new Resort("GHI", "Hong Kong Disneyland Resort", "123 Hong Kong Way, Hong Kong", "123-456-7890", 2, 3, 50, 0, 1, true, new DateTime(), DateTime.MaxValue),
                new Resort("JKL", "Shanghai Disney Resort", "123 Shanghai Way, Shanghai, China", "123-456-7890", 2, 3, 50, 0, 1, true, new DateTime(), DateTime.MaxValue),
                new Resort("MNO", "Tokyo Disney Resort", "123 Tokyo Way, Tokyo, China", "123-456-7890", 2, 3, 50, 0, 1, true, new DateTime(), DateTime.MaxValue),
                new Resort("PQR", "Walt Disney World Resort", "123 Walt Way, Orlando, FL", "123-456-7890", 2, 3, 50, 0, 1, true, new DateTime(), DateTime.MaxValue),
            };
        }
    }
}