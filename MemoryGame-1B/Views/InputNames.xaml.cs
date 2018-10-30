using System;
using System.Windows;
using System.Windows.Controls;
using MemoryGame_1B.Managers;
using MemoryGame_1B.SaveData;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Interaction logic for InputNames.xaml
    /// </summary>
    public partial class InputNames
    {
        /// <summary>
        /// First players name
        /// </summary>
        private string _namePlayer1;

        /// <summary>
        /// Second players name
        /// </summary>
        private string _namePlayer2;

        /// <summary>
        /// The size of the grid
        /// </summary>
        private GridSize _gridSize;


        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public InputNames()
        {
            InitializeComponent();
            GridSize.ItemsSource = Enum.GetValues(typeof(GridSize));
            GridSize.SelectedItem = SaveData.GridSize.Normal;
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new Main();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_namePlayer1) || string.IsNullOrEmpty(_namePlayer2)) return;
            MainWindow.Instance.Content = new NewGame(_gridSize);
            GameManager.StartGame();
        }

        /// <summary>
        /// OnChangeListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeName(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;

             if (textBox == NamePlayer1)
            {
                _namePlayer1 = textBox.Text;
            }
            else if (textBox == NamePlayer2)
            {
                _namePlayer2 = textBox.Text;
            }
        }

        /// <summary>
        /// OnChangeListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeGridSize(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;

            _gridSize = (GridSize)comboBox.SelectedItem;
        }
    }
}