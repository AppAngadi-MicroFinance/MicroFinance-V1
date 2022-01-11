using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Reports
{
    public class CollectionDetailsReport
    {
        ObservableCollection<CollectionDetailsModel> Details = new ObservableCollection<CollectionDetailsModel>();
        public CollectionDetailsReport(ObservableCollection<CollectionDetailsModel> collectionDetails)
        {
            Details = collectionDetails;
        }

        StringBuilder FileData = new StringBuilder();
        string FilePath= System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Report\\Collection Details\\");
        string FileName = "CollectionRepost_" + DateTime.Now.ToString("(dd-MMM-yyyy hh-mm-ss)")+".csv";
        public void GenerateCollectionRepost()
        {
            formData();
            if(Directory.Exists(FilePath))
            {
                string name = FilePath + FileName;
                StreamWriter writer = new StreamWriter(name);
                writer.WriteLine(FileData.ToString());
                writer.Close();
            }
            else
            {
                Directory.CreateDirectory(FilePath);
                string name = FilePath + FileName;
                StreamWriter writer = new StreamWriter(name);
                writer.WriteLine(FileData.ToString());
                writer.Close();
            }
        }

        void formData()
        {
            FileData.Append("Branch Name,Employee Name,Center Name,Customer Name,Collected On,Principal,Interest,Security Deposit,Total\n");
            foreach(CollectionDetailsModel data in Details)
            {
                FileData.Append(data.ToString() + "\n");
            }
        }
    }
}
