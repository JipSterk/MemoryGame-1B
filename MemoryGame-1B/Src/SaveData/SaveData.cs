using System.IO;
using Newtonsoft.Json;

namespace MemoryGame_1B.SaveData
{
    public enum GridSize
    {
        /// <summary>
        /// 4x4
        /// </summary>
        Normal,
        /// <summary>
        /// 6x6
        /// </summary>
        Large
    }
    /// <summary>
    /// Handles user data
    /// </summary>
    public class SaveData
    {
        /// <summary>
        /// Who's turn is it
        /// </summary>
        public Turn Turn { get; }

        /// <summary>
        /// The size of the grid
        /// </summary>
        public GridSize GridSize { get; }

        /// <summary>
        /// The CardData
        /// </summary>
        public CardData[,] CardData { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="cardData"></param>
        public SaveData(Turn turn, GridSize gridSize, CardData[,] cardData)
        {
            Turn = turn;
            GridSize = gridSize;
            CardData = cardData;
        }

        /// <summary>
        /// Saves the data to a file path
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            var serializeObject = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, serializeObject);
        }

        /// <summary>
        /// Loads the data from a file path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static SaveData Load(string path)
        {
            var text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<SaveData>(text);
        }

        public void Deconstruct(out Turn turn, out GridSize gridSize, out CardData[,] cardData)
        {
            turn = Turn;
            gridSize = GridSize;
            cardData = CardData;
        }
    }
}
