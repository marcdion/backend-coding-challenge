namespace SuggestionApi.Domain.Models.ScoringWeights
{
    /// <summary>
    /// See ADR-004 and Scoring docs for more information about the Scoring algorithm
    /// </summary>
    public class SharedScoringWeight
    {
        //Sum of all relevant weights should equal 1
        
        //Always present
        private const double POPULARITY_SCORE_WEIGHT = 0.10;
        private const double POPULATION_SCORE_WEIGHT = 0.20;
        
        //Scenario 1
        private const double DEPTH_DIFFERENCE_WITHOUT_LOCATION_SCORE_WEIGHT = 0.70;
        
        //Scenario 2
        private const double DEPTH_DIFFERENCE_WITH_LOCATION_SCORE_WEIGHT = 0.50;
        private const double LOCATION_DISTANCE_SCORE_WEIGHT = 0.20;
        
        private readonly ScoringWeight _scoringWeight
            = new ScoringWeight(POPULATION_SCORE_WEIGHT, POPULARITY_SCORE_WEIGHT,
                DEPTH_DIFFERENCE_WITH_LOCATION_SCORE_WEIGHT, DEPTH_DIFFERENCE_WITHOUT_LOCATION_SCORE_WEIGHT,
                LOCATION_DISTANCE_SCORE_WEIGHT);

        public ScoringWeight ScoringWeight => _scoringWeight;
    }
}