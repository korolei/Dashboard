using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Data
{
	[Serializable]
	public class DataValidationException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Data validation error.";

		#endregion

		#region Constructors

		public DataValidationException() : base(_defaultMessage)
		{
		}
		public DataValidationException(string message) : base(message)
		{
		}
		public DataValidationException(string message, Exception inner) : base(message, inner)
		{
		}

		protected DataValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		#endregion
	}
}
