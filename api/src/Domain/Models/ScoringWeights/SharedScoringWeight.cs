namespace SuggestionApi.Domain.Models.ScoringWeights
{
    public class SharedScoringWeight
    {
        //Sum of all relevant weights should equal 10
        
        //Always present
        private const int BASE_SCORE_WEIGHT = 1;
        private const int POPULARITY_SCORE_WEIGHT = 3;
        
        //Scenario 1
        private const int DEPTH_DIFFERENCE_WITHOUT_LOCATION_SCORE_WEIGHT = 6;
        
        //Scenario 2
        private const int DEPTH_DIFFERENCE_WITH_LOCATION_SCORE_WEIGHT = 4;
        private const int LOCATION_DISTANCE_SCORE_WEIGHT = 2;
        
        private readonly ScoringWeight _scoringWeight
            = new ScoringWeight(BASE_SCORE_WEIGHT, POPULARITY_SCORE_WEIGHT,
                DEPTH_DIFFERENCE_WITH_LOCATION_SCORE_WEIGHT, DEPTH_DIFFERENCE_WITHOUT_LOCATION_SCORE_WEIGHT,
                LOCATION_DISTANCE_SCORE_WEIGHT);

        public ScoringWeight ScoringWeight => _scoringWeight;
    }
}