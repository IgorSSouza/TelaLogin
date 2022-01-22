using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TelaLogin.Models;
namespace TelaLogin.Controllers
{
    public class ContaController : Controller
    {
        [AllowAnonymous]
        public ActionResult TelaLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;  
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult TelaLogin(Login login, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            var achou = (login.Usuario == "igor" && login.Senha == "123");

            if(achou)
            {
                FormsAuthentication.SetAuthCookie(login.Usuario, login.LembrarMe);
                if(Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login inválido");
            }

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}