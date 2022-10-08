using DisneyInformationSystem.Business.Utilities;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System;

namespace DisneyInformationSystem.ConsoleUI.Helpers
{
    /// <summary>
    /// Console String Helper class.
    /// </summary>
    public static class ConsoleStringHelper
    {
        /// <summary>
        /// Prints the exception message.
        /// </summary>
        /// <param name="exception">Exception being thrown.</param>
        public static void PrintExceptionMessage(IConsole console, Exception exception)
        {
            var exceptionType = StringHelper.ExceptionTypeStringSplit(exception);
            console.ForegroundColor(DisColors.Red);
            console.WriteLine($"Exception Type: {exceptionType}\n" +
                              $"Exception Message: {exception.Message}\n" +
                              $"Stack Trace: {exception.StackTrace}");
        }
    }
}