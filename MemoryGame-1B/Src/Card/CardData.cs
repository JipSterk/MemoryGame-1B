using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame_1B.Card
{
    /// <summary>
    /// Data for the card
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// Is card turned
        /// </summary>
        private bool _turned;

        /// <summary>
        /// The card front
        /// </summary>
        private readonly BitmapImage _cardFront;

        /// <summary>
        /// The card front
        /// </summary>
        private readonly BitmapImage _cardBack;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cardFront"></param>
        /// <param name="cardBack"></param>
        /// <param name="turned"></param>
        public CardData(BitmapImage cardFront, BitmapImage cardBack, bool turned)
        {
            _cardFront = cardFront ?? throw new ArgumentNullException(nameof(cardFront));
            _cardBack = cardBack ?? throw new ArgumentNullException(nameof(cardBack));
            _turned = turned;
        }

        /// <summary>
        /// Turns the card
        /// </summary>
        /// <returns></returns>
        public ImageSource Turn()
        {
            _turned = !_turned;
            return _turned ? _cardFront : _cardBack;
        }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="cardFrontUriSource"></param>
        /// <param name="cardBackUriSource"></param>
        /// <param name="turned"></param>
        public void Deconstruct(out string cardFrontUriSource, out string cardBackUriSource, out bool turned)
        {
            cardFrontUriSource = _cardFront.UriSource.ToString();
            cardBackUriSource = _cardBack.UriSource.ToString();
            turned = _turned;
        }
    }
}