using AutoMapper;
using cinema_api.DTOs;
using cinema_api.Entities;
using cinema_api.Services;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema_api.Controllers
{
	[ApiController]
	[Route("api/movie")]
	public class MovieController : ControllerBase
	{
		private readonly ApplicationDBContext _applicationContext;
		private readonly CloudinaryService _cloudinary;
		private readonly IMapper _mapper;

		public MovieController(ApplicationDBContext applicationContext, IMapper mapper, CloudinaryService cloudinary)
		{
			_applicationContext = applicationContext;
			_cloudinary = cloudinary;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<MovieDTO>>> GetAll()
		{
			List<Movie> movies = await _applicationContext.Movie.ToListAsync();

			List<MovieDTO> movieDTOs = _mapper.Map<List<MovieDTO>>(movies);
			return movieDTOs;
		}

		[HttpGet("{id:int}", Name = "GetMovieById")]
		public async Task<ActionResult<MovieDTO>> GetById(int id)
		{
			Movie movie = await _applicationContext.Movie.FirstOrDefaultAsync(movie => movie.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			MovieDTO movieDTO = _mapper.Map<MovieDTO>(movie);
			return movieDTO;
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromForm] CreateMovieDTO createMovieDTO)
		{
			Movie movie = _mapper.Map<Movie>(createMovieDTO);

			if (createMovieDTO.Poster != null)
			{
				ImageUploadResult imageUploadResult = await _cloudinary.UploadImageAsync(createMovieDTO.Poster);

				if (imageUploadResult.Error != null)
				{
					return BadRequest(imageUploadResult.Error.Message);
				}

				movie.PosterURL = imageUploadResult.SecureUrl.ToString();
			}

			_applicationContext.Add(movie);
			await _applicationContext.SaveChangesAsync();

			MovieDTO movieDTO = _mapper.Map<MovieDTO>(movie);
			return new CreatedAtRouteResult("GetMovieById", new { id = movie.Id }, movieDTO);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put([FromForm] UpdateMovieDTO updateMovieDTO, int id)
		{
			bool existsMovie = await _applicationContext.Movie.AnyAsync(movie => movie.Id == id);

			if (!existsMovie)
			{
				return NotFound();
			}

			Movie movie = _mapper.Map<Movie>(updateMovieDTO);
			movie.Id = id;

			if (updateMovieDTO.Poster != null)
			{
				ImageUploadResult imageUploadResult = await _cloudinary.UploadImageAsync(updateMovieDTO.Poster);

				if (imageUploadResult.Error != null)
				{
					return BadRequest(imageUploadResult.Error.Message);
				}

				movie.PosterURL = imageUploadResult.SecureUrl.ToString();
			}

			_applicationContext.Entry(movie).State = EntityState.Modified;
			await _applicationContext.SaveChangesAsync();

			return NoContent();
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch([FromBody] JsonPatchDocument<PatchMovieDTO> patchDocument, int id)
		{
			if (patchDocument == null)
			{
				return BadRequest();
			}

			Movie movie = await _applicationContext.Movie.FirstOrDefaultAsync(movie => movie.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			PatchMovieDTO movieDTO = _mapper.Map<PatchMovieDTO>(movie);

			patchDocument.ApplyTo(movieDTO, ModelState);

			bool isValidDocument = TryValidateModel(movieDTO);

			if (!isValidDocument)
			{
				return BadRequest(ModelState);
			}

			_mapper.Map(movieDTO, movie);

			await _applicationContext.SaveChangesAsync();
			return NoContent();
		}
	}
}
