using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	internal class BulkUpdateBody<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		[DataMember(Name ="doc_as_upsert")]
		public bool? DocAsUpsert { get; set; }

		[DataMember(Name ="doc")]
		[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		internal TPartialUpdate PartialUpdate { get; set; }

		[DataMember(Name ="script")]
		internal IScript Script { get; set; }

		[DataMember(Name ="scripted_upsert")]
		internal bool? ScriptedUpsert { get; set; }

		[DataMember(Name ="upsert")]
		[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		internal TDocument Upsert { get; set; }
	}
}
