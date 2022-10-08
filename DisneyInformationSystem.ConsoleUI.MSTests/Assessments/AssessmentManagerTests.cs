using DisneyInformationSystem.ConsoleUI.Assessments;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Testing.Shared;

namespace DisneyInformationSystem.ConsoleUI.MSTests.Assessments
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AssessmentManagerTests
    {
        /// <summary>
        /// Output string.
        /// </summary>
        private string _outputString;

        /// <summary>
        /// Mock of the console interface.
        /// </summary>
        private Mock<IConsole> _mockConsole;

        /// <summary>
        /// Assessment files path.
        /// </summary>
        private readonly string assessmentFilesPath = "./Assessments/";

        [TestInitialize]
        public void Initialize()
        {
            _mockConsole = new Mock<IConsole>();
            _ = _mockConsole.Setup(console => console.WriteLine(It.IsAny<string>())).Callback<string>(str => _outputString += str + "\r\n");
            _ = _mockConsole.Setup(console => console.Write(It.IsAny<string>())).Callback<string>(str => _outputString += str);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AssessmentManager_TakeAssessment_WhenAnsweringAllQuestionsRight_ShouldDisplayCorrectMessagesAndPerfectAssessmentScore()
        {
            // Arrange
            var expectedAssessmentScore = 100;
            var assessmentManager = new AssessmentManager(_mockConsole.Object, assessmentFilesPath);
            var input = new[] { "Orlando", "4" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            assessmentManager.TakeAssessment("Top Admin");

            // Assert
            var actualAssessmentScore = assessmentManager.AssessmentScore;
            Assert.AreEqual(expectedAssessmentScore, actualAssessmentScore, AssertMessage.ExpectValuesToBeEqual);
            StringAssert.Contains(_outputString, "===== Assessment =====", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Below you will take your assessment. Please allow time to complete.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "If an answer is not provided, the answer will be counted as wrong.", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "Relax....and good luck!", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "--- Question 1 ---", ConsoleUiTestHelper.ExpectStringInOutput);
            StringAssert.Contains(_outputString, "--- Question 2 ---", ConsoleUiTestHelper.ExpectStringInOutput);
        }

        [TestMethod, TestCategory("Console User Interface Test")]
        public void AssessmentManager_TakeAssessment_WhenNotAnsweringOneQuestion_ShouldSetAssessmentScoreToFifty()
        {
            // Arrange
            var expectedAssessmentScore = 50;
            var assessmentManager = new AssessmentManager(_mockConsole.Object, assessmentFilesPath);
            var input = new[] { "Orlando", "" };
            ConsoleUiTestHelper.SpecifyConsoleInput(input, _mockConsole);

            // Act
            assessmentManager.TakeAssessment("Top Admin");

            // Assert
            var actualAssessmentScore = assessmentManager.AssessmentScore;
            Assert.AreEqual(expectedAssessmentScore, actualAssessmentScore, AssertMessage.ExpectValuesToBeEqual);
        }
    }
}