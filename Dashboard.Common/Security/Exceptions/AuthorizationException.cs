using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Security
{
	[Serializable]
	public class AuthorizationException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Authorization error.";

		#endregion

		#region Constructors

		public AuthorizationException() : base(_defaultMessage)
		{
		}
		public AuthorizationException(string message) : base(message)
		{
		}
		public AuthorizationException(string message, Exception inner) : base(message, inner)
		{
		}

		protected AuthorizationException(SerializationInfo info, StreamingContext context): base(info, context)
		{
		}

		#endregion
	}
}
