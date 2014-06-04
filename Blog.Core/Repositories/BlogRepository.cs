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
        public BlogRepository()
        {
        }

        #region Blogentry

        public int SaveBlogentry(Blogentry entry, bool isNewEntry = false)
        {
            entry.Header = FilterHtmlTags(entry.Header);
            entry.Body = FilterHtmlTags(entry.Body);
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
            BlogDataContext.Current.Categories.Remove(
                BlogDataContext.Current.Categories.FirstOrDefault(c => c.ID == categoryid));
            BlogDataContext.Current.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return BlogDataContext.Current.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return BlogDataContext.Current.Categories.FirstOrDefault(c => c.ID == id);
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

        public UserProfile GetUserProfile(int id)
        {
            return BlogDataContext.Current.UserProfiles.FirstOrDefault(u => u.ID == id);
        }

        public bool EmailExists(string email)
        {
            return GetAllUserProfiles().Any(u => u.EmailLowercase == email.ToLower());
        }

        public bool DisplayNameExists(string displayName)
        {
            return GetAllUserProfiles().Any(u => u.DisplayName == displayName);
        }

        #endregion

        #region Comment

        public int SaveComment(Comment comment, bool isNewComment = false)
        {
            comment.Header = FilterHtmlTags(comment.Header);
            comment.Body = FilterHtmlTags(comment.Body);
            if (isNewComment)
            {
                BlogDataContext.Current.Comments.Add(comment);
            }
            BlogDataContext.Current.SaveChanges();
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
            BlogDataContext.Current.Comments.Remove(
                BlogDataContext.Current.Comments.FirstOrDefault(c => c.ID == commentId));
            BlogDataContext.Current.SaveChanges();
        }

        #endregion

        #region Settings

        public Setting GetBlogSettings()
        {
            Setting blogSetting = BlogDataContext.Current.Settings.First(s => s.UNIQUE_ID == Guid.Empty);
            if (blogSetting == null)
            {
                blogSetting = new Setting();
                blogSetting.UNIQUE_ID = Guid.Empty;
                blogSetting.SiteName = "Blog";
                blogSetting.FooterText = "Your Footer Text. Go to the settings to change it.";
                blogSetting.CommentsActivated = true;
                blogSetting.NumberOfEntriesPerPage = 10;

                blogSetting.RegistrationMailSubject = "Registration Confirmation";
                blogSetting.RegistrationMailBody =
                    "Your successfully registered for Blog. Please click on the link below to activate your account.</br>{0}";
                blogSetting.RegistrationMailSender = "default_registration@blog.com";

                blogSetting.WelcomeMailSubject = "Welcome to Blog.";
                blogSetting.WelcomeMailBody =
                    "Welcome to Blog. Have Fun!";
                blogSetting.WelcomeMailSender = "default_welcome@blog.com";

                blogSetting.PasswordChangeMailSubject = "Password Change Confirmation";
                blogSetting.PasswordChangeMailBody =
                    "To complete your password change, click on the link below.</br>{0}";
                blogSetting.PasswordChangeMailSender = "default_password_change@blog.com";

                blogSetting.SmtpServerAddress = "smtp@blog.com";
                blogSetting.SmtpServerUsername = "admin";
                blogSetting.SmtpServerPassword = "password";
                blogSetting.SmtpIsPasswordMandatoryForLogin = true;

                BlogDataContext.Current.Settings.Add(blogSetting);
                BlogDataContext.Current.SaveChanges();
            }
            return blogSetting;
        }

        #endregion

        private string FilterHtmlTags(string text)
        {
            Regex replaceBrWithNewline = new Regex(@"<br[\s]*/?>");
            Regex removeHtml = new Regex(@"<[^>]*>");
            Regex replaceNewlineWithBr = new Regex(@"(\r\n)|\r|\n");
            return replaceNewlineWithBr.Replace(removeHtml.Replace(replaceBrWithNewline.Replace(text, "\r\n"), ""), "<br/>");
        }

    }
}