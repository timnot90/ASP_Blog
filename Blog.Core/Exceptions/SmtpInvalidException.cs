using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Exceptions
{
    public class SmtpInvalidException : Exception
    {
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public SmtpInvalidException()
        {
        }

        public SmtpInvalidException(string key, string errorMessage)
        {
            _errors.Add( key, errorMessage );
        }

        public SmtpInvalidException(Dictionary<string, string> errors)
        {
            _errors = errors;
        }

        public Dictionary<string, string> Errors
        {
            get { return _errors; }
        }
    }
}
