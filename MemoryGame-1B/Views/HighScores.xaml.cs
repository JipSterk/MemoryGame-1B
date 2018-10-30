using System.Windows.Controls;
using System.Windows.Input;
using MemoryGame_1B.Managers;

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
        public HighScores()
        {
            InitializeComponent();

            foreach (var (name, score) in ScoreManager.ScoreEntries)
            {
                if (string.IsNullOrEmpty(Name1.Text))
                {
                    Name1.Text = name;
                    Score1.Text = score.ToString();
                }
                else if (string.IsNullOrEmpty(Name2.Text))
                {
                    Name2.Text = name;
                    Score2.Text = score.ToString();
                }
                else if (string.IsNullOrEmpty(Name3.Text))
                {
                    Name3.Text = name;
                    Score3.Text = score.ToString();
                }
            }
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToMenu(object sender, MouseButtonEventArgs e) => MainWindow.Instance.Content = new Main();
    }
}