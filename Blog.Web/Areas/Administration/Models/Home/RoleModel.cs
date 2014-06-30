namespace Blog.Web.Areas.Administration.Models.Home
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