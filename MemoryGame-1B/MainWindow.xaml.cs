using System.Windows;
using MemoryGame_1B.Views;

namespace MemoryGame_1B
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static MainWindow Instance { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            Content = new Main();
        }
    }
}