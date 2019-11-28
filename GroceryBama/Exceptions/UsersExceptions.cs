using System;
using System.Runtime.Serialization;

namespace GroceryBama.Exceptions
{
    public class UserCredentialNotMatchException : Exception
    {
        public UserCredentialNotMatchException()
        {
        }

        public UserCredentialNotMatchException(string message) : base(message)
        {
        }

        public UserCredentialNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserCredentialNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class MutipleUsersFoundException : Exception
    {
        public MutipleUsersFoundException()
        {
        }

        public MutipleUsersFoundException(string message) : base(message)
        {
        }

        public MutipleUsersFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MutipleUsersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
