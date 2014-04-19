using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models
{
    public class CategoryListItemModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CategoryListItemModel(Category source)
        {
            UpdateModel(source);
        }

        public void UpdateModel(Category source)
        {
            this.ID = source.ID;
            this.Name = source.Name;
        }

        public void UpdateSource(Category source)
        {
            source.Name = this.Name;
        }
    }
}