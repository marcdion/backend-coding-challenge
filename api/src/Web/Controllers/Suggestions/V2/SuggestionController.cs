﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Application.Suggestions.Dto;
using SuggestionApi.Domain.ErrorHandling;
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
            var weightedResults = new List<SuggestionDto>();
            var defaultResultSize = 10;
            if (string.IsNullOrEmpty(q))
                return BadRequest(new BadRequestError("The q paramter is mandatory"));
            
            if(_trie.Trie.IsEmpty())
                _seedDomainService.ResetPrefixTree();
            
            if (n.HasValue && n >= 0)
                defaultResultSize = n.Value;
            
            //Fetch suggestions
            var results = _trie.Trie.GetSuggestionsForPrefix(SanitizeInput(q)).ToList();
            
            //Add score to values
            var coords = new GeographicalLocation(latitude, longitude);
            if (coords.AreCoordinatesValid())
                weightedResults = _scoringDomainService.WeightedSuggestionsWithCoordinates(results, defaultResultSize, coords);
            else
                weightedResults = _scoringDomainService.WeightedSuggestions(results, defaultResultSize);

            return Ok(weightedResults);
        }

        private string SanitizeInput(string s)
        {
            //We are not striping diacritics, see ADR-007
            return s.Trim().ToLower();
        }
    }
}
