using System.ComponentModel;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Shared
{
    public class KeywordsModel
    {
        [DisplayName("Keywords")]
        public string Keywords { get; set; }

        public KeywordsModel( Setting settings )
        {
            Keywords = settings.Keywords;
        }
    }
}