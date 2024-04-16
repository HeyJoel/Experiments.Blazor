namespace SPASite.BlazorWebAssembly.Startup;

public class UseAntiForgeryStartupTask : IRunBeforeStartupConfigurationTask
{
    public int Ordering => (int)StartupTaskOrdering.Normal;

    public IReadOnlyCollection<Type> RunBefore => [typeof(AddEndpointRoutesStartupConfigurationTask)];

    public void Configure(IApplicationBuilder app)
    {
        app.UseAntiforgery();
    }
}
