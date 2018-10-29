using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using MemoryGame_1B.Models;
using MemoryGame_1B.SaveData;
using CardData = MemoryGame_1B.Card.CardData;

namespace MemoryGame_1B.Managers
{
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
            }

            await Task.Delay(1000);

            foreach (var cardData in MemoryGrid.Instance.CardData.Cast<CardData>().Where(x => !x.FoundPair && x.Turned)
                .ToArray())
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
        /// Removes references
        /// </summary>
        private static void NewPick()
        {
            Turn = Turn == Turn.Player1 ? Turn.Player2 : Turn.Player1;

            OnTurnChanged?.Invoke(Turn);
        }
    }
}