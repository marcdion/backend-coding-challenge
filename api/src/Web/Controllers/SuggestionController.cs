using System.Collections.Generic;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Web.Controllers
{
    [Route("suggestions/")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ILogger<SuggestionController> _logger;
        private readonly SharedTrie _trie;

        public SuggestionController(ILogger<SuggestionController> logger, 
            SharedTrie trie)
        {
            _logger = logger;
            _trie = trie;
        }

        [HttpGet]        
        public List<SuggestionDto> GetSuggestions(string q)
        {
            var results = _trie.Trie.GetSuggestionsForPrefix(q);

            return new List<SuggestionDto>
            {
                new SuggestionDto
                {
                    Name = "London, ON, Canada",
                    Latitude = 42.98339,
                    Longitude = -81.23304,
                    Score = 0.9
                },
                new SuggestionDto
                {
                    Name = "Victoria, BC, Canada",
                    Latitude = 42.98339,
                    Longitude = -81.23304,
                    Score = 0.9
                }
            };
        }
    }
}
