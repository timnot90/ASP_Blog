using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Models.Shared
{
    public class ErrorModel
    {
        public string ErrorMessage { get; set; }

        public ErrorModel()
        {
        }

        public ErrorModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}