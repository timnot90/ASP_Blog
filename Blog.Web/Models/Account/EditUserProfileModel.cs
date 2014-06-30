using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Core.Annotations;
using Blog.Core.DataAccess.Blog;
using Blog.Web.ModelValidators.Account;

namespace Blog.Web.Models.Account
{
    //[FluentValidation.Attributes.Validator(typeof(EditUserProfileModelValidator))]
    public class EditUserProfileModel
    {
        [Required(ErrorMessage = "An error occured while validatiing your model. Please Reload the page and try again.")]
        public int Id { get; set; }

        [DisplayName("Display Name")]
        [StringLength(50, ErrorMessage = "Display Name cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "The Display Name cannot be empty.")]
        public string DisplayName { get; set; }

        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "The E-Mail Address cannot be empty.")]
        [StringLength(100, ErrorMessage = "E-Mail Address cannot be longer than 100 characters.")]
        [EmailAddress(ErrorMessage = "The format of the entered E-Mail Address is invalid.")]
        public string Email { get; set; }

        [DisplayName("Street")]
        [StringLength(100, ErrorMessage = "Street cannot be longer than 100 characters.")]
        public string Street { get; set; }

        [DisplayName("House Number")]
        [StringLength(10, ErrorMessage = "House Number cannot be longer than 10 characters.")]
        public string HouseNumber { get; set; }

        [DisplayName("City")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string Town { get; set; }

        [DisplayName("ZIP")]
        [StringLength(10, ErrorMessage = "ZIP cannot be longer than 10 characters.")]
        public string Zip { get; set; }

        [DisplayName("Country")]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
        public string Country { get; set; }

        [DisplayName("Forename")]
        [StringLength(50, ErrorMessage = "Forename cannot be longer than 50 characters.")]
        public string Forename { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
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