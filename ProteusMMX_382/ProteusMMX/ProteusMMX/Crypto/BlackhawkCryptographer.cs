using PCLCrypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Crypto
{
    public static class BlackhawkCryptographer
    {
        public static byte[] Encrypt(byte[] unencrypted)
        {
            try
            {
                return TransformData(unencrypted, BlackhawkCryptographyProvider.Encryptor);
            }
            catch(Exception ex)
            {
               
                return new byte[0];
            }
        }

        public static string Encrypt(string unencrypted)
        {
            try
            {
                return Convert.ToBase64String(Encrypt(Encoding.Unicode.GetBytes(unencrypted)));
            }
            catch
            {
                return String.Empty;
            }
        }

        private static byte[] TransformData(byte[] input, ICryptoTransform transform)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                {
                    cs.Write(input, 0, input.Length);
                    cs.Dispose();

                    return ms.ToArray();
                }
            }
        }
    }
}
