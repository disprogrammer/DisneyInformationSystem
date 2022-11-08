using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.Business.MSTests
{
    /// <summary>
    /// Custom asserts class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class CustomAsserts
    {
        /// <summary>
        /// Checks if the value is zero.
        /// </summary>
        /// <param name="_">Assert extension.</param>
        /// <param name="value">Value to check.</param>
        /// <exception cref="AssertFailedException">Assert failed exception.</exception>
        public static void ValueIsZero(this Assert _, int value)
        {
            if (value == 0)
            {
                return;
            }

            throw new AssertFailedException($"Expected value to be zero, but was {value}");
        }

        /// <summary>
        /// Checks if the value is a minimum date time.
        /// </summary>
        /// <param name="_">Assert extension.</param>
        /// <param name="value">Value to check.</param>
        /// <exception cref="AssertFailedException">Assert failed exception.</exception>
        public static void DateTimeIsMinimumValue(this Assert _, DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                return;
            }

            throw new AssertFailedException($"Expected value to be the minimum date value, but was {value}");
        }
    }
}