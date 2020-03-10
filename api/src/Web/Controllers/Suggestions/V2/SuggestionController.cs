using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Application.Suggestions.Dto;
using SuggestionApi.Domain.ErrorHandling;
using SuggestionApi.Domain.Extensions;
using SuggestionApi.Domain.Helpers.Scoring;
using SuggestionApi.Domain.Helpers.Seed;
using SuggestionApi.Domain.Models.DataStructure;
using SuggestionApi.Domain.Models.Locations;

namespace SuggestionApi.Web.Controllers.Suggestions.V2
{
    [ApiController]
    [ApiVersion( "2.0" )]
    [Route("suggestions/")]
    [ApiExplorerSettings(GroupName = "2.0")]
    public class SuggestionController : ControllerBase
    {
        private readonly ILogger<SuggestionController> _logger;
        private readonly ISeedDomainService _seedDomainService;
        private readonly IScoringDomainService _scoringDomainService;
        private readonly SharedTrie _trie;

        public SuggestionController(
            ILogger<SuggestionController> logger, 
            SharedTrie trie, 
            ISeedDomainService seedDomainService, 
            IScoringDomainService scoringDomainService)
        {
            _logger = logger;
            _trie = trie;
            _seedDomainService = seedDomainService;
            _scoringDomainService = scoringDomainService;
        }

        /// <summary>
        /// Generates an array of location suggestions
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /suggestions
        ///     {
        ///        "q": London,
        ///        "latitude": "43.70011",
        ///        "longitude": -79.4163,
        ///        "n": 3
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns array of suggestions</response>
        /// <response code="400">Latitude and/or longitude format is bad</response>
        [HttpGet]        
        public IActionResult GetSuggestions(string q, double? latitude, double? longitude, int? n)
        {
            List<SuggestionDto> weightedResults;
            var defaultResultSize = 10;
            
            // Make sure q does not equal "", ", '', ' and empty
            if (!q.IsValidInput())
                return BadRequest(new BadRequestError("The q paramter is mandatory"));

            if(_trie.Trie.IsEmpty())
                _seedDomainService.ResetPrefixTree();
            
            if (n.HasValue && n >= 0)
                defaultResultSize = n.Value;
            
            var coords = new GeographicalLocation(latitude, longitude);
            if(coords.Latitude.HasValue || coords.Longitude.HasValue)
                if(!coords.AreCoordinatesValid())
                    return BadRequest(new BadRequestError("Latitude and logitude are not proper format"));

            //Fetch suggestions
            var results = _trie.Trie.GetSuggestionsForPrefix(q.SanitizeInput()).ToList();
            
            //Add score to values
            if (coords.AreCoordinatesValid())
                weightedResults = _scoringDomainService.WeightedSuggestionsWithCoordinates(results, defaultResultSize, coords);
            else
                weightedResults = _scoringDomainService.WeightedSuggestions(results, defaultResultSize);

            return Ok(weightedResults);
        }
    }
}
