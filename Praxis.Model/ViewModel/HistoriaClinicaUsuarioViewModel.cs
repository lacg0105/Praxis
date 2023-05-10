using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Model.ViewModel
{
    public class HistoriaClinicaUsuarioViewModel
    {
        public string IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Curp { get; set; }
        public Int32 IdEstatusUsuario { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int HistoriaClinicaActiva { get; set; }
    }
}
