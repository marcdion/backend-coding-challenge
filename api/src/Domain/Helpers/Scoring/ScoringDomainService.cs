using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.EditDistance;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.PopularityScore;
using SuggestionApi.Domain.Helpers.Scoring.Parameters.PopulationScore;
using SuggestionApi.Domain.Models.ScoringWeights;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring
{
    public class ScoringDomainService : IScoringDomainService
    {
        private readonly IMapper _mapper;
        private readonly SharedScoringWeight _scoringWeight;
        private readonly ILogarithmicEditDistance _editDistance;
        private readonly ILogarithmicPopularityScore _popularityScore;
        private readonly ILogarithmicPopulationScore _populationScore;

        public ScoringDomainService(IMapper mapper, 
            SharedScoringWeight scoringWeight, 
            ILogarithmicEditDistance editDistance, 
            ILogarithmicPopularityScore popularityScore, 
            ILogarithmicPopulationScore populationScore)
        {
            _mapper = mapper;
            _scoringWeight = scoringWeight;
            _editDistance = editDistance;
            _popularityScore = popularityScore;
            _populationScore = populationScore;
        }

        public List<SuggestionDto> WeightedSuggestions(List<Suggestion> suggestions, int maxValues)
        {
            var weightedSuggestions = new List<SuggestionDto>();
            var maxPopularity = suggestions.Select(x => x.Popularity).DefaultIfEmpty(0).Max();

            foreach (var suggestion in suggestions)
            {
                suggestion.WeightedScore =
                    _popularityScore.ComputeLogarithmicPopularityScore(maxPopularity, suggestion.Popularity) +
                    _populationScore.ComputeLogarithmicPopulationScore(suggestion.Population) +
                    _editDistance.ComputeLogarithmicEditDistance(suggestion);

                weightedSuggestions.Add(_mapper.Map<SuggestionDto>(suggestion));
            }
            
            return weightedSuggestions.OrderByDescending(q => q.Score).Take(maxValues).ToList();
        }

        public List<SuggestionDto> WeightedSuggestionsWithCoordinates(List<Suggestion> suggestions, double latitude, double longitude, int maxValues)
        {
            return suggestions.OrderByDescending(q => q.WeightedScore).Take(maxValues).Select(q => _mapper.Map<SuggestionDto>(q)).ToList();
        }
    }
}