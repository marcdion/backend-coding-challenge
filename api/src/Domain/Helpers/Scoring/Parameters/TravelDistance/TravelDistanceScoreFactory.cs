using System;
using System.Collections.Generic;
using System.Linq;
using SuggestionApi.Domain.Extensions;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.ScoringWeights;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring.Parameters.TravelDistance
{
    public class TravelDistanceScoreFactory : ITravelDistanceScoreFactory
    {
        public enum DistanceUnit { Miles, Kilometers };

        private readonly SharedScoringWeight _scoringWeight;
        private const int EARTH_RADIUS_KM = 6371;
        private const int EARTH_RADIUS_MILES = 3958;

        public TravelDistanceScoreFactory(SharedScoringWeight scoringWeight)
        {
            _scoringWeight = scoringWeight;
        }

        public Dictionary<int, double> CalculateTravelDistances(GeographicalLocation input, List<Suggestion> suggestions)
        {
            var dict = new Dictionary<int, double>();

            for (int i = 0; i < suggestions.Count; i++)
                dict[i] = CalculateDistanceWithHaversineDistanceFormula(input, suggestions.ElementAt(i));

            return dict;
        }

        private double CalculateDistanceWithHaversineDistanceFormula(GeographicalLocation input, Suggestion suggestion, DistanceUnit unit = DistanceUnit.Kilometers)
        {
            double radius = (unit == DistanceUnit.Kilometers) ? EARTH_RADIUS_KM : EARTH_RADIUS_MILES;
            
            var latitudeInRadians = (suggestion.Latitude.Value - input.Latitude.Value).ToRadians();
            var longitudeInRadians = (suggestion.Longitude.Value - input.Longitude.Value).ToRadians();
            
            var h1 = Math.Sin(latitudeInRadians / 2) * Math.Sin(latitudeInRadians / 2) +
                     Math.Cos(input.Latitude.Value.ToRadians()) * Math.Cos(suggestion.Latitude.Value.ToRadians()) *
                     Math.Sin(longitudeInRadians / 2) * Math.Sin(longitudeInRadians / 2);
            
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            
            return radius * h2;
        }

        public double CalculateDistanceScore(double distance, double max)
        {
            var weight = _scoringWeight.ScoringWeight.LocationDistanceScoreWeight;
            //If location and query match exactly
            if (Math.Abs(distance) < 0.001)
                return 1 * weight;
            
            return (1 - Math.Log10(distance) / Math.Log10(max)) * weight;

        }
    }
}