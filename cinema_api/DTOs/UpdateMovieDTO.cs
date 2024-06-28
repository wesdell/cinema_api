using cinema_api.Interfaces;
using cinema_api.Validations;
using System.ComponentModel.DataAnnotations;

namespace cinema_api.DTOs
{
	public class UpdateMovieDTO
	{
		public int Id { get; set; }
		[Required]
		[StringLength(150)]
		public string Name { get; set; }
		public bool OnBillboard { get; set; }
		public DateTime ReleaseDate { get; set; }
		[FileSize(fileMaxSizeMB: 4)]
		[FileType(validFileTypes: EFileType.Image)]
		public IFormFile Poster { get; set; }
	}
}
