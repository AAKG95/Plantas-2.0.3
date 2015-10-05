using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plantas_2._0.Controllers
{
    public class FichasController : Controller
    {
        
        //========================================= FISHAS ====================================================
        public ActionResult Fichas()
        {
            plantadbEntities db = new plantadbEntities(); 
            return View("Fichas", db.ficha.ToList());
        }

        //=======================================- CREATE =====================================================
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ficha Ficha)
        {
            plantadbEntities db = new plantadbEntities();
            Ficha.fecha_creacion = DateTime.Now;
            db.ficha.Add(Ficha);
            db.SaveChanges();
            return Fichas();
        }
        //========================================== EDIT ==========================================================
        [HttpGet]
        public ActionResult Edit(int id)
        {
            plantadbEntities db = new plantadbEntities(); 
            return View("Edit",db.ficha.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(ficha fic)
        {
            plantadbEntities db = new plantadbEntities();
            ficha fic2 = new ficha();
            fic2 = db.ficha.Find(fic.idficha);
            fic2.nombre = fic.nombre;
            fic2.link = fic.link;
            fic2.img_url = fic.img_url;
            fic2.activo = fic.activo;
            db.SaveChanges();
            return Fichas();
        }
      
        //============================================ DELETE ======================================================
        public ActionResult Delete(int id)
        {
            plantadbEntities db = new plantadbEntities();
            ficha fic = new ficha();
            fic = db.ficha.Find(id);
            fic.categoria.Clear();
            db.ficha.Remove(fic);
            db.SaveChanges();
            return Fichas();
        }

        //========================================= CATEGORIAS ===============================================
        [HttpGet]
        public ActionResult Categorias(int id)
        {
            plantadbEntities db = new plantadbEntities();
            List<categoria> linked_categorias = new List<categoria>();
            linked_categorias.AddRange(db.ficha.Find(id).categoria.ToList());
            List<categoria> categorias = new List<categoria>();
            categorias.AddRange(db.categoria.ToList());
            for (var i = 0; i < categorias.Count(); i++)
            {
                if (linked_categorias.Contains(categorias.ElementAt(i)))
                    categorias.ElementAt(i).activa = true;
                else
                    categorias.ElementAt(i).activa = false;
            }
            return View(categorias);
        }
        [HttpPost]
        public ActionResult Categorias(List<categoria> categorias,int id)
        {
            plantadbEntities db = new plantadbEntities();
            ficha fic = new ficha();
            fic = db.ficha.Find(id);
            fic.categoria.Clear();
            for (var i = 0; i < categorias.Count(); i++)
                if (categorias.ElementAt(i).activa == true)
                    fic.categoria.Add(db.categoria.Find(categorias.ElementAt(i).idcategoria));        
            db.SaveChanges();
            return Fichas();
        }
    }
}