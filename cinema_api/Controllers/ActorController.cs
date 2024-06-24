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
	[Route("api/actor")]
	public class ActorController : ControllerBase
	{
		private readonly ApplicationDBContext _applicationContext;
		private readonly CloudinaryService _cloudinary;
		private readonly IMapper _mapper;

		public ActorController(ApplicationDBContext applicationDBContext, IMapper mapper, CloudinaryService cloudinary)
		{
			_applicationContext = applicationDBContext;
			_cloudinary = cloudinary;
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
		public async Task<ActionResult> Post([FromForm] CreateActorDTO createActorDTO)
		{
			Actor actor = _mapper.Map<Actor>(createActorDTO);

			if (createActorDTO.Image != null)
			{
				ImageUploadResult imageUploadResult = await _cloudinary.UploadImageAsync(createActorDTO.Image);

				if (imageUploadResult.Error != null)
				{
					return BadRequest(imageUploadResult.Error.Message);
				}

				actor.ImageURL = imageUploadResult.SecureUrl.ToString();
			}

			_applicationContext.Add(actor);
			await _applicationContext.SaveChangesAsync();

			ActorDTO actorDTO = _mapper.Map<ActorDTO>(actor);
			return new CreatedAtRouteResult("GetActorById", new { id = actor.Id }, actorDTO);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put([FromForm] UpdateActorDTO updateActorDTO, int id)
		{
			Actor actor = _mapper.Map<Actor>(updateActorDTO);
			actor.Id = id;

			if (updateActorDTO.Image != null)
			{
				ImageUploadResult imageUploadResult = await _cloudinary.UploadImageAsync(updateActorDTO.Image);

				if (imageUploadResult.Error != null)
				{
					return BadRequest(imageUploadResult.Error.Message);
				}

				actor.ImageURL = imageUploadResult.SecureUrl.ToString();
			}

			_applicationContext.Entry(actor).State = EntityState.Modified;
			await _applicationContext.SaveChangesAsync();

			return NoContent();
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult> Patch([FromBody] JsonPatchDocument<PatchActorDTO> patchDocument, int id)
		{
			if (patchDocument == null)
			{
				return BadRequest();
			}

			Actor actor = await _applicationContext.Actor.FirstOrDefaultAsync(actor => actor.Id == id);

			if (actor == null)
			{
				return NotFound();
			}

			PatchActorDTO actorDTO = _mapper.Map<PatchActorDTO>(actor);

			patchDocument.ApplyTo(actorDTO, ModelState);

			bool isValidDocument = TryValidateModel(actorDTO);

			if (!isValidDocument)
			{
				return BadRequest(ModelState);
			}

			_mapper.Map(actorDTO, actor);

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
