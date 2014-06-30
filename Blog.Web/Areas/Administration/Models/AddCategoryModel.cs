using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models
{
    public class AddCategoryModel
    {
        public string Name { get; set; }

        public void UpdateSource(Category source)
        {
            source.Name = Name;
        }
    }
}