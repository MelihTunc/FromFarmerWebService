using System;
using System.Collections.Generic;
using System.Text;

namespace FromFarmer.Utilities.StringCiphers
{
    public class Base64Cipher
    {
        public static string Encode(string cleanText)
        {
            try
            {
                UnicodeEncoding encoding = new UnicodeEncoding();
                Byte[] encodedData = encoding.GetBytes(cleanText);
                return Convert.ToBase64String(encodedData);
            }
            catch
            {
                return null;
            }
        }

        public static string Decode(string encodedText)
        {
            try
            {
                UnicodeEncoding decoding = new UnicodeEncoding();
                Byte[] decodedData = Convert.FromBase64String(encodedText);
                return decoding.GetString(decodedData);
            }
            catch
            {
                return null;
            }
        }

    }
}
