using AutoMapper;
using SuggestionApi.Domain.Models;

namespace SuggestionApi.Domain.Mapping
{
    public class GeoNameMappingProfile : Profile
    {
        public GeoNameMappingProfile()
        {
            CreateMap<GeoNameInput, GeoName>()
                .ForMember(b => b.Name, map => map.MapFrom(b => b.Name.Trim().ToLower()))
                .ForMember(b => b.AdministrativeRegion, map => map.MapFrom(b => MapAdministrativeRegion(b.AdministrativeRegion)));
        }

        public string MapAdministrativeRegion(string s)
        {
            switch (s)
            {
                case "1":
                    return "AB";
                case "2":
                    return "BC";
                case "3":
                    return "MB";
                case "4":
                    return "NB";
                case "5":
                    return "NL";
                case "7":
                    return "NS";
                case "8":
                    return "ON";
                case "9":
                    return "PE";
                case "10":
                    return "QC";
                case "11":
                    return "SK";
                case "12":
                    return "YK";
                case "13":
                    return "NT";
                case "14":
                    return "NU";
                default:
                    return s;
            }
        }
    }
}