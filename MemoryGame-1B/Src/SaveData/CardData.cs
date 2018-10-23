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
        public bool Turned { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cardFrontUriSource"></param>
        /// <param name="cardBackUriSource"></param>
        /// <param name="turned"></param>
        public CardData(string cardFrontUriSource, string cardBackUriSource, bool turned)
        {
            CardFrontUriSource = cardFrontUriSource;
            CardBackUriSource = cardBackUriSource;
            Turned = turned;
        }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="cardFrontUriSource"></param>
        /// <param name="cardBackUriSource"></param>
        /// <param name="turned"></param>
        public void Deconstruct(out string cardFrontUriSource, out string cardBackUriSource, out bool turned)
        {
            cardFrontUriSource = CardFrontUriSource;
            cardBackUriSource = CardBackUriSource;
            turned = Turned;
        }
    }
}