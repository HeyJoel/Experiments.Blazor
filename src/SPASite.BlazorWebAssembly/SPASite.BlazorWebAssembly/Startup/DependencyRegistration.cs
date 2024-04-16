using Cofoundry.Core.DependencyInjection;
using SPASite.BlazorWebAssembly.Api;

namespace SPASite.BlazorWebAssembly.Startup;

public class DependencyRegistration : IDependencyRegistration
{
    public void Register(IContainerRegister container)
    {
        container.Register<MinimalApiHelper>();
    }
}
