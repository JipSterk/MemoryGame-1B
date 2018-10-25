using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Interaction logic for HighScores.xaml
    /// </summary>
    public partial class HighScores
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public HighScores() => InitializeComponent();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToMenu(object sender, MouseButtonEventArgs e) => MainWindow.Instance.Content = new Main();
    }
}
