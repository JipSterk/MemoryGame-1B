namespace MemoryGame_1B.Models
{
    /// <summary>
    /// GetServersResponse object
    /// </summary>
    public class GetServersResponse
    {
        /// <summary>
        /// The servers
        /// </summary>
        public Server[] Servers { get; set; }

        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="servers"></param>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out Server[] servers, out ResponseStatus responseStatus)
        {
            servers = Servers;
            responseStatus = ResponseStatus;
        }
    }
}