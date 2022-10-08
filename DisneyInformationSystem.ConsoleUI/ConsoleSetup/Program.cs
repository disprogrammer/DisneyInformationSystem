using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace DisneyInformationSystem.ConsoleUI.ConsoleSetup
{
    /// <summary>
    /// Program class. Class holds the Main method, that runs the console application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        /// <summary>
        /// RealConsole class implementing the IConsole interface.
        /// </summary>
        internal class RealConsole : IConsole
        {
            /// <inheritdoc />
            public void Clear()
            {
                Console.Clear();
            }

            /// <inheritdoc />
            public void ForegroundColor(ConsoleColor consoleColor)
            {
                Console.ForegroundColor = consoleColor;
            }

            /// <inheritdoc />
            public string ReadLine()
            {
                return Console.ReadLine();
            }

            /// <inheritdoc />
            public void Title(string titleString)
            {
                Console.Title = titleString;
            }

            /// <inheritdoc />
            public string TypeString(string stringLine)
            {
                foreach (var character in stringLine)
                {
                    Console.Write(character);
                    Thread.Sleep(25);
                }

                return stringLine;
            }

            /// <inheritdoc />
            public void Write(string message)
            {
                Console.Write(message);
            }

            /// <inheritdoc />
            public void WriteLine(string message)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Main method of the Disney Information System console application.
        /// </summary>
        private static void Main()
        {
            var program = new DisneyInformationSystemConsoleUi(new RealConsole());
            program.Run();
        }
    }
}