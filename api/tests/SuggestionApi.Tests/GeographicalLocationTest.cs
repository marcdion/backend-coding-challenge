using Shouldly;
using SuggestionApi.Domain.Models.Locations;
using Xunit;

namespace SuggestionApi.Tests
{
    public class GeographicalLocationTest
    {
        [Fact]
        public void GeographicalLocation_ShouldInitializeProperly()
        {
            // Arrange
            var lat = 43.70011;
            var lon = -79.4163;
            
            //Act
            var coords = new GeographicalLocation(lat, lon);

            //Assert
            coords.ShouldSatisfyAllConditions(
                () => coords.Latitude.ShouldBe(lat),
                () => coords.Longitude.ShouldBe(lon)
            );
        }
        
        [Fact]
        public void AreCoordinatesValid_ShouldReturnTrue_ForValidCoordinates()
        {   
            // Arrange
            var coords = new GeographicalLocation(43.70011, -79.4163);
            
            //Act
            var result = coords.AreCoordinatesValid();
            
            //Assert
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeTrue()
            );
        }

        [Theory]
        [InlineData(-143.70011, -79.4163)]
        [InlineData(43.70011, 200.1234)]
        [InlineData(-143.70011, 200.1234)]
        public void AreCoordinatesValid_ShouldReturnFalse_ForNotValidCoordinates(double latitude, double longitude)
        {
            // Arrange
            var coords = new GeographicalLocation(latitude, longitude);
            
            //Act
            var result = coords.AreCoordinatesValid();
            
            //Assert
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeFalse()
            );
        }
    }
}
