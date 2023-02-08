using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Deleters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Deleters
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DeleterBaseTests
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
            _outputString = null;
            _mockConsole = new Mock<IConsole>();
            _mockDatabaseReaderGateway = new Mock<IDatabaseReaderGateway>();
            _mockDatabaseWriterGateway = new Mock<IDatabaseWriterGateway>();
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteThemeParks_WhenThereAreNoThemeParksToDelete_ShouldStillHaveTrueForThemeParkOperatingProperty()
        {
            // Arrange
            var themeParks = DatabaseMockers.MockSetupListOfThemeParks();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveThemeParksByResortID("DLR")).Returns(new List<ThemePark>());
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteThemeParks("DLR", DateTime.Today);

            // Assert
            Assert.IsFalse(ThemeParksAreSetToFalse(themeParks), AssertMessage.ExpectFalse);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteThemeParks_WhenThereAreThemeParksToDelete_ShouldSetFalseForOperatingProperty()
        {
            // Arrange
            var themeParks = DatabaseMockers.MockSetupListOfThemeParks();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveThemeParksByResortID("WDW")).Returns(themeParks);
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteThemeParks("WDW", DateTime.Today);

            // Assert
            Assert.IsTrue(ThemeParksAreSetToFalse(themeParks), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteThemePark_WhenProvidingWithThemePark_ShouldSetOperatingAndClosingDate()
        {
            // Arrange
            var themePark = DatabaseMockers.MockSetupListOfThemeParks().First();
            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Update(themePark));
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteThemePark(themePark, DateTime.Today);

            // Assert
            Assert.IsFalse(themePark.Operating, AssertMessage.ExpectFalse);
            Assert.AreEqual(DateTime.Today, themePark.ClosingDate, AssertMessage.ExpectValuesToBeEqual);
            StringAssert.Contains(
                _outputString,
                $"Theme Park: {themePark.ParkName} has successfully been updated.\n- The operating value is now False.",
                ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteResortHotels_WhenThereAreNoResortHotelsToDelete_ShouldStillHaveTrueForResortHotelOperatingProperty()
        {
            // Arrange
            var resortHotels = DatabaseMockers.MockSetupListOfResortHotels();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("DLR")).Returns(new List<ResortHotel>());
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteResortHotels("DLR", DateTime.Today);

            // Assert
            Assert.IsFalse(ResortHotelsAreSetToFalse(resortHotels), AssertMessage.ExpectFalse);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteResortHotels_WhenThereAreResortHotelsToDelete_ShouldSetFalseForOperatingProperty()
        {
            // Arrange
            var resortHotels = DatabaseMockers.MockSetupListOfResortHotels();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveResortHotelsByResortID("WDW")).Returns(resortHotels);
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteResortHotels("WDW", DateTime.Today);

            // Assert
            Assert.IsTrue(ResortHotelsAreSetToFalse(resortHotels), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteResortHotel_WhenProvidingWithResortHotel_ShouldSetOperatingAndClosingDate()
        {
            // Arrange
            var resortHotel = DatabaseMockers.MockSetupListOfResortHotels().First();
            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Update(resortHotel));
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteResortHotel(resortHotel, DateTime.Today);

            // Assert
            Assert.IsFalse(resortHotel.Operating, AssertMessage.ExpectFalse);
            Assert.AreEqual(DateTime.Today, resortHotel.ClosingDate, AssertMessage.ExpectValuesToBeEqual);
            StringAssert.Contains(
                _outputString,
                $"Resort Hotel: {resortHotel.ResortHotelName} has successfully been updated.\n- The operating value is now False.",
                ConsoleUiTestHelper.ExpectStringInOutput);
        }

        private static bool ThemeParksAreSetToFalse(List<ThemePark> themeParks)
        {
            foreach (var themePark in themeParks)
            {
                if (themePark.Operating)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ResortHotelsAreSetToFalse(List<ResortHotel> resortHotels)
        {
            foreach (var resortHotel in resortHotels)
            {
                if (resortHotel.Operating)
                {
                    return false;
                }
            }

            return true;
        }
    }
}