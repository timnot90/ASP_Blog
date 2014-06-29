using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Web.Models.Account;

namespace Blog.Web.Areas.Administration.Models
{
    public class UserListModel
    {
        public List<UserListItemModel> Users { get; set; }

    }
}