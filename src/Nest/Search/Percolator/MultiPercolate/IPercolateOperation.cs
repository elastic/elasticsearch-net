using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
	public interface IPercolateOperation
	{
		[JsonProperty(PropertyName = "aggs")]
		AggregationDictionary Aggregations { get; set; }

		[JsonProperty(PropertyName = "filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlight Highlight { get; set; }

		[JsonIgnore]
		string MultiPercolateName { get; }

		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "sort")]
		IList<ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "track_scores")]
		bool? TrackScores { get; set; }

		IRequestParameters GetRequestParameters();
	}
}
