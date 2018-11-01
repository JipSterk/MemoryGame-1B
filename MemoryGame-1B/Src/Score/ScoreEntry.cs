namespace MemoryGame_1B.Score
{
    /// <summary>
    /// A score entry
    /// </summary>
    public class ScoreEntry
    {
        /// <summary>
        /// The name of the entry
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The score of the entry
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void Deconstruct(out string name, out int score)
        {
            name = Name;
            score = Score;
        }
    }
}