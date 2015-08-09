using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace eso_zh_server
{
    class Translator
    {
        public static String Translate(String text, String appKey, String lang = "en-zh")
        {
            String url = String.Format("https://translate.yandex.net/api/v1.5/tr.json/translate?lang={0}&key={1}&text={2}",
                    lang, appKey, Uri.EscapeDataString(text));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            // Ignore failures
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                response = (HttpWebResponse)we.Response;
                String message = GetMessageFromJson(GetStringFromResponse(response));
                return String.Format("翻译时出错：\r\n{0} {1}, {2}",
                        (int)response.StatusCode, response.StatusCode, message);
            }
            catch (Exception ex)
            {
                return String.Format("翻译时出错：\r\n{0}", ex.Message);
            }
            
            string responseFromServer = GetStringFromResponse(response);
            response.Close();

            return GetTextFromJson(responseFromServer);
        }

        private static String GetStringFromResponse(HttpWebResponse response)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            string responseFromServer = readStream.ReadToEnd();

            readStream.Close();
            receiveStream.Close();
            return responseFromServer;
        }

        private static String GetTextFromJson(String jsonString)
        {
            dynamic json = Json.Decode(jsonString);
            String str = null;
            try
            {
                str = json.text[0].ToString();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return str;
        }

        private static String GetMessageFromJson(String jsonString)
        {
            dynamic json = Json.Decode(jsonString);
            String str = null;
            try
            {
                str = json.message.ToString();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return str;
        }
    }
}
