namespace SuggestionApi.Domain.Models.Locations
{
    /// <summary>
    /// Class used to easily transfer and validate location coordinates
    /// </summary>
    public class GeographicalLocation
    {
        public GeographicalLocation(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        
        public bool AreCoordinatesValid()
        {
            if (!Latitude.HasValue || !Longitude.HasValue) 
                return false;

            if (Latitude.Value < -90 || Latitude.Value > 90)
                return false;
            
            if (Longitude.Value < -180 || Longitude.Value > 180)
                return false;

            return true;
        }
    }
}