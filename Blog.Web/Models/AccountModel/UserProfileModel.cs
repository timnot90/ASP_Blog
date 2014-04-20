using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.AccountModel
{
    public class UserProfileModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "The username must be declared.")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "A password must be declared.")]
        public string Password { get; set; }

        [DisplayName("E-Mail")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "The E-Mail Adress is not valid.")]
        [Required(ErrorMessage = "An E-Mail adress must be declared.")]
        public string Email { get; set; }

        [DisplayName("Street")]
        public string Street { get; set; }

        [DisplayName("House Number")]
        public string HouseNumber { get; set; }

        [DisplayName("City")]
        public string Town { get; set; }

        [DisplayName("ZIP")]
        public string Zip { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("Forename")]
        public string Forename { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Gender")]
        public char Gender { get; set; }

        public UserProfileModel()
        {
        }

        public UserProfileModel(UserProfile userProfile)
        {
            this.UpdateSource(userProfile);
        }

        public void UpdateSource(UserProfile source)
        {
            source.UserName = this.UserName;
            source.Email = this.Email;
            source.Street = this.Street;
            source.HouseNumber = this.HouseNumber;
            source.Town = this.Town;
            source.ZIP = this.Zip;
            source.Country = this.Country;
            source.Forename = this.Forename;
            source.LastName = this.LastName;
        }

        public void UpdateModel(UserProfile source)
        {
            this.UserName = source.UserName;
            this.Email = source.Email;
            this.Street = source.Street;
            this.HouseNumber = source.HouseNumber;
            this.Town = source.Town;
            this.Zip = source.ZIP;
            this.Country = source.Country;
            this.Forename = source.Forename;
            this.LastName = source.LastName;
        }

    }
}