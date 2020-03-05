using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SuggestionApi.Appplication.Seed;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Domain.Helpers
{
    public class SeedDomainService : ISeedDomainService
    {

        public void SeedPrefixTree(IEnumerable<GeoNameInput> geoNames)
        {
            // We want to order the locations by the population size. See ADR-002.md for details
            var orderedGeoNames = geoNames.OrderByDescending(q => q.Population).ToList();
            var avg = orderedGeoNames.Select(x => x.Population).DefaultIfEmpty(0).Average();
            
            var prefixTree = new Trie();

            foreach (var optimizedString in orderedGeoNames.Select(geoName => Regex.Replace(geoName.Name, @"\s+", "").ToLower()))
                prefixTree.Insert(optimizedString);
        }
    }
}