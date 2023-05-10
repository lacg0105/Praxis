using Praxis.Business.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praxis.App.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new Authentication _CurrentUser
        {
            get { return HttpContext.User as Authentication; }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}