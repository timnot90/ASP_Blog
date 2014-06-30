using System.ComponentModel;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    [FluentValidation.Attributes.Validator(typeof(EditUserProfileModelValidator))]
    public class EditUserProfileModel
    {
        public int Id { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [DisplayName("E-Mail")]
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

        [UsedImplicitly]
        public EditUserProfileModel()
        {
        }

        public EditUserProfileModel(UserProfile userProfile)
        {
            UpdateModel(userProfile);
        }

        public void UpdateSource(UserProfile source)
        {
            source.DisplayName = DisplayName;
            source.Email = Email;
            source.EmailLowercase = Email.ToLower();
            source.Street = Street;
            source.HouseNumber = HouseNumber;
            source.Town = Town;
            source.ZIP = Zip;
            source.Country = Country;
            source.Forename = Forename;
            source.LastName = LastName;
        }

        // ReSharper disable once MemberCanBePrivate.Global
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