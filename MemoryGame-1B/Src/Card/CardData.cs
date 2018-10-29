using System;
using System.Windows.Controls;
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
        /// The number of the card
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Is the pair found
        /// </summary>
        public bool FoundPair { get; set; }

        /// <summary>
        /// Is card turned
        /// </summary>
        public bool Turned { get; private set; }

        /// <summary>
        /// The card front
        /// </summary>
        private readonly BitmapImage _cardFront;

        /// <summary>
        /// The card front
        /// </summary>
        private readonly BitmapImage _cardBack;

        /// <summary>
        /// The image
        /// </summary>
        private readonly Image _image;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cardFront"></param>
        /// <param name="cardBack"></param>
        /// <param name="turned"></param>
        /// <param name="number"></param>
        public CardData(Image image, BitmapImage cardFront, BitmapImage cardBack, bool turned, int number)
        {
            _image = image ?? throw new ArgumentNullException(nameof(image));
            _cardFront = cardFront ?? throw new ArgumentNullException(nameof(cardFront));
            _cardBack = cardBack ?? throw new ArgumentNullException(nameof(cardBack));
            Turned = turned;
            Number = number;

            _image.DataContext = this;
        }
        
        /// <summary>
        /// Turns the card
        /// </summary>
        /// <returns></returns>
        public void Turn()
        {
            if (FoundPair) return;
            Turned = !Turned;
            _image.Dispatcher.Invoke(() => _image.Source = Turned ? _cardFront : _cardBack);
        }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="cardFrontUriSource"></param>
        /// <param name="cardBackUriSource"></param>
        /// <param name="turned"></param>
        /// <param name="number"></param>
        public void Deconstruct(out string cardFrontUriSource, out string cardBackUriSource, out bool turned, out int number)
        {
            cardFrontUriSource = _cardFront.UriSource.ToString();
            cardBackUriSource = _cardBack.UriSource.ToString();
            turned = Turned;
            number = Number;
        }
    }
}