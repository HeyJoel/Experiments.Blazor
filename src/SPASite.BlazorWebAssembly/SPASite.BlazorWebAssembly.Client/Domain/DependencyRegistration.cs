namespace SPASite.BlazorWebAssembly.Client.Domain;

public static class DependencyRegistration
{
    public static IServiceCollection AddClientDomain(this IServiceCollection services)
    {
        services.AddScoped(sp => new HttpClient());

        return services
            .AddTransient<SpaSiteApi>()
            .AddTransient<MembersApi>()
            .AddTransient<CatsApi>()
            .AddScoped<MemberState>();
    }
}
