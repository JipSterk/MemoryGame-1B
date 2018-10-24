using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MemoryGame_1B.Managers;
using MemoryGame_1B.SaveData;

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
        public Help()
        {
            InitializeComponent();

            var servers = Task.Run(GraphqlManager.GetServers).Result;
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

        private void Join(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var id = button.DataContext.ToString();

            SocketIoManager.JoinGame(id);

            MainWindow.Instance.Content = new NewGame(GridSize.Normal);
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
            var servers = Task.Run(GraphqlManager.GetServers).Result;
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

            MainWindow.Instance.Content = new NewGame(GridSize.Normal);
        }
    }

    /// <summary>
    /// Server Object
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The id of the server
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The name of the server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The current amount of people on the server
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="current"></param>
        public void Deconstruct(out string id, out string name, out int current)
        {
            id = Id;
            name = Name;
            current = Current;
        }
    }
}