using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MemoryGame_1B.SaveData;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page"/>
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame
    {
        /// <summary>
        /// The memoryGrid
        /// </summary>
        private MemoryGrid _memoryGrid;

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gridSize"></param>
        public NewGame(GridSize gridSize)
        {
            InitializeComponent();

            switch (gridSize)
            {
                case GridSize.Normal:
                    _memoryGrid = new MemoryGrid(Grid, 4, 4);
                    break;
                case GridSize.Large:
                    _memoryGrid = new MemoryGrid(Grid, 6, 6);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gridSize), gridSize, null);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveData"></param>
        public NewGame(SaveData.SaveData saveData) : this(saveData.GridSize)
        {
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ShowMenu(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void RestartGame(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
