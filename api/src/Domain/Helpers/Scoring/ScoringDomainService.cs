using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring
{
    public class ScoringDomainService : IScoringDomainService
    {
        private readonly IMapper _mapper;

        public ScoringDomainService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<SuggestionDto> WeightedSuggestions(List<Suggestion> suggestions, int maxValues)
        {
            return suggestions.Take(maxValues).Select(q => _mapper.Map<SuggestionDto>(q)).OrderByDescending(q => q.Score).ToList();
        }

        public List<SuggestionDto> WeightedSuggestionsWithCoordinates(List<Suggestion> suggestions, double latitude, double longitude, int maxValues)
        {
            return suggestions.Take(maxValues).Select(q => _mapper.Map<SuggestionDto>(q)).OrderByDescending(q => q.Score).ToList();
        }
    }
}