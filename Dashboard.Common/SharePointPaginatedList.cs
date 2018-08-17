using System.Collections.Generic;
using Dashboard.Common.Utilities;

namespace Dashboard.Common
{
	public class SharePointPaginatedList<T> : PaginatedListBase<T>
	{
		#region Properties

		public string CurrentPageToken { get; private set; }
		public string PreviousPageToken { get; private set; }
		public string NextPageToken { get; private set; }

		#endregion

		#region Constructors

		public SharePointPaginatedList(IEnumerable<T> source, int itemTotal, int pageNumber, int pageSize, 
			string currentPageToken, string previousPageToken, string nextPageToken)
		{
			ParameterValidator.AssertIsNotNull("source", source);
            ParameterValidator.AssertIsPositiveValue("pageNumber", pageNumber);
            ParameterValidator.AssertIsPositiveValue("pageSize", pageSize);

			Items = source;
			ItemTotal = itemTotal;
			PageNumber = pageNumber;
			PageSize = pageSize;
			CurrentPageToken = currentPageToken;
			PreviousPageToken = previousPageToken;
			NextPageToken = nextPageToken;
		}

		#endregion
	}
}
