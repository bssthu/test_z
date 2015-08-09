using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace eso_zh_server
{
    public partial class FormConfig : Form
    {
        private Worker worker;
        private Thread workerThread;

        public FormConfig()
        {
            InitializeComponent();
            // init worker thread
            worker = new Worker(this);
            worker.UpdateText += delegate(String text)
            {
                this.BeginInvoke((Worker.UpdateTextDelegate)UpdateText, text);
            };
            workerThread = new Thread(worker.Run);
            workerThread.Start();
            // load settings
            textBoxAppKey.Text = Properties.Settings.Default.appKey;
        }

        public void UpdateText(String text)
        {
            textBoxInfo.Text = text;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // save settings
            Properties.Settings.Default.appKey = textBoxAppKey.Text;
            Properties.Settings.Default.Save();
            // kill worker thread
            if (workerThread != null)
            {
                workerThread.Interrupt();
                while (workerThread.IsAlive)
                {
                    Thread.Sleep(200);
                }
            }

            base.OnClosing(e);
        }

        private void textBoxAppKey_TextChanged(object sender, EventArgs e)
        {
            if (worker != null)
            {
                worker.AppKey = textBoxAppKey.Text;
            }
        }
    }
}
