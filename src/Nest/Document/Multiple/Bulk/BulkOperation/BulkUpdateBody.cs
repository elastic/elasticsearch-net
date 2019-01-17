using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	internal class BulkUpdateBody<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		[DataMember(Name ="doc_as_upsert")]
		public bool? _DocAsUpsert { get; set; }

		[DataMember(Name ="doc")]
		[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		internal TPartialUpdate _PartialUpdate { get; set; }

		[DataMember(Name ="script")]
		internal IScript _Script { get; set; }

		[DataMember(Name ="scripted_upsert")]
		internal bool? _ScriptedUpsert { get; set; }

		[DataMember(Name ="upsert")]
		[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		internal TDocument _Upsert { get; set; }
	}
}
