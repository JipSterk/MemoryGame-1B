namespace MemoryGame_1B.Models
{
    /// <summary>
    /// CreateServerResponse of the response
    /// </summary>
    public class CreateServerResponse
    {
        /// <summary>
        /// The id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// the response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out string id, out ResponseStatus responseStatus)
        {
            id = Id;
            responseStatus = ResponseStatus;
        }
    }
}