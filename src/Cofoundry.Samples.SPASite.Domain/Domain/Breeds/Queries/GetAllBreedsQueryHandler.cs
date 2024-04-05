namespace Cofoundry.Samples.SPASite.Domain;

/// <summary>
/// This query uses IIgnorePermissionCheckHandler because the permissions
/// are already checked by the underlying custom entity query.
/// </summary>
public class GetAllBreedsQueryHandler
    : IQueryHandler<GetAllBreedsQuery, IEnumerable<Breed>>
    , IIgnorePermissionCheckHandler
{
    private readonly IContentRepository _contentRepository;

    public GetAllBreedsQueryHandler(
        IContentRepository contentRepository
        )
    {
        _contentRepository = contentRepository;
    }

    public async Task<IEnumerable<Breed>> ExecuteAsync(GetAllBreedsQuery query, IExecutionContext executionContext)
    {
        var breeds = await _contentRepository
            .CustomEntities()
            .GetByDefinition<BreedCustomEntityDefinition>()
            .AsRenderSummaries()
            .MapItem(MapBreed)
            .ExecuteAsync();

        return breeds;
    }

    /// <summary>
    /// For simplicity this logic is just repeated between handlers, but to 
    /// reduce repetition you could use a library like AutoMapper or break out
    /// the logic into a seperate mapper class and inject it in.
    /// </summary>
    private Breed MapBreed(CustomEntityRenderSummary customEntity)
    {
        var breed = new Breed
        {
            BreedId = customEntity.CustomEntityId,
            Title = customEntity.Title
        };

        return breed;
    }
}
