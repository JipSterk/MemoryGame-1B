using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MemoryGame_1B.Managers;
using MemoryGame_1B.Models;
using MemoryGame_1B.SaveData;
using Newtonsoft.Json;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Interaction logic for Online.xaml
    /// </summary>
    public partial class Online
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public Online()
        {
            InitializeComponent();

            var (servers, _) = Task.Run(GraphqlManager.GetServers).Result;
            Build(servers);
        }

        /// <summary>
        /// Builds the Servers
        /// </summary>
        /// <param name="servers"></param>
        private void Build(IReadOnlyList<Server> servers)
        {
            Clear();

            for (var i = 0; i < servers.Count; i++)
            {
                var (id, name, current) = servers[i];
                if (current > 1) continue;

                var rowDefinition = new RowDefinition
                {
                    Height = new GridLength(40)
                };

                Servers.RowDefinitions.Add(rowDefinition);

                var button = new Button
                {
                    Content = $"Name: {name} {current}/2",
                    Margin = new Thickness(0, 5, 0, 5),
                    DataContext = id
                };

                button.Click += Join;

                Grid.SetColumn(button, 1);
                Grid.SetRow(button, i);
                Servers.Children.Add(button);
            }
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Join(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var id = (string) button.DataContext;

            SocketIoManager.JoinGame(id);
            var (cardData, _) = Task.Run(() => GraphqlManager.JoinServer(id)).Result;

            var saveData = new SaveData.SaveData("", "", Turn.Player1, GridSize.Normal,
                JsonConvert.DeserializeObject<CardData[,]>(cardData));

            MainWindow.Instance.Content = new NewGame(saveData);
        }

        /// <summary>
        /// Clears the grid
        /// </summary>
        private void Clear()
        {
            Servers.Children.Clear();
            Servers.RowDefinitions.Clear();
            Servers.ColumnDefinitions.Clear();
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, RoutedEventArgs e)
        {
            var (servers, _) = Task.Run(GraphqlManager.GetServers).Result;
            Build(servers);
        }

        /// <summary>
        /// OnClickListener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateRoom(object sender, RoutedEventArgs e)
        {
            var roomNameText = RoomName.Text;
            var (id, _) = Task.Run(() => GraphqlManager.CreateServer(roomNameText)).Result;
            SocketIoManager.JoinGame(id);

            MainWindow.Instance.Content = new NewGame(GridSize.Normal, Theme.Zombie);
        }
        private void ReturnToMenu(object sender, MouseButtonEventArgs e) => MainWindow.Instance.Content = new Main();
    }
}