namespace SPASite.BlazorWebAssembly.Client.Domain;

public class CatDetails
{
    public required int CatId { get; set; }

    public required string Name { get; set; }

    public required string? Description { get; set; }

    public required int TotalLikes { get; set; }

    public required Breed? Breed { get; set; }

    public required IReadOnlyCollection<Feature> Features { get; set; }

    public required IReadOnlyCollection<ImageAssetRenderDetails> Images { get; set; }
}
