using System.Collections.Generic;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, HighlightHit>))]
	public class HighlightFieldDictionary : Dictionary<string, HighlightHit>
	{
		public HighlightFieldDictionary(IDictionary<string, HighlightHit> dictionary = null)
		{
			if (dictionary == null) return;

			foreach (var kv in dictionary) Add(kv.Key, kv.Value);
		}
	}
}
