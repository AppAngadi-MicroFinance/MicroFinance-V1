using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MicroFinance.ViewModel
{
    public class LanguageSelector
    {
        public string translate(bool Language, string id)
        {
            string Translatestring = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\Translate.xml");
            if (Language == false)
            {
                foreach (XmlNode xn in doc.SelectNodes("/Translate/English"))
                {
                    if (xn["id"].InnerText == id)
                    {
                        Translatestring = xn["Text"].InnerText;
                    }
                }
            }
            if (Language == true)
            {
                foreach (XmlNode xn in doc.SelectNodes("/Translate/Tamil"))
                {
                    if (xn["id"].InnerText == id)
                    {
                        Translatestring = xn["Text"].InnerText;
                    }
                }
            }
            return Translatestring;
        }
    }
}
