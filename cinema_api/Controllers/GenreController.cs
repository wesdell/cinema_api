using AutoMapper;
using cinema_api.DTOs;
using cinema_api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema_api.Controllers
{
	[ApiController]
	[Route("api/genre")]
	public class GenreController : ControllerBase
	{
		private readonly ApplicationDBContext _applicationContext;
		private readonly IMapper _mapper;

		public GenreController(ApplicationDBContext applicationDBContext, IMapper mapper)
		{
			_applicationContext = applicationDBContext;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<GenreDTO>>> Get()
		{
			List<Genre> genres = await _applicationContext.Genre.ToListAsync();
			List<GenreDTO> genreDTOs = _mapper.Map<List<GenreDTO>>(genres);
			return genreDTOs;
		}
	}
}
