using System.Collections.Generic;

namespace Blog.Web.Models.Home
{
    public class BlogentryListModel
    {
        public List<BlogentryListItemModel> Blogentries { get; set; }
        public int NumberOfBlogentriesPerPage { get; set; }
    }
}