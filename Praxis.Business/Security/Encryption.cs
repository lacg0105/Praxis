using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Business.Security
{
    public static class Encryption
    {
        #region Cifrado SHA 256
        public static bool ComparePasswords_SHA256(byte[] byStorePass, string strPass)
        {
            if (byStorePass == null)
            {
                //throw new NullReferenceException("No hay contraseña registrada para este usuario. Favor de verificar.");
                return false;
            }

            try
            {
                byte[] bySaltValue = new byte[16];
                int iSaltOffset = byStorePass.Length - 16;

                for (int i = 0; i < 16; i++)
                {
                    bySaltValue[i] = byStorePass[iSaltOffset + i];
                }

                byte[] bySaltedPass = GetSecureHash_SHA256(strPass, bySaltValue);

                return CompareByteArray_SHA256(byStorePass, bySaltedPass);
            }
            catch (NullReferenceException nr)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //-------------------------------------------------------------------------------------------------------------------
        private static byte[] GetSecureHash_SHA256(string strPass, byte[] salt)
        {

            //if (string.IsNullOrWhiteSpace(strPass))
            //{
            //    throw new ArgumentNullException("Contraseña", "La contraseña no cumple con el formato correcto, favor de verificar.");
            //}

            try
            {
                byte[] byPass = Encoding.UTF8.GetBytes(strPass);
                byte[] byrawSalted = new byte[byPass.Length + salt.Length];

                byPass.CopyTo(byrawSalted, 0);
                salt.CopyTo(byrawSalted, byPass.Length);

                SHA256 sha256 = SHA256.Create();
                byte[] bySaltedPass = sha256.ComputeHash(byrawSalted);

                byte[] byDbPass = new byte[bySaltedPass.Length + salt.Length];
                bySaltedPass.CopyTo(byDbPass, 0);
                salt.CopyTo(byDbPass, bySaltedPass.Length);

                return byDbPass;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (CryptographicException cr)
            {
                throw cr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        private static bool CompareByteArray_SHA256(byte[] byArray1, byte[] byArray2)
        {
            if (byArray1.Length != byArray2.Length)
            {
                //throw new IndexOutOfRangeException("Las contraseñas no coinciden. Favor de verificar.");
                return false;
            }

            try
            {
                for (int i = 0; i < byArray1.Length; i++)
                {
                    if (byArray1[i] != byArray2[i])
                        return false;
                }
                return true;
            }
            catch (IndexOutOfRangeException or)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        public static byte[] EncryptPassword_SHA256(string strPassword)
        {
            byte[] bySalt = GetSalt_SHA256();
            return GetSecureHash_SHA256(strPassword, bySalt);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private static byte[] GetSalt_SHA256()
        {
            try
            {
                var p = new RNGCryptoServiceProvider();
                var salt = new byte[16];
                p.GetBytes(salt);
                return salt;
            }
            catch (CryptographicException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        #endregion
    }
}
