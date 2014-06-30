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

        // ReSharper disable once MemberCanBePrivate.Global
        public void UpdateModel(UserProfile model)
        {
            DisplayName = model.DisplayName;
        }
    }
}