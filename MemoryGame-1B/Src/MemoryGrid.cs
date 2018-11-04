using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using MemoryGame_1B.Managers;
using MemoryGame_1B.Models;
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
        /// Singleton
        /// </summary>
        public static MemoryGrid Instance { get; private set; }

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

        /// <summary>
        /// The theme of the grid
        /// </summary>
        private readonly Theme _theme;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public MemoryGrid()
        {
            Instance = this;

            if (SocketIoManager.Online)
            {
                SocketIoManager.OnNewMove += OnNewMove;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="gridSize"></param>
        /// <param name="theme"></param>
        public MemoryGrid(Grid grid, GridSize gridSize, Theme theme) : this()
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _gridSize = gridSize;
            _theme = theme;

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

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="gridSize"></param>
        /// <param name="turn"></param>
        /// <param name="cardData"></param>
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
                    var (cardFrontUriSource, cardBackUriSource, turned, number) = cardData[i, j];

                    var cardBack = new BitmapImage(new Uri(cardBackUriSource, UriKind.Relative));
                    var cardFront = new BitmapImage(new Uri(cardFrontUriSource, UriKind.Relative));

                    BuildCard(cardFront, cardBack, i, j, number, turned);
                }
            }
        }

        /// <summary>
        /// OnNewMoveListener
        /// </summary>
        /// <param name="o"></param>
        private void OnNewMove(object o)
        {
            var (_, x, y) = JsonConvert.DeserializeObject<Move>((string) o);

            _grid.Dispatcher.Invoke(() => CardData[x - 1, y - 1].Turn());
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
                    var uriString = _theme == Theme.Zombie
                        ? $"../Images/Cards/Zombie/CardBack/CardBG{i + 1}x{j + 1}.png"
                        : "../Images/Cards/Social/CardBack/BgSocial.png";
                    var cardBack = new BitmapImage(new Uri(uriString, UriKind.Relative));

                    var (i1, cardFront) = bitmapImages.Pop();

                    BuildCard(cardFront, cardBack, i, j, i1);
                }
            }

            if (!SocketIoManager.Online) return;

            var cardData = _gridSize == GridSize.Normal ? new SaveData.CardData[4, 4] : new SaveData.CardData[6, 6];

            for (var i = 0; i < CardData.GetLength(0); i++)
            {
                for (var j = 0; j < CardData.GetLength(1); j++)
                {
                    var (cardFrontUriSource, cardBackUriSource, turned, number) = CardData[i, j];
                    cardData[i, j] = new SaveData.CardData(cardFrontUriSource, cardBackUriSource, turned, number);
                }
            }

            var serializeObject = JsonConvert.SerializeObject(cardData);
            Task.Run(() => GraphqlManager.CreateDataGrid(SocketIoManager.Room, serializeObject));
        }

        /// <summary>
        /// Builds a card
        /// </summary>
        /// <param name="cardFront"></param>
        /// <param name="cardBack"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="number"></param>
        /// <param name="turned"></param>
        private void BuildCard(BitmapImage cardFront, BitmapImage cardBack, int i, int j, int number,
            bool turned = false)
        {
            var image = new Image
            {
                Source = turned ? cardFront : cardBack,
                Margin = new Thickness(0, 10, 0, 10)
            };

            var cardData = new CardData(image, cardFront, cardBack, turned, number);

            image.DataContext = cardData;

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
        private List<(int, BitmapImage)> GetImages()
        {
            var count = _gridSize == GridSize.Normal ? 16 : 36;
            var list = new List<(int, BitmapImage)>();

            for (var i = 0; i < count; i++)
            {
                var imageNumber = i % count / 2 + 1;
                var bitmapImage =
                    new BitmapImage(new Uri($"../Images/Cards/{_theme}/CardFront/Card{_theme}{imageNumber}.png",
                        UriKind.Relative));
                list.Add((imageNumber, bitmapImage));
            }

            return list;
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            if (GameManager.CardsTurned >= 2) return;

            //GameManager.CardsTurned++;
            
            var image = (Image) sender;

            var cardData = (CardData) image.DataContext;

            if (cardData.Turned) return;

            image.RenderTransformOrigin = new Point(0.5, 0.5);

            var flipCard = new ScaleTransform
            {
                ScaleY = 1,
            };

            image.RenderTransform = flipCard;

            var animation = new DoubleAnimation
            {
                From = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = false,
            };

            flipCard.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            if (SocketIoManager.Online)
            {
                var row = Grid.GetRow(image) + 1;
                var column = Grid.GetColumn(image) + 1;

                var move = new Move
                {
                    Room = SocketIoManager.Room,
                    X = row,
                    Y = column
                };

                SocketIoManager.FlipCard(move);
            }


            cardData.Turn();

            GameManager.PickCard();
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

        /// <summary>
        /// Destructor
        /// </summary>
        ~MemoryGrid()
        {
            if (SocketIoManager.Online)
            {
                SocketIoManager.OnNewMove -= OnNewMove;
            }
        }
    }
}