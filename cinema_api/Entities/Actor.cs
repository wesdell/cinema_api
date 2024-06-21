using System.ComponentModel.DataAnnotations;

namespace cinema_api.Entities
{
	public class Actor
	{
		public int Id { get; set; }
		[Required]
		[StringLength(150)]
		public string Name { get; set; }
		public string ImageURL { get; set; }
		public DateTime Birthday { get; set; }
	}
}
