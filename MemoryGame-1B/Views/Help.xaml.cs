using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GraphQL.Client;
using GraphQL.Common.Request;
using MemoryGame_1B.Managers;
using MemoryGame_1B.SaveData;
using Quobject.SocketIoClientDotNet.Client;

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
                var (_, name, current) = servers[i];
                if (current > 1) continue;

                var rowDefinition = new RowDefinition
                {
                    Height = new GridLength(40)
                };

                Servers.RowDefinitions.Add(rowDefinition);

                var textBlock = new Button
                {
                    Content = $"Name: {name} {current}/2",
                    Margin = new Thickness(0, 5, 0, 5)
                };

                Grid.SetColumn(textBlock, 1);
                Grid.SetRow(textBlock, i);
                Servers.Children.Add(textBlock);
            }
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
            var (name, _) = Task.Run(() => GraphqlManager.CreateServer(roomNameText)).Result;
            SocketIoManager.JoinGame(name);

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
        public int Id { get; set; }
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
        /// <param name="name"></param>
        /// <param name="current"></param>
        public void Deconstruct(out int id, out string name, out int current)
        {
            id = Id;
            name = Name;
            current = Current;
        }
    }
}