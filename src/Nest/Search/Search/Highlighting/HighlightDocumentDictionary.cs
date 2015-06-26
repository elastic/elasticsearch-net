using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
	public class HighlightFieldDictionary : Dictionary<string, HighlightHit>
	{
		public HighlightFieldDictionary(IDictionary<string, HighlightHit> dictionary = null)
		{
			if (dictionary == null)
				return;
			foreach(var kv in dictionary)
			{
				this.Add(kv.Key, kv.Value);
			}
		}
	}

	[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
	public class HighlightDocumentDictionary : Dictionary<string, HighlightFieldDictionary>
	{

	}
}