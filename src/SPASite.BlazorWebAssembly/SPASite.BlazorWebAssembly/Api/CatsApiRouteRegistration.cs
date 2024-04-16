using Cofoundry.Samples.SPASite.Domain;

namespace SPASite.BlazorWebAssembly.Api;

public class CatsApiRouteRegistration : IRouteRegistration
{
    public void RegisterRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder
            .MapGroup("api/cats")
            .WithOpenApi();

        MapApis(group);
    }

    private static RouteGroupBuilder MapApis(RouteGroupBuilder group)
    {
        group.MapGet("/", async (MinimalApiHelper minimalApiHelper) =>
            {
                var query = new SearchCatSummariesQuery();
                var result = await minimalApiHelper.ExecuteQueryAsync(query);

                return result;
            })
            .WithName("Search Cats")
            .WithOpenApi();

        group.MapGet("/liked", async (IContentRepository contentRepository, MinimalApiHelper minimalApiHelper) =>
            {
                var userContext = await contentRepository
                    .Users()
                    .Current()
                    .Get()
                    .AsUserContext()
                    .ExecuteAsync();

                var signedInUser = userContext.ToSignedInContext();
                var result = await minimalApiHelper.ExecuteQueryAsync(new GetCatSummariesByMemberLikedQuery()
                {
                    UserId = userContext?.UserId ?? 0
                });

                return result;
            })
            .RequireAuthorization(AuthorizationPolicyNames.UserArea(MemberUserArea.Code))
            .WithName("Get cats liked by current member")
            .WithOpenApi();

        group.MapGet("/{catId:int}", async (int catId, MinimalApiHelper minimalApiHelper) =>
            {
                var result = await minimalApiHelper.ExecuteQueryAsync(new GetCatDetailsByIdQuery()
                {
                    CatId = catId
                });

                return result;
            })
            .WithOpenApi();

        group.MapPost("{catId:int}/likes", async (int catId, MinimalApiHelper minimalApiHelper) =>
        {
            var result = await minimalApiHelper.ExecuteCommandAsync(new SetCatLikedCommand()
            {
                CatId = catId,
                IsLiked = true
            });

            return result;
        });

        group.MapDelete("{catId:int}/likes", async (int catId, MinimalApiHelper minimalApiHelper) =>
        {
            var result = await minimalApiHelper.ExecuteCommandAsync(new SetCatLikedCommand()
            {
                CatId = catId,
                IsLiked = false
            });

            return result;
        });

        return group;
    }
}
