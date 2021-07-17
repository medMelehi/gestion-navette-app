using gestionNavette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace gestionNavette.Controllers
{
    public class AuthController : Controller
    {
        gestionNavetteEntities2 db = new gestionNavetteEntities2();

       

        [Route("login")]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [Route("loginPost")]
        public ActionResult loginSubmit(Login l)
        {
            if (!ModelState.IsValid)
            {
                return View("login");
            }
            var c = (from s in db.clients
                     where s.email == l.email && s.password == l.password
                     select s).FirstOrDefault();

            if (c == null)
            {
                TempData["indisponible"] = "Email ou Mot de passe incorrect !";
                return RedirectToAction("login");
            }
            else
            {
                if (c.admins.Count() != 0)
                {
                    Session["admin"] = "true";
                    Session["client"] = "Admin " + c.prenom + " " + c.nom;
                }
                else
                {
                    if (c.isSociete)
                    {

                        Session["isSociete"] = "oui";
                        Session["client"] = "Societe de " + c.prenom + " " + c.nom;
                    }
                    else
                    {
                        Session["client"] = c.prenom + " " + c.nom;
                    }
                }
              
                Session["idClient"] = c.Id;
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        // GET: Client/Create
        
        [Route("newClient")]
        public ActionResult newClient()
        {
            return View(); 
        }

        // POST: Client/Create
        [HttpPost]
        [Route("createClient")]
        public ActionResult CreateClient(client c)
        {            
                if (ModelState.IsValid)
            {
                db.clients.Add(c);
                db.SaveChanges();
                TempData["sign"] = "votre inscription a ètè comfirmé avec succés";
       
                return this.RedirectToAction("login");
            }
            else
            {
                return View("newClient");
            }       
        }

        [Route("newSociete")]
        public ActionResult newSociete()
        {
            return View();
        }
        [Route("createSociete")]
        public ActionResult CreateSociete(SocieteClient s)
        {
            if (ModelState.IsValid)
            {
                s.client.isSociete = true;
                db.societes.Add(s.societe);
                db.clients.Add(s.client);
                db.SaveChanges();
                TempData["sign"] = "votre inscription a ètè comfirmé avec succés";

                return RedirectToAction("login");
            }
            else
            {
                return View("newSociete");
            }

        }


 


    }
}
