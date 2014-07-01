using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class BulkUpdateBody<TDocument, TPartialUpdate> 
		where TDocument : class
		where TPartialUpdate : class
	{
		[JsonProperty(PropertyName = "doc")]
		internal TPartialUpdate _PartialUpdate { get; set; }

		[JsonProperty(PropertyName = "upsert")]
		internal TDocument _Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		public bool? _DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }
		
		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }
		
		[JsonProperty(PropertyName = "lang")]
		public string _Lang { get; set; }
	}
}