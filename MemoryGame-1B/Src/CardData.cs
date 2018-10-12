using System;
using System.Windows.Controls;

namespace MemoryGame_1B
{
    internal struct CardData
    {
        // Event for when card is clicked, supplying the card

        public readonly int ID;
        public readonly Image FrontImage;
        public readonly Image BackImage;

        public CardData(int id, Image frontImage, Image backImage)
        {
            ID = id;
            FrontImage = frontImage;
            BackImage = backImage;
        }
    }
}