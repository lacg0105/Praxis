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
    
    public partial class CatInstitucion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CatInstitucion()
        {
            this.CatUsuario = new HashSet<CatUsuario>();
        }
    
        public int IdInstitucion { get; set; }
        public int IdTipoInstitucion { get; set; }
        public string Nombre { get; set; }
        public string NombreLargo { get; set; }
        public string Carpeta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CatUsuario> CatUsuario { get; set; }
        public virtual CatTipoInstitucion CatTipoInstitucion { get; set; }
    }
}
