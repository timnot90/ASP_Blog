using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Models.Blog
{
    public class CategoryModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int NumberOfPosts { get; set; }
        public UserProfileModel Creator { get; set; }
        public DateTime CreationDate { get; private set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, um {0:t} Uhr", CreationDate);
            }
        }
        public bool IsSelected { get; set; }


        public CategoryModel()
        {
        }

        public CategoryModel(Category source)
        {
            this.UpdateModel(source);
        }

        public void UpdateModel(Category source)
        {
            this.ID = source.ID;
            this.Name = source.Name;
            this.NumberOfPosts = source.Blogentries.Count;
            this.CreationDate = source.CreationDate;
        }

        public void UpdateSource(Category source)
        {
            source.Name = this.Name;
        }
    }
}