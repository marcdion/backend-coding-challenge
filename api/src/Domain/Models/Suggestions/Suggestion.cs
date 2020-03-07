namespace SuggestionApi.Domain.Models.Suggestions
{
    public class Suggestion
    {
        public string Name { get; set; }
        
        public double? Latitude { get; set; }
        
        public double? Longitude { get; set; }
        
        public double BaseScore { get; set; }
        
        public double WeightedScore { get; set; }
        
        public int DepthDifference { get; set; }
        
        public int Popularity { get; set; }
    }
}