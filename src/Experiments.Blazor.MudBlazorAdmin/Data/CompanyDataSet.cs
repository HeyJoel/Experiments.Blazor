using System.Globalization;
using CsvHelper;

namespace Experiments.Blazor.MudBlazorAdmin.Data;

public class CompanyDataSet
{
    private static readonly Lazy<Dictionary<string, Company>> _companies = new(LoadData);

    public static IReadOnlyCollection<Company> GetAll()
    {
        return _companies.Value.Values;
    }

    public static Company? GetBySymbol(string symbol)
    {
        return _companies.Value.GetValueOrDefault(symbol);
    }

    private static Dictionary<string, Company> LoadData()
    {
        const string filePath = "Experiments.Blazor.MudBlazorAdmin.Data.SP500Companies.csv";

        var assembly = typeof(CompanyDataSet).Assembly;
        var files = assembly.GetManifestResourceNames();
        using var stream = assembly.GetManifestResourceStream(filePath);

        if (stream == null)
        {
            throw new FileNotFoundException($"Could not find embedded data set at path {filePath}");
        }
        using var reader = new StreamReader(stream);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<CompanyMap>();
        var records = csv.GetRecords<Company>().ToDictionary(c => c.Symbol, StringComparer.OrdinalIgnoreCase);

        return records;
    }
}
