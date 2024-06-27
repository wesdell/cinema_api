using System.ComponentModel.DataAnnotations;

namespace cinema_api.Entities
{
	public class Movie
	{
		public int Id { get; set; }
		[Required]
		[StringLength(150)]
		public string Name { get; set; }
		public bool OnBillboard { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string PosterURL { get; set; }
	}
}
