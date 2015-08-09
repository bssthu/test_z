using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace eso_zh_server
{
    class Worker
    {
        FormConfig frm;

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
                // do work
            }
        }
    }
}
