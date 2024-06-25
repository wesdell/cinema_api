using Microsoft.EntityFrameworkCore;

namespace cinema_api.Helpers
{
	public static class HttpContextExtensions
	{
		public async static Task InsertPaginationParameters<T>(this HttpContext httpContext, IQueryable<T> queryable, int recordsPerPage)
		{
			double amount = await queryable.CountAsync();
			double pageAmount = Math.Ceiling(amount / recordsPerPage);
			httpContext.Response.Headers.Append("recordsAmount", pageAmount.ToString());
		}
	}
}
