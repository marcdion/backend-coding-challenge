using System;
using AutoMapper;
using SuggestionApi.Appplication.Suggestions.Dto;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Mapping
{
    public class SuggestionMappingProfile : Profile
    {
        public SuggestionMappingProfile()
        {
            CreateMap<Suggestion, SuggestionDto>()
                .ForMember(b => b.Score, map => map.MapFrom(b => Math.Round(b.BaseScore, 5)));
        }
    }
}