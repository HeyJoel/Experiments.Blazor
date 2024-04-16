using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace SPASite.BlazorWebAssembly.Client.Domain;

public class SpaSiteApi
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;

    public SpaSiteApi(
        HttpClient httpClient,
        NavigationManager navigationManager
        )
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
    }

    public async Task<ApiResponse<TResult>> GetAsync<TResult>(string? path = null)
    {
        var response = await _httpClient.GetAsync($"{_navigationManager.BaseUri}api/{path}");

        if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TResult>>();

            if (result == null)
            {
                throw new Exception("Invalid response");
            }

            return result;
        }

        response.EnsureSuccessStatusCode();
        throw new InvalidOperationException($"Unexpected code reached: {nameof(response.EnsureSuccessStatusCode)} should have thrown.");
    }

    public async Task<ApiResponse> PutAsync<TQuery, TResult>(string path, TQuery query)
    {
        var result = await MakeRequestAsync<ApiResponse>(c => c.PutAsJsonAsync(
            $"{_navigationManager.BaseUri}{path}",
            query
            ));

        return result;
    }

    public async Task<ApiResponse> PutAsync(string path)
    {
        var result = await MakeRequestAsync<ApiResponse>(c => c.PutAsync(
            $"{_navigationManager.BaseUri}api/{path}",
            null
            ));

        return result;
    }

    public async Task<ApiResponse> PostAsync<TCommand>(string path, TCommand command)
    {
        var result = await MakeRequestAsync<ApiResponse>(c => c.PostAsJsonAsync(
            $"{_navigationManager.BaseUri}api/{path}",
            command
            ));

        return result;
    }

    public async Task<ApiResponse> PostAsync(string path)
    {
        var result = await MakeRequestAsync<ApiResponse>(c => c.PostAsync(
            $"{_navigationManager.BaseUri}api/{path}",
            null
            ));

        return result;
    }

    public async Task<ApiResponse> DeleteAsync(string path)
    {
        var result = await MakeRequestAsync<ApiResponse>(c => c.DeleteAsync(
            $"{_navigationManager.BaseUri}api/{path}"
            ));

        return result;
    }

    public async Task<TApiResult> MakeRequestAsync<TApiResult>(
        Func<HttpClient, Task<HttpResponseMessage>> clientMethod
        )
    {
        var response = await clientMethod(_httpClient);

        if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var result = await response.Content.ReadFromJsonAsync<TApiResult>();

            if (result == null)
            {
                throw new Exception("Invalid response");
            }

            return result;
        }

        response.EnsureSuccessStatusCode();
        throw new InvalidOperationException($"Unexpected code reached: {nameof(response.EnsureSuccessStatusCode)} should have thrown.");
    }
}
