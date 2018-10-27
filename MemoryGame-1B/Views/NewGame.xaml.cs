using System.Windows;
using System.Windows.Controls;
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
        /// <param name="gridSize"></param>
        public NewGame(GridSize gridSize)
        {
            InitializeComponent();

            _gridSize = gridSize;
            _memoryGrid = new MemoryGrid(Grid, _gridSize);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveData"></param>
        public NewGame(SaveData.SaveData saveData)
        {
            InitializeComponent();

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
                    var (cardFrontUriSource, cardBackUriSource, turned) = _memoryGrid.CardData[i, j];
                    cardData[i, j] = new CardData(cardFrontUriSource, cardBackUriSource, turned);
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