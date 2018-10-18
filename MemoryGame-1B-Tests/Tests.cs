using System.IO;
using System.Windows.Automation;
using NUnit;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace MemoryGame_1B_Tests
{
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// Creates a new Application
        /// </summary>
        /// <returns></returns>
        private static Application GetApplication() =>
            Application.Launch(Path.Combine(TestContext.CurrentContext.TestDirectory, "MemoryGame-1B.exe"));

        /// <summary>
        /// Creates new game test
        /// </summary>
        [Test]
        public void NewGame()
        {
            var application = GetApplication();
            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                var newGame = window.Get<Button>(SearchCriteria.ByText("New Game"));
                newGame.Click();
            }
        }

        /// <summary>
        /// Load game tests
        /// </summary>
        /// <param name="size"></param>
        [Test]
        [TestCase("4x4")]
        [TestCase("6x6")]
        public void LoadGame(string size)
        {
            var application = GetApplication();
            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                var loadGame = window.Get<Button>(SearchCriteria.ByText("Load Game"));

                loadGame.Click();

                using (var open = application.GetWindow("Memory Game", InitializeOption.NoCache))
                {
                    var fileNameTextBox =
                        open.Get<TextBox>(SearchCriteria.ByControlType(ControlType.Edit).AndByText("File name:"));
                    fileNameTextBox.Text = Path.Combine(Env.DocumentFolder,
                        $@"Github\MemoryGame-1B\MemoryGame-1B-Tests\MockData\SaveData\{size}.json");

                    var openButton =
                        open.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Open"));
                    openButton.Click();
                }
            }
        }
    }
}