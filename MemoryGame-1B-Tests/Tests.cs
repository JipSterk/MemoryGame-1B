using System;
using System.IO;
using System.Reflection;
using System.Windows.Automation;
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
        [Test, Order(1)]
        public void NewGame()
        {
            var application = GetApplication();
            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                var newGame = window.Get<Image>("NewGame");
                newGame.Click();
            }
        }

        [Test, Order(2)]
        public void Save()
        {
            var application = GetApplication();

            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                var newGame = window.Get<Image>("NewGame");
                newGame.Click();

                var save = window.Get<Image>("Save");
                save.Click();
                
                using (var open = application.GetWindow("Memory Game", InitializeOption.NoCache))
                {
                    var fileNameTextBox =
                        open.Get<TextBox>(SearchCriteria.ByControlType(ControlType.Edit).AndByText("File name:"));
                    fileNameTextBox.Text =
                        $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jip\MemoryGame-1B\4x4.json";

                    var saveButton =
                        open.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Save"));
                    saveButton.Click();
                }
            }
        }

        /// <summary>
        /// Load game tests
        /// </summary>
        /// <param name="size"></param>
        [Test, Order(3)]
        [TestCase("4x4")]
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
                    fileNameTextBox.Text =
                        $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jip\MemoryGame-1B\4x4.json";

                    var openButton =
                        open.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Open"));
                    openButton.Click();
                }
            }
        }
    }
}