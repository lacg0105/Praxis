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
    
    public partial class CatUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatUsuario()
        {
            this.HistoriaClinica = new HashSet<HistoriaClinica>();
        }
    
        public System.Guid IdUsuario { get; set; }
        public int IdInstitucion { get; set; }
        public int IdArea { get; set; }
        public int IdRol { get; set; }
        public int IdEstatusUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public byte[] Pwd { get; set; }
        public string Correo { get; set; }
        public string Curp { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaPwd { get; set; }
        public System.DateTime FechaAcceso { get; set; }
        public string NombreImagen { get; set; }
    
        public virtual CatArea CatArea { get; set; }
        public virtual CatEstatus_Usuario CatEstatus_Usuario { get; set; }
        public virtual CatRol CatRol { get; set; }
        public virtual CatInstitucion CatInstitucion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaClinica> HistoriaClinica { get; set; }
        public virtual PerfilUsuario PerfilUsuario { get; set; }
    }
}
