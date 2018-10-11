using System.Windows.Controls;

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
    }
}
