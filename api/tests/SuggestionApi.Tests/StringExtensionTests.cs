using Shouldly;
using SuggestionApi.Domain.Extensions;
using Xunit;

namespace SuggestionApi.Tests
{
    public class StringExtensionTests
    {
        [Fact]
        public void SanitizeInput_ShouldRemoveImproperCharacters_AndFormat()
        {
            //Arrange
            var input = "HeL\"loO";
            
            //Act
            var result = input.SanitizeInput();
            
            //Assert
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBe("helloo")
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("\"")]
        [InlineData("\"\"")]
        [InlineData("\'\'")]
        [InlineData("\'")]
        public void IsValidInput_ShouldValidateProperly(string input)
        {
            //Act
            var result = input.IsValidInput();
            result.ShouldBeFalse();
        }
    }
}