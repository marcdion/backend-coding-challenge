using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Helpers.Geo;
using SuggestionApi.Domain.Helpers.Scoring;
using SuggestionApi.Domain.Helpers.Seed;
using SuggestionApi.Domain.Models.DataStructure;

namespace SuggestionApi.Web.Controllers.Suggestions.V2
{
    [ApiController]
    [ApiVersion( "2.0" )]
    [Route("suggestions/")]
    public class SuggestionController : ControllerBase
    {
        private readonly ILogger<SuggestionController> _logger;
        private readonly ISeedDomainService _seedDomainService;
        private readonly IScoringDomainService _scoringDomainService;
        private readonly IGeoDomainService _geoDomainService;
        private readonly SharedTrie _trie;

        public SuggestionController(
            ILogger<SuggestionController> logger, 
            SharedTrie trie, 
            ISeedDomainService seedDomainService, 
            IScoringDomainService scoringDomainService, 
            IGeoDomainService geoDomainService)
        {
            _logger = logger;
            _trie = trie;
            _seedDomainService = seedDomainService;
            _scoringDomainService = scoringDomainService;
            _geoDomainService = geoDomainService;
        }

        [HttpGet]        
        public List<SuggestionDto> GetSuggestions(string q, double? latitude, double? longitude, int? n)
        {
            var defaultResultSize = 10;
            if(string.IsNullOrEmpty(q))
                return new List<SuggestionDto>();
            
            if(_trie.Trie.IsEmpty())
                _seedDomainService.ResetPrefixTree();
            
            if (n.HasValue && n >= 0)
                defaultResultSize = n.Value;
            
            //Fetch suggestions
            var results = _trie.Trie.GetSuggestionsForPrefix(q).ToList();

            //Add score to values
            List<SuggestionDto> weightedResults;
            if (_geoDomainService.AreCoordinatesValid(latitude, longitude))
                weightedResults = _scoringDomainService.WeightedSuggestionsWithCoordinates(results, latitude.Value, longitude.Value, defaultResultSize);
            else
                weightedResults = _scoringDomainService.WeightedSuggestions(results, defaultResultSize);

            return weightedResults;
        }
    }
}
