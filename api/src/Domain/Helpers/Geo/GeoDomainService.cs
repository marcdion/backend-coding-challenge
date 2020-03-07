namespace SuggestionApi.Domain.Helpers.Geo
{
    public class GeoDomainService : IGeoDomainService
    {
        public bool AreCoordinatesValid(double? latitude, double? longitude)
        {
            if (!latitude.HasValue || !longitude.HasValue) 
                return false;

            if (latitude.Value < -90 || latitude.Value > 90)
                return false;
            
            if (longitude.Value < -180 || longitude.Value > 180)
                return false;

            return true;
        }
    }
}