namespace cinema_api.DTOs
{
	public class PaginationDTO
	{
		public int Page { get; set; } = 1;
		public int RecordsPerPage { get; set; } = 10;
		public int MaxRecordsPerPage { get; set; } = 20;

		public int RecordsToShowPerPage
		{
			get
			{
				return RecordsPerPage;
			}
			set
			{
				RecordsPerPage = (value > MaxRecordsPerPage) ? MaxRecordsPerPage : value;
			}
		}
	}
}
