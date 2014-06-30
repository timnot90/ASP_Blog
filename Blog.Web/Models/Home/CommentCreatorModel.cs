using System.ComponentModel;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class CommentCreatorModel
    {
        public int Id { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        public CommentCreatorModel()
        {
        }

        public CommentCreatorModel(UserProfile userProfile)
        {
            UpdateModel(userProfile);
        }

        public void UpdateModel(UserProfile source)
        {
            if (source != null)
            {
                Id = source.ID;
                DisplayName = source.DisplayName;
            }
            else
            {
                Id = 0;
                DisplayName = "Anonymous";
            }
        }
    }
}