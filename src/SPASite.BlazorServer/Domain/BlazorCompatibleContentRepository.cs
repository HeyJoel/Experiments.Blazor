using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Extendable;

namespace SPASite.BlazorServer.Domain;

/// <summary>
/// Custom implementation of <see cref="Cofoundry.Domain.Internal.ContentRepository"/>
/// to get around DbContext lifetime issues whereby scoped lifetime isn't compatible with
/// blazor apps, but transient lifetime is not compatible with the way Cofoundry sometimes
/// nests queries, commands and utilities that expect the same db context. This implementation
/// uses a fresh service scope every time a query or command is executed.
/// </summary>
public sealed class BlazorCompatibleContentRepository
    : IContentRepository
    , IAdvancedContentRepository
    , IExtendableContentRepository
    , IDisposable
{
    private readonly IServiceScope _serviceScope;

    public BlazorCompatibleContentRepository(
        IServiceProvider serviceProvider
        )
    {
        _serviceScope = serviceProvider.CreateScope();
    }

    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        var domainRepositoryExecutor = ServiceProvider.GetRequiredService<IDomainRepositoryExecutor>();
        return domainRepositoryExecutor.ExecuteAsync(query, null);
    }

    public Task ExecuteCommandAsync(ICommand command)
    {
        var domainRepositoryExecutor = ServiceProvider.GetRequiredService<IDomainRepositoryExecutor>();
        return domainRepositoryExecutor.ExecuteAsync(command, null);
    }

    public IExtendableContentRepository WithExecutor(Func<IDomainRepositoryExecutor, IDomainRepositoryExecutor> domainRepositoryExecutorFactory)
    {
        var domainRepositoryExecutor = ServiceProvider.GetRequiredService<IDomainRepositoryExecutor>();
        var newRepository = new DecoratableContentRepository(ServiceProvider, domainRepositoryExecutor);
        newRepository.DecorateExecutor(domainRepositoryExecutorFactory);

        return newRepository;
    }

    public void Dispose()
    {
        _serviceScope?.Dispose();
    }
}
