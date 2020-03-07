using System.Collections.Generic;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Helpers.Scoring
{
    public interface IScoringDomainService
    {
        List<SuggestionDto> WeightedSuggestions(List<Suggestion> suggestions,int maxValues);
        List<SuggestionDto> WeightedSuggestionsWithCoordinates(List<Suggestion> suggestions, double latitude, double longitude, int maxValues);
    }
}