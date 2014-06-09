using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog.Core.DataAccess.Blog;
using Blog.Web.Models.Account;

namespace Blog.Web.Areas.Administration.Models
{
    public class UserListItemModel
    {
        public List<RoleModel> Roles { get; set; }

        public int Id { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "The username must be declared.")]
        public string UserName { get; set; }

        [DisplayName("Display Name")]
        [Required(ErrorMessage = "The Display Name must be declared.")]
        public string DisplayName { get; set; }

        [DisplayName("E-Mail")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "The E-Mail Adress is not valid.")]
        [Required(ErrorMessage = "An E-Mail adress must be declared.")]
        public string Email { get; set; }

        public bool IsLocked { get; set; }

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