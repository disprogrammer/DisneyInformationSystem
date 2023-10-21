using DisneyInformationSystem.Business.Exceptions.Technical;
using DisneyInformationSystem.Business.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.Business.MSTests.Utilities
{
    /// <summary>
    /// <see cref="SecurePasswordHasher"/> tests.
    /// </summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class SecurePasswordHasherTests
    {
        [TestMethod, TestCategory("Business Test")]
        public void SecurePasswordHasher_Hash_WhenProvidedPasswordString_ShouldHashThePasswordString()
        {
            // Arrange

            // Act
            var hashedPassword = SecurePasswordHasher.Hash("Waltdisney1971");

            // Assert
            StringAssert.Contains(hashedPassword, "$DIS$V1$", "We were expecting the substring to be in the hashed password, but it was not.");
        }

        [TestMethod, TestCategory("Business Test"), ExpectedException(typeof(HashedPasswordNotSupportedException))]
        public void SecurePasswordHasher_Verify_WhenHashPasswordDoesNotContainPrefixSubstring_ShouldThrowHashedPasswordNotSupportedException()
        {
            // Arrange

            // Act
            _ = SecurePasswordHasher.Verify("Waltdisney1971", "dafk34jnsdaiofy72fgnlakdfg");

            // Assert
        }

        [TestMethod, TestCategory("Business Test")]
        public void SecurePasswordHasher_Verify_WhenHashPasswordContainsPrefixSubstringAndMatchesPasswordString_ShouldReturnTrue()
        {
            // Arrange
            var password = "MagicKingdom1971";
            var hashPassword = SecurePasswordHasher.Hash(password);

            // Act
            var passwordsMatch = SecurePasswordHasher.Verify(password, hashPassword);

            // Assert
            Assert.IsTrue(passwordsMatch, AssertMessage.ExpectTrue);
        }

        [TestMethod, TestCategory("Business Test")]
        public void SecurePasswordHasher_Verify_WhenHashPasswordContainsPrefixSubstringAndDoesNotMatchPasswordString_ShouldReturnTrue()
        {
            // Arrange
            var password = "MagicKingdom1971";
            var hashPassword = SecurePasswordHasher.Hash(password);
            hashPassword = hashPassword[0..^1] + "2";

            // Act
            var passwordsMatch = SecurePasswordHasher.Verify(password, hashPassword);

            // Assert
            Assert.IsFalse(passwordsMatch, AssertMessage.ExpectFalse);
        }
    }
}