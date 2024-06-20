using System.ComponentModel.DataAnnotations;

namespace cinema_api.DTOs
{
	public class UpdateGenreDTO
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
	}
}
