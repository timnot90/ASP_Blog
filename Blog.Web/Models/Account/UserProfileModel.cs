using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.DataAccess.Blog;

namespace Blog.Web.Models.Account
{
    public class UserProfileModel
    {
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
            UpdateModel(userProfile);
        }

        public void UpdateSource(UserProfile source)
        {
            source.UserName = UserName;
            source.DisplayName = DisplayName;
            source.Email = Email;
            source.Street = Street;
            source.HouseNumber = HouseNumber;
            source.Town = Town;
            source.ZIP = Zip;
            source.Country = Country;
            source.Forename = Forename;
            source.LastName = LastName;
        }

        public void UpdateModel(UserProfile source)
        {
            Id = source.ID;
            UserName = source.UserName;
            DisplayName = source.DisplayName;
            Email = source.Email;
            Street = source.Street;
            HouseNumber = source.HouseNumber;
            Town = source.Town;
            Zip = source.ZIP;
            Country = source.Country;
            Forename = source.Forename;
            LastName = source.LastName;
        }

    }
}