using System.IO;
using System.Windows;
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
        public MainWindow() => InitializeComponent();

        private void NewGame(object sender, RoutedEventArgs e)
        {
            Content = new NewGame();
        }

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
            var value = File.ReadAllText(fileName);
            var json = JsonConvert.DeserializeObject(value);
        }

        private void HighScores(object sender, RoutedEventArgs e)
        {
            Content = new HighScores();
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            Content = new Help();
        }
    }
}
