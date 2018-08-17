using System.Collections.Generic;

namespace Dashboard.Common
{
	public abstract class PaginatedListBase<T>
	{
		public IEnumerable<T> Items { get; protected set; }
		public int ItemTotal { get; protected set; }
		public int PageNumber { get; protected set; }
		public int PageSize { get; protected set; }
	}
}
