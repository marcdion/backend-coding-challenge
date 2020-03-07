using System;
using SuggestionApi.Domain.Models.ScoringWeights;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.PopularityScore
{
    public class LogarithmicPopularityScore : ILogarithmicPopularityScore
    {
        private readonly SharedScoringWeight _scoringWeight;

        public LogarithmicPopularityScore(SharedScoringWeight scoringWeight)
        {
            _scoringWeight = scoringWeight;
        }

        public double ComputeLogarithmicPopularityScore(double maxPopularity, double popularity)
        {
            var weight = _scoringWeight.ScoringWeight.PopularityScoreWeight;
            if (maxPopularity == 1)
                return 1 * weight;
            
            return Math.Log10(popularity) / Math.Log10(maxPopularity) * weight; 
        }
    }
}