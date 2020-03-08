using System.Collections.Generic;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.TravelDistance
{
    public interface ITravelDistanceScoreFactory
    {
        Dictionary<int, double> CalculateTravelDistances(GeographicalLocation input, List<Suggestion> suggestion);
        double CalculateDistanceScore(double distance, double max);
    }
}