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
                if (rawText != newRawText && UpdateText != null)
                {
                    rawText = newRawText;
                    UpdateText(newRawText);
                }
                // TODO: translate
            }
        }
    }
}
