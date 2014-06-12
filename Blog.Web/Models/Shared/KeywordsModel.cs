using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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