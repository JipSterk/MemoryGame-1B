namespace MemoryGame_1B.Models
{
    /// <summary>
    /// Server Object
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The id of the server
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The current amount of people on the server
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="current"></param>
        public void Deconstruct(out string id, out string name, out int current)
        {
            id = Id;
            name = Name;
            current = Current;
        }
    }
}