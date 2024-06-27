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
			CreateMap<CreateGenreDTO, Genre>();
			CreateMap<UpdateGenreDTO, Genre>();

			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<CreateActorDTO, Actor>();
			CreateMap<UpdateActorDTO, Actor>();
			CreateMap<Actor, PatchActorDTO>().ReverseMap();

			CreateMap<Movie, MovieDTO>().ReverseMap();
		}
	}
}
