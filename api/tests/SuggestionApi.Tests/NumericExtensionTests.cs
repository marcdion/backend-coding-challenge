using System;
using Shouldly;
using SuggestionApi.Domain.Extensions;
using Xunit;

namespace SuggestionApi.Tests
{
    public class NumericExtensionTests
    {
        [Fact]
        public void ToRadians_ShouldConvertProperly()
        {
            // Arrange
            const double angle = 43.70011;
            
            //Act
            var rad = angle.ToRadians();

            //Assert
            rad.ShouldSatisfyAllConditions(
                () => Math.Round(rad, 4).ShouldBe(0.7627)
            );
        }
    }
}