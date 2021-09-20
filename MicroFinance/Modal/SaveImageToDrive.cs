using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string FolderPath = Path;
            if(!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            string ImagePath = FolderPath +"\\"+ FileName + ".jpg";
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Data)))
            {
                image.Save(ImagePath, ImageFormat.Jpeg);  // Or Png
            }
        }
    }
}
