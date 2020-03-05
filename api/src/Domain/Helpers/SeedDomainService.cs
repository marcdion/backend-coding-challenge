using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Domain.Helpers
{
    public class SeedDomainService : ISeedDomainService
    {
        private readonly SharedTrie _trie;

        public SeedDomainService(SharedTrie trie)
        {
            _trie = trie;
        }

        const string geoFileName = "cities_canada-usa.tsv";

        public void SeedPrefixTree()
        {
            System.Threading.Thread.Sleep(10000);
            using (var reader = new StreamReader(@$"..\..\api\src\Domain\DataSource\{geoFileName}"))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = true;
                var geoNames = csv.GetRecords<GeoNameInput>();
                
                // We want to order the locations by the population size. See ADR-002.md for details
                var orderedGeoNames = geoNames.OrderByDescending(q => q.Population).ToList();
                var avg = orderedGeoNames.Select(x => x.Population).DefaultIfEmpty(0).Average();

                foreach (var optimizedString in orderedGeoNames.Select(geoName => Regex.Replace(geoName.Name, @"\s+", "").ToLower()))
                    _trie.Trie.Insert(optimizedString);
            }
        }
    }
}