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
        public static async Task<CreateServerResponse> CreateServer(string name)
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
            return graphQlResponse.GetDataFieldAs<CreateServerResponse>("createServer");
        }


        public static async Task<DeleteServerResponse> DeleteServer(string id)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = @"
                    mutation deleteServer($input: CreateServerInput) {
                        deleteServer(input: $input) {
                            status
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
    /// 
    /// </summary>
    public class DeleteServerResponse
    {
        public Status Status { get; set; }

        public void Deconstruct(out Status status)
        {
            status = Status;
        }
    }

    public enum Status
    {
        Ok,
        Error
    }

    public class CreateServerResponse
    {
        public string Id { get; set; }
        public Status Status { get; set; }

        public void Deconstruct(out string id, out Status status)
        {
            id = Id;
            status = Status;
        }
    }
}
