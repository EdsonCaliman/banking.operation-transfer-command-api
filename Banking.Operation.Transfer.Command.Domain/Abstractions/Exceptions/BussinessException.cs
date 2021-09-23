using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Banking.Operation.Transfer.Command.Domain.Abstractions.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class BussinessException : Exception
    {
        public BussinessException()
        {
        }

        public BussinessException(string message) : base(message)
        {
        }

        public BussinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BussinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BussinessException(string type, string message) : base(message)
        {
            Message = message;
            Type = type;
        }

        public string Type { get; private set; }
        public new string Message { get; private set; }
    }
}
