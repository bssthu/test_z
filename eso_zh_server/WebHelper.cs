using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace eso_zh_server
{
    // https://codehosting.net/blog/BlogEngine/post/Simple-C-Web-Server.aspx
    class WebHelper
    {
        public String Text { get; set; }
        private HttpListener listener;
        public int Port
        {
            get;
            private set;
        }
        public String Url
        {
            get;
            private set;
        }

        public WebHelper()
        {
            Port = 20528;
            Url = "";
            Text = "";
            try
            {
                listener = new HttpListener();
                // netsh http add urlacl url=http://+:20528/ user=users
                String httpAddress = String.Format("http://+:{0}/", Port);
                listener.Prefixes.Add(httpAddress);
                listener.Start();

                String ipString = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(
                        ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
                Url = String.Format("http://{0}:{1}/", ipString, Port);
            }
            catch (Exception ex)
            {
                Port = 0;
                Trace.WriteLine(ex.Message);
            }
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                string rstr = Response(ctx.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { } // suppress any exceptions
                            finally
                            {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        }, listener.GetContext());
                    }
                }
                catch { } // suppress any exceptions
            });
        }

        public String Response(HttpListenerRequest request)
        {
            Trace.WriteLine(request.LocalEndPoint.Address.ToString());
            String bodyText = Text.Replace("\r\n", "<br />");
            String headText = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />"
                + "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">"
                + "<meta http-equiv=\"refresh\" content=\"2\"/>";
            return String.Format("<html><head>{0}</head><body>{1}</body></html>", headText, bodyText);
        }
    }
}
