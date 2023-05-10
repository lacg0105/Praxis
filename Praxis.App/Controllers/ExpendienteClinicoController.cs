using Praxis.Model.Emun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praxis.Business.DAL;

namespace Praxis.App.Controllers
{
    public class ExpendienteClinicoController : BaseController
    {
        // GET: ExpendienteClinico
        public ActionResult Index()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult HistoriaClinica()
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
    }
}