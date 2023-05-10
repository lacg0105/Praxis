using Praxis.Model;
using Praxis.Model.Emun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Praxis.Business.Helpers
{
    public static class Comun
    {
        public static string CrearFolio()
        {
            return AccessKeys.GeneratePassword();
        }
        //--------------------------------------------------------------------------------------------
        public static bool ValidarEmail(string email)
        {
            bool booValidacion = true;
            System.Text.RegularExpressions.Regex emailVal = new System.Text.RegularExpressions.Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$");
            if (emailVal.Matches(email).Count < 1) { booValidacion = false; }
            return booValidacion;
        }
        //--------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        public static string ObtenerAmbiente()
        {
            using (BDPRAXISEntities dataBaseContext = new BDPRAXISEntities())
            {
                var lstMailSettings = dataBaseContext.Parametro.ToList();
                var IdSetting = Convert.ToInt32(EnumParametros.Ambiente);
                return lstMailSettings.Where(s => s.IdParametro == IdSetting).Select(s => s.Valor).FirstOrDefault();
            }

        }
        //----------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------
        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }
        //--------------------------------------------------------------------------------------------.   

        //--------------------------------------------------------------------------------------------.   
        public static void DeleteFile(string fileFullPath)
        {
            try
            {
                System.IO.File.Delete(fileFullPath);
            }
            catch (Exception)
            {

            }
        }
        //--------------------------------------------------------------------------------------------
    }
}
