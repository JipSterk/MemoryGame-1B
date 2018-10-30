using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public Help() => InitializeComponent();

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToMenu(object sender, MouseButtonEventArgs e) => MainWindow.Instance.Content = new Main();
    }
}