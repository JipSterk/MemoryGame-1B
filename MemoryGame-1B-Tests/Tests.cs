using System;
using System.IO;
using System.Windows.Automation;
using System.Windows.Forms;
using NUnit.Framework;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using Application = TestStack.White.Application;
using Button = TestStack.White.UIItems.Button;
using ComboBox = TestStack.White.UIItems.ListBoxItems.ComboBox;
using TextBox = TestStack.White.UIItems.TextBox;

namespace MemoryGame_1B_Tests
{
    [TestFixture]
    [NonParallelizable]
    public class Tests
    {
        /// <summary>
        /// First player name
        /// </summary>
        private const string PlayerName1 = "Jip";

        /// <summary>
        /// Second player name
        /// </summary>
        private const string PlayerName2 = "Para";

        /// <summary>
        /// Save path
        /// </summary>
        private readonly string _savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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
                window.Get<Image>("NewGame").Click();
            }
        }

        /// <summary>
        /// Test the setup of the game
        /// </summary>
        [Test, Order(2)]
        public void InputNames()
        {
            var application = GetApplication();

            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                window.Get<Image>("NewGame").Click();

                window.Get<TextBox>("PlayerName1").Text = PlayerName1;
                window.Get<TextBox>("PlayerName2").Text = PlayerName2;
            }
        }

        [Test, Order(3)]
        [TestCase(0)]
        [TestCase(1)]
        public void LoadTheme(int index)
        {
            var application = GetApplication();

            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                window.Get<Image>("NewGame").Click();
                window.Get<ComboBox>("ChangeTheme").Select(index);

                window.Get<Image>("NewGame").Click();
            }
        }

        /// <summary>
        /// Test the saving of the layout
        /// </summary>
        [Test, Order(4)]
        [TestCase(0, "4x4")]
        [TestCase(1, "6x6")]
        public void SaveGame(int index, string size)
        {
            var application = GetApplication();

            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                window.Get<Image>("NewGame").Click();

                window.Get<TextBox>("PlayerName1").Text = PlayerName1;
                window.Get<TextBox>("PlayerName2").Text = PlayerName2;
                window.Get<ComboBox>("ChangeGridSize").Select(index);

                window.Get<Image>("NewGame").Click();

                window.Get<Image>("Save").Click();

                using (var open = application.GetWindow("Memory Game", InitializeOption.NoCache))
                {
                    window.Get(SearchCriteria.ByAutomationId("Item 202")).Click();
                    SendKeys.SendWait(_savePath);

                    window.Get(SearchCriteria.ByAutomationId("1001")).Click();
                    SendKeys.SendWait(size);

                    open.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Save")).Click();
                }
            }
        }


        /// <summary>
        /// Load game tests
        /// </summary>
        /// <param name="size"></param>
        [Test, Order(5)]
        [TestCase("4x4")]
        [TestCase("6x6")]
        public void LoadGame(string size)
        {
            var application = GetApplication();

            using (var window = application.GetWindow("Memory Game", InitializeOption.NoCache))
            {
                window.Get<Image>("LoadGame").Click();

                using (var open = application.GetWindow("Memory Game", InitializeOption.NoCache))
                {
                    window.Get(SearchCriteria.ByAutomationId("Item 202")).Click();
                    SendKeys.SendWait(_savePath);

                    window.Get(SearchCriteria.ByAutomationId("1148")).Click();
                    SendKeys.SendWait($"{PlayerName1}Vs{PlayerName2}{size}.json");

                    open.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Open")).Click();
                }
            }
        }
    }
}