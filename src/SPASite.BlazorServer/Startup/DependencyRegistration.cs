using Cofoundry.Core.DependencyInjection;
using SPASite.BlazorServer.App;
using SPASite.BlazorServer.Domain;

namespace SPASite.BlazorServer.Startup;

public class DependencyRegistration : IDependencyRegistration
{
    public void Register(IContainerRegister container)
    {
        container.RegisterScoped<MemberState>();
        container.Register<BlazorCompatibleContentRepository>(
            [typeof(IContentRepository), typeof(IAdvancedContentRepository), typeof(IDomainRepository)],
            RegistrationOptions.Override()
            );
    }
}
