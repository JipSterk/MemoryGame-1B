using System;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        /// Players first pick
        /// </summary>
        private static Image _image1;

        /// <summary>
        /// Players first pick
        /// </summary>
        private static CardData _pick1;

        /// <summary>
        /// Players second pick
        /// </summary>
        private static Image _image2;

        /// <summary>
        /// Players second pick
        /// </summary>
        private static CardData _pick2;

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
        /// <param name="image"></param>
        /// <param name="cardData"></param>
        public static void PickCard(Image image, CardData cardData)
        {
            if (_pick1 == null)
            {
                _image1 = image;
                _pick1 = cardData;
            }
            else if (_pick2 == null)
            {
                _image2 = image;
                _pick2 = cardData;

                Task.Run(Compare);
            }
        }

        /// <summary>
        /// Are the picks the same
        /// </summary>
        private static async Task Compare()
        {
            var same = _pick1.Number == _pick2.Number;

            await Task.Delay(1000);

            if (same)
            {
                NewPick();
            }
            else
            {
                _image1.Dispatcher.Invoke(() => _image1.Source = _pick1.Turn());
                _image2.Dispatcher.Invoke(() => _image2.Source = _pick2.Turn());

                NewPick();
            }
        }

        /// <summary>
        /// Removes references
        /// </summary>
        private static void NewPick()
        {
            _pick1 = _pick2 = null;
            _image1 = _image2 = null;

            Turn ^= Turn;

            OnTurnChanged?.Invoke(Turn);
        }
    }
}