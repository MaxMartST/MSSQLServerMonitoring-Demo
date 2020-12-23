using System;
using System.Runtime.Serialization;

namespace MSSQLServerMonitoring.Domain.Toolkit.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException( SerializationInfo serializationInfo, StreamingContext context )
            : base( serializationInfo, context )
        {
        }

        public BaseException( string message )
            : base( message )
        {
        }

        public BaseException( string message, Exception innerException )
            : base( message, innerException )
        {
        }
    }
}
