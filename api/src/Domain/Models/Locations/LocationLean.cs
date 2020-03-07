using SuggestionApi.Domain.Models.DataStructure;

namespace SuggestionApi.Domain.Models.Locations
{
    public class LocationLean
    {
        public Node Parent { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Country { get; set; }
        public string AdministrativeRegion { get; set; }
        public double? Population { get; set; }
        
        public LocationLean(Node parent, double? lat, double? lon, string country, string administrativeRegion, double? population)
        {
            Parent = parent;
            Latitude = lat;
            Longitude = lon;
            Country = country;
            AdministrativeRegion = administrativeRegion;
            Population = population;
        }
    }
}