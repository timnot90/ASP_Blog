using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models
{
    public class CategoryCreatorModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public CategoryCreatorModel()
        {
        }

        public CategoryCreatorModel(UserProfile userProfile)
        {
            UpdateModel(userProfile);
        }

        public void UpdateModel(UserProfile source)
        {
            Id = source.ID;
            DisplayName = source.DisplayName;
        }
    }
}