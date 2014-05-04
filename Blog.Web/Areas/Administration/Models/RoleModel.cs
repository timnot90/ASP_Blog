using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Areas.Administration.Models
{
    public class RoleModel
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public RoleModel(string roleName, bool isActive)
        {
            RoleName = roleName;
            IsActive = isActive;
        }
    }
}