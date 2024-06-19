﻿using System.ComponentModel.DataAnnotations;

namespace cinema_api.Entities
{
	public class Genre
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
	}
}
