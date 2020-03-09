using System.Collections.Generic;
using System.Linq;
using Shouldly;
using SuggestionApi.Domain.Models.Locations;
using Xunit;

namespace SuggestionApi.Tests.Trie
{
    public class TrieTests
    {
        [Fact]
        public void IsEmpty_ShouldReturnTrue_WhenEmpty()
        {
            //Arrange
            var trie = new Domain.Models.DataStructure.Trie();
            
            //Act
            var result = trie.IsEmpty();
            
            //Assert
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeTrue()
            );
        }

        [Fact]
        public void Reset_ShouldEmptyTrie()
        {
            //Arrange
            var trie = new Domain.Models.DataStructure.Trie();
            trie.Insert(new Location
            {
                Name = "Amos",
                Latitude = -43.0987,
                Longitude = 127.6782,
                Country = "CA",
                AdministrativeRegion = "10",
                Population = 25000
            });
            
            //Act
            trie.Reset();
            
            //Assert
            var isEmpty = trie.IsEmpty();
            isEmpty.ShouldSatisfyAllConditions(
                () => isEmpty.ShouldBeTrue()
            );
        }

        [Fact]
        public void Insert_ShouldFillTrieProperly()
        {
            //Arrange
            var trie = new Domain.Models.DataStructure.Trie();
            var values = new List<Location>
            {
                new Location
                {
                    Name = "Carleton",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                },
                new Location
                {
                    Name = "Caraquette",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                }
            };

            foreach (var value in values)
                trie.Insert(value);
            
            //Act
            var results = trie.GetSuggestionsForPrefix("Car").ToList();
            
            //Assert
            results.ShouldSatisfyAllConditions(
                () => results.Count.ShouldBe(2)
            );
        }
        
        [Fact]
        public void GetSuggestionsForPrefix_ShouldReturnTwoLocations_ForSameName()
        {
            //Arrange
            var trie = new Domain.Models.DataStructure.Trie();
            var values = new List<Location>
            {
                new Location
                {
                    Name = "Carleton",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                },
                new Location
                {
                    Name = "Carleton",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                }
            };

            foreach (var value in values)
                trie.Insert(value);
            
            //Act
            var results = trie.GetSuggestionsForPrefix("Car").ToList();
            
            //Assert
            results.ShouldSatisfyAllConditions(
                () => results.Count.ShouldBe(2)
            );
        }
        
        [Fact]
        public void GetSuggestionsForPrefix_ShouldReturnTwoLocations_WhenPrefixIsWord()
        {
            //Arrange
            var trie = new Domain.Models.DataStructure.Trie();
            var values = new List<Location>
            {
                new Location
                {
                    Name = "Mont-Saint",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                },
                new Location
                {
                    Name = "Mont-Saint-Anne",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                }
            };

            foreach (var value in values)
                trie.Insert(value);
            
            //Act
            var results = trie.GetSuggestionsForPrefix("mont-s").ToList();
            
            //Assert
            results.ShouldSatisfyAllConditions(
                () => results.Count.ShouldBe(2)
            );
        }
        
        [Fact]
        public void GetSuggestionsForPrefix_ShouldReturnProperResults()
        {
            //Arrange
            var trie = new Domain.Models.DataStructure.Trie();
            var values = new List<Location>
            {
                new Location
                {
                    Name = "Mont",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                },
                new Location
                {
                    Name = "Mont-Saint-Anne",
                    Latitude = -43.0987,
                    Longitude = 127.6782,
                    Country = "CA",
                    AdministrativeRegion = "10",
                    Population = 25000
                }
            };

            foreach (var value in values)
                trie.Insert(value);
            
            //Act
            var results = trie.GetSuggestionsForPrefix("mont-s").ToList();
            
            //Assert
            results.ShouldSatisfyAllConditions(
                () => results.Count.ShouldBe(1)
            );
        }
    }
}