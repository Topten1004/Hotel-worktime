using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Worktime.Global;
using Worktime.Models;
using Worktime.Services;
using Worktime.ViewModel;

namespace Worktime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WorktimeDbContext _db;

        public HomeController(ILogger<HomeController> logger, WorktimeDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            //Response.Cookies.Delete("jcp-Worktime-Access-Token");
            //Response.Cookies.Delete("b2n-Worktime-Access-Token");
            Response.Cookies.Delete("jard-Worktime-Access-Token");
            //Response.Cookies.Delete("ruiz-Worktime-Access-Token");

            return RedirectToAction("Login", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordVM pass, string returnUrl)
        {
            var user = _db.Users.Where(x => x.Email == pass.Email).FirstOrDefault();

            if(user == null)
                return RedirectToAction("Erreur", "Home", new { msgErr = "Can't find the email.", urlRetour = returnUrl });
            
            if(pass.Password != pass.ConfirmPassword)
                return RedirectToAction("Erreur", "Home", new { msgErr = "Password doesn't match.", urlRetour = returnUrl });


            return RedirectToAction("Erreur", "Home", new { msgErr = "Connexion refusée, mot de passe et/ou login incorrects.", urlRetour = returnUrl });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model, string returnUrl)
        {
            User user = new User();
            Employee employee = new Employee();
            ViewData["returnUrl"] = returnUrl;
            try
            {
                user = _db.Users.Where(x => x.Login == model.Login && x.Mdp == model.Mdp)?.FirstOrDefault();

                if ( user != null && user.Role == "EMPLOYEE")
                {
                    employee = _db.Employees.Where ( x => x.FirstName.ToLower() == user.FirstName.ToLower() && x.LastName.ToLower() == user.LastName.ToLower()).FirstOrDefault();

                    if (employee != null)
                    {
                        if (employee.WebAccess == true)
                        {
                            var token = TokenJWT.creerTokenJWT(model.Login, model.Mdp, _db);
                            if (token != null)
                            {
                                //Response.Cookies.Append(
                                //    "jcp-Worktime-Access-Token",
                                //    new JwtSecurityTokenHandler().WriteToken(token),
                                //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                                //);

                                //Response.Cookies.Append(
                                //    "b2n-Worktime-Access-Token",
                                //    new JwtSecurityTokenHandler().WriteToken(token),
                                //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                                //);

                                Response.Cookies.Append(
                                    "jard-Worktime-Access-Token",
                                    new JwtSecurityTokenHandler().WriteToken(token),
                                    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                                );

                                //Response.Cookies.Append(
                                //    "ruiz-Worktime-Access-Token",
                                //    new JwtSecurityTokenHandler().WriteToken(token),
                                //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                                //);


                                return RedirectToAction("Index", "Check");
                            }
                            else
                            {
                                if (returnUrl == null) returnUrl = Request.Path;
                                return RedirectToAction("Erreur", "Home", new { msgErr = "Connexion refusée, mot de passe et/ou login incorrects.", urlRetour = returnUrl });
                            }
                        }
                    }
                }
                else if ( user != null && user.Role != "EMPLOYEE")
                {
                    var token = TokenJWT.creerTokenJWT(model.Login, model.Mdp, _db);
                    if (token != null)
                    {
                        //Response.Cookies.Append(
                        //    "jcp-Worktime-Access-Token",
                        //    new JwtSecurityTokenHandler().WriteToken(token),
                        //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                        //);

                        //Response.Cookies.Append(
                        //    "b2n-Worktime-Access-Token",
                        //    new JwtSecurityTokenHandler().WriteToken(token),
                        //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                        //);

                        Response.Cookies.Append(
                            "jard-Worktime-Access-Token",
                            new JwtSecurityTokenHandler().WriteToken(token),
                            new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                        );


                        //Response.Cookies.Append(
                        //    "ruiz-Worktime-Access-Token",
                        //    new JwtSecurityTokenHandler().WriteToken(token),
                        //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                        //);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (returnUrl == null) returnUrl = Request.Path;
                        return RedirectToAction("Erreur", "Home", new { msgErr = "Connexion refusée, mot de passe et/ou login incorrects.", urlRetour = returnUrl });
                    }
                }
            }
            catch (Exception ex)
            {
                if (returnUrl == null) returnUrl = Request.Path;
                return RedirectToAction("Erreur", "Home", new { msgErr = ex.Message });
            }

            return RedirectToAction("Erreur", "Home", new { msgErr = "Connexion refusée, mot de passe et/ou login incorrects.", urlRetour = returnUrl });
        }

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated && returnUrl == null) 
                return NotFound();
            
            ViewData["returnUrl"] = returnUrl;
            var login = Request.Query.Where(q => q.Key == "login").FirstOrDefault();
            if (login.Key != null) 
                ViewData["login"] = login.Value;
            else 
                ViewData["login"] = "";

            return View();
        }

        public ActionResult ResetPassword([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")]string password)
        {
            var user = _db.Users.Where(x => x.Email == email).FirstOrDefault();

            if (user != null)
            {
                user.Mdp = password;
                _db.SaveChanges();

                var token = TokenJWT.creerTokenJWT(user.Login, user.Mdp, _db);

                //Response.Cookies.Append(
                //    "jcp-Worktime-Access-Token",
                //    new JwtSecurityTokenHandler().WriteToken(token),
                //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                //);

                //Response.Cookies.Append(
                //    "b2n-Worktime-Access-Token",
                //    new JwtSecurityTokenHandler().WriteToken(token),
                //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                //);

                Response.Cookies.Append(
                    "jard-Worktime-Access-Token",
                    new JwtSecurityTokenHandler().WriteToken(token),
                    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                );

                //Response.Cookies.Append(
                //    "ruiz-Worktime-Access-Token",
                //    new JwtSecurityTokenHandler().WriteToken(token),
                //    new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict }
                //);

                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Erreur", "Home", new { msgErr = "Aucun compte n'est associé à ce mail." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Erreur(string msgErr, string urlRetour)
        {
            return View("Erreur", new string[] { msgErr, urlRetour });
        }
    }
}