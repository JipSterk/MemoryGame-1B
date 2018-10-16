namespace MemoryGame_1B.SaveData
{
    /// <summary>
    /// Data for the card
    /// </summary>
    public class CardData
    {
        /// <summary>
        /// X index
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y index
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public CardData(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}