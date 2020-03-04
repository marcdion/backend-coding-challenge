using System.Collections.Generic;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SuggestionApi.Web.Controllers
{
    [Route("suggestions/")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ILogger<SuggestionController> _logger;

        public SuggestionController(ILogger<SuggestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]        
        public List<SuggestionDto> GetSuggestions(string q)
        {
            return new List<SuggestionDto>
            {
                new SuggestionDto
                {
                    Name = "London, ON, Canada",
                    Latitude = 42.98339,
                    Longitude = -81.23304,
                    Score = 0.9
                }
            };
        }
    }
}
