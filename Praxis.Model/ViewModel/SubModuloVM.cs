using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Praxis.Model.ViewModel
{
    public class SubModuloVM
    {
        [XmlElement("IdSubModulo")]
        public int Id { get; set; }
        [XmlElement("NombreSubModulo")]
        public string Nombre { get; set; }
        [XmlElement("IconoSubModulo")]
        public string IdIcono { get; set; }
        [XmlElement("IdModulo")]
        public int IdModulo { get; set; }
        [XmlElement("PantallaViewModel")]
        public List<PantallaViewModel> PantallaVM { get; set; }
    }
}
