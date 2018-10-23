using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MemoryGame_1B.Card
{
    public class CardData
    {
        /// <summary>
        /// The Id of the card
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The actual image
        /// </summary>
        public BitmapImage BitmapImage { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bitmapImage"></param>
        public CardData(int id, BitmapImage bitmapImage)
        {
            Id = id;
            BitmapImage = bitmapImage ?? throw new ArgumentNullException(nameof(bitmapImage));
        }
    }
}