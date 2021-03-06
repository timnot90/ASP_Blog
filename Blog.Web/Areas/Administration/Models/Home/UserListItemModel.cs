﻿using System.Collections.Generic;
using System.ComponentModel;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Areas.Administration.Models.Home
{
    public class UserListItemModel
    {
        public List<RoleModel> Roles { get; set; }

        public int Id { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [DisplayName("E-Mail")]
        public string Email { get; set; }

        public bool IsLocked { get; set; }

        [UsedImplicitly]
        public UserListItemModel()
        {
        }

        public UserListItemModel(UserProfile source)
        {
            Id = source.ID;
            UserName = source.UserName;
            DisplayName = source.DisplayName;
            Email = source.Email;
            IsLocked = source.IsLocked;
        }
    }
}