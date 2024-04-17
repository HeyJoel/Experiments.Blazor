using CsvHelper.Configuration;

namespace Experiments.Blazor.MudBlazorAdmin.Data;

public class CompanyMap : ClassMap<Company>
{
    public CompanyMap()
    {
        Map(m => m.Symbol).Name("Symbol");
        Map(m => m.Security).Name("Security");
        Map(m => m.Sector).Name("GICS Sector");
        Map(m => m.SubIndustry).Name("GICS Sub-Industry");
        Map(m => m.HeadquartersLocation).Name("Headquarters Location");
        Map(m => m.DateAdded).Name("Date added");
        Map(m => m.CentralIndexKey).Name("CIK");
        Map(m => m.YearFounded).Name("Founded");
    }
}
