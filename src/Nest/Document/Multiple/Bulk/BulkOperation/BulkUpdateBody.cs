using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	internal class BulkUpdateBody<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		[JsonProperty("doc")]
		internal TPartialUpdate _PartialUpdate { get; set; }

		[JsonProperty("upsert")]
		internal TDocument _Upsert { get; set; }

		[JsonProperty("doc_as_upsert")]
		public bool? _DocAsUpsert { get; set; }

		[JsonProperty("script")]
		internal IScript _Script { get; set; }
	}
}
