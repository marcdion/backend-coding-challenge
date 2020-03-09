using Microsoft.AspNetCore.Mvc;
using SuggestionApi.Application.Status.Dto;
using SuggestionApi.Domain.ErrorHandling;
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
        /// <response code="400">Data was empty! It was reseeded</response>
        [HttpGet]
        public IActionResult Status()
        {
            if (_trie.Trie.IsEmpty())
            {
                _seedDomainService.ResetPrefixTree();
                return BadRequest(new BadRequestError("Trie was empty. Is has been re initialized!"));
            }

            return Ok(new Ok("Everything is working fine!"));
        }
    }
}