using System;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models.Home
{
    public class CategoryListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPosts { get; set; }
        public CategoryCreatorModel Creator { get; set; }
        public DateTime CreationDate { get; private set; }
        public string CreationDateString
        {
            get
            {
                return string.Format("{0:D}, um {0:t} Uhr", CreationDate);
            }
        }

        public CategoryListItemModel(Category source)
        {
            UpdateModel(source);
        }

        public void UpdateModel(Category source)
        {
            Id = source.ID;
            Name = source.Name;
            NumberOfPosts = source.Blogentries.Count;
            CreationDate = source.CreationDate;
            Creator = new CategoryCreatorModel(source.UserProfile);
        }
    }
}