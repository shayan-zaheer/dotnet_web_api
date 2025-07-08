using AutoMapper;
using Core_Web_API.Models.DTOs;
using Core_Web_API.Models.Entities;

namespace Core_Web_API.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, ReadPlayerDto>(); // player -> dto
            CreateMap<CreatePlayerDto, Player>(); // dto -> player
        }
    }
}
