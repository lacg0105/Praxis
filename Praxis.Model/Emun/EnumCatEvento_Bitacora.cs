using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Model.Emun
{
    public enum EnumCatEvento_Bitacora
    {
        Acceso_Login_Usuario = 1,
        Acceso_Login_Contraseña = 2,
        Usuario_Inexistente = 3,
        Usuario_Bloqueado_x_Intentos_Fallidos = 4,
        Usuario_Bloqueado_x_Inactividad =5,
        Usuario_Bloqueado_x_Rotación_de_Claves =6,
        Clave_de_Acceso_Incorrecta =7,
        Cambio_de_Contraseña =8,
        Recuperación_de_Claves_de_Acceso=9,
        Alta_de_Registro = 10,
        Baja_de_Registro = 11,
        Cambio_de_Registro = 12,
    }
}
