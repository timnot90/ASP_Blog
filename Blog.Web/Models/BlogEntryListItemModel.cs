﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models
{
    public class BlogEntryListItemModel
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public List<Category> Categories { get; private set; } 
        public DateTime CreationDate { get; private set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, um {0:t} Uhr", CreationDate.Date);
            }
        }

        public BlogEntryListItemModel()
        {
        }

        public BlogEntryListItemModel(Blogentry blogentry)
        {
            this.UpdateModel(blogentry);
        }

        public void UpdateModel(Blogentry source)
        {
            this.ID = source.ID;
            this.Header = source.Header;
            this.Body = source.Body;
            this.CreationDate = source.CreationDate;
            this.Categories = source.Categories.ToList();
        }

        public void UpdateSource(Blogentry source)
        {
            source.Header = this.Header;
            source.Body = this.Body;
            source.CreationDate = this.CreationDate;
            source.Categories = this.Categories;
        }
    }
}