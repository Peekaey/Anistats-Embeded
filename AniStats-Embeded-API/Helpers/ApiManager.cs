using AniStats_Embeded_API.Interfaces;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace AniStats_Embeded_API.Helpers;

public class ApiManager : IApiManager
{
    private readonly ILogger<ApiManager> _logger;
    
    private static readonly string ApiUrl = "https://graphql.anilist.co";
    private static readonly GraphQLHttpClient client = new GraphQLHttpClient(ApiUrl, new SystemTextJsonSerializer());
    
    public ApiManager(ILogger<ApiManager> logger)
    {
        _logger = logger;
    }
    
    public async Task<T> SendGraphQLQueryAsync<T>(GraphQLRequest request)
    {
        var response = await client.SendQueryAsync<T>(request);
        return response.Data;
    }
}