using System;
using System.Globalization;

namespace WebShop.Helpers
{
    public class AppException : Exception
    {
        // Custom exception class for throwing application specific exceptions (e.g. for validation) 
        // that can be caught and handled within the application
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

    }
}
