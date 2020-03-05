using System.Collections.Generic;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Appplication.Seed
{
    public interface ISeedDomainService
    {
        void SeedPrefixTree(IEnumerable<GeoNameInput> geoNames);
    }
}