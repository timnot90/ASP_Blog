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
        public List<SelectListItem> AvailableMonths { get; set; }
        public List<SelectListItem> AvailableYears { get; set; }

        public BlogSidebarModel()
        {
            SelectedMonth = DateTime.Now.Month.ToString("D2");
            SelectedYear = DateTime.Now.Year.ToString("D4");
            AvailableMonths = new List<SelectListItem>();
            AvailableYears = new List<SelectListItem>();
        }
    }
}