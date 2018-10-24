using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MemoryGame_1B.Managers;
using MemoryGame_1B.SaveData;
using Newtonsoft.Json;
using CardData = MemoryGame_1B.Card.CardData;

namespace MemoryGame_1B
{
    /// <summary>
    /// Handles the memory grid
    /// </summary>
    public class MemoryGrid
    {
        /// <summary>
        /// The data of all cards on the grid
        /// </summary>
        public CardData[,] CardData { get; }

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
        /// The size of the grid
        /// </summary>
        private readonly GridSize _gridSize;

        public MemoryGrid()
        {
            if (SocketIoManager.Online)
            {
                SocketIoManager.OnNewMove += OnNewMove;
            }
        }

        ~MemoryGrid()
        {
            if (SocketIoManager.Online)
            {
                SocketIoManager.OnNewMove -= OnNewMove;
            }
        }

        private void OnNewMove(object o)
        {
            var (_, x, y) = JsonConvert.DeserializeObject<Move>(o.ToString());
            CardData[x, y].Turn();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="gridSize"></param>
        public MemoryGrid(Grid grid, GridSize gridSize) : this()
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _gridSize = gridSize;

            switch (_gridSize)
            {
                case GridSize.Normal:
                    _rows = _columns = 4;
                    CardData = new CardData[4, 4];
                    break;
                case GridSize.Large:
                    _rows = _columns = 6;
                    CardData = new CardData[6, 6];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Clear();
            Build();
            AddCards();
        }

        public MemoryGrid(Grid grid, GridSize gridSize, Turn turn, SaveData.CardData[,] cardData) : this()
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _gridSize = gridSize;

            switch (_gridSize)
            {
                case GridSize.Normal:
                    _rows = _columns = 4;
                    CardData = new CardData[4, 4];
                    break;
                case GridSize.Large:
                    _rows = _columns = 6;
                    CardData = new CardData[6, 6];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Clear();
            Build();

            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _columns; j++)
                {
                    var (cardFrontUriSource, cardBackUriSource, turned) = cardData[i, j];

                    var cardBack = new BitmapImage(new Uri(cardBackUriSource, UriKind.Relative));
                    var cardFront = new BitmapImage(new Uri(cardFrontUriSource, UriKind.Relative));

                    BuildCard(cardFront, cardBack, i, j, turned);
                }
            }
        }

        /// <summary>
        /// Add cards to the grid
        /// </summary>
        private void AddCards()
        {
            var bitmapImages = GetImages().Shuffle();

            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _columns; j++)
                {
                    var cardBack =
                        new BitmapImage(new Uri($"../Images/Cards/Zombies/CardBackground/CardBG{i + 1}x{j + 1}.png",
                            UriKind.Relative));

                    var cardFront = bitmapImages.Pop();

                    BuildCard(cardFront, cardBack, i, j);
                }
            }
        }

        /// <summary>
        /// Builds a card
        /// </summary>
        /// <param name="cardFront"></param>
        /// <param name="cardBack"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="turned"></param>
        private void BuildCard(BitmapImage cardFront, BitmapImage cardBack, int i, int j, bool turned = false)
        {
            var cardData = new CardData(cardFront, cardBack, turned);

            var image = new Image
            {
                Source = turned ? cardFront : cardBack,
                DataContext = cardData,
            };

            image.MouseDown += CardClick;

            CardData[i, j] = cardData;

            Grid.SetColumn(image, j);
            Grid.SetRow(image, i);
            _grid.Children.Add(image);
        }

        /// <summary>
        /// Gets all the images
        /// </summary>
        /// <returns></returns>
        private List<BitmapImage> GetImages()
        {
            var count = _gridSize == GridSize.Normal ? 16 : 36;
            var list = new List<BitmapImage>();

            for (var i = 0; i < count; i++)
            {
                var imageNumber = i % count / 2 + 1;
                var bitmapImage =
                    new BitmapImage(new Uri($"../Images/Cards/Zombies/CardFront/CardZombie{imageNumber}.png",
                        UriKind.Relative));
                list.Add(bitmapImage);
            }

            return list;
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CardClick(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;
            if (SocketIoManager.Online)
            {
                var row = Grid.GetRow(image) - 1;
                var column = Grid.GetColumn(image) - 1;

                var move = new Move
                {
                    Room = SocketIoManager.Room,
                    X = row,
                    Y = column
                };

                SocketIoManager.FlipCard(move);
            }

            var cardData = (CardData) image.DataContext;

            image.Source = cardData.Turn();
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