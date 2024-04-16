namespace SPASite.BlazorWebAssembly.Client.Domain;

public class CatsApi
{
    private const string BASE_PATH = "cats/";

    private readonly SpaSiteApi _spaSiteApi;

    public CatsApi(
        SpaSiteApi spaSiteApi
        )
    {
        _spaSiteApi = spaSiteApi;
    }

    public async Task<PagedQueryResult<CatSummary>> SearchAsync()
    {
        var response = await _spaSiteApi.GetAsync<PagedQueryResult<CatSummary>>(BASE_PATH);
        response.ThrowIfInvalidOrDataNull();

        return response.Data;
    }

    public async Task<CatDetails?> GetByIdAsync(int catId)
    {
        var response = await _spaSiteApi.GetAsync<CatDetails>($"{BASE_PATH}{catId}");
        response.ThrowIfInvalidOrDataNull();

        return response.Data;
    }

    public async Task<IReadOnlyCollection<CatSummary>> GetLikedAsync()
    {
        var response = await _spaSiteApi.GetAsync<IReadOnlyCollection<CatSummary>>($"{BASE_PATH}liked");
        response.ThrowIfInvalidOrDataNull();

        return response.Data;
    }

    public async Task SetLikedAsync(int catId, bool isLiked)
    {
        ApiResponse response;

        if (isLiked)
        {
            response = await _spaSiteApi.PostAsync($"{BASE_PATH}{catId}/likes");
        }
        else
        {
            response = await _spaSiteApi.DeleteAsync($"{BASE_PATH}{catId}/likes");
        }
        response.ThrowIfInvalid();
    }
}
