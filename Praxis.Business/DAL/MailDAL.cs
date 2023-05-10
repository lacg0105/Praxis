using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praxis.Model;
using Praxis.Model.Emun;

namespace Praxis.Business.DAL
{
    public static class MailDAL
    {
        public static CatCorreo ObtenerMail(EnumCatMail _EnumCatMail)
        {
            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                Int32 IdMail = Convert.ToInt32(_EnumCatMail);
                dataBaseContext.Configuration.ProxyCreationEnabled = false;
                var oMail = dataBaseContext.CatCorreo.Where(u => u.IdCorreo == IdMail).FirstOrDefault();
                return oMail;
            }

        }
        //--------------------------------------------------------------------------------------------
    }
}
