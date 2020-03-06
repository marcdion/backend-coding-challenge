namespace SuggestionApi.Domain.Models.ScoringWeights
{
    // See ADR-004 and Scoring docs for more information about the Scoring algorithm
    public class ScoringWeight
    {
        public ScoringWeight(int baseScoreWeight, int popularityScoreWeight,
            int depthDifferenceWithLocationScoreWeight,int depthDifferenceWithoutLocationScoreWeight, 
            int locationDistanceScoreWeight)
        {
            BaseScoreWeight = baseScoreWeight;
            PopularityScoreWeight = popularityScoreWeight;
            DepthDifferenceWithLocationScoreWeight = depthDifferenceWithLocationScoreWeight;
            DepthDifferenceWithoutLocationScoreWeight = depthDifferenceWithoutLocationScoreWeight;
            LocationDistanceScoreWeight = locationDistanceScoreWeight;
        }
        
        public int BaseScoreWeight { get; set; }
        public int PopularityScoreWeight { get; set; }
        public int DepthDifferenceWithLocationScoreWeight { get; set; }
        public int DepthDifferenceWithoutLocationScoreWeight { get; set; }
        public int LocationDistanceScoreWeight { get; set; }
    }
}