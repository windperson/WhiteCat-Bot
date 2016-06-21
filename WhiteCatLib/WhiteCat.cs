using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Web;

namespace WhiteCatLib
{
    public class WhiteCat
    {
        public static T Parse<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static string DecryptRequest(string method, string data, string key)
        {
            string decryptedData = "";

            CryptoDectectResult cryptoDectectResult = CryptoDectect.isRequestCrypted(method);


            if (cryptoDectectResult.isEncrypted)
                decryptedData = Cipher.Decrypt(data, cryptoDectectResult.isGzipped, key);
            else if (cryptoDectectResult.isGzipped)
                decryptedData = Cipher.GzUncompress(Convert.FromBase64String(data));
            else
                decryptedData = data;

            return decryptedData;
        }

        public static string DecryptResponse(string method, string body, string key)
        {
            string decrypted = "";

            CryptoDectectResult cryptoDectectResult = CryptoDectect.isResponseCrypted(method);

            if (cryptoDectectResult.isEncrypted)
                decrypted = Cipher.Decrypt(body, cryptoDectectResult.isGzipped, key);
            else if (cryptoDectectResult.isGzipped)
                decrypted = Cipher.GzUncompress(Convert.FromBase64String(body));
            else
                decrypted = body;

            return decrypted;
        }

        public static Dictionary<string, string> BodyToDict(string body)
        {
            var fields = body.Split(new char[] { '&' });
            var returns = new Dictionary<string, string>();

            foreach (var field in fields)
            {
                var data = field.Split(new char[] { '=' });
                returns.Add(data[0], HttpUtility.UrlDecode(data[1]));
            }

            return returns;
        }

        public static Dictionary<string, string> CookiesToDict(string cookies)
        {
            var fields = cookies.Split(new char[] { ';' });
            var returns = new Dictionary<string, string>();

            foreach (var field in fields)
            {
                var data = field.Split(new char[] { '=' });
                returns.Add(data[0], HttpUtility.UrlDecode(data[1]));
            }
            return returns;
        }
    }
}
