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
        private Dictionary<String, String> nouns = new Dictionary<String, String>();
        public Translator()
        {
            try
            {
                LoadNoun();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void LoadNoun()
        {
            String fileName = "noun.txt";
            StreamReader reader = new StreamReader(fileName);
            String fullText = reader.ReadToEnd();
            reader.Close();
            String[] lines = fullText.Split( new[] { '\n', '\r' } );
            foreach (String str in lines)
            {
                try
                {
                    String line = str.Trim();
                    if (line != "" && !line.StartsWith("#"))
                    {
                        String[] words = line.Split(new[] { '|' }, 2);
                        if (words.Length == 2 && !nouns.ContainsKey(words[0].Trim()))
                        {
                            nouns[words[0].Trim()] = words[1].Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public String Translate(String text, String appKey, String lang = "en-zh")
        {
            text = ReplaceNoun(text);
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
                if (response == null)
                {
                    return "翻译时出错，服务器没有响应，请检查网络连接";
                }
                else
                {
                    String message = GetMessageFromJson(GetStringFromResponse(response));
                    return String.Format("翻译时出错：\r\n{0} {1}, {2}",
                            (int)response.StatusCode, response.StatusCode, message);
                }
            }

            string responseFromServer = GetStringFromResponse(response);
            
            response.Close();
            return GetTextFromJson(responseFromServer);
        }

        private String GetStringFromResponse(HttpWebResponse response)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            string responseFromServer = readStream.ReadToEnd();

            readStream.Close();
            receiveStream.Close();
            return responseFromServer;
        }

        private String GetTextFromJson(String jsonString)
        {
            try
            {
                dynamic json = Json.Decode(jsonString);
                return json.text[0].ToString();
            }
            catch (FieldAccessException ex)
            {
                return ex.Message + "\n\n解析 JSON 时出错。";
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return null;
        }

        private String GetMessageFromJson(String jsonString)
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

        // 使用名词列表中的词的翻译
        private String ReplaceNoun(String text)
        {
            foreach (var item in nouns)
            {
                try
                {
                    text = text.Replace(item.Key, item.Value);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
            return text;
        }
    }
}
