using System;
using Blog.Core.Annotations;

namespace Blog.Core.Exceptions
{
    public class BlogDbException : Exception
    {
        [UsedImplicitly]
        public BlogDbException()
        {
        }

        public BlogDbException(string message)
            : base(message)
        {
        }

    }
}
