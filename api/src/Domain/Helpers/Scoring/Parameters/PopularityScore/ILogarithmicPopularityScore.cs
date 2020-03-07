namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.PopularityScore
{
    public interface ILogarithmicPopularityScore
    {
        double ComputeLogarithmicPopularityScore(double maxPopularity, double popularity);
    }
}