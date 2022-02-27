
namespace Library
{
    using Extensions;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class XMLHelper
    {

        public Dictionary<char, string> SpecialLetter = new Dictionary<char, string> {
            { '&', "&amp;" },            { '<', "&lt;" },            { '>', "&gt;" },            { '"', "&quot;" },            { '\'', "&lt;" },
        };
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                if (text.Contains("?>")) text = text.Split(new string[] { "?>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                if (!string.IsNullOrEmpty(text)) this.XML = XDocument.Parse(text);
                if (this.XML != null) this.Json = JsonConvert.SerializeXNode(this.XML);
                if (!string.IsNullOrEmpty(this.Json)) this.Object = JsonConvert.DeserializeObject<ExpandoObject>(this.Json);
            }
        }
        public XDocument XML { get; set; }
        public string Json { get; set; }
        public dynamic Object { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public string PrettyXML
        {
            get
            {
                if (XML == null) return string.Empty;

                var sb = new StringBuilder();
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = @"    ",
                    NewLineChars = Environment.NewLine,
                    NewLineHandling = NewLineHandling.Replace,
                };

                var doc = XML.ToXmlDocument();
                using (var writer = XmlWriter.Create(sb, settings))
                {
                    if (doc.ChildNodes[0] is XmlProcessingInstruction)
                    {
                        doc.RemoveChild(doc.ChildNodes[0]);
                    }

                    doc.Save(writer);
                    return sb.ToString();
                }
            }
        }

        public char Split { get; set; }

        public string this[string key]
        {
            get
            {
                if (this.Data == null) GetAllNode(split: this.Split);
                if (Data.ContainsKey(key)) return this.Data[key];
                else return string.Empty;
            }
        }

        public XMLHelper(string xml = "")
        {
            if (!string.IsNullOrEmpty(xml))
                this.Text = xml;
            this.Split = '|';
        }

        public string GetValue(string node, bool getAll = false)
        {
            if (this.Data == null) GetAllNode(split: this.Split);
            var values = this.Data.Where(e => e.Key.EndsWith(node)).Select(e => e.Value).ToList();
            if (values.Count == 0) return string.Empty;
            else if (values.Count == 1) return values.First();
            else
            {
                if (getAll) return string.Join(this.Split.ToString(), values);
                else return values.First();
            }
        }

        public void SetValue(string node, string value)
        {
            if (this.Data == null) GetAllNode(split: this.Split);
            var keys = this.Data.Where(e => e.Key.EndsWith(node)).Select(e => e.Key).ToList();
            foreach (var key in keys)
            {
                this.Data[key] = value;
            }
        }

        public void GetAllNode(string parent = "", char split = '|', List<string> skips = null)
        {
            this.Split = split;
            var nodes = new Dictionary<string, string>();
            this.Data = GetAllProperties(nodes, this.Object, "", this.Split, skips);
        }

        private Dictionary<string, string> GetAllProperties(Dictionary<string, string> nodes, object node, string parent = "", char split = '/', List<string> skips = null)
        {
            var obj = node as ExpandoObject;
            if (obj != null)
            {
                var keys = obj.Select(a => a.Key).ToList();
                var values = obj.Select(a => a.Value).ToList();

                for (int i = 0; i < keys.Count; i++)
                {
                    var key = parent + split + keys[i];
                    if (skips != null && skips.Contains(key))
                    {
                        XElement element = XML.XPathSelectElement("." + key.Replace(split, '/'));
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(element.ToString());
                        doc.PreserveWhitespace = false;
                        nodes[key] = doc.DocumentElement.InnerXml;
                    }
                    else
                    {
                        var value = values[i];
                        if (value == null) nodes[key] = null;
                        else
                        {
                            var type = value.GetType();
                            if (type == typeof(ExpandoObject))
                            {
                                GetAllProperties(nodes, value, key, split, skips);
                            }
                            else if (type == typeof(List<object>))
                            {
                                var lst = value as List<object>;
                                for (int j = 0; j < lst.Count; j++)
                                {
                                    var child = string.Format("{0}[{1}]", key, j);
                                    GetAllProperties(nodes, lst[j], child, split, skips);
                                }
                            }
                            else
                            {
                                nodes[key] = value.ToString();
                            }
                        }
                    }
                }
            }
            return nodes;
        }

        public XMLHelper(string xml = "", bool removeEmpty = false, bool replaceSpecialLetter = false)
        {
            if (!string.IsNullOrEmpty(xml))
                this.Text = xml;
            if (removeEmpty)
                RemoveEmptyNodes();
            if (replaceSpecialLetter)
                RemoveSpecialChars();
            this.Split = '|';
        }
        private void RemoveEmptyNodes()
        {
            XElement doc = XElement.Parse(Text);
            doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
            this.Text = doc.ToString();
        }
        private void RemoveSpecialChars()
        {
            if (this.Data == null) this.GetAllNode();
            foreach (var node in this.Data)
            {
                var newValue = node.Value;
                foreach (var c in SpecialLetter)
                    newValue = newValue.Replace(c.Key.ToString(), c.Value);
                Text.Replace(node.Value, newValue);
            }
        }
    }
}