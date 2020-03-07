namespace SuggestionApi.Domain.Helpers.Geo
{
    public interface IGeoDomainService
    {
        bool AreCoordinatesValid(double? latitude, double? longitude);
    }
}