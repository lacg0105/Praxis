using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Praxis.Business.Security;

namespace Praxis.App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Autenticación Post del Usuario con Cookie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {

            if (FormsAuthentication.CookiesSupported == false)
                return;

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName]; // valida si existe la cookie 
            if (authCookie == null)
                return;

            try
            {
                //si todo va bien desencripta la cookie 
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                Authentication userPrincipal = new Authentication(authTicket.Name);//y el resultado (userid) lo ocupo para el constructor de la clase Authentication
                HttpContext.Current.User = userPrincipal; // aquí ya viene el objeto cargado 
                System.Threading.Thread.CurrentPrincipal = System.Web.HttpContext.Current.User;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
