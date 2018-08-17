using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Data
{
	[Serializable]
	public class DataConcurrencyException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Data concurrency error.";

		#endregion

		#region Constructors

		public DataConcurrencyException() : base(_defaultMessage)
		{
		}
		public DataConcurrencyException(string message) : base(message)
		{
		}
		public DataConcurrencyException(string message, Exception inner) : base(message, inner)
		{
		}

		protected DataConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		#endregion
	}
}
