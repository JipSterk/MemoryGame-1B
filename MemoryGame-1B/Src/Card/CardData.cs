using System;
using System.Windows.Media.Imaging;

namespace MemoryGame_1B.Card
{
    public class CardData
    {
        /// <summary>
        /// The Id of the card
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The card front
        /// </summary>
        public BitmapImage CardFront { get; }

        /// <summary>
        /// The card front
        /// </summary>
        public BitmapImage CardBack { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardFront"></param>
        /// <param name="cardBack"></param>
        public CardData(int id, BitmapImage cardFront, BitmapImage cardBack)
        {
            Id = id;
            CardFront = cardFront ?? throw new ArgumentNullException(nameof(cardFront));
            CardBack = cardBack ?? throw new ArgumentNullException(nameof(cardBack));
        }
    }
}