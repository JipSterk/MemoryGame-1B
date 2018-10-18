using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MemoryGame_1B
{
    /// <summary>
    /// Handles the memory grid
    /// </summary>
    public class MemoryGrid
    {
        /// <summary>
        /// The grid
        /// </summary>
        private readonly Grid _grid;
        
        /// <summary>
        /// The amount of rows
        /// </summary>
        private readonly int _rows;

        /// <summary>
        /// The amount of columns
        /// </summary>
        private readonly int _columns;

        /// <summary>
        /// The data of all cards on the grid
        /// </summary>
        private readonly List<CardData> _cardDatas = new List<CardData>();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public MemoryGrid(Grid grid, int rows, int columns)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _rows = rows;
            _columns = columns;

            Clear();
            Build();
            AddCards();
        }

        /// <summary>
        /// Create a new Image object, with a binding to the CardData supplied
        /// </summary>
        /// <param name="data">The CardData the resulting image will have a binding with.</param>
        /// <returns>An Image object with a binding to a CardData</returns>
        private Image ConvertToImage(CardData data) => new Image {Source = data.BackImage.Source, DataContext = data};
        
        /// <summary>
        /// Add cards to the grid
        /// </summary>
        private void AddCards()
        {
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _columns; j++)
                {
//                    var uri = new Uri("Images/Placeholder.png", UriKind.Relative);

                    var backFaceImage = new Image{Source = new BitmapImage(new Uri("../Images/Placeholder.png", UriKind.Relative)) };
                    var frontFaceImage = new Image{Source = new BitmapImage(new Uri("../Images/test.gif", UriKind.Relative))};
                    var card = new CardData(i+j, frontFaceImage, backFaceImage);
                    var image = ConvertToImage(card);
                    image.MouseDown += card.OnCardClicked;
                    _cardDatas.Add(card);

                    //var image = new Image
                    //{
                    //    Source = new BitmapImage(new Uri("../Images/Placeholder.png", UriKind.Relative))
                    //};

                    Grid.SetColumn(image, j);
                    Grid.SetRow(image, i);
                    _grid.Children.Add(image);
                }
            }
        }

        /// <summary>
        /// Builds the grid
        /// </summary>
        private void Build()
        {
            for (var i = 0; i < _rows; i++)
                _grid.RowDefinitions.Add(new RowDefinition());

            for (var i = 0; i < _columns; i++)
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        /// <summary>
        /// Clears the grid
        /// </summary>
        private void Clear()
        {
            _grid.Children.Clear();
            _grid.RowDefinitions.Clear();
            _grid.ColumnDefinitions.Clear();
        }
    }
}
