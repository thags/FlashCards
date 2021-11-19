using System.Xml;

namespace FlashCards.xmlManager
{
    class XmlManager
    {
        public static string ReadConfig(string configItem)
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"web.config");
            return document.SelectNodes($"/configuration/{configItem}").Item(0).InnerText.ToString();
        }
    }
}
