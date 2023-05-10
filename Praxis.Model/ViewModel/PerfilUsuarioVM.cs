using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Model.ViewModel
{
    public class PerfilUsuarioVM
    {
        public Guid IdUsuario { get; set; }
        public DateTime FechaNacimiento { get; set; }
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
    }
}
