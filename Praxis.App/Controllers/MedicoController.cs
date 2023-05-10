using Praxis.Model.Emun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praxis.Business.DAL;
using Praxis.App.Helpers;
using Praxis.Model.ViewModel;

namespace Praxis.App.Controllers
{
    public class MedicoController : BaseController
    {
        // GET: Medico
        public ActionResult Index()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult Administracion()
        {
            ViewBag.IdPantalla = Convert.ToInt32(EnumPantalla.Historia_Clinica);
            #region Obtenemos el usuario
            string idUser = _CurrentUser.Info.IdUsuario;
            var id = Guid.Parse(idUser);
            var UserName = UserDAL.ObtenerUsuarioVM(id);
            #endregion
            ViewBag.NombreUsuario = UserName.Nombre;
            ViewBag.Curp = UserName.Curp;
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public JsonpResult ConsultaHistoriaClinicaPacientes()
        {
            var lstHistoriaClinicaPacientes = HistoriaClinicaDAL.ConsultaHistoriaClinicaPacientes();
            return this.Jsonp(lstHistoriaClinicaPacientes);
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult HistoriaClinica(string IdUsuario)
        {
            ViewBag.IdPantalla = Convert.ToInt32(EnumPantalla.Historia_Clinica);
            ViewBag.Title = "Historia Clínica";
            #region Obtenemos el usuario
            string idUser = IdUsuario;
            var id = Guid.Parse(idUser);
            var UserName = UserDAL.ObtenerUsuarioVM(id);
            if (UserName == null)
            {
                throw new HttpException((int)System.Net.HttpStatusCode.InternalServerError, "El usuario no existe.");
            }
            ViewBag.Nombre = UserName.Nombre;
            ViewBag.ApellidoPaterno = UserName.ApellidoPaterno;
            ViewBag.ApellidoMaterno = UserName.ApellidoMaterno;
            ViewBag.Imagen = UserName.NombreImagen;
            ViewBag.Curp = UserName.Curp;
            ViewBag.IdUsuario = IdUsuario;
            #endregion

            #region Consultamos si el usuario tiene historia clínica
            var historiaClinicaPaciente = HistoriaClinicaDAL.ObtenerHistoriaClinicaVM(UserName);
            if (historiaClinicaPaciente == null)
            {
                ViewBag.HistoriaClinica = "Crear";
                HistoriaClinicaViewModel _HistoriaClinicaPaciente = new HistoriaClinicaViewModel();
                return View(_HistoriaClinicaPaciente);
            }
            else
            {
                ViewBag.HistoriaClinica = "Editar";
                return View(historiaClinicaPaciente);
            }  
            #endregion
        }
        //--------------------------------------------------------------------------------------------
        [HttpPost, ValidateInput(false)]
        public JsonResult CrearHistoriaClinica(HistoriaClinicaViewModel _HistoriaClinicaViewModel)
        {
            #region Creacion de historia clínica
            var _Operation = HistoriaClinicaDAL.CrearHistoriaClinica(_HistoriaClinicaViewModel);
            #endregion

            #region JsonResult
            if (_Operation.IsSuccess == EnumOperationResult.Success)
            {

                //REGISTRO DE ALTA DE REGISTRO
                #region RegistroNavegacion
                //Guid _IdFolio = Guid.NewGuid();
                //var BitacoraOperation = BitacoraDAL.RegistroBitacoraNavegacion(_UsersViewModel, Convert.ToInt32(EnumPantalla.Usuarios), Convert.ToInt32(EnumCatEvento_Bitacora.Alta_de_Registro), "0", "IdUsuario", _Data.IdUsuarioGuid.ToString(), "", _UsersViewModel.Correo + "," + _UsersViewModel.Nombre, _IdFolio.ToString());
                //if (BitacoraOperation.IsSuccess == EnumOperationResult.Failure)
                //    return Json(OperationResult.Failure(_Operation.Message + "\n" + BitacoraOperation.Message));
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
        [HttpPost, ValidateInput(false)]
        public JsonResult EditarHistoriaClinica(HistoriaClinicaViewModel _HistoriaClinicaViewModel)
        {

            #region Editar datos del usuario
            var _Operation = HistoriaClinicaDAL.EditaHistoriaClinica(_HistoriaClinicaViewModel);
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
    }
}