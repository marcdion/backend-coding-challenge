namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.PopulationScore
{
    public interface ILogarithmicPopulationScore
    {
        double ComputeLogarithmicPopulationScore(double? population);
    }
}