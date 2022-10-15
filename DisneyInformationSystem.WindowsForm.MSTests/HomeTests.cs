using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.WindowsForm.MSTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class HomeTests
    {
        [TestMethod]
        public void HomeForm_Home_WhenCalled_ComponentsShouldBeSet()
        {
            // Arrange
            var expectedFormTitle = "Disney Information System - Main";

            // Act
        }
    }
}