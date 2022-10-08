using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DisneyInformationSystem.ConsoleUI.MSTests
{
    /// <summary>
    /// Console user interface test helper class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ConsoleUiTestHelper
    {
        /// <summary>
        /// Expect string in output assert message.
        /// </summary>
        public const string ExpectStringInOutput = "We were expecting the string to be in the output, but it was not.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleUiTestHelper"/> class.
        /// </summary>
        protected ConsoleUiTestHelper() { }

        /// <summary>
        /// Gets the console input for the mock console to read.
        /// </summary>
        /// <param name="inputArray">Input array.</param>
        /// <param name="mockConsole">Mock console interface.</param>
        public static void SpecifyConsoleInput(IEnumerable<string> inputArray, Mock<IConsole> mockConsole)
        {
            var queue = new Queue<string>(inputArray);
            _ = mockConsole.Setup(console => console.ReadLine()).Returns(() => queue.Dequeue());
        }
    }
}