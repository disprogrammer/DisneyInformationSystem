using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.WindowsForm.MSTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class HomeTests
    {
        [TestMethod, TestCategory("Windows Form Test")]
        public void HomeForm_Home_WhenDisposing_ShouldInitializeComponents()
        {
            // Arrange
            var expectedFormTitle = "Disney Information System - Home";
            var home = new Home();

            // Act
            home.Dispose();

            // Assert
            var actualFormTitle = home.Text;
            Assert.AreEqual(expectedFormTitle, actualFormTitle, AssertMessage.ExpectValuesToBeEqual);
        }

        [TestMethod, TestCategory("Windows Form Test")]
        public void HomeForm_Home_WhenNotDisposing_ShouldInitializeComponents()
        {
            // Arrange
            var expectedFormTitle = "Disney Information System - Home";

            // Act
            var home = new Home();

            // Assert
            var actualFormTitle = home.Text;
            Assert.AreEqual(expectedFormTitle, actualFormTitle, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}