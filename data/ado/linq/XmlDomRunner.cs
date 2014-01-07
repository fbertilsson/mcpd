using System.Globalization;
using System.Xml.Linq;
using System.Linq;

namespace linq
{
    internal class XmlDomRunner
    {
        public string GetAllBarAttributes(string xmlString)
        {
            //const string xmlString = @"<?xml version='1.0'?><foo><bar a='a1'/><bar a='a2'/></foo>";

            var doc = XDocument.Parse(xmlString);
            var allBarAttributes = doc.Descendants("bar").Attributes().Aggregate(string.Empty, 
                (first, a) => string.Format("{0}{1}{2}", first, string.IsNullOrEmpty(first) ? string.Empty : ", ", a.Value));
            return allBarAttributes;
        }

        public string SumBarAttributes(string xmlString)
        {
            var doc = XDocument.Parse(xmlString);
            var sumBar = doc.Descendants("bar").Attributes().Sum(x => int.Parse(x.Value));
            return sumBar.ToString(CultureInfo.InvariantCulture);
        }
    }
}