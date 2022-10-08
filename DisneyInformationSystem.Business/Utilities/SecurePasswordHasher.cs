using DisneyInformationSystem.Business.Exceptions.Technical;
using System;
using System.Security.Cryptography;

namespace DisneyInformationSystem.Business.Utilities
{
    /// <summary>
    /// Secures a user or admins password by hashing it.
    /// </summary>
    public static class SecurePasswordHasher
    {
        /// <summary>
        /// Hash Size.
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Salt size.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Gets password and hashes it.
        /// </summary>
        /// <param name="password">User's password</param>
        /// <returns>New hash string.</returns>
        public static string Hash(string password)
        {
            return HashString(password, 10000);
        }

        /// <summary>
        /// Verify that the password matches the hash password.
        /// </summary>
        /// <param name="password">User password string.</param>
        /// <param name="hashPassword">User hashed password string.</param>
        /// <returns>True if user's given password matches their hashed password; otherwise, false.</returns>
        public static bool Verify(string password, string hashPassword)
        {
            if (!IsHashSupported(hashPassword))
            {
                throw new HashedPasswordNotSupportedException("The hash type is not supported. It must contain $DIS$V1$.");
            }

            var splitHashString = hashPassword.Replace("$DIS$V1$", "").Split('$');
            var iterations = int.Parse(splitHashString[0]);
            var base64Hash = splitHashString[1];
            var hashBytes = Convert.FromBase64String(base64Hash);
            var salt = new byte[SaltSize];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);

            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the hash string is supported if it contains a certain string.
        /// </summary>
        /// <param name="hashString">Hash string</param>
        /// <returns>True if hash string contains partial string; otherwise, false.</returns>
        private static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$DIS$V1$");
        }

        /// <summary>
        /// Hashes a password.
        /// </summary>
        /// <param name="password">Password string.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>Hashed password.</returns>
        private static string HashString(string password, int iterations)
        {
            var salt = new byte[SaltSize];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return $"$DIS$V1${iterations}${base64Hash}";
        }
    }
}