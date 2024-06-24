using cinema_api.Services;
using cinema_api.Utils;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

namespace cinema_api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddAutoMapper(typeof(Program));

			builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
			builder.Services.AddScoped<CloudinaryService>();

			var app = builder.Build();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
