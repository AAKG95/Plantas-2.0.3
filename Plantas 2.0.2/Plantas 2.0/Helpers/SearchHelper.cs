using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantas_2._0.Helpers
{
    public class SearchHelper
    {
        public int getCategoriaIdfromName(List<categoria> categorias,string Name) 
        {
            for (var i = 0; i < categorias.Count(); i++)
                if (string.Equals(categorias.ElementAt(i).desc,Name,StringComparison.OrdinalIgnoreCase))
                    return categorias.ElementAt(i).idcategoria;
            return -1;
        }

        public int getFichaIdfromName(List<ficha> fichas, string Name) 
        {
            for (var i = 0; i < fichas.Count(); i++)
                if (string.Equals(fichas.ElementAt(i).nombre, Name, StringComparison.OrdinalIgnoreCase))
                    return fichas.ElementAt(i).idficha;
            return -1;
        }
    }
}