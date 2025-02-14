using AniStats_Embeded_API.Interfaces;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace AniStats_Embeded_API.Service;

public class ApiManagerService : IApiManager
{
    private readonly ILogger<ApiManagerService> _logger;
    
    private static readonly string ApiUrl = "https://graphql.anilist.co";
    private static readonly GraphQLHttpClient Client = new GraphQLHttpClient(ApiUrl, new SystemTextJsonSerializer());
    
    public ApiManagerService(ILogger<ApiManagerService> logger)
    {
        _logger = logger;
    }
    
    public async Task<T> SendGraphQLQueryAsync<T>(GraphQLRequest request)
    {
        var response = await Client.SendQueryAsync<T>(request);
        return response.Data;
    }
}