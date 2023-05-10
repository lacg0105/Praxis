using Praxis.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Praxis.Business.DAL;
using System.Threading;

namespace Praxis.Business.Security
{
    public class Authentication : IPrincipal
    {
        IIdentity _identity;
        UsuariosViewModel _DatosUsuario;
        public static UsuariosViewModel Current { get { return Thread.CurrentPrincipal as UsuariosViewModel; } }
        public IIdentity Identity { get { return _identity; } }

        /// <summary>
        /// Objeto ViewModel de Usuario Cargado
        /// </summary>
        public UsuariosViewModel Info
        {
            get { return _DatosUsuario; }
        }

        /// <summary>
        /// Constructor del Objeto Authentication
        /// </summary>
        /// <param name="UserID">Objeto ViewModel del Usuario</param>
        public Authentication(string UserID)
        {
            _identity = new GenericIdentity(UserID);
            try
            {
                EnsureUserData(new Guid(UserID)); // metodo que carga los datos del usuario autenticado
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar los Datos al Objeto Usuario para Autenticación
        /// </summary>
        /// <param name="UserID">Objeto ViewModel del Usuario</param>
        protected virtual void EnsureUserData(Guid UserID)
        {
            try
            {
                _DatosUsuario = UserDAL.ObtenerUsuarioVM(UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        /// <summary>
        /// Objeto Rol de Clase Heredada
        /// </summary>
        /// <param name="role">Rol</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
