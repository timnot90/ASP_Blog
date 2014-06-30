using System.ComponentModel;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Account
{
    public class UserModel
    {
        public int Id { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        public bool IsLocked { get; set; }

        [UsedImplicitly]
        public UserModel()
        {
        }

        public UserModel(UserProfile userProfile)
        {
            UpdateModel(userProfile);
        }

        public void UpdateSource(UserProfile source)
        {
            source.DisplayName = DisplayName;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void UpdateModel(UserProfile source)
        {
            Id = source.ID;
            DisplayName = source.DisplayName;
            IsLocked = source.IsLocked;
        }

    }
}