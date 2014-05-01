using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Home
{
    public class CategoryListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryListItemModel(Category source)
        {
            UpdateModel(source);
        }

        public void UpdateModel(Category source)
        {
            Id = source.ID;
            Name = source.Name;
        }

        public void UpdateSource(Category source)
        {
            source.Name = Name;
        }
    }
}