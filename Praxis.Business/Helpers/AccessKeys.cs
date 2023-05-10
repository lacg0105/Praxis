using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Praxis.Business.Helpers
{
    public static class AccessKeys
    {
        // Definición de los longitudes de las contraseñas
        private static int minimumLength = 8;
        private static int maximumLength = 15;

        // Definición de caracteres soportados en las contraseñas
        private static string lowerCaseRule = "abcdefghijkmnopqrstuvwxyz";
        private static string upperCaseRule = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private static string numberRule = "1234567890";
        // private static string symbolRule = "!#$%&()*+-/:;<=>?@[]_{|}";
        private static string symbolRule = "!$()*:;[]_{|}";

        #region GENERACIÓN_CONTRASEÑAS
        public static string GeneratePassword()
        {
            try
            {
                return GeneratePassword(minimumLength, maximumLength);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        private static string GeneratePassword(int minLength, int maxLength)
        {
            RNGCryptoServiceProvider rngservice = new RNGCryptoServiceProvider();
            int intnextCharIdx;
            int intnextGroupIdx;
            int intnextLeftGroupsOrderIdx;
            int intlastCharIdx;
            int intlastLeftGroupsOrderIdx;
            byte[] bytrandomBytes = new byte[4];
            char[] chrpasswordArray = null;
            char[][] charGroups = new char[][]
                    {
                        lowerCaseRule.ToCharArray(),
                        upperCaseRule.ToCharArray(),
                        numberRule.ToCharArray(),
                        //symbolRule.ToCharArray(),
                    };

            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                throw new IndexOutOfRangeException("Error en la longitud Mínima/Máxima de la Contraseña");

            try
            {
                int[] charsLeftInGroup = new int[charGroups.Length];
                for (int i = 0; i < charsLeftInGroup.Length; i++)
                    charsLeftInGroup[i] = charGroups[i].Length;

                int[] leftGroupsOrder = new int[charGroups.Length];
                for (int i = 0; i < leftGroupsOrder.Length; i++)
                    leftGroupsOrder[i] = i;

                rngservice.GetBytes(bytrandomBytes);

                int seed = BitConverter.ToInt32(bytrandomBytes, 0);
                Random random = new Random(seed);


                if (minLength < maxLength)
                    chrpasswordArray = new char[random.Next(minLength, maxLength + 1)];
                else
                    chrpasswordArray = new char[minLength];

                intlastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

                for (int i = 0; i < chrpasswordArray.Length; i++)
                {
                    if (intlastLeftGroupsOrderIdx == 0)
                        intnextLeftGroupsOrderIdx = 0;
                    else
                        intnextLeftGroupsOrderIdx = random.Next(0, intlastLeftGroupsOrderIdx);

                    intnextGroupIdx = leftGroupsOrder[intnextLeftGroupsOrderIdx];
                    intlastCharIdx = charsLeftInGroup[intnextGroupIdx] - 1;
                    if (intlastCharIdx == 0)
                        intnextCharIdx = 0;
                    else
                        intnextCharIdx = random.Next(0, intlastCharIdx + 1);

                    chrpasswordArray[i] = charGroups[intnextGroupIdx][intnextCharIdx];

                    if (intlastCharIdx == 0)
                        charsLeftInGroup[intnextGroupIdx] = charGroups[intnextGroupIdx].Length;
                    else
                    {
                        if (intlastCharIdx != intnextCharIdx)
                        {
                            char temp = charGroups[intnextGroupIdx][intlastCharIdx];
                            charGroups[intnextGroupIdx][intlastCharIdx] =
                                        charGroups[intnextGroupIdx][intnextCharIdx];
                            charGroups[intnextGroupIdx][intnextCharIdx] = temp;
                        }
                        charsLeftInGroup[intnextGroupIdx]--;
                    }

                    if (intlastLeftGroupsOrderIdx == 0)
                        intlastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                    else
                    {
                        if (intlastLeftGroupsOrderIdx != intnextLeftGroupsOrderIdx)
                        {
                            int temp = leftGroupsOrder[intlastLeftGroupsOrderIdx];
                            leftGroupsOrder[intlastLeftGroupsOrderIdx] =
                                        leftGroupsOrder[intnextLeftGroupsOrderIdx];
                            leftGroupsOrder[intnextLeftGroupsOrderIdx] = temp;
                        }
                        intlastLeftGroupsOrderIdx--;
                    }
                }
                return new string(chrpasswordArray);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
