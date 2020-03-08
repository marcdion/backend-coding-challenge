using System;
using SuggestionApi.Domain.Models.ScoringWeights;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.PopulationScore
{
    public class LogarithmicPopulationScoreFactoryFactory : ILogarithmicPopulationScoreFactory
    {
        private readonly SharedScoringWeight _scoringWeight;
        private const double MAX_POPULATION_SIZE = 8175133;

        public LogarithmicPopulationScoreFactoryFactory(SharedScoringWeight scoringWeight)
        {
            _scoringWeight = scoringWeight;
        }

        public double ComputeLogarithmicPopulationScore(double? population)
        {
            var weight = _scoringWeight.ScoringWeight.PopulationScoreWeight;
            if (population.HasValue)
                return Math.Log10(population.Value) / Math.Log10(MAX_POPULATION_SIZE) * weight;
            
            return 0;       
        }
    }
}