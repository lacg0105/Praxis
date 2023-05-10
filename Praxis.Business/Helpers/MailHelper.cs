using Praxis.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Praxis.Model.Emun;

namespace Praxis.Business.Helpers
{
    public static class MailHelper
    {
        private static string smailPruebas { get; set; }
        private static string sAmbiente { get; set; }
        private static string sUsuarioSMTP { get; set; }
        private static string sDisplayName { get; set; }
        private static string sSMTP { get; set; }
        private static string sContraseñaSMTP = ConfigurationManager.AppSettings.Get("ContraseñaSMTP").ToString().Trim();
        //private static string sContraseñaSMTP = "chocolateteamo";
        public static bool enviaCorreo(string strEmailTo, string CC, string Bcc, string Subject, string contenido)
        {
            CargaParametros();

            if (!Comun.ValidarEmail(strEmailTo))
                return false;

            System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage();
            Email.From = new MailAddress(sUsuarioSMTP, sDisplayName);

            if (sAmbiente.Equals("DEV"))
            {
                if (smailPruebas.Contains(";"))
                {
                    var sMails = smailPruebas.Split(';');
                    foreach (var itemMail in sMails)
                    {
                        Email.To.Add(itemMail);
                    }
                }
                else
                {
                    Email.To.Add(smailPruebas);
                }
            }
            else
            {
                Email.To.Add(strEmailTo);

                if (!String.IsNullOrEmpty(CC))
                {
                    if (CC.Contains(";"))
                    {
                        var sMailsCC = CC.Split(';');
                        foreach (var itemMail in sMailsCC)
                        {
                            Email.CC.Add(itemMail);
                        }
                    }
                    else
                    {
                        Email.CC.Add(CC);
                    }
                }

                if (!String.IsNullOrEmpty(Bcc))
                {
                    if (Bcc.Contains(";"))
                    {
                        var sMailsBcc = Bcc.Split(';');
                        foreach (var itemMail in sMailsBcc)
                        {
                            Email.Bcc.Add(itemMail);
                        }
                    }
                    else
                    {
                        Email.Bcc.Add(Bcc);
                    }

                }
            }

            Email.Subject = Subject;
            Email.IsBodyHtml = true;
            Email.Body = contenido;

            SmtpClient clienteSmtp = new SmtpClient(sSMTP);
            clienteSmtp.Port = 587;
            clienteSmtp.Credentials = new System.Net.NetworkCredential(sUsuarioSMTP, sContraseñaSMTP);
            clienteSmtp.EnableSsl = true;
            try
            {
                clienteSmtp.Send(Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        //------------------------------------------------------------------------------------------------------------
        private static void CargaParametros()
        {
            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                Int32 IdSetting = 0;
                var lstMailSettings = dataBaseContext.Parametro.ToList();

                //----------------
                IdSetting = 0;
                IdSetting = Convert.ToInt32(EnumParametros.CorreosDEV);
                smailPruebas = lstMailSettings.Where(s => s.IdParametro == IdSetting).Select(s => s.Valor).FirstOrDefault();
                //----------------

                //----------------
                IdSetting = 0;
                IdSetting = Convert.ToInt32(EnumParametros.Ambiente);
                sAmbiente = lstMailSettings.Where(s => s.IdParametro == IdSetting).Select(s => s.Valor).FirstOrDefault();
                //----------------

                //----------------
                IdSetting = 0;
                IdSetting = Convert.ToInt32(EnumParametros.UsuarioSMTP);
                sUsuarioSMTP = lstMailSettings.Where(s => s.IdParametro == IdSetting).Select(s => s.Valor).FirstOrDefault();
                //----------------

                //----------------
                IdSetting = 0;
                IdSetting = Convert.ToInt32(EnumParametros.DisplayName);
                sDisplayName = lstMailSettings.Where(s => s.IdParametro == IdSetting).Select(s => s.Valor).FirstOrDefault();
                //----------------

                //----------------
                IdSetting = 0;
                IdSetting = Convert.ToInt32(EnumParametros.SMTP);
                sSMTP = lstMailSettings.Where(s => s.IdParametro == IdSetting).Select(s => s.Valor).FirstOrDefault();
                //----------------
            }


        }
        //------------------------------------------------------------------------------------------------------------
    }
}
