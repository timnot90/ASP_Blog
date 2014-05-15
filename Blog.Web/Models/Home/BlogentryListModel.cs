using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Models.Home
{
    public class BlogentryListModel
    {
        public List<BlogEntryListItemModel> Blogentries { get; set; }
        public FilterModel Filter { get; set; }

        public BlogentryListModel()
        {
        }

        public BlogentryListModel( List<BlogEntryListItemModel>  entries)
        {
            this.Blogentries = entries;
        }
    }
}