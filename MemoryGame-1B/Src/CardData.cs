using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MemoryGame_1B
{
    internal class CardData
    {
        public delegate CardData CardDataDelegate();
        public event CardDataDelegate OnFacingImageChanged;

        public readonly int Id;
        public readonly Image FrontImage;
        public readonly Image BackImage;

        public CardData(int id, Image frontImage, Image backImage)
        {
            Id = id;
            FrontImage = frontImage;
            BackImage = backImage;
        }

        public void OnCardClicked(object sender, MouseButtonEventArgs args)
        {
            Image card = (Image) sender;

            if (card.Source == FrontImage.Source) return;
            
            card.Source =  FrontImage.Source;
            OnFacingImageChanged?.Invoke();
        }
    }
}