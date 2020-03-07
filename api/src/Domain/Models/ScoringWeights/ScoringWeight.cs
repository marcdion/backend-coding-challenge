namespace SuggestionApi.Domain.Models.ScoringWeights
{
    // See ADR-004 and Scoring docs for more information about the Scoring algorithm
    public class ScoringWeight
    {
        public ScoringWeight(double populationScoreWeight, double popularityScoreWeight,
            double depthDifferenceWithLocationScoreWeight,double depthDifferenceWithoutLocationScoreWeight, 
            double locationDistanceScoreWeight)
        {
            PopulationScoreWeight = populationScoreWeight;
            PopularityScoreWeight = popularityScoreWeight;
            DepthDifferenceWithLocationScoreWeight = depthDifferenceWithLocationScoreWeight;
            DepthDifferenceWithoutLocationScoreWeight = depthDifferenceWithoutLocationScoreWeight;
            LocationDistanceScoreWeight = locationDistanceScoreWeight;
        }
        
        public double PopulationScoreWeight { get; set; }
        public double PopularityScoreWeight { get; set; }
        public double DepthDifferenceWithLocationScoreWeight { get; set; }
        public double DepthDifferenceWithoutLocationScoreWeight { get; set; }
        public double LocationDistanceScoreWeight { get; set; }
    }
}