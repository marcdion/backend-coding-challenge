using AutoMapper;
using Shouldly;
using SuggestionApi.Domain.Mapping;
using SuggestionApi.Domain.Models.Locations;
using Xunit;

namespace SuggestionApi.Tests.Mapping
{
    public class LocationMappingProfileTests
    {
        private static IMapper MockMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LocationMappingProfile());
            });
            
            return mockMapper.CreateMapper();
        }

        [Fact]
        public void LocationInput_ToLocation_ShouldMapProperly()
        {
            //Arrange
            var mapper = MockMapper();
            var locationInput = new LocationInput
            {
                Name = "Amos",
                Latitude = -43.0987,
                Longitude = 127.6782,
                Country = "CA",
                AdministrativeRegion = "10",
                Population = 25000
            };
            
            //Act
            var mapped = mapper.Map<Location>(locationInput);
            
            //Assert
            mapped.ShouldSatisfyAllConditions(
                () => mapped.Name.ShouldBe("Amos"),
                () => mapped.Latitude.ShouldBe(-43.0987),
                () => mapped.Longitude.ShouldBe(127.6782),
                () => mapped.Country.ShouldBe("Canada"),
                () => mapped.AdministrativeRegion.ShouldBe("QC"),
                () => mapped.Population.ShouldBe(25000)
            );
        }
    }
}