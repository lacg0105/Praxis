using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Model.ViewModel
{
    public class UsuariosViewModel
    {
        public string IdUsuario { get; set; }
        public int IdInstitucion { get; set; }
        public string NombreInstitucion { get; set; }
        public Int32 IdArea { get; set; }
        public string NombreArea { get; set; }
        public Int32 IdRol { get; set; }
        public string NombreRol { get; set; }
        public Int32 IdEstatusUsuario { get; set; }
        public string NombreEstatus { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public byte [] Pwd { get; set; }
        public string PwdStr { get; set; }
        public string Correo { get; set; }
        public string Curp { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaPwd { get; set; }
        public DateTime FechaAcceso { get; set; }
        public string NombreImagen { get; set; }

        public int PropiedadPrueba { get; set; }
        public string Cedula { get; set; }
        public string Host { get; set; }
        public Guid IdUsuarioGuid { get; set; }
        public string Folio { get; set; }

    }
}
