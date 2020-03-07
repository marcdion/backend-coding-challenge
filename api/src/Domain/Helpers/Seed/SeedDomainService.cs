using System.Globalization;
using System.IO;
using System.Linq;
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
        private readonly SharedScoringWeight _scoringWeight;
        private readonly IMapper _mapper;

        public SeedDomainService(SharedTrie trie, IMapper mapper, SharedScoringWeight scoringWeight)
        {
            _trie = trie;
            _mapper = mapper;
            _scoringWeight = scoringWeight;
        }

        const string geoFileName = "cities_canada-usa.tsv";

        public void SeedPrefixTree()
        {
            using (var reader = new StreamReader(@$"..\..\api\src\Domain\DataSource\{geoFileName}"))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = true;
                var geoNames = csv.GetRecords<LocationInput>();
                
                // We want to order the locations by the population size. See ADR-002.md for details
                var orderedGeoNames = geoNames.OrderByDescending(q => q.Population).ToList();
                var ceilingValue = orderedGeoNames.Select(x => x.Population).DefaultIfEmpty(0).Max() / _scoringWeight.ScoringWeight.BaseScoreWeight;

                foreach (var geoName in orderedGeoNames)
                    _trie.Trie.Insert(_mapper.Map<Location>(geoName), ceilingValue);
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