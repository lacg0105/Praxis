using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Business.Helpers;
using Praxis.Model.ViewModel;
using Praxis.Model;
using Praxis.Model.Emun;
using Praxis.Business.Security;

namespace Praxis.Business.DAL
{
    public static class BitacoraDAL
    {
        /// <summary>
        /// Registro de Log de Claves de Acceso
        /// </summary>
        /// <param name="_UsuariosViewModel"></param>
        /// <param name="Evento"></param>
        /// <param name="Campo"></param>
        /// <param name="ValorActal"></param>
        /// <param name="ValorNuevo"></param>
        /// <returns></returns>
        public static Operation RegistroBitacoraClavesAcceso(UsuariosViewModel _UsuariosViewModel, Int32 Evento, string Campo, string ValorActal, string ValorNuevo)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    BitacoraClaveAcceso _tblBitacora = new BitacoraClaveAcceso()
                    {
                        Id = 0,
                        IdUsuario = _UsuariosViewModel.IdUsuario == null ? "00000000-0000-0000-0000-000000000000" : _UsuariosViewModel.IdUsuario,
                        IdUsuarioConsulta = " ",
                        IdEvento = Evento,
                        FechaRegistro = DateTime.Now,
                        Campo = Campo,
                        ValorActual = ValorActal,
                        ValorNuevo = ValorNuevo,
                        Acuse = " ",
                        Host = _UsuariosViewModel.Host,
                    };
                    dataBaseContext.BitacoraClaveAcceso.Add(_tblBitacora);
                    dataBaseContext.SaveChanges();

                    return Operation.Success("El Registro de la Bitacora fue Exitoso");
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }

        public static Operation RegistrosBitacoraNavegacion(UsuariosViewModel _UsuariosViewModel, Int32 Pantalla , Int32 Evento, string LlaveRegistro , List<string> Campo, List<string> ValorActal, List<string> ValorNuevo, string Parametro, Guid Folio)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    for (int i = 0; i < Campo.Count; i++)
                    {
                        BitacoraNavegacion _tblBitacora = new BitacoraNavegacion()
                        {
                            IdBitacora = 0,
                            IdUsuario = _UsuariosViewModel.IdUsuario == null ? "00000000-0000-0000-0000-000000000000" : _UsuariosViewModel.IdUsuario,
                            IdPantalla = Pantalla,
                            IdEvento = Evento,
                            FechaRegistro = DateTime.Now,
                            IdRegistro = LlaveRegistro,
                            Campo = Campo[i].ToString(),
                            ValorActual = ValorActal[i].ToString(),
                            ValorNuevo = ValorNuevo[i].ToString(),
                            Parametro = Parametro,
                            Acuse = null,
                            Folio = Folio,
                            Host = ""
                        };
                        dataBaseContext.BitacoraNavegacion.Add(_tblBitacora);
                    }

                    dataBaseContext.SaveChanges();

                    return Operation.Success("El Registro de la Bitacora fue Exitoso");
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }

        public static Operation RegistroBitacoraNavegacion(UsuariosViewModel _UsuariosViewModel, Int32 Pantalla, Int32 Evento, string LlaveRegistro, string Campo, string ValorActal, string ValorNuevo, string Parametro, string Folio)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    BitacoraNavegacion _tblBitacora = new BitacoraNavegacion()
                    {
                        IdBitacora = 0,
                        IdUsuario = _UsuariosViewModel.IdUsuario == null ? "00000000-0000-0000-0000-000000000000" : _UsuariosViewModel.IdUsuario,
                        IdPantalla = Pantalla,
                        IdEvento = Evento,
                        FechaRegistro = DateTime.Now,
                        IdRegistro = LlaveRegistro,
                        Campo = Campo,
                        ValorActual = ValorActal,
                        ValorNuevo = ValorNuevo,
                        Parametro = Parametro,
                        Acuse = null,
                        Folio = Guid.Parse(Folio),
                        Host = ""
                    };
                    dataBaseContext.BitacoraNavegacion.Add(_tblBitacora);
                    dataBaseContext.SaveChanges();

                    return Operation.Success("El Registro de la Bitacora fue Exitoso");
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure("Error al guardar en bitácora:\n" + ex.Message);
            }
        }
    }
}
