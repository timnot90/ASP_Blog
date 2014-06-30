using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models
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
    }
}