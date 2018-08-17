using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Data
{
	[Serializable]
	public class DataAccessException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Data access error.";

		#endregion

		#region Constructors

		public DataAccessException() : base(_defaultMessage)
		{
		}
		public DataAccessException(string message) : base(message)
		{
		}
		public DataAccessException(string message, Exception inner) : base(message, inner)
		{
		}

		protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		#endregion
	}
}
