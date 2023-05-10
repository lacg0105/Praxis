using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Model.ViewModel
{
    public class InstitucionesVM
    {
        public Int32 IdInstitucion { get; set; }
        public string NombreInstitucion { get; set; }
        public Int32 IdTipoInstitucion { get; set; }

        public string NombreTipoInstitucion { get; set; }
        
        public string NombreLargo { get; set; }
        public string Carpeta { get; set; }
    }
}
