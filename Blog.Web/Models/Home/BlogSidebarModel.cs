using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Web.Models.Home
{
    public class BlogSidebarModel
    {
        public List<CategoryModel> Categories { get; set; }
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }
        public List<string> AvailableMonths { get; set; }
        public List<string> AvailableYears { get; set; }

        public BlogSidebarModel()
        {
            SelectedMonth = DateTime.Now.Month.ToString("D2");
            SelectedYear = DateTime.Now.Year.ToString("D4");
            AvailableMonths = new List<string>();
            AvailableYears = new List<string>();
        }
    }
}