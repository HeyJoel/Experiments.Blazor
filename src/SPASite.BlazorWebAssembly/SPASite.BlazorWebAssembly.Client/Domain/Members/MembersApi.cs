namespace SPASite.BlazorWebAssembly.Client.Domain;

public class MembersApi
{
    private const string BASE_PATH = "members/";

    private readonly SpaSiteApi _spaSiteApi;

    public MembersApi(
        SpaSiteApi spaSiteApi
        )
    {
        _spaSiteApi = spaSiteApi;
    }

    public async Task<MemberSummary?> GetCurrentAsync()
    {
        var response = await _spaSiteApi.GetAsync<MemberSummary?>($"{BASE_PATH}current");
        response.ThrowIfInvalid();

        return response.Data;
    }

    public async Task<ApiResponse> RegisterAsync(RegisterMemberAndSignInCommand command)
    {
        var response = await _spaSiteApi.PostAsync($"{BASE_PATH}register", command);
        return response;
    }

    public async Task<ApiResponse> SignInAsync(SignMemberInCommand command)
    {
        var response = await _spaSiteApi.PostAsync($"{BASE_PATH}sign-in", command);
        return response;
    }

    public async Task SignOutAsync()
    {
        var response = await _spaSiteApi.PostAsync($"{BASE_PATH}sign-out");
        response.ThrowIfInvalid();
    }
}
