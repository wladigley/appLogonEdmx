using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LogonWEB.Models;
using LogonWEB.Manager;
using System.Collections.Generic;

namespace LogonWEB.Controllers
{
  //  [Authorize]
    public class AccountController : Controller
    {
        DesenvolvedorSASEntities cotextController;

        public AccountController()
        {
            cotextController = new DesenvolvedorSASEntities();
        }
/*
 ***********************************************************************
 ******_Aqui está as actions pertinentes ao gerenciamento da TimLine_***
 ***********************************************************************
 */
        public ActionResult GetTimeLineList()
        {
           
            if (SessionManager.sessionLogada())
            {
                return PartialView("~/Views/PartialViews/TimeLineList.cshtml", this.GetPostByUserSession());
            }
            else
            {
                return RedirectToAction("Login");
            }
           

        }

        public ActionResult PostTimeLine(Publicacao model)
        {
            try
            {
                int idUserSession = Convert.ToInt32(Session["LogedUserID"]);
                model.Id_Usuario = idUserSession;
                model.DataCadastro = DateTime.Now;
                model.DataUltAlteracao = DateTime.Now;
                //model.Privacidade = cotextController.Publicacao.Where(x=>x.Privacidade ==)
                cotextController.Publicacao.Add(model);
                cotextController.SaveChanges();
            }
            catch (Exception e)
            {

            }
            //return PartialView("~/Views/PartialViews/TimeLineList.cshtml", this.GetPostByUserSession());
            return RedirectToAction("GetTimeLineList");
        }
        
        //Objeto Lista de publicação e de Usuarios via Laze release
        private List<Publicacao> GetPostByUserSession()
        {
            List<Publicacao> publishUser = new List<Publicacao>();
            try
            {
                int idUserSession = Convert.ToInt32(Session["LogedUserID"]);
                //publishUser = cotextController.Publicacao.Where(x => x.Id_Usuario == idUserSession).OrderByDescending(o => o.DataCadastro).ThenBy(a => a.Id_Usuario).ToList();
                publishUser = cotextController.Publicacao.OrderByDescending(o => o.DataCadastro).ThenBy(a => a.Id_Usuario).ToList();

                if (publishUser == null)
                    publishUser = new List<Publicacao>();
            }catch(Exception e)
            {

            }
            return publishUser;
        }

        /*******_Aqui TERMINA as actions da TimLine_***************************/

        /*
         *****************************************************************************
         ******_Aqui está as actions pertinentes a  o gerenciamento da partial login_***
         *****************************************************************************
         */
        public ActionResult GetLoginPartial()
        {
            Usuarios usuariosModel = new Usuarios();
                int idUserSession = Convert.ToInt32(Session["LogedUserID"]);
                usuariosModel = cotextController.Usuarios.Where(x => x.Id_Usuario == idUserSession).FirstOrDefault();

            if (usuariosModel == null)
                usuariosModel = new Usuarios();
            return PartialView("~/Views/PartialViews/LoginPartial.cshtml", usuariosModel);
           
        }
        /*
         *****************************************************************************
         ******_Aqui Termina as actions da partial login_*****************************
         *****************************************************************************
         */
        /*
        *****************************************************************************
        ******_Aqui está as actions pertinentes a  o gerenciamento da partial login_***
        *****************************************************************************
        */
        public ActionResult GetUsuarioToolsPartial()
        {
            Usuarios usuariosModel = new Usuarios();
            int idUserSession = Convert.ToInt32(Session["LogedUserID"]);
            usuariosModel = cotextController.Usuarios.Where(x => x.Id_Usuario == idUserSession).FirstOrDefault();

            if (usuariosModel == null)
                usuariosModel = new Usuarios();
            return PartialView("~/Views/PartialViews/UsuarioTools.cshtml", usuariosModel);

        }
        /*
         *****************************************************************************
         ******_Aqui Termina as actions da partial login_*****************************
         *****************************************************************************
         */

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logar(Usuarios user)
        {
            if (ModelState.IsValid)
            {
               var contexto = new DesenvolvedorSASEntities();
               var auth =  contexto.Usuarios.Where(x => x.Login == user.Login && x.Senha == user.Senha).FirstOrDefault();
                
                if (auth != null){
                    Session["LogedUserID"] = auth.Id_Usuario.ToString();
                    Session["LogedUserFullName"] = auth.Nome.ToString();
                    Session["LogedUserAdmin"] = auth.UserAdmin;
                    Session["UserSession"] = auth;
                    
                    return RedirectToAction("TimeLine");
                }
            }
           
            ModelState.AddModelError("Logar", "Login ou senha incorretos!!!");
            return View("Login");
        }

        public ActionResult TimeLine()
        {
            
            if (SessionManager.sessionLogada())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        
    }
}