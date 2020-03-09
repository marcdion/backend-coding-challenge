using AutoMapper;
using Shouldly;
using SuggestionApi.Application.Suggestions.Dto;
using SuggestionApi.Domain.Mapping;
using SuggestionApi.Domain.Models.Suggestions;
using Xunit;

namespace SuggestionApi.Tests.Mapping
{
    public class SuggestionMappingProfileTests
    {
        private static IMapper MockMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SuggestionMappingProfile());
            });
            
            return mockMapper.CreateMapper();
        }

        [Fact]
        public void Suggestion_ToSuggestionDto_ShouldMapProperly()
        {
            //Arrange
            var mapper = MockMapper();
            var suggestion = new Suggestion
            {
                Name = "Amos",
                FullName = "Amos, Qc, Canada",
                Latitude = -43.0987,
                Longitude = 127.6782,
                Population = 25000,
                WeightedScore = 0.5
            };
            
            //Act
            var mapped = mapper.Map<SuggestionDto>(suggestion);
            
            //Assert
            mapped.ShouldSatisfyAllConditions(
                () => mapped.Name.ShouldBe("Amos, Qc, Canada"),
                () => mapped.Latitude.ShouldBe(-43.0987),
                () => mapped.Longitude.ShouldBe(127.6782),
                () => mapped.Score.ShouldBe(0.5)
            );
        }
    }
}