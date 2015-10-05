using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plantas_2._0.Models;
using Plantas_2._0.Helpers;

namespace Plantas_2._0.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //================================================== FORMA DE BUSQUEDA 1 ================================================
        [HttpGet]
        public ActionResult Search_1()
        {
            plantadbEntities db = new plantadbEntities();
            List<categoria> categorias = new List<categoria>();
            categorias.AddRange(db.categoria.ToList());
            for (var i = 0; i < categorias.Count(); i++)
                if (categorias.ElementAt(i).activa == true)
                    categorias.ElementAt(i).activa = false;

            return View(categorias);
        }
        [HttpPost]
        public ActionResult Search_1(List<categoria> cats) 
        {
            try
            {
                List<int> ids = new List<int>();
                for (var x = 0; x < cats.Count(); x++)
                    if (cats.ElementAt(x).activa)
                        ids.Add(cats.ElementAt(x).idcategoria);
                return Results(ids,false);
            }
            catch(SystemException)
            {
                return View("Index");
            }
        }
        //================================================== FORMA DE BUSQUEDA 2 ================================================
        [HttpGet]
        public ActionResult Search_2() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search_2(SearchModel model)
        {
            plantadbEntities db = new plantadbEntities();
            var searchHelper = new SearchHelper();
            bool fich_search = true;

            List<categoria> categorias = new List<categoria>();
            categorias.AddRange(db.categoria.ToList());

            List<int> ids = new List<int>();

            string[] words = model.Categorias_String.Split(',');
            foreach (string word in words) 
            {
                int temp = searchHelper.getCategoriaIdfromName(categorias, word);
                if (temp > 0) 
                {
                    ids.Add(temp);
                    fich_search = false;
                }

            }  

            if(fich_search)
            {
                List<ficha> fichas = new List<ficha>();
                fichas.AddRange(db.ficha.ToList());

                foreach (string word in words)
                {
                    int temp = searchHelper.getFichaIdfromName(fichas,word);
                    if (temp > 0)
                    {
                        ids.Add(temp);
                    }

                }
            }

            return Results(ids,fich_search);
        }
        //====================================================== RESULTADOS =====================================================
        public ActionResult Results(List<int> ids,bool Mode) 
        {
            plantadbEntities db = new plantadbEntities();
            List<ficha> fichas = new List<ficha>();

            List<ficha> fichas2 = new List<ficha>();
            fichas2.AddRange(db.ficha.ToList());

            ficha temp = new ficha();

            if (Mode == false) 
            {
                for (var i = 0; i < db.ficha.Count(); i++)
                {
                    temp = fichas2.ElementAt(i);
                    bool add = true; ;
                    for (var j = 0; j < ids.Count() && add; j++)
                    {
                        for (var k = 0; k < temp.categoria.Count(); k++)
                        {
                            add = false;
                            if (temp.categoria.ElementAt(k).idcategoria == ids.ElementAt(j))
                            {
                                add = true;
                                break;
                            }
                        }
                    }
                    if (add)
                        fichas.Add(temp);
                }
            }
            if (Mode) 
            {
                for (var i = 0; i < ids.Count(); i++) 
                {
                    temp = db.ficha.Find(ids.ElementAt(i));
                    if (temp != null)
                        fichas.Add(temp);
                }    
            }

            return View("Results",fichas);
        }
        //====================================================== CONTACTOS =====================================================
        public ActionResult Contacto()
        {
            return View();
        }
    }
}