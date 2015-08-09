using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace eso_zh_server
{
    class Worker
    {
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
                // capture & decode
                String newRawText = QrDecoder.MultiDecode(ScreenCapturer.Capture());
                // translate & display
                if (newRawText != null && rawText != newRawText && UpdateText != null)
                {
                    rawText = newRawText;
                    UpdateText(rawText);
                    String translated = Translator.Translate(rawText, String.Copy(AppKey));
                    UpdateText(String.Format("{0}\r\n\r\n\r\n{1}", rawText, translated.Replace("\n", "\r\n")));
                }
            }
        }
    }
}
