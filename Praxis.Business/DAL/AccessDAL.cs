using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Business.Helpers;
using Praxis.Model.ViewModel;
using Praxis.Model;
using Praxis.Model.Emun;
using System.Data.SqlClient;
using Praxis.Business.Security;

namespace Praxis.Business.DAL
{
    public static class AccessDAL
    {
        public static Operation ValidarUsuario(UsuariosViewModel _UsuariosViewModel)
        {
            try
            {
                #region Validación de usuario
                var UserName = UserDAL.ConsultaUsuarioExistente(_UsuariosViewModel.Correo, _UsuariosViewModel.Curp);
                if (UserName == null)
                {
                    //REGISTRO DE USUARIO INEXISTENTE
                    #region Log Clave Acceso
                    BitacoraDAL.RegistroBitacoraClavesAcceso(_UsuariosViewModel, Convert.ToInt32(EnumCatEvento_Bitacora.Usuario_Inexistente), "Correo", _UsuariosViewModel.Correo, "");
                    #endregion
                    return Operation.Failure("El usuario " + _UsuariosViewModel.Correo + " no existe.");
                }
                #endregion

                #region Validación del estatus
                if (UserName.IdEstatusUsuario == Convert.ToInt32(EnumEstatusUsuario.Inactivo))
                {
                    return Operation.Failure("El usuario no se encuentra activo, por favor contacte al administrador del sistema.");
                }
                else if (UserName.IdEstatusUsuario == Convert.ToInt32(EnumEstatusUsuario.Bloqueado_por_intentos_fallidos))
                {
                    return Operation.Failure("Usuario bloqueado por intentos fallidos, por favor contacte al administrador del sistema.");
                }
                else if (UserName.IdEstatusUsuario == Convert.ToInt32(EnumEstatusUsuario.Bloqueado_por_Rotación_de_Claves_de_Acceso))
                {
                    return Operation.Failure("Usuario bloqueado por rotación de clave de acceso, por favor contacte al administrador del sistema.");
                }
                else if (UserName.IdEstatusUsuario == Convert.ToInt32(EnumEstatusUsuario.Bloqueado_por_Inactividad_de_Claves_de_Acceso))
                {
                    return Operation.Failure("Usuario bloqueado por inactividad de clave de acceso, por favor contacte al administrador del sistema.");
                }
                else if (UserName.IdEstatusUsuario == Convert.ToInt32(EnumEstatusUsuario.Bloqueado_por_Falta_de_Suscripción))
                {
                    return Operation.Failure("Usuario bloqueado por falta de suscripción, por favor contacte al administrador del sistema.");
                }
                #endregion

                return Operation.Success("", UserName);

            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }

        }
        //--------------------------------------------------------------------------------------------
        public static Operation IngresaSistema(UsuariosViewModel _UsuariosViewModel)
        {
            try
            {
                #region Validación del estatus
                if (_UsuariosViewModel.IdEstatusUsuario != Convert.ToInt32(EnumEstatusUsuario.Activo))
                {
                    return Operation.Failure("El usuario no se encuentra activo, por favor contacte al administrador del sistema.");
                }
                #endregion

                #region Validación de contraseña
                var ContraseñaBD = UserDAL.ConsultaContraseñaUsuario(_UsuariosViewModel.Correo, _UsuariosViewModel.Curp);
                if (!Encryption.ComparePasswords_SHA256(ContraseñaBD.Pwd, _UsuariosViewModel.PwdStr))
                {
                    //REGISTRO DE CLAVE DE ACCESO INCORRECTA
                    #region Log Clave Acceso
                    BitacoraDAL.RegistroBitacoraClavesAcceso(ContraseñaBD, Convert.ToInt32(EnumCatEvento_Bitacora.Clave_de_Acceso_Incorrecta), "Correo", _UsuariosViewModel.Correo, "");
                    #endregion
                    return Operation.Failure("La contraseña es incorrecta.");
                }
                #endregion

                return Operation.Success("", ContraseñaBD.IdUsuario.ToString());

            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }

        }
        //--------------------------------------------------------------------------------------------
        public static Operation RecuperarCuenta(UsuariosViewModel _UsuariosViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Validación de usuario
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.Correo == _UsuariosViewModel.Correo).FirstOrDefault();
                    if (UserName == null)
                    {
                        return Operation.Failure("El usuario " + _UsuariosViewModel.Correo + " no existe.");
                    }

                    #region Validación del estatus
                    if (UserName.IdEstatusUsuario != Convert.ToInt32(EnumEstatusUsuario.Activo))
                    {
                        return Operation.Failure("El usuario no se encuentra activo, por favor contacte al administrador del sistema.");
                    }
                    #endregion

                    #endregion

                    #region Desactivar todos los folios anteriores
                    var lstFolios = dataBaseContext.FolioClaveAcceso.Where(u => u.IdUsuario == UserName.IdUsuario).ToList();
                    foreach (FolioClaveAcceso _lstFolio in lstFolios)
                    {
                        _lstFolio.Activo = false;
                    }
                    #endregion

                    #region Creación de folio temporal
                    String sFolio = Comun.CrearFolio();
                    FolioClaveAcceso _tblPWDTemp = new FolioClaveAcceso()
                    {
                        IdFolio = Guid.NewGuid(),
                        Folio = sFolio,
                        FechaExpiracion = DateTime.Now.AddDays(7),
                        IdUsuario = UserName.IdUsuario,
                        Activo = true
                    };
                    #endregion

                    dataBaseContext.FolioClaveAcceso.Add(_tblPWDTemp);
                    dataBaseContext.SaveChanges();

                    return Operation.Success("Se ha enviado un correo con las instruciones para la recuperación de tu contraseña.", sFolio);
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }


        }
        //--------------------------------------------------------------------------------------------
        public static Operation ValidaFolio(string IdFolio)
        {
            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                #region Validación de folio
                var _Folio = dataBaseContext.FolioClaveAcceso.Where(u => u.Folio == IdFolio).FirstOrDefault();
                if (_Folio == null)
                {
                    return Operation.Failure("El folio no es válido.");
                }

                if (!Convert.ToBoolean(_Folio.Activo))
                {
                    return Operation.Failure("El folio ya no se encuentra activo.");
                }

                if (_Folio.FechaExpiracion < DateTime.Today)
                {
                    return Operation.Failure("La vigencia del folio ya ha expirado, por favor solicite un nuevo folio.");
                }
                #endregion

                return Operation.Success("Folio Correcto.", _Folio.IdUsuario);
            }

        }
        //--------------------------------------------------------------------------------------------
        public static MenuViewModel ObtenerPantallasconPermisos(Guid IdUser)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    Serializer _Serializer = new Serializer();
                    StringBuilder sbMenu = new StringBuilder();
                    var lstXML = dataBaseContext.sp_ModuloAcceso_Modulos_ConsultaxUsuario(IdUser.ToString()).ToList();
                    foreach (var item in lstXML)
                    {
                        sbMenu.Append(item);
                    }
                    string sXml = sbMenu.ToString();
                    MenuViewModel _MenuViewModel = _Serializer.Deserialize<MenuViewModel>(sXml);
                    return _MenuViewModel;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------

    }
}
