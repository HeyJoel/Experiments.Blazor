namespace SPASite.BlazorWebAssembly.Client.Domain;

/// <summary>
/// Represents a single error when validating an object. Can be thrown by 
/// wrapping the error in <see cref="ValidationErrorException"/>.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Represents a single error when validating an object.
    /// </summary>
    public ValidationError()
    {
    }

    /// <summary>
    /// Zero or more properties that the error message applies to.
    /// </summary>
    public IReadOnlyCollection<string> Properties { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Client-friendly text describing the error.
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Optional alphanumeric code representing the error that can be detected by the
    /// client to use in conditional UI flow. Errors codes are typically lowercase and 
    /// use a dash-separated namespacing convention e.g. "cf-my-entity-example-condition.
    /// </summary>
    public string? ErrorCode { get; set; }
}
