using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.EditDistance;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.PopularityScore;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.PopulationScore;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.TravelDistance;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.ScoringWeights;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring
{
    public class ScoringDomainService : IScoringDomainService
    {
        private readonly IMapper _mapper;
        private readonly SharedScoringWeight _scoringWeight;
        private readonly ILogarithmicEditDistanceFactory _editDistanceFactory;
        private readonly ILogarithmicPopularityScoreFactory _popularityScoreFactory;
        private readonly ILogarithmicPopulationScoreFactory _populationScoreFactory;
        private readonly ITravelDistanceScoreFactory _travelDistanceScoreFactory;

        public ScoringDomainService(IMapper mapper, 
            SharedScoringWeight scoringWeight, 
            ILogarithmicEditDistanceFactory editDistanceFactory, 
            ILogarithmicPopularityScoreFactory popularityScoreFactory, 
            ILogarithmicPopulationScoreFactory populationScoreFactory, 
            ITravelDistanceScoreFactory travelDistanceScoreFactory)
        {
            _mapper = mapper;
            _scoringWeight = scoringWeight;
            _editDistanceFactory = editDistanceFactory;
            _popularityScoreFactory = popularityScoreFactory;
            _populationScoreFactory = populationScoreFactory;
            _travelDistanceScoreFactory = travelDistanceScoreFactory;
        }

        public List<SuggestionDto> WeightedSuggestions(List<Suggestion> suggestions, int maxValues)
        {
            var weightedSuggestions = new List<SuggestionDto>();
            var maxPopularity = suggestions.Select(x => x.Popularity).DefaultIfEmpty(0).Max();

            foreach (var suggestion in suggestions)
            {
                suggestion.WeightedScore =
                    _popularityScoreFactory.ComputeLogarithmicPopularityScore(maxPopularity, suggestion.Popularity) +
                    _populationScoreFactory.ComputeLogarithmicPopulationScore(suggestion.Population) +
                    _editDistanceFactory.ComputeLogarithmicEditDistance(suggestion, false);
               
                weightedSuggestions.Add(_mapper.Map<SuggestionDto>(suggestion));
            }
            
            return weightedSuggestions.OrderByDescending(q => q.Score).Take(maxValues).ToList();
        }

        public List<SuggestionDto> WeightedSuggestionsWithCoordinates(List<Suggestion> suggestions, int maxValues, GeographicalLocation location)
        {
            var weightedSuggestions = new List<SuggestionDto>();
            var maxPopularity = suggestions.Select(x => x.Popularity).DefaultIfEmpty(0).Max();

            var distances = _travelDistanceScoreFactory.CalculateTravelDistances(location, suggestions);
            var maximumDistance = distances.Values.Max();

            for (var i = 0; i < suggestions.Count; i++)
            {
                var suggestion = suggestions.ElementAt(i);
                suggestions.ElementAt(i).WeightedScore =
                    _popularityScoreFactory.ComputeLogarithmicPopularityScore(maxPopularity, suggestion.Popularity) +
                    _populationScoreFactory.ComputeLogarithmicPopulationScore(suggestion.Population) +
                    _editDistanceFactory.ComputeLogarithmicEditDistance(suggestion, true) +
                    _travelDistanceScoreFactory.CalculateDistanceScore(distances[i], maximumDistance);
                
                weightedSuggestions.Add(_mapper.Map<SuggestionDto>(suggestion));
            }
            
            return weightedSuggestions.OrderByDescending(q => q.Score).Take(maxValues).ToList();
        }
    }
}