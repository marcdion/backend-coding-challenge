using Microsoft.AspNetCore.Mvc;
using SuggestionApi.Application.Status.Dto;
using SuggestionApi.Domain.Helpers.Seed;
using SuggestionApi.Domain.Models.DataStructure;

namespace SuggestionApi.Web.Controllers
{
    [Route("status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly SharedTrie _trie;
        private readonly ISeedDomainService _seedDomainService;

        public StatusController(SharedTrie trie, 
            ISeedDomainService seedDomainService)
        {
            _trie = trie;
            _seedDomainService = seedDomainService;
        }

        /// <summary>
        /// Returns the API status for Uptime Robot
        /// </summary>
        /// <response code="200">Data has been seeded successfully!</response>
        [HttpGet]
        public StatusDto Status()
        {
            if (_trie.Trie.IsEmpty())
            {
                _seedDomainService.ResetPrefixTree();
                return new StatusDto
                {
                    Status = "Trie was empty. Is has been re initialized!",
                };
            }
            
            return new StatusDto
            {
                Status = "Everything is fine!",
            };
        }
    }
}