using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project20180804.Core.Web.Models.Login;
using System.Net;

namespace Project20180804.Core.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login(LoginBindingModel model)
        {
            if (model.UserName.ToLower() == "admin" && model.Password.ToLower() == "123")
            {
                HttpContext.Session.SetString("LoginState", "Successed");

                return Ok();
            }

            Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            return View();
        }

        public IActionResult LoginOut()
        {
            HttpContext.Session.SetString("LoginState", "Failed");

            return Ok();
        }
    }
}