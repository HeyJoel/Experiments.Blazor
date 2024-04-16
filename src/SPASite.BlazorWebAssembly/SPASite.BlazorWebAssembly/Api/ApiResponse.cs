using System.ComponentModel.DataAnnotations;
using Cofoundry.Core;
using Cofoundry.Core.Validation;

namespace SPASite.BlazorWebAssembly.Api;

/// <summary>
/// A simple data container for returning the result of a command or query
/// from a rest api, structuring data and errors in a consistent response.
/// </summary>
/// <typeparam name="T">Type of the data being returned</typeparam>
public class ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// Any additional data to send back to the response.
    /// </summary>
    public T? Data { get; set; }
}

/// <summary>
/// Represents the result of executing a command or function in a rest api,
/// structuring errors in a consistent response.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// <see langword="true"/> if the request executed successfully; otherwise <see langword="false"/>.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Collection of any validation errors discovered when executing the request
    /// </summary>
    public IReadOnlyCollection<ValidationError> Errors { get; set; } = Array.Empty<ValidationError>();

    public static ApiResponse Success()
    {
        return new()
        {
            IsValid = true
        };
    }

    public static ApiResponse<TResult> Success<TResult>(TResult result)
    {
        return new()
        {
            Data = result,
            IsValid = true
        };
    }

    public static ApiResponse Error(ValidationException error)
    {
        return new()
        {
            Errors = FormatValidationErrors(error),
            IsValid = false
        };
    }

    public static ApiResponse Error(NotPermittedException error)
    {
        return new()
        {
            Errors = [new ValidationError(error.Message)],
            IsValid = false
        };
    }

    private static ValidationError[] FormatValidationErrors(ValidationException? ex)
    {
        if (ex == null)
        {
            return [];
        }

        var validationErrors = MapValidationExceptionToErrors(ex);

        // De-dup and order by prop name.
        return validationErrors
            .GroupBy(e =>
            {
                var propKey = string.Empty;
                if (e.Properties != null)
                {
                    propKey = string.Join("+", e.Properties);
                }

                return new { e.Message, propKey };
            })
            .OrderBy(g => g.Key.propKey)
            .Select(g => g.First())
            .ToArray();

    }

    private static IEnumerable<ValidationError> MapValidationExceptionToErrors(ValidationException ex)
    {
        if (ex.ValidationResult is CompositeValidationResult compositeResult)
        {
            foreach (var result in compositeResult.Results)
            {
                yield return MapValidationError(result);
            }
        }
        else
        {
            ValidationError error;

            if (ex.ValidationResult != null)
            {
                error = MapValidationError(ex.ValidationResult);
            }
            else
            {
                error = new ValidationError()
                {
                    Message = ex.Message
                };
            }

            if (ex is ValidationErrorException exceptionWithCode)
            {
                error.ErrorCode = exceptionWithCode.ErrorCode;
            }

            yield return error;
        }
    }

    private static ValidationError MapValidationError(ValidationResult result)
    {
        var error = new ValidationError()
        {
            Message = result.ErrorMessage ?? "Unknown Error",
            Properties = result.MemberNames.ToArray()
        };

        return error;
    }
}
