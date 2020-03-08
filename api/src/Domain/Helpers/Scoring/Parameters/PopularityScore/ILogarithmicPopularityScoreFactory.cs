namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.PopularityScore
{
    public interface ILogarithmicPopularityScoreFactory
    {
        double ComputeLogarithmicPopularityScore(double maxPopularity, double popularity);
    }
}