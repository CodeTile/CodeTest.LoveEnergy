using System.Diagnostics.CodeAnalysis;

namespace CodeTest.ThunderWings.Data.Paging
{
	public interface IPagedList
	{
		int CurrentPage { get; set; }
		int PageSize { get; set; }
		int TotalCount { get; set; }
		int TotalPages { get; set; }
	}

	/// <summary>
	/// https://bsavindu1998.medium.com/using-a-custom-pagelist-class-for-generic-pagination-in-net-core-2403a14c0c15
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class PagedList<T> : List<T>, IPagedList
	{
		public PagedList()
		{ }

		public PagedList(IEnumerable<T> items, int count, int currentPage, int pageSize)
		{
			CurrentPage = currentPage;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			PageSize = pageSize;
			TotalCount = count;
			AddRange(items);
		}

		public int CurrentPage { get; set; }

		public int PageSize { get; set; }

		public int TotalCount { get; set; }

		public int TotalPages { get; set; }

		public static PagedList<T> Create(IQueryable<T> source, IPageingFilter filter)
		{
			return Create(source, filter.PageNumber, filter.PageSize);
		}

		public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
		{
			if (pageNumber < 1)
				pageNumber = 1;
			if (pageSize < 1)
				pageSize = 25;
			if (source == null)
				return new PagedList<T>([], 0, pageNumber, pageSize);
			var count = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
	}
}