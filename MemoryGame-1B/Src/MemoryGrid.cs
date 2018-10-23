using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MemoryGame_1B.Card;

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
        /// Add cards to the grid
        /// </summary>
        private void AddCards()
        {
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _columns; j++)
                {
                    var bitmapImage = new BitmapImage(new Uri("../Images/test.gif", UriKind.Relative));

                    var cardData = new CardData(i + j, bitmapImage);
                    
                    var image = new Image
                    {
                        Source = new BitmapImage(new Uri("../Images/Placeholder.png", UriKind.Relative)),
                        DataContext = cardData,
                    };
                    image.MouseDown += CardClick;

                    _cardDatas.Add(cardData);

                    Grid.SetColumn(image, j);
                    Grid.SetRow(image, i);
                    _grid.Children.Add(image);
                }
            }
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CardClick(object sender, MouseButtonEventArgs e)
        {
            var image = (Image) sender;
            var cardData = (CardData)image.DataContext;
            image.Source = cardData.BitmapImage;
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