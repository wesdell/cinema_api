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


		[HttpGet("{id:int}", Name = "GetActorById")]
		public async Task<ActionResult<ActorDTO>> GetById(int id)
		{
			Actor actor = await _applicationContext.Actor.FirstOrDefaultAsync(actor => actor.Id == id);

			if (actor == null)
			{
				return NotFound();
			}

			ActorDTO actorDTO = _mapper.Map<ActorDTO>(actor);
			return actorDTO;
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateActorDTO createActorDTO)
		{
			Actor actor = _mapper.Map<Actor>(createActorDTO);

			_applicationContext.Add(actor);
			await _applicationContext.SaveChangesAsync();

			ActorDTO actorDTO = _mapper.Map<ActorDTO>(actor);
			return new CreatedAtRouteResult("GetActorById", new { id = actor.Id }, actorDTO);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put([FromBody] UpdateActorDTO updateActorDTO, int id)
		{
			Actor actor = _mapper.Map<Actor>(updateActorDTO);
			actor.Id = id;

			_applicationContext.Entry(actor).State = EntityState.Modified;
			await _applicationContext.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			bool existsActor = await _applicationContext.Actor.AnyAsync(actor => actor.Id == id);

			if (!existsActor)
			{
				return NotFound();
			}

			_applicationContext.Remove(new { Id = id });
			await _applicationContext.SaveChangesAsync();

			return NoContent();
		}
	}
}
