using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praxis.Model.ViewModel;
using Praxis.Business.DAL;
using Praxis.Model.Emun;
using Praxis.Business.Helpers;
using System.Web.Security;

namespace Praxis.App.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LockScreen()
        {
            return View();
        }
        //--------------------------------------------------
        public JsonResult ValidarUsuario(UsuariosViewModel _UsersViewModel)
        {
            var _Operation = AccessDAL.ValidarUsuario(_UsersViewModel);
            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                //REGISTRO DE LOGIN 1 
                #region Log Clave Acceso
                BitacoraDAL.RegistroBitacoraClavesAcceso(_UsersViewModel, Convert.ToInt32(EnumCatEvento_Bitacora.Acceso_Login_Usuario), "Correo", _UsersViewModel.Correo, "");
                #endregion
                return Json(OperationResult.Success(_Operation.Message, _Operation.Data));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion

        }
        //--------------------------------------------------------------------------------------------
        public JsonResult IngresaSistema(UsuariosViewModel _UsersViewModel)
        {
            var _Operation = AccessDAL.IngresaSistema(_UsersViewModel);
            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                UserDAL.ActualizaFechaAcceso(_UsersViewModel);
                var ContraseñaBD = UserDAL.ConsultaContraseñaUsuario(_UsersViewModel.Correo, _UsersViewModel.Curp);
                //REGISTRO DE USUARIO LOGIN 2
                #region Log Clave Acceso
                BitacoraDAL.RegistroBitacoraClavesAcceso(ContraseñaBD, Convert.ToInt32(EnumCatEvento_Bitacora.Acceso_Login_Contraseña), "Correo", _UsersViewModel.Correo, "");
                #endregion

                SetAuthCookie(_Operation.Data.ToString());
                return Json(OperationResult.Success(_Operation.Message, _Operation.Data));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion

        }
        //--------------------------------------------------------------------------------------------
        private void SetAuthCookie(string sValue)
        {
            FormsAuthentication.SetAuthCookie(sValue, false);
        }
        //--------------------------------------------------------------------------------------------
        public JsonResult BloquearUsuario(UsuariosViewModel _UsersViewModel)
        {
            var _Operation = UserDAL.BloquearUsuario(_UsersViewModel);
            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                #region Envio de correo
                var oMail = MailDAL.ObtenerMail(EnumCatMail.BloqueoClaveAcceso);
                var _Usr = UserDAL.ObtenerUsuarioVM(_UsersViewModel.Correo);
                string sHTML = oMail.ContenidoHTML;
                sHTML = sHTML.Replace("[Nombre]", _Usr.Nombre);
                sHTML = sHTML.Replace("[Tipo Bloqueo]", _Usr.NombreEstatus);
                MailHelper.enviaCorreo(_UsersViewModel.Correo, oMail.BCC, oMail.BCCO, oMail.Asunto, sHTML);
                #endregion
                return Json(OperationResult.Success(_Operation.Message));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion
        }
        //--------------------------------------------------------------------------------------------
        public JsonResult RecuperarCuenta(UsuariosViewModel _UsersViewModel)
        {
            var _Operation = AccessDAL.RecuperarCuenta(_UsersViewModel);

            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                #region Envio de correo
                var oMail = MailDAL.ObtenerMail(EnumCatMail.RecuperacionContraseña);
                var _Usr = UserDAL.ObtenerUsuarioVM(_UsersViewModel.Correo);
                var sUrl = Request.ServerVariables["HTTP_HOST"].ToString() + new UrlHelper(Request.RequestContext).Action("LoginTemp", "Login", new { id = _Operation.Data });
                string sHTML = oMail.ContenidoHTML;
                sHTML = sHTML.Replace("[Nombre]", _Usr.Nombre);
                sHTML = sHTML.Replace("[Folio]", _Operation.Data.ToString());
                sHTML = sHTML.Replace("[href]", sUrl);
                MailHelper.enviaCorreo(_UsersViewModel.Correo, oMail.BCC, oMail.BCCO, oMail.Asunto, sHTML);
                #endregion

                //REGISTRO DE RECUPERACIÒN DE CONTRASEÑA
                #region Log Clave Acceso
                BitacoraDAL.RegistroBitacoraClavesAcceso(_Usr, Convert.ToInt32(EnumCatEvento_Bitacora.Recuperación_de_Claves_de_Acceso), "Correo", _Usr.Correo, "");
                #endregion

                return Json(OperationResult.Success(_Operation.Message));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion

        }
        //--------------------------------------------------------------------------------------------  
        public ActionResult LoginTemp(string id)
        {
            #region Validación de folio
            var _Operation = AccessDAL.ValidaFolio(id);
            #endregion

            #region JsonResult
            if (_Operation.IsSuccess != EnumOperationResult.Success)
            {
                return RedirectToAction("InternalServerError", "Login");
            }
            #endregion

            ViewBag.IdUser = _Operation.Data;
            ViewBag.Folio = id;

            return View();
        }
        //--------------------------------------------------------------------------------------------
        public JsonResult CambiarPwd(UsuariosViewModel _UsersViewModel)
        {
            var _Operation = UserDAL.CambiarPwd(_UsersViewModel);
            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {
                SetAuthCookie(_Operation.Data.ToString());
                return Json(OperationResult.Success(_Operation.Message));
            }
            else
            {
                return Json(OperationResult.Failure(_Operation.Message));
            }
            #endregion
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult InternalServerError()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
        //--------------------------------------------------------------------------------------------
    }
}
