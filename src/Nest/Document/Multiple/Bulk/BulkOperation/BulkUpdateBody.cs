using Newtonsoft.Json;

namespace Nest
{
	internal class BulkUpdateBody<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		[JsonProperty("doc_as_upsert")]
		public bool? _DocAsUpsert { get; set; }

		[JsonProperty("doc")]
		[JsonConverter(typeof(CollapsedSourceConverter))]
		internal TPartialUpdate _PartialUpdate { get; set; }

		[JsonProperty("script")]
		internal IScript _Script { get; set; }

		[JsonProperty("scripted_upsert")]
		internal bool? _ScriptedUpsert { get; set; }

		[JsonProperty("if_seq_no")]
		internal long? IfSequenceNumber { get; set; }

		[JsonProperty("upsert")]
		[JsonConverter(typeof(CollapsedSourceConverter))]
		internal TDocument _Upsert { get; set; }

		[JsonProperty("if_primary_term")]
		internal long? IfPrimaryTerm { get; set; }

		[JsonProperty("_source")]
		internal Union<bool, ISourceFilter> Source { get; set; }
	}
}
