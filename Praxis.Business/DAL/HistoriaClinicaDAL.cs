using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Model;
using Praxis.Model.Emun;
using Praxis.Model.ViewModel;
using Praxis.Business.Helpers;
using System.IO;
using Praxis.Business.Security;

namespace Praxis.Business.DAL
{
    public static class HistoriaClinicaDAL
    {
        /// <summary>
        /// Consulta las Historias Clinicas de los Pacientes
        /// </summary>
        /// <returns>Retorna una Lista de los Pacientes y sus Historias Clinicas</returns>
        public static List<HistoriaClinicaUsuarioViewModel> ConsultaHistoriaClinicaPacientes()
        {
            List<HistoriaClinicaUsuarioViewModel> _ListaHistoriaClinica = new List<HistoriaClinicaUsuarioViewModel>();
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    using (var cmd = dataBaseContext.Database.Connection.CreateCommand())
                    {
                        dataBaseContext.Database.Connection.Open();
                        cmd.CommandText = "EXECUTE [dbo].[sp_ModuloMedico_HistoriaClinica_ConsultaPacientes]";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HistoriaClinicaUsuarioViewModel _HistoriaClinica = new HistoriaClinicaUsuarioViewModel()
                                {
                                    IdUsuario = (reader.IsDBNull(reader.GetOrdinal("IdUsuario"))) ? "" : reader.GetGuid(0).ToString(),
                                    Correo = (reader.IsDBNull(reader.GetOrdinal("Correo"))) ? "" : reader.GetString(1),
                                    Nombre = (reader.IsDBNull(reader.GetOrdinal("Nombre"))) ? "" : reader.GetString(2),
                                    ApellidoPaterno = (reader.IsDBNull(reader.GetOrdinal("ApellidoPaterno"))) ? "" : reader.GetString(3),
                                    ApellidoMaterno = (reader.IsDBNull(reader.GetOrdinal("ApellidoMaterno"))) ? "" : reader.GetString(4),
                                    Curp = (reader.IsDBNull(reader.GetOrdinal("Curp"))) ? "" : reader.GetString(5),
                                    IdEstatusUsuario = reader.GetInt32(6),
                                    FechaActualizacion = (reader.IsDBNull(reader.GetOrdinal("FechaActualizacion"))) ? reader.GetDateTime(7) : reader.GetDateTime(7),
                                    HistoriaClinicaActiva = reader.GetInt32(8),
                                };
                                _ListaHistoriaClinica.Add(_HistoriaClinica);
                            }
                            reader.Close();
                            dataBaseContext.Database.Connection.Close();
                            return _ListaHistoriaClinica;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BD: " + ex.Message + "\nFavor de comunicarse con el administrador.");
            }
        }

        /// <summary>
        /// Obtiene Historia Clinica de Usuario
        /// </summary>
        /// <param name="_UsuarioViewModel">Objeto de Usuario del Tipo ViewModel</param>
        /// <returns>Objeto de Historia Clinica del Tipo ViewModel</returns>
        public static HistoriaClinicaViewModel ObtenerHistoriaClinicaVM(UsuariosViewModel _UsuarioViewModel)
        {
            HistoriaClinicaViewModel _HistoriaClinica;
            string strIdUsuario = "";

            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                dataBaseContext.Configuration.ProxyCreationEnabled = false;

                try
                {
                    strIdUsuario = _UsuarioViewModel.IdUsuario.ToString();
                    var id = Guid.Parse(strIdUsuario);
                    _HistoriaClinica = (from x in dataBaseContext.HistoriaClinica
                                        where x.IdUsuario == id
                                        select new HistoriaClinicaViewModel
                                        {
                                            IdUsuario = _UsuarioViewModel.IdUsuario
                                            ,
                                            IdSexo = x.IdSexo
                                            ,
                                            IdEstadoCivil = x.IdEstadoCivil
                                            ,
                                            IdReligion = x.IdReligion
                                            ,
                                            IdTipoSangre = x.IdTipoSangre
                                            ,
                                            IdEscolaridad = x.IdEscolaridad
                                            ,
                                            Activo = x.Activo
                                            ,
                                            FechaNacimiento = x.FechaNacimiento
                                            ,
                                            FechaActualizacion = x.FechaActualizacion
                                            ,
                                            LugarNacimiento = x.LugarNacimiento
                                            ,
                                            LugarResidencia = x.LugarResidencia
                                            ,
                                            Ocupacion = x.Ocupacion
                                            ,
                                            NumeroSeguridadSocial = x.NumeroSeguridadSocial
                                            ,
                                            PolizaSeguro = x.PolizaSeguro
                                            ,
                                            AbueloPaterno = x.AbueloPaterno
                                            ,
                                            AbuelaPaterno = x.AbuelaPaterno
                                            ,
                                            AbueloMaterno = x.AbueloMaterno
                                            ,
                                            AbuelaMaterno = x.AbuelaMaterno
                                            ,
                                            Padre = x.Padre
                                            ,
                                            Madre = x.Madre
                                            ,
                                            Hermano = x.Hermano
                                            ,
                                            Hijo = x.Hijo
                                            ,
                                            Vivienda = x.Vivienda
                                            ,
                                            Zoonosis = x.Zoonosis
                                            ,
                                            Habitos = x.Habitos
                                            ,
                                            IngestaLiquido = x.IngestaLiquido
                                            ,
                                            Higiene = x.Higiene
                                            ,
                                            Inmunizacion = x.Inmunizacion
                                            ,
                                            LenguaIndigena = x.LenguaIndigena
                                            ,
                                            Combe = x.Combe
                                            ,
                                            ViajesRecientes = x.ViajeReciente
                                            ,
                                            ExposicionBiomasa = x.ExposicionBiomasa
                                            ,
                                            CronicoDegenerativo = x.CronicoDegenerativo
                                            ,
                                            Quirurjicos = x.Quirurjicos
                                            ,
                                            Hospitalizacion = x.Hospitalizacion
                                            ,
                                            Infectocontagioso = x.Infectocontagioso
                                            ,
                                            Traumatico = x.Traumatico
                                            ,
                                            Toxicomania = x.Toxicomania
                                            ,
                                            Transfusional = x.Transfusional
                                            ,
                                            Alergia = x.Alergia
                                            ,
                                            ConsumoTabaco = x.ConsumoTabaco
                                            ,
                                            ConsumoAlcohol = x.ConsumoAlcohol
                                            ,
                                            InicioVidaSexual = x.InicioVidaSexual
                                            ,
                                            NumeroParejaSexual = x.NumeroParejaSexual
                                            ,
                                            Parejas = x.Parejas
                                            ,
                                            CantidadHijo = x.CantidadHijo
                                            ,
                                            MetodoPlanificacion = x.MetodoPlanificacion
                                            ,
                                            InfeccionSexual = x.InfeccionSexual
                                            ,
                                            Menarca = x.Menarca
                                            ,
                                            Telarca = x.Telarca
                                            ,
                                            Gesta = x.Gesta
                                            ,
                                            Parto = x.Parto
                                            ,
                                            Aborto = x.Aborto
                                            ,
                                            Cesarea = x.Cesarea
                                            ,
                                            Papanicolau = x.Papanicolau
                                            ,
                                            Mastografia = x.Mastografia
                                        }).FirstOrDefault<HistoriaClinicaViewModel>();

                    return _HistoriaClinica;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en el Sistema: " + ex.Message + "\nFavor de Comunicarse con el Administrador.");
                }
            }

        }
        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// Obtiene Consulta de Historias Clinicas
        /// </summary>
        /// <returns>Lista de Historias Clinicas del Tipo ViewModel </returns>
        public static List<HistoriaClinicaViewModel> ConsultaHistoriasClinicasVM()
        {
            List<HistoriaClinicaViewModel> lstHistoriaClinica = new List<HistoriaClinicaViewModel>();
            HistoriaClinicaViewModel _historiaClinica = new HistoriaClinicaViewModel();

            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                dataBaseContext.Configuration.ProxyCreationEnabled = false;

                try
                {
                    var historiasClinicas = dataBaseContext.HistoriaClinica.Where(x => x.Activo == true).ToList();

                    foreach (var iHistoriaClinica in historiasClinicas)
                    {
                        _historiaClinica = new HistoriaClinicaViewModel()
                        {
                            IdUsuario = _historiaClinica.IdUsuario
                                        ,
                            IdSexo = Convert.ToInt32(_historiaClinica.IdSexo)
                                        ,
                            IdEstadoCivil = Convert.ToInt32(_historiaClinica.IdEstadoCivil)
                                        ,
                            IdReligion = Convert.ToInt32(_historiaClinica.IdReligion)
                                        ,
                            IdTipoSangre = Convert.ToInt32(_historiaClinica.IdTipoSangre)
                                        ,
                            IdEscolaridad = Convert.ToInt32(_historiaClinica.IdEscolaridad)
                                        ,
                            Activo = Convert.ToBoolean(_historiaClinica.Activo)
                                        ,
                            FechaNacimiento = _historiaClinica.FechaNacimiento
                                        ,
                            FechaActualizacion = _historiaClinica.FechaActualizacion
                                        ,
                            LugarNacimiento = _historiaClinica.LugarNacimiento
                                        ,
                            LugarResidencia = _historiaClinica.LugarResidencia
                                        ,
                            Ocupacion = _historiaClinica.Ocupacion
                                        ,
                            NumeroSeguridadSocial = _historiaClinica.NumeroSeguridadSocial
                                        ,
                            PolizaSeguro = _historiaClinica.PolizaSeguro
                                        ,
                            AbueloPaterno = _historiaClinica.AbueloPaterno
                                        ,
                            AbuelaPaterno = _historiaClinica.AbuelaPaterno
                                        ,
                            AbueloMaterno = _historiaClinica.AbueloMaterno
                                        ,
                            AbuelaMaterno = _historiaClinica.AbuelaMaterno
                                        ,
                            Padre = _historiaClinica.Padre
                                        ,
                            Madre = _historiaClinica.Madre
                                        ,
                            Hermano = _historiaClinica.Hermano
                                        ,
                            Hijo = _historiaClinica.Hijo
                                        ,
                            Vivienda = _historiaClinica.Vivienda
                                        ,
                            Zoonosis = _historiaClinica.Zoonosis
                                        ,
                            Habitos = _historiaClinica.Habitos
                                        ,
                            IngestaLiquido = _historiaClinica.IngestaLiquido
                                        ,
                            Higiene = _historiaClinica.Higiene
                                        ,
                            Inmunizacion = _historiaClinica.Inmunizacion
                                        ,
                            LenguaIndigena = _historiaClinica.LenguaIndigena
                                        ,
                            Combe = _historiaClinica.Combe
                                        ,
                            ViajesRecientes = _historiaClinica.ViajesRecientes
                                        ,
                            ExposicionBiomasa = _historiaClinica.ExposicionBiomasa
                                        ,
                            CronicoDegenerativo = _historiaClinica.CronicoDegenerativo
                                        ,
                            Quirurjicos = _historiaClinica.Quirurjicos
                                        ,
                            Hospitalizacion = _historiaClinica.Hospitalizacion
                                        ,
                            Infectocontagioso = _historiaClinica.Infectocontagioso
                                        ,
                            Traumatico = _historiaClinica.Traumatico
                                        ,
                            Toxicomania = _historiaClinica.Toxicomania
                                        ,
                            Transfusional = _historiaClinica.Transfusional
                                        ,
                            Alergia = _historiaClinica.Alergia
                                        ,
                            ConsumoTabaco = _historiaClinica.ConsumoTabaco
                                        ,
                            ConsumoAlcohol = _historiaClinica.ConsumoAlcohol
                                        ,
                            InicioVidaSexual = _historiaClinica.InicioVidaSexual
                                        ,
                            NumeroParejaSexual = _historiaClinica.NumeroParejaSexual
                                        ,
                            Parejas = _historiaClinica.Parejas
                                        ,
                            CantidadHijo = _historiaClinica.CantidadHijo
                                        ,
                            MetodoPlanificacion = _historiaClinica.MetodoPlanificacion
                                        ,
                            InfeccionSexual = _historiaClinica.InfeccionSexual
                                        ,
                            Menarca = _historiaClinica.Menarca
                                        ,
                            Telarca = _historiaClinica.Telarca
                                        ,
                            Gesta = _historiaClinica.Gesta
                                        ,
                            Parto = _historiaClinica.Parto
                                        ,
                            Aborto = _historiaClinica.Aborto
                                        ,
                            Cesarea = _historiaClinica.Cesarea
                                        ,
                            Papanicolau = _historiaClinica.Papanicolau
                                        ,
                            Mastografia = _historiaClinica.Mastografia
                        };
                        lstHistoriaClinica.Add(_historiaClinica);
                    }

                    return lstHistoriaClinica;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en el Sistema: " + ex.Message + "\nFavor de comunicarse con el administrador.");
                }
            }

        }
        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// Crea Historia Clinica de un Usuario Determinado
        /// </summary>
        /// <param name="_HistoriaClinicaViewModel">Objeto de Historia Clinica del Tipo ViewModel</param>
        /// <returns>Respuesta de Resultado de la Creación</returns>
        public static Operation CrearHistoriaClinica(HistoriaClinicaViewModel _HistoriaClinicaViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Valida Historia Creada
                    string strIdUsuario = _HistoriaClinicaViewModel.IdUsuario.ToString();
                    var id = Guid.Parse(strIdUsuario);
                    var Usuario = UserDAL.ObtenerUsuarioVM(id);
                    var historiaClinica = ObtenerHistoriaClinicaVM(Usuario);
                    if (historiaClinica != null)
                    {
                        return Operation.Failure("La Historia Clínica del Usuario: " + Usuario.Nombre + ", ya existe, favor de verificar");
                    }
                    #endregion

                    #region Crea Historia Clinica
                    HistoriaClinica _tblHistoriaClinica = new HistoriaClinica
                    {
                        IdUsuario = Guid.Parse(_HistoriaClinicaViewModel.IdUsuario)
                        ,
                        IdSexo = Convert.ToInt32(_HistoriaClinicaViewModel.IdSexo)
                        ,
                        IdEstadoCivil = Convert.ToInt32(_HistoriaClinicaViewModel.IdEstadoCivil)
                        ,
                        IdReligion = Convert.ToInt32(_HistoriaClinicaViewModel.IdReligion)
                        ,
                        IdTipoSangre = Convert.ToInt32(_HistoriaClinicaViewModel.IdTipoSangre)
                        ,
                        IdEscolaridad = Convert.ToInt32(_HistoriaClinicaViewModel.IdEscolaridad)
                        ,
                        Activo = Convert.ToBoolean(_HistoriaClinicaViewModel.Activo)
                        ,
                        FechaNacimiento = _HistoriaClinicaViewModel.FechaNacimiento
                        ,
                        //FechaActualizacion = _HistoriaClinicaViewModel.FechaActualizacion
                        FechaActualizacion = DateTime.Now
                        ,
                        LugarNacimiento = _HistoriaClinicaViewModel.LugarNacimiento
                        ,
                        LugarResidencia = _HistoriaClinicaViewModel.LugarResidencia
                        ,
                        Ocupacion = _HistoriaClinicaViewModel.Ocupacion
                        ,
                        NumeroSeguridadSocial = _HistoriaClinicaViewModel.NumeroSeguridadSocial
                        ,
                        PolizaSeguro = _HistoriaClinicaViewModel.PolizaSeguro
                        ,
                        AbueloPaterno = _HistoriaClinicaViewModel.AbueloPaterno
                        ,
                        AbuelaPaterno = _HistoriaClinicaViewModel.AbuelaPaterno
                        ,
                        AbueloMaterno = _HistoriaClinicaViewModel.AbueloMaterno
                        ,
                        AbuelaMaterno = _HistoriaClinicaViewModel.AbuelaMaterno
                        ,
                        Padre = _HistoriaClinicaViewModel.Padre
                        ,
                        Madre = _HistoriaClinicaViewModel.Madre
                        ,
                        Hermano = _HistoriaClinicaViewModel.Hermano
                        ,
                        Hijo = _HistoriaClinicaViewModel.Hijo
                        ,
                        Vivienda = _HistoriaClinicaViewModel.Vivienda
                        ,
                        Zoonosis = _HistoriaClinicaViewModel.Zoonosis
                        ,
                        Habitos = _HistoriaClinicaViewModel.Habitos
                        ,
                        IngestaLiquido = _HistoriaClinicaViewModel.IngestaLiquido
                        ,
                        Higiene = _HistoriaClinicaViewModel.Higiene
                        ,
                        Inmunizacion = _HistoriaClinicaViewModel.Inmunizacion
                        ,
                        LenguaIndigena = _HistoriaClinicaViewModel.LenguaIndigena
                        ,
                        Combe = _HistoriaClinicaViewModel.Combe
                        ,
                        ViajeReciente = _HistoriaClinicaViewModel.ViajesRecientes
                        ,
                        ExposicionBiomasa = _HistoriaClinicaViewModel.ExposicionBiomasa
                        ,
                        CronicoDegenerativo = _HistoriaClinicaViewModel.CronicoDegenerativo
                        ,
                        Quirurjicos = _HistoriaClinicaViewModel.Quirurjicos
                        ,
                        Hospitalizacion = _HistoriaClinicaViewModel.Hospitalizacion
                        ,
                        Infectocontagioso = _HistoriaClinicaViewModel.Infectocontagioso
                        ,
                        Traumatico = _HistoriaClinicaViewModel.Traumatico
                        ,
                        Toxicomania = _HistoriaClinicaViewModel.Toxicomania
                        ,
                        Transfusional = _HistoriaClinicaViewModel.Transfusional
                        ,
                        Alergia = _HistoriaClinicaViewModel.Alergia
                        ,
                        ConsumoTabaco = _HistoriaClinicaViewModel.ConsumoTabaco
                        ,
                        ConsumoAlcohol = _HistoriaClinicaViewModel.ConsumoAlcohol
                        ,
                        InicioVidaSexual = _HistoriaClinicaViewModel.InicioVidaSexual
                        ,
                        NumeroParejaSexual = _HistoriaClinicaViewModel.NumeroParejaSexual
                        ,
                        Parejas = _HistoriaClinicaViewModel.Parejas
                        ,
                        CantidadHijo = _HistoriaClinicaViewModel.CantidadHijo
                        ,
                        MetodoPlanificacion = _HistoriaClinicaViewModel.MetodoPlanificacion
                        ,
                        InfeccionSexual = _HistoriaClinicaViewModel.InfeccionSexual
                        ,
                        Menarca = _HistoriaClinicaViewModel.Menarca
                        ,
                        Telarca = _HistoriaClinicaViewModel.Telarca
                        ,
                        Gesta = _HistoriaClinicaViewModel.Gesta
                        ,
                        Parto = _HistoriaClinicaViewModel.Parto
                        ,
                        Aborto = _HistoriaClinicaViewModel.Aborto
                        ,
                        Cesarea = _HistoriaClinicaViewModel.Cesarea
                        ,
                        Papanicolau = _HistoriaClinicaViewModel.Papanicolau
                        ,
                        Mastografia = _HistoriaClinicaViewModel.Mastografia
                    };
                    dataBaseContext.HistoriaClinica.Add(_tblHistoriaClinica);
                    #endregion
                    dataBaseContext.SaveChanges();

                    #region RegistroNavegacion
                    Guid _IdFolio = Guid.NewGuid();
                    #endregion

                    return Operation.Success("La Historia Clínica del Usuario : " + Usuario.Nombre + " fue Creada con Exito", _IdFolio);
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// Edita Historia Clinica de un Usuario Determinado
        /// </summary>
        /// <param name="_HistoriaClinicaViewModel">Objeto de Historia Clinica del Tipo ViewModel</param>
        /// <returns>Respuesta de Resultado de la Edición</returns>
        public static Operation EditaHistoriaClinica(HistoriaClinicaViewModel _HistoriaClinicaViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Valida Historia Creada
                    string strIdUsuario = _HistoriaClinicaViewModel.IdUsuario.ToString();
                    var id = Guid.Parse(strIdUsuario);
                    var _usuario = UserDAL.ObtenerUsuarioVM(id);
                    var _historiaClinica = dataBaseContext.HistoriaClinica.Where(x => x.IdUsuario == id).FirstOrDefault();
                    if (_historiaClinica == null)
                    {
                        return Operation.Failure("La Historia Clínica del Usuario: " + _usuario.Nombre + ", no existe, favor de verificar");
                    }
                    #endregion

                    #region Edita Historia Clinica
                    _historiaClinica.IdUsuario = Guid.Parse(_HistoriaClinicaViewModel.IdUsuario);
                    _historiaClinica.IdSexo = Convert.ToInt32(_HistoriaClinicaViewModel.IdSexo);
                    _historiaClinica.IdEstadoCivil = Convert.ToInt32(_HistoriaClinicaViewModel.IdEstadoCivil);
                    _historiaClinica.IdReligion = Convert.ToInt32(_HistoriaClinicaViewModel.IdReligion);
                    _historiaClinica.IdTipoSangre = Convert.ToInt32(_HistoriaClinicaViewModel.IdTipoSangre);
                    _historiaClinica.IdEscolaridad = Convert.ToInt32(_HistoriaClinicaViewModel.IdEscolaridad);
                    _historiaClinica.Activo = Convert.ToBoolean(_HistoriaClinicaViewModel.Activo);
                    _historiaClinica.FechaNacimiento = _HistoriaClinicaViewModel.FechaNacimiento;
                    //_historiaClinica.FechaActualizacion = _HistoriaClinicaViewModel.FechaActualizacion;
                    _historiaClinica.FechaActualizacion = DateTime.Now;
                    _historiaClinica.LugarNacimiento = _HistoriaClinicaViewModel.LugarNacimiento;
                    _historiaClinica.LugarResidencia = _HistoriaClinicaViewModel.LugarResidencia;
                    _historiaClinica.Ocupacion = _HistoriaClinicaViewModel.Ocupacion;
                    _historiaClinica.NumeroSeguridadSocial = _HistoriaClinicaViewModel.NumeroSeguridadSocial;
                    _historiaClinica.PolizaSeguro = _HistoriaClinicaViewModel.PolizaSeguro;
                    _historiaClinica.AbueloPaterno = _HistoriaClinicaViewModel.AbueloPaterno;
                    _historiaClinica.AbuelaPaterno = _HistoriaClinicaViewModel.AbuelaPaterno;
                    _historiaClinica.AbueloMaterno = _HistoriaClinicaViewModel.AbueloMaterno;
                    _historiaClinica.AbuelaMaterno = _HistoriaClinicaViewModel.AbuelaMaterno;
                    _historiaClinica.Padre = _HistoriaClinicaViewModel.Padre;
                    _historiaClinica.Madre = _HistoriaClinicaViewModel.Madre;
                    _historiaClinica.Hermano = _HistoriaClinicaViewModel.Hermano;
                    _historiaClinica.Hijo = _HistoriaClinicaViewModel.Hijo;
                    _historiaClinica.Vivienda = _HistoriaClinicaViewModel.Vivienda;
                    _historiaClinica.Zoonosis = _HistoriaClinicaViewModel.Zoonosis;
                    _historiaClinica.Habitos = _HistoriaClinicaViewModel.Habitos;
                    _historiaClinica.IngestaLiquido = _HistoriaClinicaViewModel.IngestaLiquido;
                    _historiaClinica.Higiene = _HistoriaClinicaViewModel.Higiene;
                    _historiaClinica.Inmunizacion = _HistoriaClinicaViewModel.Inmunizacion;
                    _historiaClinica.LenguaIndigena = _HistoriaClinicaViewModel.LenguaIndigena;
                    _historiaClinica.Combe = _HistoriaClinicaViewModel.Combe;
                    _historiaClinica.ViajeReciente = _HistoriaClinicaViewModel.ViajesRecientes;
                    _historiaClinica.ExposicionBiomasa = _HistoriaClinicaViewModel.ExposicionBiomasa;
                    _historiaClinica.CronicoDegenerativo = _HistoriaClinicaViewModel.CronicoDegenerativo;
                    _historiaClinica.Quirurjicos = _HistoriaClinicaViewModel.Quirurjicos;
                    _historiaClinica.Hospitalizacion = _HistoriaClinicaViewModel.Hospitalizacion;
                    _historiaClinica.Infectocontagioso = _HistoriaClinicaViewModel.Infectocontagioso;
                    _historiaClinica.Traumatico = _HistoriaClinicaViewModel.Traumatico;
                    _historiaClinica.Toxicomania = _HistoriaClinicaViewModel.Toxicomania;
                    _historiaClinica.Transfusional = _HistoriaClinicaViewModel.Transfusional;
                    _historiaClinica.Alergia = _HistoriaClinicaViewModel.Alergia;
                    _historiaClinica.ConsumoTabaco = _HistoriaClinicaViewModel.ConsumoTabaco;
                    _historiaClinica.ConsumoAlcohol = _HistoriaClinicaViewModel.ConsumoAlcohol;
                    _historiaClinica.InicioVidaSexual = _HistoriaClinicaViewModel.InicioVidaSexual;
                    _historiaClinica.NumeroParejaSexual = _HistoriaClinicaViewModel.NumeroParejaSexual;
                    _historiaClinica.Parejas = _HistoriaClinicaViewModel.Parejas;
                    _historiaClinica.CantidadHijo = _HistoriaClinicaViewModel.CantidadHijo;
                    _historiaClinica.MetodoPlanificacion = _HistoriaClinicaViewModel.MetodoPlanificacion;
                    _historiaClinica.InfeccionSexual = _HistoriaClinicaViewModel.InfeccionSexual;
                    _historiaClinica.Menarca = _HistoriaClinicaViewModel.Menarca;
                    _historiaClinica.Telarca = _HistoriaClinicaViewModel.Telarca;
                    _historiaClinica.Gesta = _HistoriaClinicaViewModel.Gesta;
                    _historiaClinica.Parto = _HistoriaClinicaViewModel.Parto;
                    _historiaClinica.Aborto = _HistoriaClinicaViewModel.Aborto;
                    _historiaClinica.Cesarea = _HistoriaClinicaViewModel.Cesarea;
                    _historiaClinica.Papanicolau = _HistoriaClinicaViewModel.Papanicolau;
                    _historiaClinica.Mastografia = _HistoriaClinicaViewModel.Mastografia;
                    #endregion
                    dataBaseContext.SaveChanges();

                    #region RegistroNavegacion
                    Guid _IdFolio = Guid.NewGuid();
                    #endregion

                    return Operation.Success("La Historia Clínica del Usuario : " + _usuario.Nombre + " fue Modificada con Exito", _IdFolio);
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------

        /// <summary>
        /// Borra Historia Clinica de un Usuario Determinado
        /// </summary>
        /// <param name="_HistoriaClinicaViewModel">Objeto de Historia Clinica del Tipo ViewModel</param>
        /// <returns>Respuesta de Resultado del Borrado</returns>
        public static Operation BorraHistoriaClinica(HistoriaClinicaViewModel _HistoriaClinicaViewModel)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;

                    #region Valida Historia Creada
                    var _usuario = UserDAL.ObtenerUsuarioVM(_HistoriaClinicaViewModel.IdUsuario);
                    var _historiaClinica = dataBaseContext.HistoriaClinica.Where(x => x.IdUsuario == Guid.Parse(_HistoriaClinicaViewModel.IdUsuario)).FirstOrDefault();
                    if (_historiaClinica == null)
                    {
                        return Operation.Failure("La Historia Clínica del Usuario: " + _usuario.Nombre + ", no existe, favor de verificar");
                    }
                    #endregion

                    #region Borra Historia Clinica
                    dataBaseContext.HistoriaClinica.Remove(_historiaClinica);
                    #endregion
                    dataBaseContext.SaveChanges();

                    #region RegistroNavegacion
                    Guid _IdFolio = Guid.NewGuid();
                    #endregion

                    return Operation.Success("La Historia Clínica del Usuario : " + _usuario.Nombre + " fue Borrada con Exito", _IdFolio);
                }
            }
            catch (Exception ex)
            {
                return Operation.Failure(ex.Message);
            }
        }
        //--------------------------------------------------------------------------------------------
    }
}
