using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public NewGame()
        {
            InitializeComponent();

            _memoryGrid = new MemoryGrid(Grid, 4, 4);
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
        private void aPicture_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("test on click");
        }

    }
}
