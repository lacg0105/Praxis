using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Praxis.Model.ViewModel
{
    public class ModuloVM
    {
        [XmlElement("IdModulo")]
        public int Id { get; set; }
        [XmlElement("NombreModulo")]
        public string Nombre { get; set; }
        [XmlElement("IdIcono")]
        public string IdIcono { get; set; }
        [XmlElement("SubModuloVM")]
        public virtual List<SubModuloVM> SubModuloVM { get; set; }
    }
}
