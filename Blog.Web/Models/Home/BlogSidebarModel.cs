using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Web.Models.Home
{
    public class BlogSidebarModel
    {
        public List<CategoryModel> Categories { get; set; }
        public Dictionary<string, string> AvailableMonths { get; set; }
        public List<string> AvailableYears { get; set; }
//        public string SearchText { get; set; }

        public BlogSidebarModel()
        {
            AvailableMonths = new Dictionary<string, string>();
            AvailableYears = new List<string>();
        }
    }
}