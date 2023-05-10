using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praxis.App.Helpers;
using Praxis.Business.DAL;
using Praxis.Business.Helpers;
using Praxis.Model.Emun;
using Praxis.Model.ViewModel;

namespace Praxis.App.Controllers
{
    public class CatalogosController : Controller
    {
        // GET: Catalogos
        public ActionResult Index()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------
        #region Usuarios
        public ActionResult Usuarios()
        {
            ViewBag.IdPantalla = Convert.ToInt32(EnumPantalla.Usuarios);
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public JsonpResult ObtenerUsuarios()
        {
            var lstUsers = UserDAL.ObtenerUsuarios();
            return this.Jsonp(lstUsers);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult InformacionUsuarios(string idUsuario)
        {
            ViewBag.IdSubSubModule = Convert.ToInt32(EnumPantalla.Usuarios);
            ViewBag.Title = "Información Usuarios";
            if (String.IsNullOrEmpty(idUsuario))
            {
                ViewBag.Mov = "NUEVO";
                return View(new UsuariosViewModel());
            }

            #region Recuperamos al usuario
            var idUser = Guid.Parse(idUsuario);
            var UserName = UserDAL.ObtenerUsuarioVM(idUser);
            if (UserName == null)
            {
                throw new HttpException((int)System.Net.HttpStatusCode.InternalServerError, "El usuario no existe.");
            }
            #endregion

            ViewBag.Mov = "EDITAR";
            return View(UserName);
        }
        //--------------------------------------------------------------------------------------------
        [HttpPost, ValidateInput(false)]
        public JsonResult CrearUsuario(UsuariosViewModel _UsersViewModel, HttpPostedFileBase ArchivoFoto)
        {
            #region Creacion del usuario
            string path = Server.MapPath("~/Content/Img/Usuarios/");
            var _Operation = UserDAL.CrearUsuario(_UsersViewModel, ((ArchivoFoto == null) ? null : ArchivoFoto.InputStream),
                ((ArchivoFoto == null) ? "" : System.IO.Path.GetExtension(ArchivoFoto.FileName)), path);
            #endregion

            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                var _Data = (UsuariosViewModel)_Operation.Data;
                #region Envio de correo
                var oMail = MailDAL.ObtenerMail(EnumCatMail.ActivacionCuenta);
                var sUrl = Request.ServerVariables["HTTP_HOST"].ToString() + new UrlHelper(Request.RequestContext).Action("LoginTemp", "Login", new { id = _Data.Folio });
                string sHTML = oMail.ContenidoHTML;
                sHTML = sHTML.Replace("[Nombre]", _UsersViewModel.Nombre);
                sHTML = sHTML.Replace("[href]", sUrl);
                MailHelper.enviaCorreo(_UsersViewModel.Correo, oMail.BCC, oMail.BCCO, oMail.Asunto, sHTML);

                //REGISTRO DE ALTA DE REGISTRO
                #region RegistroNavegacion
                Guid _IdFolio = Guid.NewGuid();
                var BitacoraOperation = BitacoraDAL.RegistroBitacoraNavegacion(_UsersViewModel, Convert.ToInt32(EnumPantalla.Usuarios), Convert.ToInt32(EnumCatEvento_Bitacora.Alta_de_Registro), "0", "IdUsuario", _Data.IdUsuarioGuid.ToString(), "", _UsersViewModel.Correo + "," + _UsersViewModel.Nombre, _IdFolio.ToString());
                if (BitacoraOperation.IsSuccess == EnumOperationResult.Failure)
                    return Json(OperationResult.Failure(_Operation.Message + "\n" + BitacoraOperation.Message));
                #endregion

                return Json(OperationResult.Success(_Operation.Message));
                #endregion
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion
        }
        //--------------------------------------------------------------------------------------------
        [HttpPost, ValidateInput(false)]
        public JsonResult EditarUsuario(UsuariosViewModel _UsersViewModel, HttpPostedFileBase ArchivoFoto)
        {

            #region Editar datos del usuario
            string path = Server.MapPath("~/Content/Img/Usuarios/");
            var _Operation = UserDAL.EditarUsuario(_UsersViewModel, ((ArchivoFoto == null) ? null : ArchivoFoto.InputStream),
                                ((ArchivoFoto == null) ? "" : System.IO.Path.GetExtension(ArchivoFoto.FileName)), path);
            #endregion

            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                return Json(OperationResult.Success(_Operation.Message));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion
        }
        //--------------------------------------------------------------------------------------------
        public JsonResult EliminarUsuario(string IdUsuario)
        {
            #region Eliminar usuario
            var _Operation = UserDAL.BorrarUsuario(IdUsuario);
            #endregion

            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                return Json(OperationResult.Success(_Operation.Message));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion

        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerPerfilUsuario(Guid IdUsuario)
        {
            var UsuarioVM = UserDAL.ObtenerUsuarioVM(IdUsuario);
            if (UsuarioVM.NombreRol == "Medico")
            {
                var PerfilUsuario = UserDAL.ObtenerPerfilUsuarioVM(IdUsuario);
                return this.Jsonp(PerfilUsuario);
            }
            else
            {
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
            }

        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Áreas
        public JsonpResult ObtenerAreas()
        {
            var lstAreas = CatalogosDAL.ObtenerAreas();
            return this.Jsonp(lstAreas);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Roles
        public JsonpResult ObtenerRoles()
        {
            var lstAreas = CatalogosDAL.ObtenerRoles();
            return this.Jsonp(lstAreas);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Estatus Usuario
        public JsonpResult ObtenerEstatusUsuario()
        {
            var lstAreas = CatalogosDAL.ObtenerEstatusUsuario();
            return this.Jsonp(lstAreas);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Institución
        public JsonpResult ObtenerInstitucion()
        {
            var lstInstitucion = CatalogosDAL.ObtenerInstitucion();
            return this.Jsonp(lstInstitucion);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Sexo
        public JsonpResult ObtenerSexo()
        {
            var lstInsFin = CatalogosDAL.ObtenerSexo();
            return this.Jsonp(lstInsFin);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerSexoSelect2()
        {
            var lstInsFin = CatalogosDAL.ObtenerSexo();
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdSexo,
                text = x.NombreSexo
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerSexoPorIdSelect2(int IdSexo)
        {
            var lstInsFin = CatalogosDAL.ObtenerSexoPorId(IdSexo);
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdSexo,
                text = x.NombreSexo
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Religión
        public JsonpResult ObtenerReligion()
        {
            var lstInsFin = CatalogosDAL.ObtenerReligion();
            return this.Jsonp(lstInsFin);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerReligionSelect2()
        {
            var lstInsFin = CatalogosDAL.ObtenerReligion();
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdReligion,
                text = x.NombreReligion
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerReligionPorIdSelect2(int IdReligion)
        {
            var lstInsFin = CatalogosDAL.ObtenerReligionPorIdSelect2(IdReligion);
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdReligion,
                text = x.NombreReligion
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Tipo Sangre
        public JsonpResult ObtenerTipoSangre()
        {
            var lstInsFin = CatalogosDAL.ObtenerTipoSangre();
            return this.Jsonp(lstInsFin);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerTipoSangreSelect2()
        {
            var lstInsFin = CatalogosDAL.ObtenerTipoSangre();
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdTipoSangre,
                text = x.NombreTipoSangre
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerTipoSangrePorIdSelect2(int IdTipoSangre)
        {
            var lstInsFin = CatalogosDAL.ObtenerTipoSangrePorIdSelect2(IdTipoSangre);
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdTipoSangre,
                text = x.NombreTipoSangre
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Escolaridad
        public ActionResult ObtenerEscolaridadSelect2()
        {
            var lstInsFin = CatalogosDAL.ObtenerEscolaridad();
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdEscolaridad,
                text = x.NombreEscolaridad
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerEscolaridadPorIdSelect2(int IdEscolaridad)
        {
            var lstInsFin = CatalogosDAL.ObtenerEscolaridadPorIdSelect2(IdEscolaridad);
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdEscolaridad,
                text = x.NombreEscolaridad
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        #endregion

        #region Estado Civil
        public ActionResult ObtenerEstadoCivilSelect2()
        {
            var lstInsFin = CatalogosDAL.ObtenerEstadoCivil();
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdEstadoCivil,
                text = x.NombreEstadoCivil
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerEstadoCivilPorIdSelect2(int IdEstadoCivil)
        {
            var lstInsFin = CatalogosDAL.ObtenerEstadoCivilPorIdSelect2(IdEstadoCivil);
            var modificarData = lstInsFin.Select(x => new
            {
                id = x.IdEstadoCivil,
                text = x.NombreEstadoCivil
            });
            return Json(new { items = modificarData }, JsonRequestBehavior.AllowGet);
        }
        //--------------------------------------------------------------------------------------------
        #endregion


    }
}