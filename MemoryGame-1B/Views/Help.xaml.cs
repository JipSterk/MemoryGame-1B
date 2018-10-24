using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GraphQL.Client;
using GraphQL.Common.Request;

namespace MemoryGame_1B.Views
{
    /// <inheritdoc cref="Page" />
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help
    {
        public static readonly GraphQLClient GraphQlClient = new GraphQLClient("http://localhost:8000/graphql");

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public Help()
        {
            InitializeComponent();

            var servers = Task.Run(GetServers).Result;
            Build(servers);
        }

        /// <summary>
        /// Gets the servers
        /// </summary>
        /// <returns></returns>
        private static async Task<Server[]> GetServers()
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    query getServers {
                        getServers {
                            name
                            current
                        }
                    }"
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<Server[]>("getServers");
        }

        /// <summary>
        /// Gets the servers
        /// </summary>
        /// <returns></returns>
        private static async Task<Server> CreateServer(string name)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    mutation createServer($input: CreateServerInput) {
                        createServer(input: $input) {
                            id
                            status
                        }
                    }",
                Variables = new
                {
                    input = new
                    {
                        Name = name
                    }
                }
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<Server>("createServer");
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
                var (name, current) = servers[i];
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
            var servers = Task.Run(GetServers).Result;
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
            var (name, current) = Task.Run(() => CreateServer(roomNameText)).Result;
        }
    }

    /// <summary>
    /// Server Object
    /// </summary>
    public class Server
    {
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
        public void Deconstruct(out string name, out int current)
        {
            name = Name;
            current = Current;
        }
    }
}