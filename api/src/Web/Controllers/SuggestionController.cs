using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuggestionApi.Domain.Models;
using SuggestionApi.Domain.Models.DataStructure;
using SuggestionApi.Domain.Models.Suggestions;

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
        public List<Suggestion> GetSuggestions(string q)
        {
            if(string.IsNullOrEmpty(q))
                return new List<Suggestion>();
                
            var results = _trie.Trie.GetSuggestionsForPrefix(q);
            return results.ToList();
        }
    }
}
