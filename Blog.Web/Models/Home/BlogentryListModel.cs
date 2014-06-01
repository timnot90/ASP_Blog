using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Models.Home
{
    public class BlogentryListModel
    {
        public List<BlogEntryListItemModel> Blogentries { get; set; }
        public int NumberOfBlogentriesPerPage { get; set; }

        public BlogentryListModel()
        {
        }
    }
}