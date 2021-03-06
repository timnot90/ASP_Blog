﻿using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [UsedImplicitly]
        public CategoryModel()
        {
        }

        public CategoryModel(Category source)
        {
            UpdateModel(source);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void UpdateModel(Category source)
        {
            Id = source.ID;
            Name = source.Name;
        }
    }
}