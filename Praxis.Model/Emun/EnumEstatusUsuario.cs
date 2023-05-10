using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Model.Emun
{
    public enum EnumEstatusUsuario
    {
        Activo = 1,
        Inactivo = 2,
        Bloqueado_por_intentos_fallidos = 3,
        Bloqueado_por_Rotación_de_Claves_de_Acceso = 4,
        Bloqueado_por_Inactividad_de_Claves_de_Acceso = 5,
        Bloqueado_por_Falta_de_Suscripción = 6,
        Borrado =7

    }
}
