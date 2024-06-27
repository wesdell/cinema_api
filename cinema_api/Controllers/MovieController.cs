using AutoMapper;
using cinema_api.DTOs;
using cinema_api.Entities;
using cinema_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema_api.Controllers
{
	[ApiController]
	[Route("api/movie")]
	public class MovieController
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
	}
}
