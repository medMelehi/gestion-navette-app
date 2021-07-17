using gestionNavette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gestionNavette.Controllers
{
    public class HomeController : Controller
    {
        gestionNavetteEntities2 db = new gestionNavetteEntities2();

        [Route("")]
        public ActionResult Index()
        {
            var trajets = db.trajets.ToList();

            if (Session["idClient"]!=null && Session["idSociete"] == null)
            {
                int id = (int)Session["idClient"];
               trajets = (from t in db.trajets
                               join tr in db.transports on t.id_transport equals tr.Id
                               where  tr.capacite != 0
                               select t)
                               .ToList();
            }
         

            if (trajets.Count() != 0 )
            {
                ViewBag.trajets = trajets;
            }
           
            return View();
        }

        

        
      
    }
}