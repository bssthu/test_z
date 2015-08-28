using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace eso_zh_server
{
    class Worker
    {
        private Translator translator;

        public delegate void UpdateTextDelegate(String text);
        public event UpdateTextDelegate UpdateText;

        public String AppKey
        {
            get
            {
                lock (_AppKey)
                {
                    return _AppKey;
                }
            }
            set
            {
                lock (_AppKey)
                {
                    _AppKey = value;
                }
            }
        }
        private String _AppKey = "";

        FormConfig frm;
        String rawText;

        public Worker(FormConfig frm)
        {
            this.frm = frm;
            translator = new Translator();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException) // ok
                {
                    return;
                }
                try
                {
                    // capture & decode
                    String newRawText = QrDecoder.MultiDecode(ScreenCapturer.Capture());
                    // translate & display
                    if (newRawText != null && rawText != newRawText && UpdateText != null)
                    {
                        rawText = newRawText;
                        UpdateText(rawText);
                        String translated = translator.Translate(rawText, String.Copy(AppKey));
                        UpdateText(String.Format("{0}\r\n\r\n\r\n{1}", rawText, translated.Replace("\n", "\r\n")));
                    }
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    UpdateText(String.Format("{0}\r\n\r\n找不到 zxing.dll, 请检查或重新下载软件。", ex.Message));
                    return;
                }
                catch (Exception ex)
                {
                    UpdateText(ex.Message);
                    return;
                }
            }
        }
    }
}
