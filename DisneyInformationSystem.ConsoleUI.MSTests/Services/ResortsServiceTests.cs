using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
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
    public class ResortsServiceTests
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
        public void ResortsService_Add_WhenNotProvidingAcronym_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Thank you for your contributions to the Disney Information System!", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        [DataRow("WD1", "The acronym should consist of only letters.")]
        [DataRow("WD", "The acronym is less than or greater than three letters long.")]
        [DataRow("WDWR", "The acronym is less than or greater than three letters long.")]
        public void ResortsService_Add_WhenAcronymIsNotValid_ShouldThrowResortAcronymInvalidException(string input, string exceptionMessage)
        {
            // Arrange
            var consoleInput = new[] { input, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, exceptionMessage, ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenAcronymAlreadyExists_ShouldDisplayMessage()
        {
            var consoleInput = new[] { "WDW", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(consoleInput, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "The provided acronym is already used for another resort. Please try again.", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenResortNameIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Resort Name: ", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenAddressOfResortIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Address of Resort", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenPhoneNumberIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Phone Number: ", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenPhoneNumberIsInvalid_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Phone number cannot be more than 10 digits.", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenNumberOfThemeParksIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Theme Parks (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenNumberOfResortHotelsIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "2", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Resort Hotels (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenNumberOfPartnerHotelsIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "2", "3", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Partner Hotels (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenNumberOfWaterParksIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "2", "3", "40", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Water Parks (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenNumberOfEntertinamentVenuesIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "2", "3", "40", "0", "asdf", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Number of Entertainment Venues (number only):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenOperatingIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "2", "3", "40", "0", "1", "", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Is the resort in operation (Y/N)? NOTE: Not providing 'Y' will result in the field defaulting to FALSE.", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenOpeningDateIsNotProvided_ShouldNotAddResortToDatabase()
        {
            // Arrange
            var input = new[] { "DLR", "Disneyland Resort", "123 Disneyland Drive, Anaheim, CA 92802", "1234567890", "2", "3", "40", "0", "1", "y", "", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "Opening Date (YYYY-MM-DD):", ConsoleUiTestHelper.ExpectStringInOutput);
            Assert.IsTrue(!_outputString.Contains("The resort was added to the database successfully!"), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Add_WhenAllInformationIsProvided_ShouldAddResortToDatabase()
        {
            // Arrange
            var pin = "DLR";
            var resortName = "Disneyland Resort";
            var address = "123 Disneyland Drive, Anaheim, CA 92802";
            var phoneNumber = "123-456-7890";
            var themeParks = "2";
            var hotels = "3";
            var partner = "40";
            var water = "0";
            var entertainment = "1";
            var operating = "y";
            var opening = "1955-07-17";

            var input = new[] { pin, resortName, address, "1234567890", themeParks, hotels, partner, water, entertainment, operating, opening, "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);
            DatabaseReaderGatewaySetup();

            var resort = new Resort(
                pin,
                resortName,
                address,
                phoneNumber,
                Convert.ToInt32(themeParks),
                Convert.ToInt32(hotels),
                Convert.ToInt32(partner),
                Convert.ToInt32(water),
                Convert.ToInt32(entertainment),
                true,
                Convert.ToDateTime(opening),
                DateTime.MaxValue);

            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Insert(resort));

            var resortsService = new ResortsService(_mockConsole.Object, new Resort(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Add();

            // Assert
            StringAssert.Contains(_outputString, "The resort was added to the database successfully!", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Update_WhenUpdatingResort_ShouldDisplayResortInformation()
        {
            // Arrange
            var input = new[] { "y", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var resortsService = new ResortsService(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Update();

            // Assert
            StringAssert.Contains(_outputString, "=== Resort Information ===", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Update_WhenNotUpdatingResort_ShouldDisplayResortInformation()
        {
            // Arrange
            var input = new[] { "n", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var resortsService = new ResortsService(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Update();

            // Assert
            StringAssert.Contains(_outputString, "1. Theme Park", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Update_WhenSelectingIsInvalid_ShouldDisplayAppropriateMessage()
        {
            // Arrange
            var input = new[] { "ope" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            var resortsService = new ResortsService(_mockConsole.Object, DatabaseMockers.MockSetupListOfResorts().First(), _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Update();

            // Assert
            StringAssert.Contains(_outputString, "Yes or No was not inputted. No information was updated.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void ResortsService_Delete_WhenProvidedWithResort_ShouldSetOperatingToFalse()
        {
            // Arrange
            var resort = DatabaseMockers.MockSetupListOfResorts().First();
            var listOfThemeParks = DatabaseMockers.MockSetupListOfThemeParks();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortByPin(resort.PIN)).Returns(resort);
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfThemeParks()).Returns(listOfThemeParks);
            var resortsService = new ResortsService(_mockConsole.Object, resort, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            resortsService.Delete();

            // Assert
            StringAssert.Contains(_outputString, "Resort has successfully been updated. The operating value is now FALSE.", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        private void DatabaseReaderGatewaySetup()
        {
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfResorts()).Returns(DatabaseMockers.MockSetupListOfResorts());
        }
    }
}