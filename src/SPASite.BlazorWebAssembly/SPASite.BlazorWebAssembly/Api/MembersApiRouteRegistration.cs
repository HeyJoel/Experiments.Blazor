using Cofoundry.Samples.SPASite.Domain;
using Microsoft.AspNetCore.Mvc;

namespace SPASite.BlazorWebAssembly.Api;

public class MembersApiRouteRegistration : IRouteRegistration
{
    public void RegisterRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder
            .MapGroup("api/members")
            .WithOpenApi();

        MapApis(group);
    }

    private static RouteGroupBuilder MapApis(RouteGroupBuilder group)
    {
        group.MapGet("/current", async (MinimalApiHelper minimalApiHelper) =>
            {
                var query = new GetCurrentMemberSummaryQuery();
                var result = await minimalApiHelper.ExecuteQueryAsync(query);

                return result;
            })
            .WithName("Get current member")
            .WithOpenApi();

        group.MapPost("register", async (
            [FromBody] RegisterMemberAndSignInCommand command,
            MinimalApiHelper minimalApiHelper
            ) =>
            {
                var result = await minimalApiHelper.ExecuteCommandAsync(command);

                return result;
            });

        group.MapPost("sign-in", async (
            [FromBody] SignMemberInCommand command,
            MinimalApiHelper minimalApiHelper
            ) =>
        {
            var result = await minimalApiHelper.ExecuteCommandAsync(command);

            return result;
        });

        group.MapPost("sign-out", async (
            MinimalApiHelper minimalApiHelper
            ) =>
            {
                var result = await minimalApiHelper.ExecuteCommandAsync(new SignMemberOutCommand());

                return result;
            });

        return group;
    }
}
