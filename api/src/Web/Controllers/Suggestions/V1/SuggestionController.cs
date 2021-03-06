﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Application.Suggestions.Dto;
using SuggestionApi.Domain.ErrorHandling;
using SuggestionApi.Domain.Helpers.Scoring;
using SuggestionApi.Domain.Helpers.Seed;
using SuggestionApi.Domain.Models.DataStructure;

namespace SuggestionApi.Web.Controllers.Suggestions.V1
{
    [ApiController]
    [ApiVersion( "1.0", Deprecated = true )]
    [Route("suggestions/")]
    [ApiExplorerSettings(GroupName = "1.0")]
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
        /// Generates an array of location suggestions (Deprecated)
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
            //To implement
            return BadRequest(new BadRequestError("This API endpoint is deprecated"));
        }
    }
}
