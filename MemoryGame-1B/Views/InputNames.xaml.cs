using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MemoryGame_1B.SaveData;

namespace MemoryGame_1B.Views
{
    /// <summary>
    /// Interaction logic for InputNames.xaml
    /// </summary>
    public partial class InputNames : Page
    {
        public InputNames() => InitializeComponent();

        private string _namePlayer1;
        private string _namePlayer2;
        private GridSize _selectedGridSize = GridSize.Large;

        private void NewGame(object sender, RoutedEventArgs e)
        {
            // Save names of Player1 and Player2 from TextBoxes somewhere, somehow
            MainWindow.Instance.Content = new NewGame(GridSize.Large);
        }

        private void MainMenu(object sender, RoutedEventArgs e) => MainWindow.Instance.Content = new Main();
    }
}
