using AForge.Video;
using AForge.Video.DirectShow;
using MicroFinance.Utils;
using MicroFinance.ViewModel;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for Capture.xaml
    /// </summary>
    public partial class Capture : Page
    {
        public static BitmapImage SavedImage;
        public BitmapImage tempimg;
        public FilterInfoCollection FilterInfoCollection;
        public VideoCaptureDevice captureDevice;
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        public Capture()
        {
            InitializeComponent();
            if(IsCameraAvailable())
            {
                CaptureBtn.Visibility = Visibility.Visible;
            }
            else
            {
                CaptureBtn.Visibility = Visibility.Collapsed;
            }
        }
        private void closebtn_Click(object sender, RoutedEventArgs e)
        {

        }

        public void OpenCamera()
        {
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in FilterInfoCollection)
            {
                DeviceList.Items.Add(filterInfo.Name);
            }
            if (DeviceList.Items.Count <= 0)
            {
                message = language.translate(SystemFunction.IsTamil, "AE5");//No WebCam in your System.
                MessageBox.Show(message);
            }
            else
            {
                DeviceList.SelectedIndex = 0;
                captureDevice = new VideoCaptureDevice(FilterInfoCollection[DeviceList.SelectedIndex].MonikerString);
                captureDevice.NewFrame += CaptureDevice_NewFrame;
               
            }
        }
        private bool IsCameraAvailable()
        {
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (FilterInfoCollection.Count == 0)
                return false;
            else
                return true;

        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var temp = eventArgs.Frame.Clone();
            BitmapImage bitmap = BitmapToBitmapImage((Bitmap)temp);
            byte[] arr = Convertion(bitmap);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(arr))
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                    LiveImage.Source = imageSource;
                }));
            }
        }
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

        private void CaptureBtn_Click(object sender, RoutedEventArgs e)
        {

            CapturePanel.IsOpen = true;
            OpenCamera();
            captureDevice.Start();
        }

        private void CamCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DeviceList.Items.Count > 0)
            {
                captureDevice.SignalToStop();
            }
           CapturePanel.IsOpen = false;
        }
        public void SaveCaptureImage(BitmapSource bitmap)
        {
            SavedImage = new BitmapImage();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            if (bitmap != null)
            {
                if (captureDevice.IsRunning)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(memoryStream);
                    memoryStream.Position = 0;
                    SavedImage.BeginInit();
                    SavedImage.StreamSource = memoryStream;
                    SavedImage.EndInit();
                    memoryStream.Close();
                }
                else
                {
                    captureDevice.SignalToStop();
                }
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "W4");//Please Capture..
                MainWindow.StatusMessageofPage(0, message);
            }
        }

        private void CapturePhoto_Click(object sender, RoutedEventArgs e)
        {
            CapturedImage.Source = null;
            tempimg = new BitmapImage();
            tempimg = LiveImage.Source as BitmapImage;
            CapturedImage.Source = tempimg; 
            Savebtn.IsEnabled = true;
            if (CapturedImage.Source != null)
            {
                CapturePhoto.Content = "Re-Take";
                Savebtn.Visibility = Visibility.Visible;
            }

        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            if(tempimg!=null)
            {
                if (DeviceList.Items.Count > 0)
                {
                    captureDevice.SignalToStop();
                }
                CapImg.Source = tempimg;
                SavedImage = tempimg;
                CapturePanel.IsOpen = false;
                message = language.translate(SystemFunction.IsTamil, "SA11");//Successfully Image Is Captured...
                AddCustomer.StatusMessageWhileCapturingImage(1, message);
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "W6");//"Capture Image First"
                MainWindow.StatusMessageofPage(0, message);
            }
           

        }

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            tempimg = new BitmapImage();
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Image Files (*.png *.jpg *.bmp) |*.png; *.jpg;; *jpeg; *.bmp|All Files(*.*) |*.*";
            openFileDlg.Title = "Choose Image";
            openFileDlg.InitialDirectory = @"C:\";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                string FileFrom = openFileDlg.FileName;
                var FilePath = FileFrom.Split('\\');
                
                var sk = ResizeImageQuality(FileFrom);
                string FileName = FilePath[FilePath.Length - 1];
                //tempimg.BeginInit();
                //tempimg.UriSource = new Uri(FileFrom);
                //tempimg.EndInit();
                //CapImg.Source = tempimg;
                //SavedImage = tempimg;
                CapImg.Source = sk;
                SavedImage = sk;
            }
            
        }

      private  BitmapImage ResizeImageQuality(string filepathwithextension)
        {
            Bitmap myBitmap;
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myBitmap = new Bitmap(filepathwithextension);
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 10L);
            myEncoderParameters.Param[0] = myEncoderParameter;
          //myBitmap.Save("Shapes025.jpg", myImageCodecInfo, myEncoderParameters);
          return  MyExtension.ToBitmapImage(myBitmap);

        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        

    }

    public static class MyExtension
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
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
