using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

namespace eso_zh_server
{
    static class QrDecoder
    {
        public static String MultiDecode(Bitmap bitmap)
        {
            // convert
            if (bitmap == null)
            {
                return null;
            }
            LuminanceSource luminanceSource = new BitmapLuminanceSource(bitmap);
            BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(luminanceSource));

            // decode
            ZXing.Result result = (new MultiFormatReader()).decode(binaryBitmap);
            if (result == null)
            {
                return null;
            }

            String text = result.Text;
            return text;
        }
    }
}
