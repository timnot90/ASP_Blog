using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Exceptions
{
    public class BlogDbException : Exception
    {
        public BlogDbException()
        {
        }

        public BlogDbException(string message)
            : base(message)
        {
        }

    }
}
