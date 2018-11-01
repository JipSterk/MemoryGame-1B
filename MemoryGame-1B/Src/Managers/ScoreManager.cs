using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MemoryGame_1B.SaveData;
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
        /// All Scores
        /// </summary>
        private static Dictionary<int, int> ScoreTable4x4 { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// All Scores
        /// </summary>
        private static Dictionary<int, int> ScoreTable6x6 { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// Path where to save and load from
        /// </summary>
        private static readonly string ScoresPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Scores.json";

        /// <summary>
        /// Path to the 4x4 Score Table
        /// </summary>
        private static readonly string ScoreTablePath4x4 = new Uri("../../Resources/ScoreTable4x4.json", UriKind.Relative).ToString();

        /// <summary>
        /// Path to the 6x6 Score Table
        /// </summary>
        private static readonly string ScoreTablePath6x6 = new Uri("../../Resources/ScoreTable6x6.json", UriKind.Relative).ToString();

        /// <summary>
        /// Constructor
        /// </summary>
        static ScoreManager()
        {
            if (File.Exists(ScoresPath)) LoadScores();
            if (File.Exists(ScoreTablePath4x4)) LoadScoreTable4x4();
            if (File.Exists(ScoreTablePath6x6)) LoadScoreTable6x6();
        }

        /// <summary>
        /// Loads the 4x4 score table
        /// </summary>
        private static void LoadScoreTable4x4()
        {
            var text = File.ReadAllText(ScoreTablePath4x4);
            ScoreTable4x4 = JsonConvert.DeserializeObject<Dictionary<int, int>>(text);
        }

        /// <summary>
        /// Loads the 6x6 score table
        /// </summary>
        private static void LoadScoreTable6x6()
        {
            var text = File.ReadAllText(ScoreTablePath6x6);
            ScoreTable6x6 = JsonConvert.DeserializeObject<Dictionary<int, int>>(text);
        }

        /// <summary>
        /// Loads the scores
        /// </summary>
        private static void LoadScores()
        {
            var text = File.ReadAllText(ScoresPath);
            ScoreEntries = JsonConvert.DeserializeObject<List<ScoreEntry>>(text);
        }

        /// <summary>
        /// Saves the scores
        /// </summary>
        private static void SaveScores()
        {
            var serializeObject = JsonConvert.SerializeObject(ScoreEntries);
            File.WriteAllText(ScoresPath, serializeObject);
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

            ScoreEntries = ScoreEntries.OrderByDescending(x => x.Score).ToList();

            SaveScores();
        }

        /// <summary>
        /// Return score according to the ScoreTable
        /// </summary>
        /// <returns></returns>
        public static int GetScore(GridSize gridSize)
        {
            var scoreTable = gridSize == GridSize.Normal ? ScoreTable4x4 : ScoreTable6x6;
            var keyValuePair = scoreTable.ElementAt(0);
            scoreTable.Remove(0);
            return keyValuePair.Value;
        }
    }
}