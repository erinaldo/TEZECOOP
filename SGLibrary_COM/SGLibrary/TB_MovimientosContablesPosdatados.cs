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
    
    public partial class TB_MovimientosContablesPosdatados
    {
        public decimal IdMovimiento { get; set; }
        public string dsMovimiento { get; set; }
        public Nullable<decimal> IdRecibo { get; set; }
        public string IdProveedor { get; set; }
        public string dsProveedor { get; set; }
        public Nullable<int> cdConcepto { get; set; }
        public string tpConcepto { get; set; }
        public string dsConcepto { get; set; }
        public string tpOperacion { get; set; }
        public Nullable<double> vlPesos { get; set; }
        public Nullable<double> vlDolares { get; set; }
        public Nullable<double> vlEuros { get; set; }
        public string nrRecibo { get; set; }
        public string nrFactura { get; set; }
        public Nullable<decimal> nrCaja { get; set; }
        public string dsUsuario { get; set; }
        public Nullable<System.DateTime> dtMovimiento { get; set; }
        public string dsObservacion { get; set; }
        public Nullable<int> nrAnio { get; set; }
        public string dsUsuario_Supervisor { get; set; }
        public Nullable<decimal> nrCajaPuesto { get; set; }
        public string tpCajaImputacion { get; set; }
        public string dsUsuarioCajaPuesto { get; set; }
        public string tpMovimiento { get; set; }
        public System.DateTime dtFechaPosdata { get; set; }
        public Nullable<decimal> nrCupon { get; set; }
        public Nullable<int> IdConciliacion { get; set; }
        public Nullable<bool> flProcesado { get; set; }
    
        public virtual TB_Usuarios TB_Usuarios { get; set; }
    }
}
