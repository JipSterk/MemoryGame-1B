using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using MemoryGame_1B.Models;
using MemoryGame_1B.SaveData;
using MemoryGame_1B.Score;
using MemoryGame_1B.Views;
using CardData = MemoryGame_1B.Card.CardData;

namespace MemoryGame_1B.Managers
{
    /// <summary>
    /// Handles all the game logic
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// Who's turn is it
        /// </summary>
        public static Turn Turn { get; private set; }

        /// <summary>
        /// The event listener for changing the turn
        /// </summary>
        public static event Action<Turn> OnTurnChanged;

        /// <summary>
        /// The event listener for updating the score
        /// </summary>
        public static event Action<Turn, int> OnScoreChanged;

        /// <summary>
        /// First players name
        /// </summary>
        public static string NamePlayer1;

        /// <summary>
        /// Second players name
        /// </summary>
        public static string NamePlayer2;

        /// <summary>
        /// First players score
        /// </summary>
        public static int ScorePlayer1 { get; set; }

        /// <summary>
        /// Second players score
        /// </summary>
        public static int ScorePlayer2 { get; set; }

        /// <summary>
        /// Start a new game
        /// </summary>
        public static void StartGame()
        {
            Turn = Turn.Random();

            OnTurnChanged?.Invoke(Turn);
        }

        /// <summary>
        /// Picks a card
        /// </summary>
        public static void PickCard()
        {
            var cardData = MemoryGrid.Instance.CardData.Cast<CardData>().Where(x => !x.FoundPair && x.Turned).ToArray();

            if (cardData.Length < 2) return;

            var (lhs, rhs) = Tuple.Create(cardData[0], cardData[1]);
            Task.Run(() => Compare(lhs, rhs));
        }

        /// <summary>
        /// Are the picks the same
        /// </summary>
        private static async Task Compare(CardData lhs, CardData rhs)
        {
            if (lhs.Number == rhs.Number)
            {
                SoundManager.PlayRandom();

                lhs.FoundPair = rhs.FoundPair = true;

                switch (Turn)
                {
                    case Turn.Player1:
                        ScorePlayer1 += 100;
                        break;
                    case Turn.Player2:
                        ScorePlayer2 += 100;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                OnScoreChanged?.Invoke(Turn, Turn == Turn.Player1 ? ScorePlayer1 : ScorePlayer2);
            }

            await Task.Delay(1000);

            var array = MemoryGrid.Instance.CardData.Cast<CardData>().ToArray();

            if (array.Length - array.Count(x => x.Turned) == 2)
            {
                foreach (var cardData in array.Where(x => !x.FoundPair))
                    cardData.FoundPair = true;

                await Task.Delay(5000);
                EndGame();
                return;
            }

            foreach (var cardData in array.Where(x => !x.FoundPair && x.Turned))
            {
                cardData.Turn();

                if (!SocketIoManager.Online) continue;
                cardData.Image.Dispatcher.Invoke(() =>
                {
                    var row = Grid.GetRow(cardData.Image) + 1;
                    var column = Grid.GetColumn(cardData.Image) + 1;

                    var move = new Move
                    {
                        Room = SocketIoManager.Room,
                        X = row,
                        Y = column
                    };

                    SocketIoManager.FlipCard(move);
                });
            }

            NewPick();
        }

        /// <summary>
        /// Ends Game
        /// </summary>
        private static void EndGame()
        {
            if (SocketIoManager.Online)
            {
                SocketIoManager.LeaveGame(SocketIoManager.Room);
                Task.Run(() => GraphqlManager.DeleteServer(SocketIoManager.Room));
            }

            var b = ScorePlayer1 > ScorePlayer2;

            var scoreEntry = new ScoreEntry
            {
                Name = b ? NamePlayer1 : NamePlayer2,
                Score = b ? ScorePlayer1 : ScorePlayer2
            };

            ScoreManager.AddEntry(scoreEntry);

            MainWindow.Instance.Content = new Main();
        }

        /// <summary>
        /// Removes references
        /// </summary>
        private static void NewPick()
        {
            Turn = Turn == Turn.Player1 ? Turn.Player2 : Turn.Player1;

            OnTurnChanged?.Invoke(Turn);
        }
    }
}