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
            worker = new Worker(this);
            workerThread = new Thread(worker.Run);
            workerThread.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
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
    }
}
