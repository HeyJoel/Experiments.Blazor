using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Extendable;

namespace SPASite.BlazorServer.Domain;

/// <summary>
/// Allows <see cref="BlazorCompatibleContentRepository"/> to decorate
/// execution without starting a new service scope. more or less
/// a direct copy of <see cref="Cofoundry.Domain.Internal.ContentRepository"/>.
/// </summary>
internal class DecoratableContentRepository
    : IContentRepository
    , IAdvancedContentRepository
    , IExtendableContentRepository
{
    private IDomainRepositoryExecutor _domainRepositoryExecutor;

    public DecoratableContentRepository(
        IServiceProvider serviceProvider,
        IDomainRepositoryExecutor domainRepositoryExecutor
        )
    {
        ServiceProvider = serviceProvider;
        _domainRepositoryExecutor = domainRepositoryExecutor;
    }

    public virtual IServiceProvider ServiceProvider { get; private set; }

    public virtual Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        return _domainRepositoryExecutor.ExecuteAsync(query, null);
    }

    public virtual Task ExecuteCommandAsync(ICommand command)
    {
        return _domainRepositoryExecutor.ExecuteAsync(command, null);
    }

    public IExtendableContentRepository WithExecutor(Func<IDomainRepositoryExecutor, IDomainRepositoryExecutor> domainRepositoryExecutorFactory)
    {
        var newRepository = new DecoratableContentRepository(ServiceProvider, _domainRepositoryExecutor);
        newRepository.DecorateExecutor(domainRepositoryExecutorFactory);

        return newRepository;
    }

    internal void DecorateExecutor(Func<IDomainRepositoryExecutor, IDomainRepositoryExecutor> domainRepositoryExecutorFactory)
    {
        ArgumentNullException.ThrowIfNull(domainRepositoryExecutorFactory);

        var newExecutor = domainRepositoryExecutorFactory.Invoke(_domainRepositoryExecutor);
        if (newExecutor == null)
        {
            throw new InvalidOperationException(nameof(domainRepositoryExecutorFactory) + " did not return an instance.");
        }

        _domainRepositoryExecutor = newExecutor;
    }
}
