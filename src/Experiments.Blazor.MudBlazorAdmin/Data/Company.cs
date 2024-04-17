namespace Experiments.Blazor.MudBlazorAdmin.Data;

public class Company()
{
    public string Symbol { get; set; } = string.Empty;
    public string Security { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string SubIndustry { get; set; } = string.Empty;
    public string HeadquartersLocation { get; set; } = string.Empty;
    public DateOnly DateAdded { get; set; }
    public string CentralIndexKey { get; set; } = string.Empty;
    public string YearFounded { get; set; } = string.Empty;
    public string DetailsUrl => $"/companies/{Symbol}";
}
