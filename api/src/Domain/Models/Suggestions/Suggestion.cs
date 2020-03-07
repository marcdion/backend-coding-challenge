namespace SuggestionApi.Domain.Models.Suggestions
{
    public class Suggestion
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double WeightedScore { get; set; }
        public double DepthDifference { get; set; }
        public double Popularity { get; set; }
        
        public double? Population { get; set; }
    }
}