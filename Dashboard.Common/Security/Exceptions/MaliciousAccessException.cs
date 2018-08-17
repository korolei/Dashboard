using System;
using System.Runtime.Serialization;

namespace Dashboard.Common.Security
{
    /// <summary>
    /// The exception that is thrown when any type of malicious access to an application is detected.
    /// </summary>
    [Serializable]
    public class MaliciousAccessException : Exception
    {
        #region Fields

        private const string _defaultMessage = "Malicious Access.";

        #endregion

        #region Constructors

        public MaliciousAccessException()
            : base(_defaultMessage)
        {
        }
        public MaliciousAccessException(string message)
            : base(message)
        {
        }
        public MaliciousAccessException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MaliciousAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
