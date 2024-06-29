using cinema_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace cinema_api
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MovieGenre>().HasKey(opt => new { opt.MovieId, opt.GenreId });

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Genre> Genre { get; set; }
		public DbSet<Actor> Actor { get; set; }
		public DbSet<Movie> Movie { get; set; }
		public DbSet<MovieGenre> MovieGenre { get; set; }
		public DbSet<MovieActor> MovieActor { get; set; }
	}
}
