using DisneyInformationSystem.Business.Database.Gateways;
using DisneyInformationSystem.Business.Database.Records;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using DisneyInformationSystem.ConsoleUI.Deleters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfThemeParks()).Returns(themeParks);
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteThemeParks("DLR");

            // Assert
            Assert.IsFalse(ThemeParksAreSetToFalse(themeParks), AssertMessage.ExpectFalse);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteThemeParks_WhenThereAreThemeParksToDelete_ShouldSetFalseForOperatingProperty()
        {
            // Arrange
            var themeParks = DatabaseMockers.MockSetupListOfThemeParks();
            _ = _mockDatabaseReaderGateway.Setup(gateway => gateway.RetrieveListOfThemeParks()).Returns(themeParks);
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteThemeParks("WDW");

            // Assert
            Assert.IsTrue(ThemeParksAreSetToFalse(themeParks), AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DeleterBase_DeleteThemePark_WhenProvidingWithThemePark_ShouldSetOperatingPropertyToFalse()
        {
            // Arrange
            var themePark = DatabaseMockers.MockSetupListOfThemeParks().First();
            _ = _mockDatabaseWriterGateway.Setup(gateway => gateway.Update(themePark));
            var deleterBase = new DeleterBase(_mockConsole.Object, _mockDatabaseReaderGateway.Object, _mockDatabaseWriterGateway.Object);

            // Act
            deleterBase.DeleteThemePark(themePark);

            // Assert
            Assert.IsTrue(ThemeParkIsSetToFalse(themePark), AssertMessage.ExpectTrue);
            StringAssert.Contains(
                _outputString,
                $"Theme Park: {themePark.ParkName} has successfully been updated. The operating value is now False.",
                ConsoleUiTestHelper.ExpectStringInOutput);
        }

        /// <summary>
        /// Checks if the Operating property has been set or not for each theme park.
        /// </summary>
        /// <param name="themeParks">List of theme parks.</param>
        /// <returns>True if Operating has been set to False for each theme park; false otherwise.</returns>
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

        /// <summary>
        /// Checks if a theme park's Operating property has been set to False.
        /// </summary>
        /// <param name="themePark">Theme park.</param>
        /// <returns>True if the Operating property has been set to False; false otherwise.</returns>
        private static bool ThemeParkIsSetToFalse(ThemePark themePark)
        {
            if (themePark.Operating)
            {
                return false;
            }

            return true;
        }
    }
}