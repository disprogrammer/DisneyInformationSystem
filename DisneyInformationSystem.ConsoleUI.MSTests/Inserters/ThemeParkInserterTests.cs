using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Inserters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Inserters
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ThemeParkInserterTests
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
        /// Mock of the database writer gateway interface.
        /// </summary>
        private Mock<IDatabaseWriterGateway> _mockDatabaseWriterGateway;

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _ = _mockConsole.Setup(console => console.TypeString(It.IsAny<string>())).Callback<string>(str => _outputString += str);
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNotProvidedWithAcronym_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            Assert.IsTrue(!_outputString.Contains("Theme Park Name:"), AssertMessage.ExpectTrue);
            StringAssert.Contains(_outputString, "Theme Park Acronym (3 letters):", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenParkAcronymAlreadyExists_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "MKP", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            Assert.IsTrue(!_outputString.Contains("Theme Park Name:"), AssertMessage.ExpectTrue);
            StringAssert.Contains(_outputString, "The provided acronym is already used for another theme park. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("MK1", "The resort acronym should consist only letters.")]
        [DataRow("MK", "The resort acronym is less than or greater than three letters long.")]
        [DataRow("MKPP", "The resort acronym is less than or greater than three letters long.")]
        public void ThemeParkInserter_Add_WhenAcronymIsInvalid_ShouldNotAddThemeParkToDatabase(string input, string expectedOutputString)
        {
            // Arrange
            var consoleInput = new[] { input, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, expectedOutputString, ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Theme Park Name:"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenThemeParkNameIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Theme Park Name:", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Address of Theme Park:"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenAddressOfThemeParkIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Address of Theme Park:", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Phone Number:"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenPhoneNumberIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Phone Number:", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("For the entries of inputting numbers, 0 is defaulted if nothing is entered."), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNumberOfLandsIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Lands (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Number of Attractions (number only):"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNumberOfAttractionsIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Attractions (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Number of Shops (number only):"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNumberOfShopsIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "20", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Shops (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Number of Restaurants (number only):"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNumberOfRestaurantsIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "20", "30", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Restaurants (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Number of Tours (number only):"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNumberOfToursIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "20", "30", "40", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Tours (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("Number of Restrooms (number only):"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNumberOfRestroomsIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "20", "30", "40", "5", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Restrooms (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenOperatingIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "20", "30", "40", "5", "15", "", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            Assert.IsTrue(!_outputString.Contains("The theme park was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenOpeningDateIsNotProvided_ShouldNotAddThemeParkToDatabase()
        {
            // Arrange
            var input = new[] { "EPC", "EPCOT", "1982 Discover Way, Orlando, FL, 12345", "0987654321", "", "4", "20", "30", "40", "5", "15", "y", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            Assert.IsTrue(!_outputString.Contains("The theme park was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenNotProvidingAnyTransportationButEverythingElse_ShouldAddThemeParkToDatabase()
        {
            // Arrange
            var pin = "EPC";
            var parkName = "EPCOT";
            var address = "1982 Discover Way, Orlando, FL, 12345";
            var phoneNumber = "098-765-4321";
            var lands = "4";
            var attractions = "20";
            var shops = "30";
            var restaurants = "40";
            var tours = "5";
            var restrooms = "15";
            var operating = "y";
            var openingDate = "1982-10-01";

            var input = new[] { pin, parkName, address, "0987654321", "", lands, attractions, shops, restaurants, tours, restrooms, operating, openingDate, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themePark = new ThemePark(
                pin,
                "WDW",
                parkName,
                address,
                phoneNumber,
                "Car",
                Convert.ToInt32(lands),
                Convert.ToInt32(attractions),
                Convert.ToInt32(shops),
                Convert.ToInt32(restaurants),
                Convert.ToInt32(tours),
                Convert.ToInt32(restrooms),
                true,
                Convert.ToDateTime(openingDate),
                DateTime.MaxValue);

            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Insert(themePark));

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "The theme park was added to the database successfully!", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ThemeParkInserter_Add_WhenProvidingTransportationAndEverythingElse_ShouldAddThemeParkToDatabase()
        {
            // Arrange
            var pin = "EPC";
            var parkName = "EPCOT";
            var address = "1982 Discover Way, Orlando, FL, 12345";
            var phoneNumber = "098-765-4321";
            var transportation = "Car, Boat, Skyliner, Walk, Monorail";
            var lands = "4";
            var attractions = "20";
            var shops = "30";
            var restaurants = "40";
            var tours = "5";
            var restrooms = "15";
            var operating = "y";
            var openingDate = "1982-10-01";

            var input = new[] { pin, parkName, address, "0987654321", transportation, lands, attractions, shops, restaurants, tours, restrooms, operating, openingDate, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var themePark = new ThemePark(
                pin,
                "WDW",
                parkName,
                address,
                phoneNumber,
                transportation,
                Convert.ToInt32(lands),
                Convert.ToInt32(attractions),
                Convert.ToInt32(shops),
                Convert.ToInt32(restaurants),
                Convert.ToInt32(tours),
                Convert.ToInt32(restrooms),
                true,
                Convert.ToDateTime(openingDate),
                DateTime.MaxValue);

            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Insert(themePark));

            var themeParkInserter = new ThemeParkInserter(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object, "WDW");

            // Act
            themeParkInserter.Add();

            // Assert
            StringAssert.Contains(_outputString, "The theme park was added to the database successfully!", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        private void DatabaseReaderGatewaySetup()
        {
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfThemeParks()).Returns(DatabaseMockers.MockSetupListOfThemeParks());
        }
    }
}