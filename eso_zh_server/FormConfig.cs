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
        private WebHelper webHelper;
        private Thread workerThread;

        public FormConfig()
        {
            InitializeComponent();
            this.Text = String.Format("esozh - v{0}", System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
            // init worker thread
            worker = new Worker(this);
            worker.UpdateText += delegate(String text)
            {
                this.BeginInvoke((Worker.UpdateTextDelegate)UpdateText, text);
            };
            webHelper = new WebHelper();
            if (webHelper.Port > 0)
            {
                webHelper.Run();
                textBoxPort.Text = webHelper.Port.ToString();
            }
            workerThread = new Thread(worker.Run);
            workerThread.Start();
            // load settings
            textBoxAppKey.Text = Properties.Settings.Default.appKey;
        }

        public void UpdateText(String text)
        {
            textBoxInfo.Text = text;
            webHelper.Text = text;
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

        private void buttonCheckUpdate_Click(object sender, EventArgs e)
        {
            buttonCheckUpdate.Enabled = false;
            UpdateChecker checker = new UpdateChecker();
            checker.checkUpdate();
        }

        private void buttonDefaultKey_Click(object sender, EventArgs e)
        {
            textBoxAppKey.Text = "trnsl.1.1.20150827T155316Z.5d2df7ac4748b73a.06bdb80394e47e10a50aaf1ad82f737a98702265";
        }
    }
}
