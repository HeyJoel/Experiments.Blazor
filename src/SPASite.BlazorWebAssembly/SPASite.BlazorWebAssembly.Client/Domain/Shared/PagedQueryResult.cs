namespace SPASite.BlazorWebAssembly.Client.Domain;

/// <summary>
/// <para>
/// Represents the result of a paged query, including the original 
/// query paging settings and stats about the results returned including
/// the total page count and the total number of items. 
/// </para>
/// <para>
/// If the result of the query needs to be mapped to another model type 
/// you can use the ChangeType(newItems) method to convert the result.
/// </para>
/// </summary>
public class PagedQueryResult<TResult>
{
    /// <summary>
    /// The items returned from the query.
    /// </summary>
    public IReadOnlyCollection<TResult> Items { get; set; } = Array.Empty<TResult>();

    /// <summary>
    /// Total number of items in the result before paging was applied.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// Current (1-based) page number being returned.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items requested in the page (may not be equal to
    /// the actual number of items returned).
    /// </summary>
    public int PageSize { get; set; }
}
