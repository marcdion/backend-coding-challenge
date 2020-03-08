namespace SuggestionApi.Application.Suggestions.Dto
{
    /// <summary>
    /// Data transfer object used to transfer data to client
    /// </summary>
    public class SuggestionDto
    {
        public string Name { get; set; }
        
        public double? Latitude { get; set; }
        
        public double? Longitude { get; set; }
        public double Score { get; set; }
    }
}