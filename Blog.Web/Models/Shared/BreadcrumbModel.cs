namespace Blog.Web.Models.Shared
{
    public class BreadcrumbModel
    {
        public string Description { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }

        public BreadcrumbModel(string description)
        {
            Description = description;
        }

        public BreadcrumbModel(string description, string actionName, string controllerName, string areaName = "")
        {
            Description = description;
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
        }
    }
}