//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plantas_2._0
{
    using System;
    using System.Collections.Generic;
    
    public partial class categoria
    {
        public categoria()
        {
            this.ficha = new HashSet<ficha>();
        }
    
        public int idcategoria { get; set; }
        public string desc { get; set; }
        public bool activa { get; set; }
        public string color { get; set; }
    
        public virtual ICollection<ficha> ficha { get; set; }
    }
}