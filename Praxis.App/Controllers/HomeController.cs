using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praxis.Business.DAL;
using Praxis.Business.Security;
using Praxis.Model.ViewModel;

namespace Praxis.App.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //--------------------------------------------------
        public ActionResult GetMenu()
        {

            string idUser = _CurrentUser.Info.IdUsuario;
            #region Obtenemos el usuario
            var id = Guid.Parse(idUser);
            var UserName = UserDAL.ObtenerUsuarioVM(id);
            #endregion

            #region Obtenemos el menú

            MenuViewModel _MenuViewModel = AccessDAL.ObtenerPantallasconPermisos(id);

            if (_MenuViewModel.ModuloVM == null || _MenuViewModel.ModuloVM.Count == 0)
            {
                return PartialView("_TopNavbar2", new MenuViewModel()
                {
                    NombreUsuario = UserName.Nombre,
                    IdUsuario = UserName.IdUsuario.ToString(),
                    NombreRol = UserName.NombreRol,
                    IdRol = UserName.IdRol,
                });
            }
            _MenuViewModel.NombreUsuario = UserName.Nombre;
            _MenuViewModel.IdUsuario = UserName.IdUsuario.ToString();
            _MenuViewModel.Img = UserName.NombreImagen.ToString();
            _MenuViewModel.NombreRol = UserName.NombreRol;
            _MenuViewModel.IdRol = UserName.IdRol;
            #endregion

            return PartialView("_TopNavbar2", _MenuViewModel);
        }
        //--------------------------------------------------------------------------------------------
    }
}