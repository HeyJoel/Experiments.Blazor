namespace SPASite.BlazorServer.App;

public class MemberState
{
    private readonly IDomainRepository _domainRepository;
    private bool _isLoaded;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public MemberState(
        IDomainRepository domainRepository
        )
    {
        _domainRepository = domainRepository;
    }

    public MemberSummary? Member { get; set; }

    public IReadOnlyCollection<CatSummary> LikedCats { get; set; } = Array.Empty<CatSummary>();

    public event Action? OnMemberChange;

    public event Action? OnLikesChange;

    // Note: To support static (login pages) and serverside interactive
    // pages we can run into issues where an interactive server page needs
    // to reload the state after pre-render i.e. the app container and layout
    // does not need to be re-rendered and stays static.
    // The best way so far I can see to handle this is to us ethis method to
    // ensure the state is loaded in the interactive server pages, but it needs
    // to be threadsafe. Perhaps this wouldn't be an issue if we moved the login
    // pages to RazorPages and made the whole app serverside-interactive?
    public async Task EnsureLoadedAsync()
    {
        if (_isLoaded)
        {
            return;
        }
        await _semaphore.WaitAsync();

        try
        {
            if (!_isLoaded)
            {
                await ReloadAsync();
                _isLoaded = false;
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task ReloadAsync()
    {
        Member = await _domainRepository.ExecuteQueryAsync(new GetCurrentMemberSummaryQuery());
        OnMemberChange?.Invoke();
        await ReloadLikesAsync();
    }

    public async Task ReloadLikesAsync()
    {
        if (Member == null)
        {
            LikedCats = Array.Empty<CatSummary>();
            return;
        }
        LikedCats = await _domainRepository.ExecuteQueryAsync(new GetCatSummariesByMemberLikedQuery()
        {
            UserId = Member.UserId
        });
        OnLikesChange?.Invoke();
    }

    public void Clear()
    {
        Member = null;
        LikedCats = Array.Empty<CatSummary>();
        OnMemberChange?.Invoke();
        OnLikesChange?.Invoke();
    }

    public bool IsLiked(int catId)
    {
        return Member != null && LikedCats.Any(c => c.CatId == catId);
    }
}
