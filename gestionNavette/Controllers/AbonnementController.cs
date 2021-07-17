using gestionNavette.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gestionNavette.Controllers
{
    public class AbonnementController : Controller
    {
        gestionNavetteEntities2 db = new gestionNavetteEntities2();
         static SelectList transports = null;
   
        // GET: Abonnement
        [Route("newTrajet")]
        public ActionResult newTrajet()
        {
            if (Session["isSociete"] != null)
            {
                int idClient = (int)Session["idClient"];
                var st = (from s in db.societes
                          where s.id_client == idClient
                          select s).First();
                int idSociete = st.Id;
                var transport = (from t in db.transports
                               where t.id_societe == idSociete
                               select t).ToList();
                if (transport.Count() == 0)
                {
                    return RedirectToAction("newTrajetTransport");
                }
                else
                {
                    transports = new SelectList(transport, "id", "marque");
                    ViewBag.transports = transports;
                    return View();
                }
            }
            else
            {
                TempData["condition"] = "Dabord, vous devais faire l'inscription de votre Societe !!";
                return RedirectToAction("newSociete", "Auth");
            }
        }
        [Route("newTrajetTransport")]
        public ActionResult newTrajetTransport()
        {
           

            return View();
        }
        [HttpPost]
        [Route("newTrajetTransport")]
        public ActionResult newTrajetTransport(FormCollection t)
        {
            TempData["vd"] = t["trajet.villeD"];
            TempData["va"] = t["trajet.villeA"];
            TempData["hd"] = t["trajet.heureD"];
            TempData["ha"] = t["trajet.heureA"];
            TempData["p"] = t["trajet.prix"];

            return View();
        }


        [Route("CreateTrajet")]
        [HttpPost]
        public  ActionResult CreateTrajet(TrajetTransport t)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.transports = transports;
                return View("newTrajet", t);      
            }
            else
            {
               // t.trajet.id_transport = 
                db.trajets.Add(t.trajet);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("CreateTrajetTransport")]
        [HttpPost]
        public ActionResult CreateTrajetTransport(TrajetTransport t)
        {
            if (!ModelState.IsValid)
            {              
                 return View("newTrajetTransport");
            }
            else
            {
                int idClient = (int)Session["idClient"];
                var st = (from s in db.societes
                              where s.id_client == idClient
                              select s).First();
                t.transport.id_societe = st.Id;
                db.transports.Add(t.transport);
                db.trajets.Add(t.trajet);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        [Route("details/{id}")]
        public ActionResult details(int? id)
        {
            
            var trajet = (from t in db.trajets
                          where t.Id == id
                          select t).First();
            var transport = (from tr in db.transports
                             where tr.Id == trajet.id_transport
                             select tr).First();
            var societe = (from s in db.societes
                           where s.Id == transport.id_societe
                           select s).First();
            ViewBag.trajet = trajet;
            ViewBag.transport = transport;
            ViewBag.societe = societe;
            if (Session["idClient"]!=null)
            {
                int idc = (int)Session["idClient"];
                int ab = (from a in db.abonnements
                          where a.id_client == idc && a.id_trajet == trajet.Id
                          select a).ToList().Count();
                if (ab != 0)
                {
                    ViewBag.abExiste = "oui";
                }
            }
       
            return View();
        }
        [HttpPost]    
        [Route("abonner/{idtj}/{idtp}")]
        public ActionResult abonner(int idtj,int idtp, abonnement a)
        {

            if (!ModelState.IsValid)
            {
                var trajet = (from t in db.trajets
                              where t.Id == idtj
                              select t).First();
                var transport = (from tr in db.transports
                                 where tr.Id == trajet.id_transport
                                 select tr).First();
                var societe = (from s in db.societes
                               where s.Id == transport.id_societe
                               select s).First();
                ViewBag.trajet = trajet;
                ViewBag.transport = transport;
                ViewBag.societe = societe;
                return View("details");
            }
            else
            {     
                var transport = (from tr in db.transports
                                 where tr.Id == idtp
                                 select tr).First();

                if (transport != null) { 
                    if (Session["idClient"] != null)
                    {
                        abonnement ab = new abonnement
                        {
                            id_client = (int)Session["idClient"],
                            id_trajet = idtj,
                            nbrMois = a.nbrMois

                        };
                        transport.capacite = transport.capacite - 1;
                        db.abonnements.Add(ab);
                        db.SaveChanges();
                       
                    }

                   
                }
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("newDemande")]
        public ActionResult newDemande()
        {
            if (Session["idClient"]!=null && Session["idSociete"]==null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [Route("createDemande")]
        public ActionResult createDemande(demande d)
        {
            if (!ModelState.IsValid)
            {
                return View("newDemande");
            }

            int trajet = (from t in db.trajets
                          where t.villeD == d.villeD && t.villeA == d.villeA 
                          && t.heureD == d.heureD
                          select t).ToList().Count();

            if (trajet != 0)
            {
               
                TempData["trajetExiste"] = "trajet existe ";
                return RedirectToAction("Index", "Home");
            }
           
            int id = (int)Session["idClient"];
            var nbrdemande2 = (from t in db.demandes
                               where t.villeD == d.villeD && t.villeA == d.villeA &&
                               t.heureD == d.heureD
                               && t.id_client == id
                               select t).ToList().Count();

            if (nbrdemande2 != 0)
            {
                TempData["demandeExiste"] = "demande existe";
                return RedirectToAction("Index", "Home");
            }

            var nbrdemande1 = (from t in db.demandes
                               where t.villeD == d.villeD && t.villeA == d.villeA &&
                               t.heureD == d.heureD
                               select t).ToList().Count();

            if (nbrdemande1 != 0)
            {
                TempData["nbrDemande"] = nbrdemande1;
            }
            else
            {
                TempData["nbrDemande"] = "0";
            }
            demande d1 = new demande
            {
                villeD = d.villeD,
                villeA = d.villeA,
                heureD = d.heureD,
                id_client = (int)Session["idClient"]
            };
            db.demandes.Add(d1);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Route("mesAbonnements")] 
        public ActionResult mesAbonnements()
        {
            if (Session["idClient"]!=null && Session["idSociete"]==null)
            {
                int id = (int)Session["idClient"];
                var ab = (from a in db.abonnements
                          where a.id_client == id
                          select a).ToList();
                if (ab.Count()!=0)
                {
                    ViewBag.mesAbonnements = ab;
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [Route("mesTrajets")]
        public ActionResult mesTrajets()
        {
            if (Session["isSociete"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                int id = (int)Session["idClient"];
                var tr = (from t in db.trajets
                          where t.transport.societe.id_client == id
                          select t).ToList();
                if (tr.Count()!=0)
                {
                    ViewBag.mesTrajets = tr;
                }
                return View();
            }
            
        }
        [Route("monTrajets/{id}")]
        public ActionResult monTrajets(int? id)
        {
            if (Session["isSociete"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var trajet = (from t in db.trajets
                              where t.Id == id
                              select t).First();

                ViewBag.monTrajet = trajet;
                return View();
            }
      
        }
        [Route("listDemandes")]
        public ActionResult listDemandes()
        {
            if (Session["isSociete"] == null && Session["admin"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var dm = db.demandes
                            .OrderBy(m => m.villeD)
                            .DistinctBy(m => new { m.villeD, m.villeA,m.heureD })
                            .ToList();
                if (dm.Count() != 0)
                {
                    ViewBag.listDemandes = dm;
                }
               
                return View();
            }

        }
        [HttpPost]
        public ActionResult chercher(trajet tt)
        {
            var tr = (from t in db.trajets
                      where t.villeD == tt.villeD &&
                      t.villeA == tt.villeA
                      select t).ToList();
            if (tr.Count()!=0)
            {
                ViewBag.trajets = tr;
            }
            else
            {
                TempData["notResult"] = "true";
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}