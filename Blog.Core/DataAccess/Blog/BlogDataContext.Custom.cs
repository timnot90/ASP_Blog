using System.Web;

namespace Blog.Core.DataAccess.Blog
{
    public partial class BlogDataContext
    {
        #region Request-wide Singleton

        private const string ContextkeyDatacontext = "{CD01E2F2-3893-4580-8D19-9C948B6F33BD}";

        public static BlogDataContext Current
        {
            get
            {
                var result =
                        HttpContext.Current.Items[ContextkeyDatacontext] as BlogDataContext;
                if (result == null)
                {
                    result = new BlogDataContext();
                    HttpContext.Current.Items.Add(ContextkeyDatacontext, result);
                }
                return result;
            }
        }

        #endregion
    }
}
