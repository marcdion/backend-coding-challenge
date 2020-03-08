using System;
using SuggestionApi.Domain.Models.ScoringWeights;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.EditDistance
{
    public class LogarithmicEditDistanceFactory : ILogarithmicEditDistanceFactory
    {
        private readonly SharedScoringWeight _scoringWeight;

        public LogarithmicEditDistanceFactory(SharedScoringWeight scoringWeight)
        {
            _scoringWeight = scoringWeight;
        }

        public double ComputeLogarithmicEditDistance(Suggestion suggestion, bool areCoordinatesValid)
        {
            var weight = areCoordinatesValid ? _scoringWeight.ScoringWeight.DepthDifferenceWithLocationScoreWeight : _scoringWeight.ScoringWeight.DepthDifferenceWithoutLocationScoreWeight;
            if (suggestion.DepthDifference == 0 || suggestion.DepthDifference == 1)
                return 1 * weight;
            
            return (1 - Math.Log10(suggestion.DepthDifference) / Math.Log10(suggestion.Name.Length)) * weight;        }
    }
}