#region License

// Copyright (c) 2010 Thomas Andre H. Johansen
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion


using System.Collections.Generic;
using System.Xml;
using Wayloop.Highlight.Patterns;


namespace Wayloop.Highlight.Collections
{
    public class PatternCollection : List<Pattern>
    {
        public PatternCollection()
        {
        }


        public PatternCollection(IEnumerable<Pattern> collection) : base(collection)
        {
        }


        public static PatternCollection GetAllPatterns(XmlDocument xmlConfiguration, Definition definition)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlConfiguration.OuterXml);
            var patterns = new PatternCollection();
            var xpath = string.Format("definitions/definition[@name='{0}']/pattern", definition.Name);
            var patternNodes = document.SelectNodes(xpath);
            if (patternNodes != null) {
                foreach (XmlNode node in patternNodes) {
                    Pattern pattern;
                    var innerText = node.Attributes["type"].InnerText;
                    if (innerText.CompareTo("block") == 0) {
                        pattern = new BlockPattern(node);
                    }
                    else if (innerText.CompareTo("markup") == 0) {
                        pattern = new MarkupPattern(node);
                    }
                    else {
                        pattern = new WordPattern(node);
                    }
                    patterns.Add(pattern);
                }
            }

            return patterns;
        }
    }
}