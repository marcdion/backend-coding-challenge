using Microsoft.AspNetCore.Mvc;
using SuggestionApi.Domain.Helpers.Seed;

namespace SuggestionApi.Web.Controllers
{
    [Route("seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ISeedDomainService _seedDomainService;

        public SeedController(ISeedDomainService seedDomainService)
        {
            _seedDomainService = seedDomainService;
        }

        /// <summary>
        /// Generates the data in the prefix tree from the TSV file
        /// </summary>
        /// <response code="200">Data has been seeded successfully</response>
        [HttpPost]
        public IActionResult Seed()
        {
            _seedDomainService.ResetPrefixTree();
            return Ok();
        }
    }
}    