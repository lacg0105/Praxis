using Praxis.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Model;
using Praxis.Business.Helpers;
using Praxis.Model.Emun;
using System.IO;
using Praxis.Business.Security;
using Praxis.Business.DAL;
using System.ComponentModel;

namespace Praxis.Business.DAL
{
    public static class UserDAL
    {
        public static UsuariosViewModel ConsultaUsuarioExistente(string Correo, string Curp)
        {
            UsuariosViewModel _Usuario;
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    using (var cmd = dataBaseContext.Database.Connection.CreateCommand())
                    {
                        dataBaseContext.Database.Connection.Open();
                        cmd.CommandText = "EXECUTE [dbo].[sp_ModuloAcceso_Login1_ConsultaUsuario] '" + Correo + "' , '" + Curp + "'";
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _Usuario = new UsuariosViewModel()
                                {
                                    NombreImagen = (reader.IsDBNull(reader.GetOrdinal("NombreImagen"))) ? "" : reader.GetString(0),
                                    Nombre = (reader.IsDBNull(reader.GetOrdinal("NombreUsuario"))) ? "" : reader.GetString(1),
                                    ApellidoPaterno = (reader.IsDBNull(reader.GetOrdinal("ApellidoPaterno"))) ? "" : reader.GetString(2),
                                    ApellidoMaterno = (reader.IsDBNull(reader.GetOrdinal("ApellidoPaterno"))) ? "" : reader.GetString(3),
                                    Curp = (reader.IsDBNull(reader.GetOrdinal("Curp"))) ? "" : reader.GetString(4),
                                    NombreRol = (reader.IsDBNull(reader.GetOrdinal("NombreRol"))) ? "" : reader.GetString(5),
                                    IdEstatusUsuario = reader.GetInt32(6),
                                    NombreEstatus = (reader.IsDBNull(reader.GetOrdinal("NombreEstatus"))) ? "" : reader.GetString(7),
                                    Correo = (reader.IsDBNull(reader.GetOrdinal("Correo"))) ? "" : reader.GetString(8),
                                    IdUsuario = (reader.IsDBNull(reader.GetOrdinal("IdUsuario"))) ? "" : reader.GetGuid(10).ToString(),
                                    Host = "HOST",
                                    Cedula = (reader.IsDBNull(reader.GetOrdinal("Cedula"))) ? "" : reader.GetString(9)
                                };
                                reader.Close();
                                dataBaseContext.Database.Connection.Close();
                                return _Usuario;
                            }
                            else
                            {
                                _Usuario = null;
                                return _Usuario;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BD: " + ex.Message + "\nFavor de comunicarse con el administrador.");
            }
        }
        //--------------------------------------------------------------------------------------------
        public static UsuariosViewModel ConsultaContraseñaUsuario(string Correo, string Curp)
        {
            UsuariosViewModel _Usuario;
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    using (var cmd = dataBaseContext.Database.Connection.CreateCommand())
                    {
                        dataBaseContext.Database.Connection.Open();
                        cmd.CommandText = "EXECUTE [dbo].[sp_ModuloAcceso_Login2_ConsultaContraseñaUsuario] '" + Correo + "' , '" + Curp + "'";
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _Usuario = new UsuariosViewModel()
                                {
                                    //Pwd = (reader.IsDBNull(reader.GetOrdinal("Pwd"))) ? "" : reader.GetString(0)
                                    IdUsuario = reader.GetGuid(0).ToString(),
                                    Pwd = (byte[])reader[1]
                                    //Pwd = BitConverter.GetBytes(reader.get)
                                    //System.Text.Encoding.UTF8.GetBytes(reader.GetString(1))
                                };
                                reader.Close();
                                dataBaseContext.Database.Connection.Close();
                                return _Usuario;
                            }
                            else
                            {
                                _Usuario = null;
                                return _Usuario;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BD: " + ex.Message + "\nFavor de comunicarse con el administrador.");
            }
        }
        //--------------------------------------------------------------------------------------------
        public static Operation BloquearUsuario(UsuariosViewModel _UsuariosViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Validación de usuario
                    var ContraseñaBD = UserDAL.ConsultaContraseñaUsuario(_UsuariosViewModel.Correo, _UsuariosViewModel.Curp);
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.Correo == _UsuariosViewModel.Correo).FirstOrDefault();
                    if (UserName == null)
                    {
                        return Operation.Failure("El usuario " + _UsuariosViewModel.Correo + " no existe.");
                    }
                    #endregion

                    #region Bloquear usuario
                    UserName.IdEstatusUsuario = Convert.ToInt32(EnumEstatusUsuario.Bloqueado_por_intentos_fallidos);
                    dataBaseContext.SaveChanges();
                    #endregion

                    //REGISTRO DE USUARIO BLOQUEADO X INTENTOS FALLIDOS
                    #region Log Clave Acceso
                    BitacoraDAL.RegistroBitacoraClavesAcceso(ContraseñaBD, Convert.ToInt32(EnumCatEvento_Bitacora.Usuario_Bloqueado_x_Intentos_Fallidos), "Correo", _UsuariosViewModel.Correo, "");
                    #endregion


                    return Operation.Success("Ha superado el número de intentos, su cuenta ha sido bloqueada por seguridad. Por favor contacte al administrador del sistema.");
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------
        public static UsuariosViewModel ObtenerUsuarioVM(string Correo)
        {
            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                dataBaseContext.Configuration.ProxyCreationEnabled = false;
                var UserName = (from u in dataBaseContext.CatUsuario
                                join r in dataBaseContext.CatRol on u.IdRol equals r.IdRol
                                join a in dataBaseContext.CatArea on u.IdArea equals a.IdArea
                                join s in dataBaseContext.CatEstatus_Usuario on u.IdEstatusUsuario equals s.IdEstatusUsuario
                                where u.Correo == Correo
                                select new UsuariosViewModel
                                {
                                    IdUsuario = u.IdUsuario.ToString(),
                                    Nombre = u.Nombre,
                                    ApellidoPaterno = u.ApellidoPaterno,
                                    ApellidoMaterno = u.ApellidoMaterno,
                                    Pwd = u.Pwd,
                                    FechaPwd = u.FechaPwd,
                                    Correo = u.Correo,
                                    FechaCreacion = u.FechaCreacion,
                                    IdEstatusUsuario = u.IdEstatusUsuario,
                                    NombreEstatus = s.Nombre,
                                    IdRol = u.IdRol,
                                    NombreRol = r.Nombre,
                                    IdArea = u.IdArea,
                                    NombreArea = a.Nombre,
                                    NombreImagen = u.NombreImagen,

                                }).FirstOrDefault<UsuariosViewModel>();

                return UserName;
            }
        }
        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// Obtiene Objeto Usuario a traves del IdUsuario
        /// </summary>
        /// <param name="IdUsuario">IdUsuario</param>
        /// <returns>Obtiene Objeto ViewModel del Usuario</returns>
        public static UsuariosViewModel ObtenerUsuarioVM(Guid IdUsuario)
        {
            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                dataBaseContext.Configuration.ProxyCreationEnabled = false;
                var UserName = (from u in dataBaseContext.CatUsuario
                                join r in dataBaseContext.CatRol on u.IdRol equals r.IdRol
                                join a in dataBaseContext.CatArea on u.IdArea equals a.IdArea
                                join i in dataBaseContext.CatInstitucion on u.IdInstitucion equals i.IdInstitucion
                                join s in dataBaseContext.CatEstatus_Usuario on u.IdEstatusUsuario equals s.IdEstatusUsuario
                                where u.IdUsuario == IdUsuario
                                select new UsuariosViewModel
                                {
                                    IdUsuario = u.IdUsuario.ToString(),
                                    IdInstitucion = u.IdInstitucion,
                                    Nombre = u.Nombre,
                                    ApellidoPaterno = u.ApellidoPaterno,
                                    ApellidoMaterno = u.ApellidoMaterno,
                                    Pwd = u.Pwd,
                                    FechaPwd = u.FechaPwd,
                                    Correo = u.Correo,
                                    FechaCreacion = u.FechaCreacion,
                                    IdEstatusUsuario = u.IdEstatusUsuario,
                                    NombreEstatus = s.Nombre,
                                    IdRol = u.IdRol,
                                    NombreRol = r.Nombre,
                                    IdArea = u.IdArea,
                                    NombreArea = a.Nombre,
                                    NombreImagen = u.NombreImagen,
                                    Curp = u.Curp

                                }).FirstOrDefault<UsuariosViewModel>();

                return UserName;
            }
        }
        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// Crea Nuevo Usuario con los Permisos por Default
        /// </summary>
        /// <param name="_UsuariosViewModel">Gestion de Datos desde la Vista</param>
        /// <param name="Archivo">Nombre Archivo de Imagen</param>
        /// <param name="Extension">Extensión Archivo de Imagen</param>
        /// <param name="Ruta">Ruta Archivo de Imagen</param>
        /// <returns>Retorna el Resultado del Proceso [True | False]</returns>
        public static Operation CrearUsuario(UsuariosViewModel _UsuariosViewModel, Stream Archivo, string Extension, string Ruta)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Valida Usuario
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.Correo == _UsuariosViewModel.Correo).FirstOrDefault();
                    if (UserName != null)
                    {
                        return Operation.Failure("El usuario " + _UsuariosViewModel.Correo + ", ya existe, favor de verificar");
                    }
                    #endregion
                    #region Crea Usuario
                    string sPwdTemp = AccessKeys.GeneratePassword();
                    Guid _IdUsuario = Guid.NewGuid();
                    CatUsuario _tblUser = new CatUsuario()
                    {
                        IdUsuario = _IdUsuario,
                        IdInstitucion = _UsuariosViewModel.IdInstitucion,
                        IdArea = _UsuariosViewModel.IdArea,
                        IdRol = _UsuariosViewModel.IdRol,
                        IdEstatusUsuario = Convert.ToInt32(EnumEstatusUsuario.Inactivo),
                        Nombre = _UsuariosViewModel.Nombre.Trim(),
                        ApellidoPaterno = _UsuariosViewModel.ApellidoPaterno.Trim(),
                        ApellidoMaterno = _UsuariosViewModel.ApellidoMaterno.Trim(),
                        Pwd = Encryption.EncryptPassword_SHA256(sPwdTemp),
                        Correo = _UsuariosViewModel.Correo.Trim(),
                        Curp = _UsuariosViewModel.Curp.Trim(),
                        FechaCreacion = DateTime.Now,
                        FechaPwd = DateTime.Now,
                        FechaAcceso = DateTime.Now,
                        NombreImagen = _IdUsuario.ToString() + Extension
                    };
                    dataBaseContext.CatUsuario.Add(_tblUser);

                    #region Clave Acceso Temporal
                    string sFolio = Comun.CrearFolio();
                    FolioClaveAcceso _tblFolioTemp = new FolioClaveAcceso()
                    {
                        IdFolio = Guid.NewGuid(),
                        IdUsuario = _tblUser.IdUsuario,
                        Folio = sFolio,
                        FechaExpiracion = DateTime.Now.AddDays(7),
                        Activo = true
                    };
                    dataBaseContext.FolioClaveAcceso.Add(_tblFolioTemp);
                    #endregion

                    //Se crean permisos con base a los accesos x modulo del rol
                    #region Crea Permisos Modulos
                    var lstPantallas = (from u in dataBaseContext.ModuloxRol
                                        where u.IdInstitucion == _tblUser.IdInstitucion && u.IdRol == _tblUser.IdRol
                                        select new PantallaViewModel
                                        {
                                            IdPantalla = u.IdPantalla,
                                            Aprobar = (u.Aprobar == true) ? 1 : 0,
                                            Escritura = (u.Escritura == true) ? 1 : 0,
                                            Lectura = (u.Lectura == true) ? 1 : 0,
                                        }).ToList<PantallaViewModel>();

                    //Se guardan los permisos del usuario x modulo del rol
                    foreach (var _Pantalla in lstPantallas)
                    {
                        ModuloxUsuario _tblModuloxUsuario = new ModuloxUsuario()
                        {
                            IdPantalla = _Pantalla.IdPantalla,
                            IdUsuario = _tblUser.IdUsuario.ToString(),
                            Aprobar = (_Pantalla.Aprobar == 1) ? true : false,
                            Escritura = (_Pantalla.Escritura == 1) ? true : false,
                            Lectura = (_Pantalla.Lectura == 1) ? true : false,
                        };
                        dataBaseContext.ModuloxUsuario.Add(_tblModuloxUsuario);
                    }
                    #endregion
                    dataBaseContext.SaveChanges();
                    #endregion
                    #region Guarda Imagen
                    if (Archivo != null)
                    {
                        string NombreArchivo = _IdUsuario.ToString() + Extension;
                        string RutaArchivo = Ruta + NombreArchivo;
                        Comun.SaveStreamToFile(RutaArchivo, Archivo);
                    }
                    #endregion
                    UsuariosViewModel _usuario = new UsuariosViewModel()
                    {
                        IdUsuarioGuid = _IdUsuario,
                        Folio = sFolio
                    };
                    return Operation.Success("El usuario se creo con exito", _usuario);  
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// Actualiza la Fecha de Acceso al Ingreso al Sistema
        /// </summary>
        /// <param name="_UsuariosViewModel">Objeto ViewModel de Usuario</param>
        public static void ActualizaFechaAcceso(UsuariosViewModel _UsuariosViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Actualiza Fecha
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.Correo == _UsuariosViewModel.Correo).FirstOrDefault();
                    UserName.FechaAcceso = DateTime.Now.Date;
                    dataBaseContext.SaveChanges();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// Actualiza la Fecha de Password del Sistema
        /// </summary>
        /// <param name="_UsuariosViewModel">Objeto ViewModel de Usuario</param>
        public static void ActualizaFechaPwd(UsuariosViewModel _UsuariosViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Actualiza Fecha
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.Correo == _UsuariosViewModel.Correo).FirstOrDefault();
                    UserName.FechaPwd = DateTime.Now.Date;
                    dataBaseContext.SaveChanges();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// Actualizar la Informaciòn del Usuario
        /// </summary>
        /// <param name="_UsuariosViewModel">Gestion de Datos desde la Vista</param>
        /// <param name="Archivo">Nombre Archivo de Imagen</param>
        /// <param name="Extension">Extensión Archivo de Imagen</param>
        /// <param name="Ruta">Ruta Archivo de Imagen</param>
        /// <returns>Retorna el Resultado del Proceso [True | False]</returns>
        public static Operation EditarUsuario(UsuariosViewModel _UsuariosViewModel, Stream Archivo, string Extension, string Ruta)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    #region Valida Usuario
                    var _IdUsuario = Guid.Parse(_UsuariosViewModel.IdUsuario);
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.IdUsuario == _IdUsuario).FirstOrDefault();
                    if (UserName == null)
                    {
                        return Operation.Failure("El usuario no existe, favor de verificar");
                    }
                    #endregion
                    #region Edita Usuario
                    UserName.IdArea = _UsuariosViewModel.IdArea;
                    //UserName.IdRol = _UsuariosViewModel.IdRol;
                    //UserName.IdInstitucion = _UsuariosViewModel.IdInstitucion;
                    UserName.IdEstatusUsuario = _UsuariosViewModel.IdEstatusUsuario;
                    UserName.Nombre = _UsuariosViewModel.Nombre;
                    UserName.ApellidoPaterno = _UsuariosViewModel.ApellidoPaterno;
                    UserName.ApellidoMaterno = _UsuariosViewModel.ApellidoMaterno;
                    UserName.Curp = _UsuariosViewModel.Curp;
                    UserName.FechaCreacion = DateTime.Now;

                    if (UserName.IdRol != _UsuariosViewModel.IdRol || UserName.IdInstitucion != _UsuariosViewModel.IdInstitucion)
                    {
                        #region Borra Permisos Actaules
                        var lstAccesoActual = dataBaseContext.ModuloxUsuario.Where(u => u.IdUsuario == _UsuariosViewModel.IdUsuario.ToString()).ToList();
                        foreach (var _acceso in lstAccesoActual)
                        {
                            dataBaseContext.ModuloxUsuario.Attach(_acceso);
                            dataBaseContext.ModuloxUsuario.Remove(_acceso);
                        }
                        dataBaseContext.SaveChanges();
                        #endregion

                        #region Crea Permisos Modulos
                        var lstPantallas = (from u in dataBaseContext.ModuloxRol
                                            where u.IdInstitucion == _UsuariosViewModel.IdInstitucion && u.IdRol == _UsuariosViewModel.IdRol
                                            select new PantallaViewModel
                                            {
                                                IdPantalla = u.IdPantalla,
                                                Aprobar = (u.Aprobar == true) ? 1 : 0,
                                                Escritura = (u.Escritura == true) ? 1 : 0,
                                                Lectura = (u.Lectura == true) ? 1 : 0,
                                            }).ToList<PantallaViewModel>();

                        //Se guardan los permisos del usuario x modulo del rol
                        foreach (var _Pantalla in lstPantallas)
                        {
                            ModuloxUsuario _tblModuloxUsuario = new ModuloxUsuario()
                            {
                                IdPantalla = _Pantalla.IdPantalla,
                                IdUsuario = UserName.IdUsuario.ToString(),
                                Aprobar = (_Pantalla.Aprobar == 1) ? true : false,
                                Escritura = (_Pantalla.Escritura == 1) ? true : false,
                                Lectura = (_Pantalla.Lectura == 1) ? true : false,
                            };
                            dataBaseContext.ModuloxUsuario.Add(_tblModuloxUsuario);
                        }
                        dataBaseContext.SaveChanges();
                        #endregion
                    }

                    UserName.IdRol = _UsuariosViewModel.IdRol;
                    UserName.IdInstitucion = _UsuariosViewModel.IdInstitucion;
                    #endregion

                    #region Cambio Contraseña
                    //if (!String.IsNullOrEmpty(_UsuariosViewModel.PwdStr))
                    //{

                    //    if (Comun.ValidarContraseña(_UsuariosViewModel.PwdStr))
                    //    {
                    //        return Operation.Failure("La contraseña debe contener mayúsculas, minúsculas, números, al menos 1 carácter especial [+!&#64;#$&*] y longitud superior a 8 caracteres.");
                    //    }

                    //    var L2EQuery1 = dataBaseContext.PWDHistorial.OrderByDescending(a => a.FechaCreacion).Take(3);
                    //    var L2EQuery = L2EQuery1.Where(a => a.IdUsuario == UserName.Id && a.Pwd == UserName.Pwd);

                    //    if (L2EQuery != null && L2EQuery.Count() > 0)
                    //    {
                    //        return Operation.Failure("La contraseña ya ha sido utilizada anteriormente, por favor intente con otra.");
                    //    }


                    //    UserName.Pwd = Encryption.EncryptPassword_SHA256(_UsuariosViewModel.PwdStr);
                    //    UserName.FechaPwd = DateTime.Now.AddMonths(3);
                    //    PWDHistorial _tblPWDHistory = new PWDHistorial()
                    //    {
                    //        FechaCreacion = DateTime.Now,
                    //        IdUsuario = UserName.Id,
                    //        Pwd = UserName.Pwd,
                    //    };
                    //    dataBaseContext.PWDHistorial.Add(_tblPWDHistory);
                    //}
                    #endregion
                    #region Cambiar Imagen
                    if (Archivo != null)
                    {
                        UserName.NombreImagen = Extension;
                        string NombreArchivo = _IdUsuario.ToString() + Extension;
                        string RutaArchivo = Ruta + NombreArchivo;
                        Comun.DeleteFile(RutaArchivo);
                        Comun.SaveStreamToFile(RutaArchivo, Archivo);
                    }

                    #endregion
                    dataBaseContext.SaveChanges();
                    //REGISTRO DE CAMBIO DE REGISTRO
                    #region RegistroNavegacion
                    Guid _IdFolio = Guid.NewGuid();
                    var BitacoraOperation = BitacoraDAL.RegistroBitacoraNavegacion(_UsuariosViewModel, Convert.ToInt32(EnumPantalla.Usuarios), Convert.ToInt32(EnumCatEvento_Bitacora.Cambio_de_Registro), UserName.IdUsuario.ToString(), "IdUsuario", _IdUsuario.ToString(), "", _UsuariosViewModel.Correo + "," + _UsuariosViewModel.Nombre, _IdFolio.ToString());
                    if (BitacoraOperation.IsSuccess == EnumOperationResult.Failure)
                        return Operation.Failure(BitacoraOperation.Message);
                    #endregion
                    return Operation.Success("La Informaciòn del Usuario " + _UsuariosViewModel.Nombre 
                        + ", se ha Actualizado Correctamente");
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }

        public static Operation CambiarPwd(UsuariosViewModel _UsuariosViewModel)
        {
            bool boolContraseña = false;
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var id = Guid.Parse(_UsuariosViewModel.IdUsuario);
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.IdUsuario == id).FirstOrDefault();

                    #region Validación de usuario
                    if (UserName == null)
                    {
                        return Operation.Failure("El usuario " + _UsuariosViewModel.Correo + " no existe.");
                    }
                    UserName.FechaPwd = DateTime.Now.AddMonths(3);

                    UserName.Pwd = Encryption.EncryptPassword_SHA256(_UsuariosViewModel.PwdStr);
                    UserName.IdEstatusUsuario = Convert.ToInt32(EnumEstatusUsuario.Activo);
                    #endregion

                    #region Validación de contraseñas anteriores
                    var L2EQuery1 = dataBaseContext.HistoriaClaveAcceso.Where(a => a.IdUsuario == UserName.IdUsuario).OrderByDescending(a => a.FechaCreacion).Take(3).ToList();
                    foreach (var historial in L2EQuery1)
                    {
                        boolContraseña = Encryption.ComparePasswords_SHA256(historial.Pwd, _UsuariosViewModel.PwdStr);
                        if (boolContraseña == true)
                            return Operation.Failure("La contraseña ya ha sido utilizada anteriormente, por favor intente con otra.");
                    }
                    #endregion

                    #region Desactivar todos los folios anteriores
                    var lstFolios = dataBaseContext.FolioClaveAcceso.Where(u => u.IdUsuario == UserName.IdUsuario).ToList();
                    foreach (FolioClaveAcceso _lstFolio in lstFolios)
                    {
                        _lstFolio.Activo = false;
                    }
                    #endregion

                    #region Guardamos en el historico de contraseñas

                    HistoriaClaveAcceso _tblPWDHistory = new HistoriaClaveAcceso()
                    {
                        FechaCreacion = DateTime.Now,
                        IdUsuario = UserName.IdUsuario,
                        Pwd = UserName.Pwd,
                    };
                    dataBaseContext.HistoriaClaveAcceso.Add(_tblPWDHistory);

                    #endregion
                    dataBaseContext.SaveChanges();
                    return Operation.Success(UserName.IdUsuario.ToString(), UserName.IdUsuario.ToString());
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------
        public static List<UsuariosViewModel> ObtenerUsuarios()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstUsers = (from u in dataBaseContext.CatUsuario
                                    join r in dataBaseContext.CatRol on u.IdRol equals r.IdRol
                                    join a in dataBaseContext.CatArea on u.IdArea equals a.IdArea
                                    join s in dataBaseContext.CatEstatus_Usuario on u.IdEstatusUsuario equals s.IdEstatusUsuario
                                    join i in dataBaseContext.CatInstitucion on u.IdInstitucion equals i.IdInstitucion
                                    select new UsuariosViewModel
                                    {
                                        IdUsuario = u.IdUsuario.ToString(),
                                        Nombre = u.Nombre,
                                        ApellidoPaterno = u.ApellidoPaterno,
                                        ApellidoMaterno = u.ApellidoMaterno,
                                        Pwd = u.Pwd,
                                        FechaPwd = u.FechaPwd,
                                        Correo = u.Correo,
                                        FechaCreacion = u.FechaCreacion,
                                        IdEstatusUsuario = u.IdEstatusUsuario,
                                        NombreEstatus = s.Nombre,
                                        IdRol = u.IdRol,
                                        NombreRol = r.Nombre,
                                        IdArea = u.IdArea,
                                        NombreArea = a.Nombre,
                                        NombreImagen = u.NombreImagen,
                                        IdInstitucion = i.IdInstitucion,
                                        FechaAcceso = u.FechaAcceso,
                                        NombreInstitucion = i.Nombre
                                    }).Where(x => x.IdEstatusUsuario != (int)EnumEstatusUsuario.Borrado).OrderByDescending(x => x.FechaCreacion).ToList<UsuariosViewModel>();
                    return lstUsers;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// Cambia a Estado de Borrado el Usuario
        /// </summary>
        /// <param name="_UsuariosViewModel">Gestion de Datos desde la Vista</param>
        /// <param name="Archivo">Nombre Archivo de Imagen</param>
        /// <param name="Extension">Extensión Archivo de Imagen</param>
        /// <param name="Ruta">Ruta Archivo de Imagen</param>
        /// <returns>Retorna el Resultado del Proceso [True | False]</returns>
        public static Operation BorrarUsuario(string IdUsuario)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    #region Valida Usuario
                    var _IdUsuario = Guid.Parse(IdUsuario);
                    var UserName = dataBaseContext.CatUsuario.Where(u => u.IdUsuario == _IdUsuario).FirstOrDefault();
                    if (UserName == null)
                    {
                        return Operation.Failure("El usuario no existe, favor de verificar");
                    }
                    #endregion
                    #region Edita Usuario
                    UserName.IdEstatusUsuario = (int)EnumEstatusUsuario.Borrado;
                    #endregion
                    dataBaseContext.SaveChanges();
                    //REGISTRO DE BORRADO DE REGISTRO
                    #region RegistroNavegacion
                    var _userModel = ObtenerUsuarioVM(UserName.Correo);
                    Guid _IdFolio = Guid.NewGuid();
                    var BitacoraOperation = BitacoraDAL.RegistroBitacoraNavegacion(_userModel, Convert.ToInt32(EnumPantalla.Usuarios), Convert.ToInt32(EnumCatEvento_Bitacora.Baja_de_Registro), UserName.IdUsuario.ToString(), "IdUsuario", _IdUsuario.ToString(), "", UserName.Correo + "," + UserName.Nombre, _IdFolio.ToString());
                    if (BitacoraOperation.IsSuccess == EnumOperationResult.Failure)
                        return Operation.Failure("El Usuario " + UserName.Nombre + ", se ha Borrado Correctamente.\n" + BitacoraOperation.Message);
                    #endregion
                    return Operation.Success("El Usuario " + UserName.Nombre
                        + ", se ha Borrado Correctamente");
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------
        public static PerfilUsuarioVM ObtenerPerfilUsuarioVM(Guid IdUsuario)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var Perfil = (from u in dataBaseContext.PerfilUsuario
                                  where u.IdUsuario == IdUsuario
                                  select new PerfilUsuarioVM
                                  {
                                      IdUsuario = u.IdUsuario,
                                      FechaNacimiento = u.FechaNacimiento,
                                      Rfc = u.Rfc,
                                      Profesion = u.Profesion,
                                      Cedula = u.Cedula,
                                      Consultorio = u.Consultorio,
                                      Direccion = u.Direccion,
                                      AlcaldiaMunicipio = u.AlcaldiaMunicipio,
                                      Estado = u.Estado,
                                      CodigoPostal = u.CodigoPostal,
                                      CostoConsulta = u.CostoConsulta,
                                      ConsultaDomicilio = u.ConsultaDomicilio,
                                      ConsultaVideollamada = u.ConsultaVideollamada
                                  }).FirstOrDefault<PerfilUsuarioVM>();
                    return Perfil;
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
