using cinema_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace cinema_api
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Genre> Genre { get; set; }
	}
}
