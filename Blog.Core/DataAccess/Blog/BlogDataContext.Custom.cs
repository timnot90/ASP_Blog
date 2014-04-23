using System.Web;

namespace Blog.Core.DataAccess.Blog
{
    public partial class BlogDataContext
    {
        #region Request-wide Singleton

        private const string CONTEXTKEY_DATACONTEXT = "{CD01E2F2-3893-4580-8D19-9C948B6F33BD}";

        public static BlogDataContext Current
        {
            get
            {
                BlogDataContext result =
                        HttpContext.Current.Items[CONTEXTKEY_DATACONTEXT] as BlogDataContext;
                if (result == null)
                {
                    result = new BlogDataContext();
                    HttpContext.Current.Items.Add(CONTEXTKEY_DATACONTEXT, result);
                }
                return result;
            }
        }

        #endregion
    }
}
