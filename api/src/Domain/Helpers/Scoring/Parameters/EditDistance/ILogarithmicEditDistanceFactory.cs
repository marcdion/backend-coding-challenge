using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.EditDistance
{
    public interface ILogarithmicEditDistanceFactory
    {
        double ComputeLogarithmicEditDistance(Suggestion suggestion, bool areCoordinatesValid);
    }
}