using System.ComponentModel.DataAnnotations;

namespace cinema_api.DTOs
{
	public class MovieDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool OnBillboard { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string PosterURL { get; set; }
	}
}
