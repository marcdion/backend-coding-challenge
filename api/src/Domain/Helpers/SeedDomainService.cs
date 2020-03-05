using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using CsvHelper;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Domain.Helpers
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

        const string geoFileName = "cities_canada-usa.tsv";

        public void SeedPrefixTree()
        {
            using (var reader = new StreamReader(@$"..\..\api\src\Domain\DataSource\{geoFileName}"))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.HasHeaderRecord = true;
                var geoNames = csv.GetRecords<GeoNameInput>();
                
                // We want to order the locations by the population size. See ADR-002.md for details
                var orderedGeoNames = geoNames.OrderByDescending(q => q.Population).ToList();
                var avg = orderedGeoNames.Select(x => x.Population).DefaultIfEmpty(0).Average();

                foreach (var geoName in orderedGeoNames)
                    _trie.Trie.Insert(_mapper.Map<GeoName>(geoName));
            }
        }
    }
}