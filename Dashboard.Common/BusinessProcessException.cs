using System;
using System.Runtime.Serialization;

namespace Dashboard.Common
{
    [Serializable]
    public class BusinessProcessException : Exception
    {
        public BusinessProcessException() : this("A business process exception has occurred", null) { }
        public BusinessProcessException(string message) : this(message, null) { }
        public BusinessProcessException(string message, Exception inner)
            : base(message, inner)
        { }

        protected BusinessProcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
