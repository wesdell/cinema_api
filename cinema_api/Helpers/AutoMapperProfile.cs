using AutoMapper;
using cinema_api.DTOs;
using cinema_api.Entities;

namespace cinema_api.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Genre, GenreDTO>().ReverseMap();
		}
	}
}
