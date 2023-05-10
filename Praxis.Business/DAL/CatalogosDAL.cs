using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Business.DAL;
using Praxis.Model;
using Praxis.Model.ViewModel;

namespace Praxis.Business.DAL
{
    public class CatalogosDAL
    {
        #region Áreas
        public static List<AreasVM> ObtenerAreas()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from a in dataBaseContext.CatArea
                                     select new AreasVM
                                     {
                                         IdArea = a.IdArea,
                                         NombreArea = a.Nombre
                                     }).ToList<AreasVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Roles
        public static List<RolesVM> ObtenerRoles()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from r in dataBaseContext.CatRol
                                     select new RolesVM
                                     {
                                         IdRol = r.IdRol,
                                         NombreRol = r.Nombre
                                     }).ToList<RolesVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Estatus Usuario
        public static List<EstatusUsuarioVM> ObtenerEstatusUsuario()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from e in dataBaseContext.CatEstatus_Usuario
                                     select new EstatusUsuarioVM
                                     {
                                         IdEstatusUsuario = e.IdEstatusUsuario,
                                         NombreEstatus = e.Nombre
                                     }).ToList<EstatusUsuarioVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Instituciones
        public static List<InstitucionesVM> ObtenerInstitucion()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatInstitucion
                                     join t in dataBaseContext.CatTipoInstitucion on i.IdTipoInstitucion equals t.IdTipoInstitucion
                                     select new InstitucionesVM
                                     {
                                         IdInstitucion = i.IdInstitucion,
                                         NombreInstitucion = i.Nombre,
                                         IdTipoInstitucion = i.IdTipoInstitucion,
                                         NombreTipoInstitucion = t.Nombre,
                                         NombreLargo = i.NombreLargo,
                                         Carpeta = i.Carpeta
                                     }).ToList<InstitucionesVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Sexo
        public static List<SexoVM> ObtenerSexo()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatSexo
                                     select new SexoVM
                                     {
                                         IdSexo = i.IdSexo,
                                         NombreSexo = i.Nombre
                                     }).ToList<SexoVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        public static List<SexoVM> ObtenerSexoPorId(int IdSexo)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatSexo
                                     where i.IdSexo == IdSexo
                                     select new SexoVM
                                     {
                                         IdSexo = i.IdSexo,
                                         NombreSexo = i.Nombre
                                     }).ToList<SexoVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Religion
        public static List<ReligionVM> ObtenerReligion()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatReligion
                                     select new ReligionVM
                                     {
                                         IdReligion = i.IdReligion,
                                         NombreReligion = i.Nombre
                                     }).ToList<ReligionVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        public static List<ReligionVM> ObtenerReligionPorIdSelect2(int IdReligion)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatReligion
                                     where i.IdReligion == IdReligion
                                     select new ReligionVM
                                     {
                                         IdReligion = i.IdReligion,
                                         NombreReligion = i.Nombre
                                     }).ToList<ReligionVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Tipo Sangre
        public static List<TipoSangreVM> ObtenerTipoSangre()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatTipoSangre
                                     select new TipoSangreVM
                                     {
                                         IdTipoSangre = i.IdTipoSangre,
                                         NombreTipoSangre = i.Nombre
                                     }).ToList<TipoSangreVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        public static List<TipoSangreVM> ObtenerTipoSangrePorIdSelect2(int IdTipoSangre)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatTipoSangre
                                     where i.IdTipoSangre == IdTipoSangre
                                     select new TipoSangreVM
                                     {
                                         IdTipoSangre = i.IdTipoSangre,
                                         NombreTipoSangre = i.Nombre
                                     }).ToList<TipoSangreVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Escolaridad
        public static List<EscolaridadVM> ObtenerEscolaridad()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatEscolaridad
                                     select new EscolaridadVM
                                     {
                                         IdEscolaridad = i.IdEscolaridad,
                                         NombreEscolaridad = i.Nombre
                                     }).ToList<EscolaridadVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------------------------------------- 
        public static List<EscolaridadVM> ObtenerEscolaridadPorIdSelect2(int IdEscolaridad)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatEscolaridad
                                     where i.IdEscolaridad == IdEscolaridad
                                     select new EscolaridadVM
                                     {
                                         IdEscolaridad = i.IdEscolaridad,
                                         NombreEscolaridad = i.Nombre
                                     }).ToList<EscolaridadVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Estado Civil
        public static List<EstadoCivilVM> ObtenerEstadoCivil()
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatEstadoCivil
                                     select new EstadoCivilVM
                                     {
                                         IdEstadoCivil = i.IdEstadoCivil,
                                         NombreEstadoCivil = i.Nombre
                                     }).ToList<EstadoCivilVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        public static List<EstadoCivilVM> ObtenerEstadoCivilPorIdSelect2(int IdEstadoCivil)
        {
            try
            {
                using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
                {
                    dataBaseContext.Configuration.ProxyCreationEnabled = false;
                    var lstInsFin = (from i in dataBaseContext.CatEstadoCivil
                                     where i.IdEstadoCivil == IdEstadoCivil
                                     select new EstadoCivilVM
                                     {
                                         IdEstadoCivil = i.IdEstadoCivil,
                                         NombreEstadoCivil = i.Nombre
                                     }).ToList<EstadoCivilVM>();
                    return lstInsFin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        #endregion
    }
}
