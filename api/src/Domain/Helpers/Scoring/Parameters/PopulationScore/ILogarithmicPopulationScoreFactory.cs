namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.PopulationScore
{
    public interface ILogarithmicPopulationScoreFactory
    {
        double ComputeLogarithmicPopulationScore(double? population);
    }
}