using System.IO;
using System.Windows;
using MemoryGame_1B.SaveData;
using MemoryGame_1B.Views;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MemoryGame_1B
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow() => InitializeComponent();

        /// <summary>
        /// Click listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame(object sender, RoutedEventArgs e)
        {
            Content = new NewGame(GridSize.Normal);
        }

        /// <summary>
        /// Click listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGame(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "Json documents (.json)|*.json"
            };

            var showDialog = openFileDialog.ShowDialog();
            if (showDialog != true) return;
            
            var fileName = openFileDialog.FileName;
            if (!File.Exists(fileName)) return;
            var saveData = SaveData.SaveData.Load(fileName);

            Content = new NewGame(saveData);
        }

        /// <summary>
        /// Click listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighScores(object sender, RoutedEventArgs e)
        {
            Content = new HighScores();
        }

        /// <summary>
        /// Click listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help(object sender, RoutedEventArgs e)
        {
            Content = new Help();
        }
    }
}
