using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MemoryGame_1B.Managers;
using MemoryGame_1B.SaveData;
using Microsoft.Win32;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page"/>
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame
    {
        /// <summary>
        /// The size of the grid
        /// </summary>
        private readonly GridSize _gridSize;

        /// <summary>
        /// The memoryGrid
        /// </summary>
        private readonly MemoryGrid _memoryGrid;

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        private NewGame()
        {
            InitializeComponent();

            GameManager.OnTurnChanged += ToggleTurn;
        }

        /// <summary>
        /// First players name
        /// </summary>
        private readonly string _namePlayer1;

        /// <summary>
        /// First players name
        /// </summary>
        private readonly string _namePlayer2;

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gridSize"></param>
        public NewGame(GridSize gridSize, string namePlayer1, string namePlayer2) : this()
        {
            _gridSize = gridSize;
            _memoryGrid = new MemoryGrid(Grid, _gridSize);
            _namePlayer1 = namePlayer1;
            _namePlayer2 = namePlayer2;
        }

        /// <summary>
        /// Toggles the turn
        /// </summary>
        private void ToggleTurn(Turn turn)
        {
            Player1.Dispatcher.Invoke(() =>
                Player1.Foreground = new SolidColorBrush(turn == Turn.Player1 ? Colors.Green : Colors.Black));
            Player2.Dispatcher.Invoke(() =>
                Player2.Foreground = new SolidColorBrush(turn == Turn.Player2 ? Colors.Green : Colors.Black));
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveData"></param>
        public NewGame(SaveData.SaveData saveData) : this()
        {
            var (turn, gridSize, cardData) = saveData;

            _memoryGrid = new MemoryGrid(Grid, gridSize, turn, cardData);
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save(object sender, RoutedEventArgs e)
        {
            var cardData = _gridSize == GridSize.Normal ? new CardData[4, 4] : new CardData[6, 6];

            for (var i = 0; i < _memoryGrid.CardData.GetLength(0); i++)
            {
                for (var j = 0; j < _memoryGrid.CardData.GetLength(1); j++)
                {
                    var (cardFrontUriSource, cardBackUriSource, turned, number) = _memoryGrid.CardData[i, j];
                    cardData[i, j] = new CardData(cardFrontUriSource, cardBackUriSource, turned, number);
                }
            }

            var saveData = new SaveData.SaveData(Turn.Player2, _gridSize, cardData);

            var saveFileDialog = new SaveFileDialog
            {
                FileName = "Player1VsPlayer2",
                DefaultExt = ".json",
                Filter = "Json documents (.json)|*.json"
            };

            var showDialog = saveFileDialog.ShowDialog();

            if (showDialog != true) return;

            var fileName = saveFileDialog.FileName;
            saveData.Save(fileName);
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMenu(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartGame(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new Main();
    }
}