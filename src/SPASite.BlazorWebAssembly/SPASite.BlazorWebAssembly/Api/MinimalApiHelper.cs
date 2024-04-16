using System.ComponentModel.DataAnnotations;
using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SPASite.BlazorWebAssembly.Api;

/// <summary>
/// Somewhat equivalent to <see cref="IApiResponseHelper"/> but for minimal
/// APIs to cut down on boilerplate on standard API responses.
/// </summary>
public class MinimalApiHelper
{
    private readonly IDomainRepository _domainRepository;

    public MinimalApiHelper(
        IDomainRepository domainRepository
        )
    {
        _domainRepository = domainRepository;
    }

    public async Task<Results<Ok<ApiResponse<TResult>>, BadRequest<ApiResponse>, JsonHttpResult<ApiResponse>>> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        try
        {
            var result = await _domainRepository.ExecuteQueryAsync(query);
            return TypedResults.Ok(ApiResponse.Success(result));
        }
        catch (ValidationException ex)
        {
            return TypedResults.BadRequest(ApiResponse.Error(ex));
        }
        catch (NotPermittedException ex)
        {
            return TypedResults.Json(ApiResponse.Error(ex), statusCode: 403);
        }
    }

    public async Task<Results<Ok<ApiResponse>, BadRequest<ApiResponse>, JsonHttpResult<ApiResponse>>> ExecuteCommandAsync(ICommand command)
    {
        try
        {
            await _domainRepository.ExecuteCommandAsync(command);
            return TypedResults.Ok(ApiResponse.Success());
        }
        catch (ValidationException ex)
        {
            return TypedResults.BadRequest(ApiResponse.Error(ex));
        }
        catch (NotPermittedException ex)
        {
            return TypedResults.Json(ApiResponse.Error(ex), statusCode: 403);
        }
    }
}
