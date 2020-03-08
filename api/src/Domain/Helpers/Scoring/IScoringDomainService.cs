using System.Collections.Generic;
using SuggestionApi.Application.Suggestions.Dto;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring
{
    public interface IScoringDomainService
    {
        List<SuggestionDto> WeightedSuggestions(List<Suggestion> suggestions, int maxValues);
        List<SuggestionDto> WeightedSuggestionsWithCoordinates(List<Suggestion> suggestions, int maxValues, GeographicalLocation location);
    }
}