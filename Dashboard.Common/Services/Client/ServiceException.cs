using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Services.Client
{
	[Serializable]
	public class ServiceException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Service error.";

		#endregion

		#region Constructors

		public ServiceException() : base(_defaultMessage)
		{
		}
		public ServiceException(string message) : base(message)
		{
		}
		public ServiceException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		#endregion
	}
}
