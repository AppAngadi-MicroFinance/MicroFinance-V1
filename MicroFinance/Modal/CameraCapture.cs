using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MicroFinance.Modal
{
    class CameraCapture
    {
        //public static BitmapImage SavedImage;
        //public BitmapImage tempimg;
        //public FilterInfoCollection FilterInfoCollection;
        //public VideoCaptureDevice captureDevice;

        //public void OpenCamera()
        //{
        //    FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        //    foreach (FilterInfo filterInfo in FilterInfoCollection)
        //    {
        //        DeviceList.Items.Add(filterInfo.Name);
        //    }
        //    if (DeviceList.Items.Count <= 0)
        //    {
        //        MessageBox.Show("No WebCam in your System.");
        //    }
        //    else
        //    {
        //        DeviceList.SelectedIndex = 0;
        //        captureDevice = new VideoCaptureDevice(FilterInfoCollection[DeviceList.SelectedIndex].MonikerString);
        //        captureDevice.NewFrame += CaptureDevice_NewFrame;

        //    }
        //}
        //private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //    var temp = eventArgs.Frame.Clone();
        //    BitmapImage bitmap = BitmapToBitmapImage((Bitmap)temp);
        //    byte[] arr = Convertion(bitmap);
        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(arr))
        //    {
        //        this.Dispatcher.Invoke((Action)(() =>
        //        {
        //            var imageSource = new BitmapImage();
        //            imageSource.BeginInit();
        //            imageSource.StreamSource = ms;
        //            imageSource.CacheOption = BitmapCacheOption.OnLoad;
        //            imageSource.EndInit();
        //            LiveImage.Source = imageSource;
        //        }));
        //    }
        //}
        public byte[] Convertion(BitmapImage image)
        {
            byte[] Data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                Data = ms.ToArray();
            }
            return Data;
        }

        public BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }

    }
}
