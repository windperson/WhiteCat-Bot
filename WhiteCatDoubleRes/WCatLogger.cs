using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;

using WhiteCatLib;
using WhiteCatLib.Response;

using Titanium.Web.Proxy.EventArguments;

using Newtonsoft.Json;

namespace WhiteCatLogger
{
    class WCatLogger
    {
        public static string logPath = Directory.GetCurrentDirectory() + "\\";

        private static Dictionary<string, string> existsKeys = new Dictionary<string, string>();


        public static void ReloadKeys()
        {
            existsKeys.Clear();

            if (!Directory.Exists(logPath + "keys"))
                Directory.CreateDirectory(logPath + "keys");

            foreach (var file in Directory.EnumerateFiles(logPath + "keys"))
            {
                string[] t = file.Split(new char[] { '\\' });
                string name = t[t.Length - 1];
                existsKeys.Add(name, File.ReadAllText(file));
            }

        }

        public static void SaveKey(string id, string key)
        {
            if (existsKeys.ContainsKey(id))
                existsKeys.Remove(id);

            existsKeys.Add(id, key);

            if (!Directory.Exists(logPath + "keys"))
                Directory.CreateDirectory(logPath + "keys");

            File.WriteAllText(logPath + "keys\\" + id, key);
        }

        public static async Task OnRequest(object sender, SessionEventArgs e)
        {
            try
            {

                Regex regex = new Regex("http://app.wcproject.so-net.tw/ajax/(.*)");

                var url = e.WebSession.Request.Url;

                if (!regex.IsMatch(url))
                    return;


                var method = regex.Match(url).Groups[1].Value;

                var reqLogPath = logPath + "requests\\" + method;

                var cookieId = "";

                foreach (var header in e.WebSession.Request.RequestHeaders)
                    if (header.Name.Equals("Cookie") && header.Value.Contains("wcatpt"))
                        cookieId = BodyToDict(header.Value)["wcatpt"].Split(new char[] { ':' })[0];


                Console.WriteLine(url);

                var body = Encoding.UTF8.GetString(await e.GetRequestBody());

                var fields = BodyToDict(body);

                CryptoDectectResult cryptInfo = CryptoDectect.isRequestCrypted(method);

                if (cryptInfo != null)
                {
                    string writed = body + Environment.NewLine + Environment.NewLine;

                    string key = Cipher.DEFAULT_NETWORKHASH;

                    if (!cryptInfo.isDefultKey && !cookieId.Equals("") && existsKeys.ContainsKey(cookieId))
                        key = existsKeys[cookieId];

                    string decryptedData = "";

                    if (fields.ContainsKey("data"))
                    {
                        decryptedData = WhiteCat.DecryptRequest(method, fields["data"], key);
                    }

                    switch (method)
                    {
                        case "regist/checkregister":
                            break;
                        case "regist/create":
                            break;
                        case "quest/complete":
                            QuestComplete response = WhiteCat.Parse<QuestComplete>(decryptedData);

                            response.gold *= 3;

                            int count = response.itemIds.Count;

                            for (var i = 0; i < count * 2; i++)
                            {
                                response.itemIds.Add(response.itemIds[i]);
                            }
                            string fixedData = JsonConvert.SerializeObject(response);
                            string fixedEncrypted = Cipher.EncryptRJ128(key, Cipher.DEFAULT_IV_128, fixedData);
                            fields["data"] = fixedEncrypted;
                            await  e.SetRequestBodyString(DictionaryToBody(fields));
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Detected unrecognized method: " + method);
                    Console.WriteLine("Content: " + body);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        //Modify response
        public static async Task OnResponse(object sender, SessionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("http://app.wcproject.so-net.tw/ajax/(.*)");

                var url = e.WebSession.Request.Url;

                if (!regex.IsMatch(url))
                    return;

                var method = regex.Match(url).Groups[1].Value;

                var resLogPath = logPath + "response\\" + method;

                var body = Encoding.UTF8.GetString(await e.GetResponseBody());

                var cookieId = "";

                foreach (var header in e.WebSession.Response.ResponseHeaders)
                    if (header.Name.Equals("Set-Cookie") && header.Value.Contains("wcatpt"))
                        cookieId = CookiesToDict(header.Value)["wcatpt"].Split(new char[] { ':' })[0];

                if (cookieId.Equals(""))
                {
                    foreach (var header in e.WebSession.Request.RequestHeaders)
                        if (header.Name.Equals("Cookie") && header.Value.Contains("wcatpt"))
                            cookieId = CookiesToDict(header.Value)["wcatpt"].Split(new char[] { ':' })[0];
                }

                CryptoDectectResult cryptInfo = CryptoDectect.isResponseCrypted(method);

                string key = Cipher.DEFAULT_NETWORKHASH;

                if (!cryptInfo.isDefultKey && !cookieId.Equals("") && existsKeys.ContainsKey(cookieId))
                    key = existsKeys[cookieId];

                string decrypted = WhiteCat.DecryptResponse(method, body, key);

                switch (method)
                {
                    case "regist/checkregister":
                        break;
                    case "regist/create":
                        RegistCreate res = WhiteCat.Parse<RegistCreate>(decrypted);
                        SaveKey(cookieId, res.result.uh);
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                e.IsValid = true;

            return Task.FromResult(0);
        }

        public static Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            return Task.FromResult(0);
        }

        private static Dictionary<string, string> BodyToDict(string body)
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

        private static Dictionary<string, string> CookiesToDict(string cookies)
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


        private static string DictionaryToBody(Dictionary<string,string> dics)
        {
            string body = "";

            foreach(var key in dics.Keys)
            {
                if (!body.Equals(""))
                    body += "&";
                body += key + "=" + HttpUtility.UrlEncode(dics[key]);
            }

            return body;
        }
    }
}
