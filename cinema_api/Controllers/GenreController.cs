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
		public async Task<ActionResult<List<GenreDTO>>> GetAll()
		{
			List<Genre> genres = await _applicationContext.Genre.ToListAsync();

			List<GenreDTO> genreDTOs = _mapper.Map<List<GenreDTO>>(genres);
			return genreDTOs;
		}

		[HttpGet("{id:int}", Name = "GetById")]
		public async Task<ActionResult<GenreDTO>> GetById(int id)
		{
			Genre genre = await _applicationContext.Genre.FirstOrDefaultAsync(genere => genre.Id == id);

			if (genre == null)
			{
				return NotFound();
			}

			GenreDTO genreDTO = _mapper.Map<GenreDTO>(genre);
			return genreDTO;
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateGenreDTO createGenreDTO)
		{
			Genre newGenre = _mapper.Map<Genre>(createGenreDTO);

			_applicationContext.Add(newGenre);
			await _applicationContext.SaveChangesAsync();

			GenreDTO genreDTO = _mapper.Map<GenreDTO>(newGenre);
			return new CreatedAtRouteResult("GetById", new { id = genreDTO.Id }, genreDTO);
		}
	}
}
