using AutoMapper;
using cinema_api.DTOs;
using cinema_api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema_api.Controllers
{
	[ApiController]
	[Route("api/actor")]
	public class ActorController : ControllerBase
	{
		private readonly ApplicationDBContext _applicationContext;
		private readonly IMapper _mapper;

		public ActorController(ApplicationDBContext applicationDBContext, IMapper mapper)
		{
			_applicationContext = applicationDBContext;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<ActorDTO>>> GetAll()
		{
			List<Actor> authors = await _applicationContext.Actor.ToListAsync();

			List<ActorDTO> authorDTOs = _mapper.Map<List<ActorDTO>>(authors);
			return authorDTOs;
		}
	}
}
