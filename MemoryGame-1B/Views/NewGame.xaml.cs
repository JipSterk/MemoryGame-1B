using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            GameManager.OnScoreChanged += ScoreChanged;

            Player1Name.Text = GameManager.NamePlayer1;
            Player2Name.Text = GameManager.NamePlayer2;
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gridSize"></param>
        public NewGame(GridSize gridSize) : this()
        {
            _gridSize = gridSize;
            _memoryGrid = new MemoryGrid(Grid, _gridSize);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveData"></param>
        public NewGame(SaveData.SaveData saveData) : this()
        {
            var (playerName1, playerName2, turn, gridSize, cardData) = saveData;

            Player1Name.Text = playerName1;
            Player2Name.Text = playerName2;

            _memoryGrid = new MemoryGrid(Grid, gridSize, turn, cardData);
        }

        /// <summary>
        /// Toggles the turn
        /// </summary>
        private void ToggleTurn(Turn turn)
        {
            Player1Name.Dispatcher.Invoke(() =>
                Player1Name.Foreground = new SolidColorBrush(turn == Turn.Player1 ? Colors.Green : Colors.Black));
            Player2Name.Dispatcher.Invoke(() =>
                Player2Name.Foreground = new SolidColorBrush(turn == Turn.Player2 ? Colors.Green : Colors.Black));
        }

        /// <summary>
        /// Updates the score
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="score"></param>
        private void ScoreChanged(Turn turn, int score)
        {
            Player1Score.Dispatcher.Invoke(() =>
            {
                if(turn == Turn.Player1) Player1Score.Text = $"Score: {score}";
            });

            Player2Score.Dispatcher.Invoke(() =>
            {
                if (turn == Turn.Player2) Player2Score.Text = $"Score: {score}";
            });
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

            var saveData = new SaveData.SaveData(Player1Name.Text, Player2Name.Text, GameManager.Turn, _gridSize, cardData);

            var saveFileDialog = new SaveFileDialog
            {
                FileName = $"{Player1Name.Text}Vs{Player2Name.Text}",
                DefaultExt = ".json",
                Filter = "Json documents (.json)|*.json",
                CheckPathExists = false,
                CheckFileExists = false,
                OverwritePrompt = false
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
        private void ShowMenu(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new Main();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartGame(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new InputNames();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToMenu(object sender, MouseButtonEventArgs e) => MainWindow.Instance.Content = new Main();

        /// <summary>
        /// Deconstructor
        /// </summary>
        ~NewGame()
        {
            GameManager.OnTurnChanged -= ToggleTurn;
            GameManager.OnScoreChanged -= ScoreChanged;
        }
    }
}