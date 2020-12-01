using ShoppingCart.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingCart.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
               
            // GET: Account
            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Login(Models.Membership model)
            {
                using (ShoppingCartEntities sc = new ShoppingCartEntities())
                {
                    bool isvalid = sc.Users.Any(x => x.UserName == model.UserName && x.UserPassword == model.Password);
               Users cat = sc.Users.Where(x => x.UserName == model.UserName && x.UserPassword == model.Password).SingleOrDefault();
                if (isvalid)
                    {
                        Session["UserId"] = cat.UserId.ToString();
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Country");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username and Password. Type correct Username and Password.");
                    }
                }
                return View();
            }

            public ActionResult Signup()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Signup(Users model)
            {
                using (ShoppingCartEntities sc = new ShoppingCartEntities())
                {
                model.IsActive = true;
                    sc.Users.Add(model);
                    sc.SaveChanges();
                }
                return RedirectToAction("Login");
            }

            public ActionResult Logout()
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
        }
    }
