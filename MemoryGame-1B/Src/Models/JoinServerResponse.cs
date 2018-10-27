namespace MemoryGame_1B.Models
{
    /// <summary>
    /// JoinServerResponse object
    /// </summary>
    public class JoinServerResponse
    {
        /// <summary>
        /// The card data for that game
        /// </summary>
        public string CardData { get; set; }

        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="cardData"></param>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out string cardData, out ResponseStatus responseStatus)
        {
            cardData = CardData;
            responseStatus = ResponseStatus;
        }
    }
}