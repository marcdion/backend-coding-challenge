using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.EditDistance
{
    public interface ILogarithmicEditDistance
    {
        double ComputeLogarithmicEditDistance(Suggestion suggestion);
    }
}