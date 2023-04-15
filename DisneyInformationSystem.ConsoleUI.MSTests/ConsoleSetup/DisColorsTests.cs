using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.ConsoleSetup
{
    /// <summary>
    /// <see cref="DisColors"/> tests.
    /// </summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class DisColorsTests
    {
        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_Blue_WhenCallingProperty_ShouldSetConsoleColorToBlue()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.Blue;

            // Act
            var actualConsoleColor = DisColors.Blue;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_Cyan_WhenCallingProperty_ShouldSetConsoleColorToCyan()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.Cyan;

            // Act
            var actualConsoleColor = DisColors.Cyan;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_DarkGray_WhenCallingProperty_ShouldSetConsoleColorToDarkGray()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.DarkGray;

            // Act
            var actualConsoleColor = DisColors.DarkGray;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_Green_WhenCallingProperty_ShouldSetConsoleColorToGreen()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.Green;

            // Act
            var actualConsoleColor = DisColors.Green;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_Red_WhenCallingProperty_ShouldSetConsoleColorToRed()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.Red;

            // Act
            var actualConsoleColor = DisColors.Red;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_White_WhenCallingProperty_ShouldSetConsoleColorToWhite()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.White;

            // Act
            var actualConsoleColor = DisColors.White;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void DisColors_Yellow_WhenCallingProperty_ShouldSetConsoleColorToYellow()
        {
            // Arrange
            var expectedConsoleColor = ConsoleColor.Yellow;

            // Act
            var actualConsoleColor = DisColors.Yellow;

            // Assert
            Assert.AreEqual(expectedConsoleColor, actualConsoleColor, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}