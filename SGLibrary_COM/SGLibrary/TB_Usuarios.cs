//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_Usuarios
    {
        public TB_Usuarios()
        {
            this.TB_MovimientosContables = new HashSet<TB_MovimientosContables>();
        }
    
        public string dsUsuario { get; set; }
        public string tpAcceso { get; set; }
        public string nmNombre { get; set; }
        public string nmApellido { get; set; }
        public string dsPassword { get; set; }
        public string dsDomicilio { get; set; }
        public string nmLocalidad { get; set; }
        public string cdPostal { get; set; }
        public string nrTelefono { get; set; }
        public string dsEmail { get; set; }
        public string dsPassword_OLD { get; set; }
        public Nullable<double> nrCUIL { get; set; }
        public Nullable<bool> flBloqueado { get; set; }
        public string tpNivelJerarquico { get; set; }
        public Nullable<System.DateTime> dtActualizacion { get; set; }
        public Nullable<bool> flEliminar { get; set; }
    
        public virtual ICollection<TB_MovimientosContables> TB_MovimientosContables { get; set; }
    }
}
