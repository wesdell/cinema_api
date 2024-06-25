using cinema_api.DTOs;

namespace cinema_api.Helpers
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
		{
			return queryable.Skip((paginationDTO.Page - 1) * paginationDTO.RecordsToShowPerPage).Take(paginationDTO.RecordsToShowPerPage);
		}
	}
}
