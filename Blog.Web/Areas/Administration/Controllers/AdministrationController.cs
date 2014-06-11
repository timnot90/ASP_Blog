﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.Web.Areas.Administration.Models;
using Blog.Web.Areas.Administration.Services;
using Blog.Web.Services;

namespace Blog.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = CustomRoles.Administrator)]
    public class AdministrationController : Controller
    {
        readonly IBlogAdministrationService _service = new BlogAdministrationService();

        public ActionResult Users()
        {
            return View(_service.GetUserListModel());
        }

        public void ChangeRole(int id, string newRole, bool added)
        {
            _service.ChangeRole( id, newRole, added );
        }

        public ActionResult _UserListItem()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BlogSettings()
        {
            return View(_service.GetBlogSettings());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogSettings(BlogSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.StoreSettings( model );
                    model.SuccessfullySaved = true;
                    return View( model );
                }
                catch (SmtpException)
                {
                    ModelState.AddModelError( "SmtpInvalid", "The given smtp settings are invalid." );
                }
            }
            model.SuccessfullySaved = false;
            return View(model);
        }

        public void SetUserLockedState(int id, bool state)
        {
            _service.SetUserLockedState( id, state );
        }
    }
}
