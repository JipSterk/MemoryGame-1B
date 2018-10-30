using System;

namespace MemoryGame_1B.SaveData
{
    /// <summary>
    /// Data for the card
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// The front card's source
        /// </summary>
        public string CardFrontUriSource { get; }

        /// <summary>
        /// The back card's source
        /// </summary>
        public string CardBackUriSource { get; }

        /// <summary>
        /// Is card turned
        /// </summary>
        public bool Turned { get; }

        /// <summary>
        /// The number
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cardFrontUriSource"></param>
        /// <param name="cardBackUriSource"></param>
        /// <param name="turned"></param>
        public CardData(string cardFrontUriSource, string cardBackUriSource, bool turned, int number)
        {
            CardFrontUriSource = cardFrontUriSource ?? throw new ArgumentNullException(nameof(cardFrontUriSource));
            CardBackUriSource = cardBackUriSource ?? throw new ArgumentNullException(nameof(cardBackUriSource));
            Turned = turned;
            Number = number;
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
            cardFrontUriSource = CardFrontUriSource;
            cardBackUriSource = CardBackUriSource;
            turned = Turned;
            number = Number;
        }
    }
}