using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Marvel.Api
{
    /// <summary>
    /// Util class.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Generates MD5 hash for a given input
        /// </summary>
        /// <param name="input">Input to be hashed</param>
        /// <returns>Input md5 digest</returns>
        public static string CalculateMd5Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = MD5.HashData(inputBytes);

            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the current unix timestamp
        /// </summary>
        public static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// Returns the description attribute of an enum
        /// </summary>
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString() ?? string.Empty);
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString() ?? string.Empty;
        }
    }
}