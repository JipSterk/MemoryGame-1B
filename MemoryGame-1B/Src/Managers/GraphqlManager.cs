using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
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
        public static async Task<Server[]> GetServers()
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    query getServers {
                        getServers {
                            id
                            name
                            current
                        }
                    }"
            };

            var graphQlResponse = await GraphQlClient.PostAsync(graphQlRequest);
            return graphQlResponse.GetDataFieldAs<Server[]>("getServers");
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
