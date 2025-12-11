using RailwayManagement.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BCrypt.Net;

namespace RailwayManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private RailwayDBContext db = new RailwayDBContext();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ViewBag.Message = "Email already exists!";
                    return View();
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
                db.Users.Add(user);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Registration successful. Please login!";
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                ViewBag.Message = "Invalid email or password";
                return View();
            }

            if (user.Role == "Admin" && user.Password == password)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Session["UserId"] = user.UserId; 
                return RedirectToAction("AdminDashboard", "Admin");
            }
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Session["UserId"] = user.UserId; 
                return RedirectToAction("Search", "Train");
            }

            ViewBag.Message = "Invalid email or password";
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }
    }
}
