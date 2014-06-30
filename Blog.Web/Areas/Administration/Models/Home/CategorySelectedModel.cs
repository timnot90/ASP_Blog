using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models.Home
{
    public class CategorySelectedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        // ReSharper disable once UnusedMember.Global
        public CategorySelectedModel()
        {
        }

        public CategorySelectedModel(Category source)
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