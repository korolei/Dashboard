using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Security
{
	[Serializable]
	public class AuthenticationException : Exception
	{
		#region Fields

		private const string _defaultMessage = "Authentication error.";

		#endregion

		#region Constructors

		public AuthenticationException() : base(_defaultMessage)
		{
		}
		public AuthenticationException(string message) : base(message)
		{
		}
		public AuthenticationException(string message, Exception inner) : base(message, inner)
		{
		}

		protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		#endregion
	}
}
