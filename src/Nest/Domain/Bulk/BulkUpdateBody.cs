using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class BulkUpdateBody<T, K> 
		where T : class
		where K : class
	{
		[JsonProperty(PropertyName = "doc")]
		internal K _Document { get; set; }
		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }
		
		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }
		
		[JsonProperty(PropertyName = "upsert")]
		internal object _Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		public bool? _DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "lang")]
		public string _Lang { get; set; }
	}
}