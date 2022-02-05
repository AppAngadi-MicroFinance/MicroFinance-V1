using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MicroFinance.Modal
{
    public class SaveImageToDrive
    {
        public SaveImageToDrive(string Path,string FileName,byte[] Data)
        {
            SaveImage(Path, FileName, Data);
        }
        public SaveImageToDrive()
        {

        }
        public static void SaveImage(string Path,string FileName,byte[] Data)
        {
            if(Data!=null)
            {
                string FolderPath = Path;
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                    //MainWindow.StatusMessageofPage(0, "Drive Sync First!.....");
                }
                else
                {
                    string ImagePath = FolderPath + "\\" + FileName + ".jpg";
                    if (File.Exists(ImagePath) == false)
                    {
                        using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Data)))
                        {
                            image.Save(ImagePath, ImageFormat.Jpeg);
                        }
                    }
                    else
                    {

                        System.IO.FileStream stream = new System.IO.FileStream(ImagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        if (null == stream)
                        {
                            File.Delete(ImagePath);
                            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Data)))
                            {
                                image.Save(ImagePath, ImageFormat.Jpeg);
                            }
                        }
                        else
                        {
                            stream.Close();

                            File.Delete(ImagePath);
                            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Data)))
                            {
                                image.Save(ImagePath, ImageFormat.Jpeg);
                            }
                        }
                    }
                }
            }   
        }


        public static BitmapImage GetImage(string FolderPath,string FileName)
        {
           // BitmapImage bmp = new BitmapImage(@"Asserts\Icons\NoImageFound.jpg");
           // Uri DefaultImage = new Uri("../Asserts/Icons/NoImageFound.jpg");

            Uri DefalutImage=new Uri("pack://application:,,,/MicroFinance;component/Asserts/Icons/NoImageFound.jpg");

            // BitmapImage defaultimage = @"..\\Asserts\\Icons\\NoImageFound.jpg";
            if (Directory.Exists(FolderPath))
            {
                try
                {
                    string filepath = FolderPath + "\\" + FileName + ".jpg";
                    Uri ImagePath = new Uri(FolderPath + "\\" + FileName + ".jpg");

                    if (File.Exists(filepath))
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        image.UriSource = new Uri(filepath, UriKind.RelativeOrAbsolute);
                        image.EndInit();
                        return image;
                    }
                }
                catch(Exception ex)
                {

                }
                
            }
            //getimage(DefalutImage);
            return new BitmapImage(DefalutImage);
        }


       static BitmapImage getimage(Uri uri)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = uri;
            image.EndInit();
            return image;
        }

        private static byte[] converterImgToByte(BitmapImage x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

        private static BitmapImage ByteToBI(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
