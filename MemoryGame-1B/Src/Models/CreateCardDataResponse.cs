namespace MemoryGame_1B.Models
{
    /// <summary>
    /// CreateCardDataResponse object
    /// </summary>
    public class CreateCardDataResponse
    {
        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out ResponseStatus responseStatus)
        {
            responseStatus = ResponseStatus;
        }
    }
}