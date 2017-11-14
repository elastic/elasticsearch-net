using Newtonsoft.Json;

namespace Nest
{
	internal class BulkUpdateBody<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		[JsonProperty("doc")]
		[JsonConverter(typeof(CollapsedSourceConverter))]
		public TPartialUpdate _PartialUpdate { get; set; }

		[JsonProperty("upsert")]
		[JsonConverter(typeof(CollapsedSourceConverter))]
		public TDocument _Upsert { get; set; }

		[JsonProperty("doc_as_upsert")]
		public bool? _DocAsUpsert { get; set; }

		[JsonProperty("script")]
		public IScript _Script { get; set; }

		[JsonProperty("scripted_upsert")]
		public bool? _ScriptedUpsert { get; set; }
	}
}
