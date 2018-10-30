using System;
using System.Collections.Generic;
using System.IO;
using MemoryGame_1B.Score;
using Newtonsoft.Json;

namespace MemoryGame_1B.Managers
{
    /// <summary>
    /// Handles all the high score
    /// </summary>
    public static class ScoreManager
    {
        /// <summary>
        /// All Scores
        /// </summary>
        public static List<ScoreEntry> ScoreEntries { get; set; } = new List<ScoreEntry>();

        /// <summary>
        /// Path where to save and load from
        /// </summary>
        private static readonly string Path = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Constructor
        /// </summary>
        static ScoreManager()
        {
            if (File.Exists(Path)) LoadScores();
        }

        /// <summary>
        /// Loads the scores
        /// </summary>
        private static void LoadScores()
        {
            var text = File.ReadAllText(Path);
            ScoreEntries = JsonConvert.DeserializeObject<List<ScoreEntry>>(text);
        }

        /// <summary>
        /// Saves the scores
        /// </summary>
        private static void SaveScores()
        {
            var serializeObject = JsonConvert.SerializeObject(ScoreEntries);
            File.WriteAllText(Path, serializeObject);
        }

        /// <summary>
        /// Adds score entry
        /// </summary>
        /// <param name="scoreEntry"></param>
        public static void AddEntry(ScoreEntry scoreEntry)
        {
            ScoreEntries.Add(scoreEntry);

            for (var i = ScoreEntries.Count - 1; i >= 0; i--)
            {
                if (ScoreEntries.Count > 3)
                    ScoreEntries.RemoveAt(0);
            }

            SaveScores();
        }
    }
}