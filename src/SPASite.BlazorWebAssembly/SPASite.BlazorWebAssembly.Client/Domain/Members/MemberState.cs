namespace SPASite.BlazorWebAssembly.Client.Domain;

public class MemberState
{
    private readonly MembersApi _membersApi;
    private readonly CatsApi _catsApi;

    public MemberState(
        MembersApi membersApi,
        CatsApi catsApi
        )
    {
        _membersApi = membersApi;
        _catsApi = catsApi;
    }

    public MemberSummary? Member { get; set; }

    public IReadOnlyCollection<CatSummary> LikedCats { get; set; } = Array.Empty<CatSummary>();

    public event Action? OnMemberChange;

    public event Action? OnLikesChange;

    public async Task ReloadAsync()
    {
        Member = await _membersApi.GetCurrentAsync();
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
        LikedCats = await _catsApi.GetLikedAsync();
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
