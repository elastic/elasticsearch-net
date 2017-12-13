#region License
//MIT License
//
//Copyright (c) 2017 Dave Glick
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DocGenerator.Buildalyzer
{
	internal static class XDocumentExtensions
	{
		public static IEnumerable<XElement> GetDescendants(this XDocument document, string name) =>
			document.Descendants().Where(x => string.Equals(x.Name.LocalName, name, StringComparison.OrdinalIgnoreCase));

		public static IEnumerable<XElement> GetDescendants(this XElement element, string name) =>
			element.Descendants().Where(x => string.Equals(x.Name.LocalName, name, StringComparison.OrdinalIgnoreCase));

		public static string GetAttributeValue(this XElement element, string name) =>
			element.Attributes().FirstOrDefault(x => string.Equals(x.Name.LocalName, name, StringComparison.OrdinalIgnoreCase))?.Value;

		// Adds a child element with the same namespace as the parent
		public static void AddChildElement(this XElement element, string name, string value) =>
			element.Add(new XElement(XName.Get(name, element.Name.NamespaceName), value));
	}
}
