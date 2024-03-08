using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admins admin)
        {
            SqlConnection conn = Commands.ConnectToDb();
            var command = new SqlCommand(Queries.AdminData, conn);

            command.Parameters.AddWithValue("@username", admin.Username);
            command.Parameters.AddWithValue("@password", admin.Password);

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                FormsAuthentication.SetAuthCookie(reader["AdminId"].ToString(), false);
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorLogin"] = true;
            return RedirectToAction("Index", "Login");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}