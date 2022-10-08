using DisneyInformationSystem.ConsoleUI.ConsoleSetup;
using DisneyInformationSystem.ConsoleUI.ConsoleSetup.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DisneyInformationSystem.ConsoleUI.Assessments
{
    /// <summary>
    /// Assessment Manager class.
    /// </summary>
    public class AssessmentManager
    {
        /// <summary>
        /// Use of the <see cref="IConsole"/> interface.
        /// </summary>
        private readonly IConsole _console;

        /// <summary>
        /// The path to the assessment files.
        /// </summary>
        private readonly string _assessmentFilesPath;

        /// <summary>
        /// Gets or sets the assessment score.
        /// </summary>
        public int AssessmentScore { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="AssessmentManager"/>.
        /// </summary>
        /// <param name="console">Console interface object.</param>
        /// <param name="assessmentFilesPath">Assessment files path.</param>
        public AssessmentManager(IConsole console, string assessmentFilesPath)
        {
            _console = console;
            _assessmentFilesPath = assessmentFilesPath;
        }

        /// <summary>
        /// Allows a registering person for an admin to take an assessment.
        /// </summary>
        /// <param name="adminType">Admin type.</param>
        public void TakeAssessment(string adminType)
        {
            var finished = false;
            while (!finished)
            {
                _console.ForegroundColor(DisColors.Cyan);
                _console.WriteLine("\n===== Assessment =====");

                _console.ForegroundColor(DisColors.DarkGray);
                _console.WriteLine("Below you will take your assessment. Please allow time to complete.");
                _console.WriteLine("If an answer is not provided, the answer will be counted as wrong.");
                _console.WriteLine("Relax....and good luck!");

                var assessmentFiles = Directory.GetFiles(_assessmentFilesPath);
                if (assessmentFiles.Any())
                {
                    RetrieveFileAndReadLinesForUserToCompleteAssessment(adminType, assessmentFiles);
                    finished = true;
                }
            }
        }

        /// <summary>
        /// Retrieves the correct file for the admin type and allows user to take the assessment.
        /// </summary>
        /// <param name="adminType">Admin type.</param>
        /// <param name="assessmentFiles">Array of assessment files.</param>
        private void RetrieveFileAndReadLinesForUserToCompleteAssessment(string adminType, string[] assessmentFiles)
        {
            var assessmentFilePath = assessmentFiles.FirstOrDefault(assessmentName => assessmentName.Contains(adminType));
            var lines = File.ReadLines(assessmentFilePath).ToList();
            decimal numberOfQuestions = lines.Count;
            decimal correctAnswers = 0;
            var questionCount = 1;

            foreach (var line in lines)
            {
                _console.ForegroundColor(DisColors.Blue);
                _console.WriteLine($"\n--- Question {questionCount} ---");

                var questionAndAnswer = line.Split("(");
                var question = questionAndAnswer[0];
                var answer = questionAndAnswer[1].Remove(questionAndAnswer[1].Length - 1).ToLower();

                _console.ForegroundColor(DisColors.White);
                _console.WriteLine(question);
                var userInput = _console.Prompt("> ").ToLower();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    questionCount++;
                    continue;
                }

                if (answer.Contains(userInput))
                {
                    correctAnswers++;
                    questionCount++;
                }
            }

            var score = correctAnswers / numberOfQuestions;
            AssessmentScore = Convert.ToInt32(score * 100);
        }
    }
}