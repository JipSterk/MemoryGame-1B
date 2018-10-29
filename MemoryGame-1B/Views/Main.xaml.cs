using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MemoryGame_1B.SaveData;
using Microsoft.Win32;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public Main() => InitializeComponent();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame(object sender, RoutedEventArgs e) =>
            MainWindow.Instance.Content = new NewGame(GridSize.Normal);

        /// <summary>
        /// OnClickListener
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

            MainWindow.Instance.Content = new NewGame(saveData);
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighScores(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new HighScores();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new Help();


        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Online(object sender, MouseButtonEventArgs e) => MainWindow.Instance.Content = new Online();
    }
}