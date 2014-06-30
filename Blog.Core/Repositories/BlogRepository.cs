using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using Blog.Core.DataAccess.Blog;
using Blog.Core.Exceptions;

namespace Blog.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        #region Blogentry

        public int SaveBlogentry(Blogentry entry, bool isNewEntry = false)
        {
            if (isNewEntry)
            {
                entry.CreationDate = DateTime.Now;
                BlogDataContext.Current.Blogentries.Add(entry);
            }
            BlogDataContext.Current.SaveChanges();
            return entry.ID;
        }

        public List<Blogentry> GetAllBlogentries()
        {
            return BlogDataContext.Current.Blogentries.OrderBy(e => e.CreationDate).ToList();
        }

        public Blogentry GetBlogentry(int id)
        {
            return BlogDataContext.Current.Blogentries.FirstOrDefault(e => e.ID == id);
        }

        public void DeleteBlogentry(int id)
        {
            try
            {
                Blogentry entryToDelete = BlogDataContext.Current.Blogentries.FirstOrDefault( b => b.ID == id );
                if (entryToDelete != null)
                {
                    BlogDataContext.Current.Blogentries.Remove( entryToDelete );
                    BlogDataContext.Current.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new BlogDbException(ex.Message);
            }
        }

        #endregion

        #region Category

        public int SaveCategory(Category category, bool isNewEntry = false)
        {
            if (isNewEntry)
            {
                category.CreationDate = DateTime.Now;
                BlogDataContext.Current.Categories.Add(category);
            }
            BlogDataContext.Current.SaveChanges();
            return category.ID;
        }

        public void DeleteCategory(int categoryid)
        {
            try
            {
                Category categoryToDelete = BlogDataContext.Current.Categories.FirstOrDefault( c => c.ID == categoryid );
                if (categoryToDelete != null)
                {
                    BlogDataContext.Current.Categories.Remove( categoryToDelete );
                    BlogDataContext.Current.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new BlogDbException(ex.Message);
            }
        }

        public List<Category> GetAllCategories()
        {
            return BlogDataContext.Current.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return BlogDataContext.Current.Categories.FirstOrDefault(c => c.ID == id);
        }
        public Category GetCategoryByName(string name)
        {
            return BlogDataContext.Current.Categories.FirstOrDefault(c => c.Name == name);
        }

        #endregion

        #region UserProfile

        public int SaveUserProfile(UserProfile userProfile, bool isNewProfile = false)
        {
            if (isNewProfile)
            {
                BlogDataContext.Current.UserProfiles.Add(userProfile);
            }
            try
            {
                BlogDataContext.Current.SaveChanges();
                return userProfile.ID;
            }
            catch (DbEntityValidationException)
            {
                return 0;
            }
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            return BlogDataContext.Current.UserProfiles.OrderBy(u => u.UserName).ToList();
        }

        public UserProfile GetUserProfileById(int id)
        {
            return BlogDataContext.Current.UserProfiles.FirstOrDefault(u => u.ID == id);
        }

        public UserProfile GetUserProfileByUsername(string username)
        {
            string userNameLowercase = username.ToLower();
            return BlogDataContext.Current.UserProfiles.FirstOrDefault(u => u.UserNameLowercase == userNameLowercase);
        }

        public bool EmailExists(string email)
        {
            return GetAllUserProfiles().Any(u => u.EmailLowercase == email.ToLower());
        }

        public bool DisplayNameExists(string displayName)
        {
            return GetAllUserProfiles().Any(u => u.DisplayName == displayName);
        }

        public UserProfile GetUserByRegistrationToken(string token)
        {
            return
                BlogDataContext.Current.UserProfiles.FirstOrDefault(
                    u =>
                        u.ID ==
                        BlogDataContext.Current.webpages_Membership.FirstOrDefault(m => m.ConfirmationToken == token)
                            .UserId);
        }

        public void SetUserLockedSate(int userId, bool state)
        {
            UserProfile user = BlogDataContext.Current.UserProfiles.FirstOrDefault(u => u.ID == userId);
            if (user != null)
            {
                user.IsLocked = state;
                BlogDataContext.Current.SaveChanges();
            }
        }

        #endregion

        #region Comment

        public int SaveComment(Comment comment, bool isNewComment = false)
        {
            if (isNewComment)
            {
                BlogDataContext.Current.Comments.Add(comment);
            }
            try
            {
                BlogDataContext.Current.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return comment.ID;
        }

        public List<Comment> GetAllComments()
        {
            return BlogDataContext.Current.Comments.OrderBy(c => c.CreationDate).ToList();
        }

        public Comment GetComment(int id)
        {
            return BlogDataContext.Current.Comments.FirstOrDefault(c => c.ID == id);
        }

        public void DeleteComment(int commentId)
        {
            try
            {
                Comment commentToDelete = BlogDataContext.Current.Comments.FirstOrDefault( c => c.ID == commentId );
                if (commentToDelete != null)
                {
                    BlogDataContext.Current.Comments.Remove( commentToDelete );
                    BlogDataContext.Current.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new BlogDbException( ex.Message );
            }
        }

        #endregion

        #region Settings

        public Setting GetBlogSettings()
        {
            Setting blogSetting = BlogDataContext.Current.Settings.FirstOrDefault(s => s.UNIQUE_ID == Guid.Empty);
            if (blogSetting == null)
            {
                blogSetting = new Setting();
                blogSetting.UNIQUE_ID = Guid.Empty;
                blogSetting.SiteName = "Blog";
                blogSetting.FooterText = "<h3 id='footer'>Your HTML Footer Text. Go to the settings to change it.</h1>" + 
                                            "\r\n<style>" + 
                                            "#footer {" +
                                            "\r\ncolor: grey" + 
                                            "\r\n}" + 
                                            "\r\n</style>";
                blogSetting.CommentsActivated = true;
                blogSetting.NumberOfEntriesPerPage = 5;

                blogSetting.RegistrationMailSubject = "Registration Confirmation";
                blogSetting.RegistrationMailBody =
                    "Hello " + EmailPlaceholders.RegistrationMailPlaceholderUsername + ",<br/>You are successfully registered for Blog. Please click on the link below in order to activate your account.<br/>" + EmailPlaceholders.RegistrationMailPlaceholderActivationLink;
                blogSetting.RegistrationMailSender = "default_registration@blog.com";

                blogSetting.WelcomeMailSubject = "Welcome to Blog.";
                blogSetting.WelcomeMailBody =
                    "Hello " + EmailPlaceholders.WelcomeMailPlaceholderUsername + "Welcome to Blog. Have Fun!";
                blogSetting.WelcomeMailSender = "default_welcome@blog.com";

                blogSetting.PasswordChangeMailSubject = "Password Change Confirmation";
                blogSetting.PasswordChangeMailBody =
                    "Hello " + EmailPlaceholders.PasswordChangeMailPlaceholderUsername + ",<br/>To complete your password change, click on the link below.<br/>" + EmailPlaceholders.PasswordChangeMailPlaceholderSecondStepLink;
                blogSetting.PasswordChangeMailSender = "default_password_change@blog.com";

                blogSetting.SmtpServerAddress = "smtp.blog.com";
                blogSetting.SmtpServerUsername = "admin";
                blogSetting.SmtpServerPassword = "password";
                blogSetting.SmtpIsPasswordMandatoryForLogin = true;

                BlogDataContext.Current.Settings.Add(blogSetting);
                BlogDataContext.Current.SaveChanges();
            }
            return blogSetting;
        }

        public void StoreSettings(Setting settings)
        {
            try
            {
                settings.SmtpServerUsername = settings.SmtpServerUsername ?? "";
                settings.SmtpServerPassword = settings.SmtpServerPassword ?? "";
                BlogDataContext.Current.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BlogDbException(ex.Message);
            }
        }

        #endregion
    }
}