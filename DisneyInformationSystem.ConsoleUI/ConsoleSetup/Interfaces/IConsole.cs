using System;

namespace DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces
{
    /// <summary>
    /// Console interface.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Clears the console.
        /// </summary>
        void Clear();

        /// <summary>
        /// Sets the color of the text in the console application window.
        /// </summary>
        void ForegroundColor(ConsoleColor consoleColor);

        /// <summary>
        /// Reads the input from the console application window.
        /// </summary>
        /// <returns>User input string.</returns>
        string ReadLine();

        /// <summary>
        /// Sets the title of the console application window.
        /// </summary>
        /// <param name="titleString">String to set as the window title.</param>
        void Title(string titleString);

        /// <summary>
        /// Prints a string one character at a time.
        /// </summary>
        /// <param name="stringLine">String line to delay its characters.</param>
        /// <returns>The initial string line message.</returns>
        string TypeString(string stringLine);

        /// <summary>
        /// Prints message on application window.
        /// </summary>
        /// <param name="message">String on application window.</param>
        void Write(string message);

        /// <summary>
        /// Prints message on application window and then enters into a new line.
        /// </summary>
        /// <param name="message">String on application window.</param>
        void WriteLine(string message);
    }

    /// <summary>
    /// Extension methods for the <see cref="IConsole"/> interface.
    /// </summary>
    public static class ExtensionMethodsForIConsole
    {
        /// <summary>
        /// Prints string on console application window and prompts for a user input.
        /// </summary>
        /// <param name="console">Console interface object.</param>
        /// <param name="prompt">Message displayed on the console application window.</param>
        /// <returns>User string input.</returns>
        public static string Prompt(this IConsole console, string prompt)
        {
            console.Write(prompt);
            return console.ReadLine();
        }
    }
}