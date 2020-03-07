namespace SuggestionApi.Domain.Helpers.Seed
{
    public interface ISeedDomainService
    {
        void SeedPrefixTree();
        void ResetPrefixTree();
    }
}