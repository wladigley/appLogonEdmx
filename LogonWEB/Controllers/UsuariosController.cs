using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LogonWEB.Models;
using LogonWEB.Manager;

namespace LogonWEB.Controllers
{
    public class UsuariosController : Controller
    {
        private DesenvolvedorSASEntities db = new DesenvolvedorSASEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            if(SessionManager.sessionLogada() && SessionManager.sessionAdminLogada())
            {
                return View(db.Usuarios.ToList());
            }
            else
            {
                if (SessionManager.sessionLogada())
                {
                    return RedirectToAction("Timeline", "Account");
                }
                return RedirectToAction("Login","Account");
            }
            
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {

            if (SessionManager.sessionLogada())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Usuarios model = new Usuarios();
            return View(model);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Usuario,Nome,Login,Senha,Email,Imagem,ImgUplView,Status,DataCadastro,DataUltAlteração,UserAdmin")] Usuarios usuarios)//, HttpPostedFileBase file
        {
            if (ModelState.IsValid)
            {
                byte[] img = new byte[0];
                if (usuarios.ImgUplView == null)
                {
                    img = null;
                }else
                {
                    img = new byte[usuarios.ImgUplView.ContentLength];
                    usuarios.ImgUplView.InputStream.Read(img, 0, usuarios.ImgUplView.ContentLength);
                }
                
                usuarios.Imagem = img;
                usuarios.DataCadastro = DateTime.Now;
                usuarios.DataUltAlteração = DateTime.Now;
                db.Usuarios.Add(usuarios);
                db.SaveChanges();

                TempData["notice"] = "Usuario Gravado com sucesso!";
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            int idUserSession = Convert.ToInt32(Session["LogedUserID"]);
            Usuarios usuarios = db.Usuarios.Find(id);
            if (SessionManager.sessionLogada() && SessionManager.sessionAdminLogada() ||  idUserSession == usuarios.Id_Usuario)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


            
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Usuario, Nome, Login, Senha, Email, Imagem, ImgUplView, Status, DataCadastro, DataUltAlteração, UserAdmin")] Usuarios usuarios)
        {
                if (ModelState.IsValid)
                {
                      byte[] img = new byte[0];
                        if (usuarios.ImgUplView == null)
                        {
                            img = null;
                        }else
                        {
                            img = new byte[usuarios.ImgUplView.ContentLength];
                            usuarios.ImgUplView.InputStream.Read(img, 0, usuarios.ImgUplView.ContentLength);
                        }
                
                    usuarios.Imagem = img;
                    //usuarios.DataUltAlteração =;
                    db.Entry(usuarios).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {

            if (SessionManager.sessionLogada() && SessionManager.sessionAdminLogada())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuarios usuarios = db.Usuarios.Find(id);
                if (usuarios == null)
                {
                    return HttpNotFound();
                }
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (SessionManager.sessionLogada() && SessionManager.sessionAdminLogada())
            {
                Usuarios usuarios = db.Usuarios.Find(id);
                db.Usuarios.Remove(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
