namespace CodeTest.ThunderWings.Data.Paging
{
	public interface IPageingFilter
	{
		int PageNumber { get; set; }
		int PageSize { get; set; }
	}

	public class PageingFilter : IPageingFilter
	{
		private int _pageSize = 25;
		public int PageNumber { get; set; } = 1;

		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}

		internal int MaxPageSize { get; set; } = 100;
	}
}