using cinema_api.Interfaces;
using cinema_api.Validations;
using System.ComponentModel.DataAnnotations;

namespace cinema_api.DTOs
{
	public class UpdateActorDTO
	{
		[Required]
		[StringLength(150)]
		public string Name { get; set; }
		public DateTime Birthday { get; set; }
		[FileType(validFileTypes: EFileType.Image)]
		[FileSize(fileMaxSizeMB: 4)]
		public IFormFile Image { get; set; }
	}
}
