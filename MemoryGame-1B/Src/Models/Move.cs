namespace MemoryGame_1B.Models
{
    /// <summary>
    /// Move object
    /// </summary>
    public class Move
    {
        /// <summary>
        /// The current room
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// The x coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The y coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="room"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out string room, out int x, out int y)
        {
            room = Room;
            x = X;
            y = Y;
        }
    }
}