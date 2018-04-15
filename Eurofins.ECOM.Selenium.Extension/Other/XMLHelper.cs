using System.Xml;

namespace Eurofins.Selenium.Extension.Other
{
    public static class XMLHelper
    {
        public static void UpdateXmlNodeInnerText(string xmlFilePath, string nodeXPath, string innerText)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(xmlFilePath);

                string nodePath = nodeXPath;
                XmlNode node = document.SelectSingleNode(nodePath);

                node.InnerText = innerText;
                document.Save(xmlFilePath);
            }
            catch
            { throw; }
        }

        public static void UpdateXmlNodeInnerXML(string xmlFilePath, string nodeXPath, string innerXML)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(xmlFilePath);

                string nodePath = nodeXPath;
                XmlNode node = document.SelectSingleNode(nodePath);

                node.InnerXml = innerXML;
                document.Save(xmlFilePath);
            }
            catch
            { throw; }
        }

        public static string ReadXmlNodeInnerText(string xmlFilePath, string nodeXPath)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(xmlFilePath);

                string nodePath = nodeXPath;
                XmlNode node = document.SelectSingleNode(nodePath);

                return node.InnerText;
            }
            catch
            { throw; }
        }
    }
}
