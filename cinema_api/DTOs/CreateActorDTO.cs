using System.ComponentModel.DataAnnotations;

namespace cinema_api.DTOs
{
	public class CreateActorDTO
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }
		public DateTime Birthday { get; set; }
	}
}
