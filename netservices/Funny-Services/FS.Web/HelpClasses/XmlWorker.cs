using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace FS.Web.HelpClasses
{
    public class XmlWorker
    {
        public string GetInfo(string url, string tag)
        {
            try
            {
                XDocument Inventory = GetXmlDocument(url);
                string stringFromXml = (from c in Inventory.Descendants(tag) select c).First().Value;
                return stringFromXml;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public XDocument GetXmlDocument(string url)
        {
            string XmlString = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader strm = new StreamReader(response.GetResponseStream());
            XmlString = strm.ReadToEnd();
            XDocument XmlDoc = XDocument.Parse(XmlString);
            return XmlDoc;
        }
    }
}