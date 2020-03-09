using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using CsvHelper;
using SuggestionApi.Domain.Models.DataStructure;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.ScoringWeights;

namespace SuggestionApi.Domain.Helpers.Seed
{
    public class SeedDomainService : ISeedDomainService
    {
        private readonly SharedTrie _trie;
        private readonly IMapper _mapper;

        public SeedDomainService(SharedTrie trie, IMapper mapper)
        {
            _trie = trie;
            _mapper = mapper;
        }

        const string locationFileName = "cities_canada-usa.tsv";

        public void SeedPrefixTree()
        {
            using (var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @$"..\..\api\src\Domain\DataSource\{locationFileName}"), Encoding.GetEncoding("iso-8859-1")))
            {
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = true;
                var locations = csv.GetRecords<LocationInput>();
                
                // We want to order the locations by the population size. See ADR-002.md for details
                var orderedLocations = locations.OrderByDescending(q => q.Population).ToList();

                foreach (var locationInput in orderedLocations)
                    _trie.Trie.Insert(_mapper.Map<Location>(locationInput));
            }
        }

        public void ResetPrefixTree()
        {
            //Reset trie
            _trie.Trie.Reset();
            SeedPrefixTree();
        }
    }
}