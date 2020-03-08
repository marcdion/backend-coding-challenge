namespace SuggestionApi.Domain.Models.Locations
{
    /// <summary>
    /// Location information that is used in the prefix tree insertion
    /// </summary>
    public class Location
    {
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Country { get; set; }
        public string AdministrativeRegion { get; set; }
        public double? Population { get; set; }
    }
}