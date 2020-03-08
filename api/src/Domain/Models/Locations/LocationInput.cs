using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SuggestionApi.Domain.Models.Locations
{
    /// <summary>
    /// Location information that is imported from the TSV file with CSV Helper
    /// </summary>
    public class LocationInput
    {
        [Name("ascii")]
        public string Name { get; set; }
        
        [Name("lat")]
        public double? Latitude { get; set; }
        
        [Name("long")]
        public double? Longitude { get; set; }
        
        [Name("country")]
        public string Country { get; set; }
        
        [Name("admin1")]
        public string AdministrativeRegion { get; set; }
        
        [Name("population")]
        public double? Population { get; set; }
    }
}