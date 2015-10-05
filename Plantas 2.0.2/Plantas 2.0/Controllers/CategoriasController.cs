using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plantas_2._0.Controllers
{
    public class CategoriasController : Controller
    {
        //======================================== CATEGORIAS ===============================================
        public ActionResult Categorias()
        {
            plantadbEntities db = new plantadbEntities();
            return View("Categorias",db.categoria.ToList());
        }
        //=======================================- CREATE =====================================================
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(categoria Categoria)
        {
            plantadbEntities db = new plantadbEntities();
            db.categoria.Add(Categoria);
            db.SaveChanges();
            return Categorias();
        }
        //========================================== EDIT ==========================================================
        [HttpGet]
        public ActionResult Edit(int id)
        {
            plantadbEntities db = new plantadbEntities();
            return View("Edit",db.categoria.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(categoria cat)
        {
            plantadbEntities db = new plantadbEntities();
            categoria cat2 = new categoria();
            cat2 = db.categoria.Find(cat.idcategoria);
            cat2.activa = cat.activa;
            cat2.desc = cat.desc;
            cat2.color = cat.color;
            db.SaveChanges();
            return Categorias();
        }
        //============================================ DELETE ======================================================
        [HttpGet]
        public ActionResult Delete(int id)
        {
            plantadbEntities db = new plantadbEntities();
            categoria cat = new categoria();
            List<ficha> fichas = new List<ficha>();
            fichas.AddRange(db.ficha.ToList());

            cat = db.categoria.Find(id);
            for (var i = 0; i < db.ficha.Count(); i++)
            {
                if (fichas.ElementAt(i).categoria.Contains(cat)) 
                {
                    ficha fich = new ficha();
                    fich = db.ficha.Find(i);
                    fich.categoria.Remove(cat);
                }
            }

            db.categoria.Remove(cat);
            db.SaveChanges();
            return Categorias();
        }
    }
}