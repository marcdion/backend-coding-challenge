using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Helpers.Geo;
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
            //To implement
            return new List<SuggestionDto>();
        }
    }
}
