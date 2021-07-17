using gestionNavette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gestionNavette.Controllers
{
    public class AdminController : Controller
    {
        gestionNavetteEntities2 db = new gestionNavetteEntities2();

        [Route("listClients")]
        public ActionResult listClients()
        {
            var cl = (from c in db.clients
                      where c.isSociete == false
                      && c.admins.Count() == 0
                      select c).ToList();
            if (cl.Count()!=0)
            {
                ViewBag.listClients = cl;
            }
            return View();
        }


        [Route("listSocietes")]
        public ActionResult listSocietes()
        {
            var st = (from s in db.societes
                      select s).ToList();
            if (st.Count()!=0)
            {
                ViewBag.listSocietes = st;
            }
            return View();
        }
    }
}