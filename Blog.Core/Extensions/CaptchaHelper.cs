using System;
using System.Web;
using System.Web.Mvc;

namespace Blog.Core.Extensions
{
    public static class CaptchaHelper
    {
        private static readonly Random Random = new Random();
        private const string CaptchaBaseString = "{0} + {1} = ";

        private const string KeyCaptchaResult = "EXPECTED_CAPTCHA_RESULT";


        // ReSharper disable once UnusedParameter.Global
        public static MvcHtmlString SimpleCaptcha(this HtmlHelper helper)
        {
            return new MvcHtmlString(GenerateSimpleCaptcha());
        }

        private static string GenerateSimpleCaptcha()
        {
            int number1 = Random.Next(0, 50);
            int number2 = Random.Next(0, 50);
            HttpContext.Current.Session.Add(KeyCaptchaResult, number1 + number2);
            return string.Format(CaptchaBaseString, number1, number2);
        }

        public static bool ValidateCaptchaResult(string result)
        {
            try
            {
                bool captchaIsValid = (HttpContext.Current.Session[KeyCaptchaResult].ToString()).Equals(result);
                return captchaIsValid;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
