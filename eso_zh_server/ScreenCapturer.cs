using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace eso_zh_server
{
    static class ScreenCapturer
    {
        public static Bitmap Capture()
        {
            Screen screen = Screen.PrimaryScreen;
            int width = screen.Bounds.Width;
            int height = screen.Bounds.Height;
            Image image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            try
            {
                graphics.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
            //image.Save(@".\test.png");
            return (Bitmap)image;
        }
    }
}
