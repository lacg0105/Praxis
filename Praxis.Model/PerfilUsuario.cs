//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Praxis.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PerfilUsuario
    {
        public System.Guid IdUsuario { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Rfc { get; set; }
        public string Profesion { get; set; }
        public string Cedula { get; set; }
        public string Consultorio { get; set; }
        public string Direccion { get; set; }
        public string AlcaldiaMunicipio { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public Nullable<double> CostoConsulta { get; set; }
        public Nullable<bool> ConsultaDomicilio { get; set; }
        public Nullable<bool> ConsultaVideollamada { get; set; }
    
        public virtual CatUsuario CatUsuario { get; set; }
    }
}
