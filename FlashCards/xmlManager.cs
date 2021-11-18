using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlashCards.xmlManager
{
    class XmlManager
    {
        public static string ReadConfig(string configItem)
        {
            //add a try/catch, if the document doesnt exist or it got messed up this may cause the program to crash
            //create a default web.config file if one does not exist, or if it fails to load
            XmlDocument document = new XmlDocument();
            document.Load(@"web.config");
            return document.SelectNodes($"/configuration/{configItem}").Item(0).InnerText.ToString();
        }
    }
}
