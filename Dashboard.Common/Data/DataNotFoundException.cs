using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Data
{
	[Serializable]
	public class DataNotFoundException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Data not found.";

		#endregion

		#region Constructors

		public DataNotFoundException() : base(_defaultMessage)
		{
		}
		public DataNotFoundException(string message) : base(message)
		{
		}
		public DataNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		protected DataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		#endregion
	}
}
