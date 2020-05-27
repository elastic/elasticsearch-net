using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class PreviewTransformResponse<TTransform> : ResponseBase
	{
		/// <summary>
		/// A preview of documents produced by the transform
		/// </summary>
		[DataMember(Name = "preview")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public IReadOnlyCollection<TTransform> Preview { get; internal set; } = EmptyReadOnly<TTransform>.Collection;

		/// <summary>
		/// The generated destination index.
		/// </summary>
		[DataMember(Name = "generated_dest_index")]
		public IIndexState GeneratedDestinationIndex { get; internal set; }
	}
}
