﻿using System.Net;
using System.Text;
using System.Text.Json;
using AniStats_Embeded_API.Helpers;
using AniStats_Embeded_API.Helpers.Extensions;
using AniStats_Embeded_API.Interfaces;
using AniStats_Embeded_API.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace AniStats_Embeded_API.Service;

public class AnilistApiService : IAnilistApiService
{
    private readonly ILogger<AnilistApiService> _logger;
    private readonly IApiManager _apiManager;
    
    private static string GetUserQuery => File.ReadAllText("GraphQLQueries/GetAnilistBasicUserProfile.graphql");
    private static string GetUserAndStatOverviewQuery => File.ReadAllText("GraphQLQueries/GetAnilistBasicUserProfileWithMinimumStats.graphql");
    public AnilistApiService(ILogger<AnilistApiService> logger, IApiManager apiManager)
    {
        _logger = logger;
        _apiManager = apiManager;
    }

    public async Task<AnilistApiUserResult> GetUserDataForApi(string username)
    {
        try
        {
            var request = new GraphQLRequest
            {
                Query = GetUserAndStatOverviewQuery,
                Variables = new { name = username }
            };
            var response = await _apiManager.SendGraphQLQueryAsync<AnilistUserStatsResponseDto>(request);
            
            var jsonResponse = JsonSerializer.Serialize(response);
            return AnilistApiUserResult.AsSuccess(response);
            
        }  catch (GraphQLHttpRequestException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                return AnilistApiUserResult.AsFailure(HttpStatusCode.NotFound, "User not found");
            }
            _logger.LogUnhandledException(e, "GraphQLHttpRequestException from Anilist Api");
            return AnilistApiUserResult.AsFailure(HttpStatusCode.InternalServerError, "GraphQLHttpRequestException from Anilist Api");
        }
        catch (Exception e)
        {
            _logger.LogUnhandledException(e, "Unhandled Exception fetching data from Anilist Api");
            return AnilistApiUserResult.AsFailure(HttpStatusCode.InternalServerError, "Unhandled Exception fetching data from Anilist Api");
        }
    }
    
    public async Task<AnilistUserStatsResponseDto?> GetUserData(string username)
    {
        try
        {
            var request = new GraphQLRequest
            {
                Query = GetUserAndStatOverviewQuery,
                Variables = new { name = username }
            };
            var response = await _apiManager.SendGraphQLQueryAsync<AnilistUserStatsResponseDto>(request);
            return response;
        } catch (Exception e)
        {
            _logger.LogUnhandledException(e, "Unhandled Exception fetching data from Anilist Api");
            return null;
        }
       
    }



}