using System;
using System.Collections.Generic;
using System.Linq;
using SuggestionApi.Appplication.Seed;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Domain.Helpers
{
    public class SeedDomainService : ISeedDomainService
    {

        public void SeedPrefixTree(IEnumerable<GeoNameInput> geoNames)
        {
            // We want to order the locations by the population size. See ADR-002.md for details
            IEnumerable<GeoNameInput> orderedGeoNames = geoNames.OrderBy(q => q.Population);
            var avg = orderedGeoNames.Select(x => x.Population).DefaultIfEmpty(0).Average();
            var prefixTree = new Trie();
            
            foreach (var geoName in orderedGeoNames)
            {
                prefixTree.Insert(geoName.Name);
            }
        }
    }
}