//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_Conciliacion
    {
        public TB_Conciliacion()
        {
            this.TB_ConciliacionDetalle = new HashSet<TB_ConciliacionDetalle>();
        }
    
        public int IdConciliacion { get; set; }
        public Nullable<System.DateTime> dtConciliacion { get; set; }
        public string dsUsuario { get; set; }
        public Nullable<decimal> nrCajaAdm { get; set; }
        public Nullable<System.DateTime> dtModificacion { get; set; }
        public string flestado { get; set; }
        public Nullable<int> idArchivo { get; set; }
        public string formato { get; set; }
    
        public virtual ICollection<TB_ConciliacionDetalle> TB_ConciliacionDetalle { get; set; }
        public virtual TB_ArchivoTarjeta TB_ArchivoTarjeta { get; set; }
    }
}
