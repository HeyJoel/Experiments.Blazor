namespace SPASite.BlazorServer.Startup;

public class RouteRegistration : IRouteRegistration
{
    public void RegisterRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapRazorComponents<App.App>()
            .AddInteractiveServerRenderMode();
    }
}
