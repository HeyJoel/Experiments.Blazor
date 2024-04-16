using SPASite.BlazorWebAssembly.Client.Pages;

namespace SPASite.BlazorWebAssembly.Startup;

public class RouteRegistration : IRouteRegistration
{
    public void RegisterRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapRazorComponents<App.App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Home).Assembly);
    }
}
