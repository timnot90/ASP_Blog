using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class BlogentryCreatorModel
    {
        public string DisplayName { get; set; }

        public BlogentryCreatorModel(UserProfile profile)
        {
            UpdateModel( profile );
        }

        public void UpdateModel(UserProfile model)
        {
            DisplayName = model.DisplayName;
        }
    }
}