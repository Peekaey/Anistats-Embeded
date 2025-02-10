using GraphQL;

namespace AniStats_Embeded_API.Interfaces;

public interface IApiManager
{
    Task<T> SendGraphQLQueryAsync<T>(GraphQLRequest request);
}