using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Elasticsearch.Net
{
	internal class MapsApiAttribute : Attribute
	{
		public MapsApiAttribute(string restSpecName) : this(restSpecName, null) { }

		public MapsApiAttribute(string restSpecName, string parametersCommaSeparated = null)
		{
			RestSpecName = restSpecName;
			Parameters = new OrderedHashSet();
			if (!string.IsNullOrWhiteSpace(parametersCommaSeparated))
			{
				var args = parametersCommaSeparated.Split(',').Select(s => s.Trim());
				foreach(var a in args) Parameters.Add(a);
			}
		}

		public string RestSpecName { get; }
		public KeyedCollection<string, string> Parameters { get; }

		public class OrderedHashSet : KeyedCollection<string, string>
		{
			protected override string GetKeyForItem(string item) => item;
		}

	}
}
