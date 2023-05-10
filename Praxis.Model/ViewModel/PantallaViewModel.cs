using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Praxis.Model.ViewModel
{
    public class PantallaViewModel
    {
        [XmlElement("IdPantalla")]
        public int IdPantalla { get; set; }

        [XmlElement("IconoPantalla")]
        public string IdIcono { get; set; }

        [XmlElement("NombrePantalla")]
        public string Nombre { get; set; }

        [XmlElement("Controlador")]
        public string Controlador { get; set; }

        [XmlElement("Accion")]
        public string Accion { get; set; }

        [XmlElement("Aprobar")]
        public int Aprobar { get; set; }

        [XmlElement("Escritura")]
        public int Escritura { get; set; }

        [XmlElement("Lectura")]
        public int Lectura { get; set; }

        [XmlElement("NombreSubModulo")]
        public string NombreSubModulo { get; set; }

    }
}
