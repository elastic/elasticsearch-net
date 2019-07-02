using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class TermVectorsResponse : ResponseBase
	{
		/// <summary>
		/// TermVector API returns 200 even if <see cref="Found"/>;
		/// </summary>
		public override bool IsValid => base.IsValid && Found;
		
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="term_vectors")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<Field, TermVector>))]
		public IReadOnlyDictionary<Field, TermVector> TermVectors { get; internal set; } = EmptyReadOnly<Field, TermVector>.Dictionary;

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		[DataMember(Name ="_type")]
		public string Type { get; internal set; }

		[DataMember(Name ="_version")]
		public long Version { get; internal set; }
	}
}
