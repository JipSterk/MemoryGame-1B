using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using MemoryGame_1B.SaveData;
using MemoryGame_1B.Views;

namespace MemoryGame_1B.Managers
{
    /// <summary>
    /// Handles all graphql requests
    /// </summary>
    public static class GraphqlManager
    {
        /// <summary>
        /// The graphql client
        /// </summary>
        public static readonly GraphQLClient GraphQlClient = new GraphQLClient("https://sleepy-falls-91203.herokuapp.com/graphql");

        /// <summary>
        /// Gets the servers
        /// </summary>
        /// <returns></returns>
        public static async Task<GetServersResponse> GetServers()
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    query getServers {
                        getServers {
                            servers {
                                id
                                name
                                current
                            }
                            responseStatus
                        }
                    }"
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<GetServersResponse>("getServers");
        }

        /// <summary>
        /// Creates a server
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<CreateServerResponse> CreateServer(string name)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    mutation createServer($input: CreateServerInput) {
                        createServer(input: $input) {
                            id
                            responseStatus
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
            return graphQlResponse.GetDataFieldAs<CreateServerResponse>("createServer");
        }

        /// <summary>
        /// Deletes a server
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<DeleteServerResponse> DeleteServer(string id)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    mutation deleteServer($input: CreateServerInput) {
                        deleteServer(input: $input) {
                            responseStatus
                        }
                    }",
                Variables = new
                {
                    input = new
                    {
                        Id = id
                    }
                }
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<DeleteServerResponse>("deleteServer");
        }

        /// <summary>
        /// Joins a server
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<JoinServerResponse> JoinServer(string id)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    query joinServer($input: JoinServerInput) {
                        joinServer(input: $input) {
                            cardData
                            responseStatus
                        }
                    }",
                Variables =
                {
                    input = new
                    {
                        Id = id
                    }
                }
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<JoinServerResponse>("joinServer");
        }

        /// <summary>
        /// Send the grid to the server
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardData"></param>
        /// <returns></returns>
        public static async Task<CreateCardDataResponse> CreateDataGrid(string id, string cardData)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    mutation createDataGrid($input: CreateDataGridInput) {
                        createDataGrid(input: $input) {
                            responseStatus
                        }
                    }",
                Variables = new
                {
                    input = new
                    {
                        Id = id,
                        CardData = cardData
                    }
                }
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<CreateCardDataResponse>("createDataGrid");
        }
    }

    /// <summary>
    /// GetServersResponse object
    /// </summary>
    public class GetServersResponse
    {
        /// <summary>
        /// The servers
        /// </summary>
        public Server[] Servers { get; set; }

        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="servers"></param>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out Server[] servers, out ResponseStatus responseStatus)
        {
            servers = Servers;
            responseStatus = ResponseStatus;
        }
    }

    /// <summary>
    /// JoinServerResponse object
    /// </summary>
    public class JoinServerResponse
    {
        /// <summary>
        /// The card data for that game
        /// </summary>
        public string CardData { get; set; }

        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="cardData"></param>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out string cardData, out ResponseStatus responseStatus)
        {
            cardData = CardData;
            responseStatus = ResponseStatus;
        }
    }

    /// <summary>
    /// CreateCardDataResponse object
    /// </summary>
    public class CreateCardDataResponse
    {
        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out ResponseStatus responseStatus)
        {
            responseStatus = ResponseStatus;
        }
    }

    /// <summary>
    /// DeleteServerResponse object
    /// </summary>
    public class DeleteServerResponse
    {
        /// <summary>
        /// The response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out ResponseStatus responseStatus)
        {
            responseStatus = ResponseStatus;
        }
    }

    /// <summary>
    /// ResponseStatus of the response
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// Ok status
        /// </summary>
        Ok,
        /// <summary>
        /// Error Status
        /// </summary>
        Error
    }

    /// <summary>
    /// CreateServerResponse of the response
    /// </summary>
    public class CreateServerResponse
    {
        /// <summary>
        /// The id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// the response status
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Deconstructs this object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responseStatus"></param>
        public void Deconstruct(out string id, out ResponseStatus responseStatus)
        {
            id = Id;
            responseStatus = ResponseStatus;
        }
    }
}
