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
        public static Dictionary<string, string> ReadConfig()
        {
            Dictionary<string, string> configs = new Dictionary<string, string>();

            //add a try/catch, if the document doesnt exist or it got messed up this may cause the program to crash
            //create a default web.config file if one does not exist, or if it fails to load
            XmlTextReader textReader = new XmlTextReader("web.config");
            XmlDocument document = new XmlDocument();
            document.Load(@"web.config");

            configs.Add("test", document.SelectNodes("/configuration/test").Item(0).InnerText.ToString());
            configs.Add("anotherTest", document.SelectNodes("/configuration/anotherTest").Item(0).InnerText.ToString());
            return configs;
        }
    }
}
