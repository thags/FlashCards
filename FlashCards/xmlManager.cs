using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlashCards
{
    class XmlManager
    {
        public static string ReadConfig()
        {
            XmlTextReader textReader = new XmlTextReader("web.config");
            XmlDocument document = new XmlDocument();
            document.Load(@"web.config");

            return document.SelectNodes("/configuration/test").Item(0).InnerText.ToString();
        }
    }
}
