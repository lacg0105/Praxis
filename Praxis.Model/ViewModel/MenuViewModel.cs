using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Praxis.Model.ViewModel
{
    [XmlRoot("MenuViewModel")]
    public class MenuViewModel
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Img { get; set; }

        public Nullable<Int32> IdRol { get; set; }
        public string NombreRol { get; set; }

        [XmlElement("ModuloVM")]
        public List<ModuloVM> ModuloVM { get; set; }
    }
}
