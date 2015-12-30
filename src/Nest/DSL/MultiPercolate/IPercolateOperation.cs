using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPercolateOperation
	{

		string Id { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "track_scores")]
		bool? TrackScores { get; set; }

		[JsonProperty(PropertyName = "sort")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "facets")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		FilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "aggs")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		IRequestParameters GetRequestParameters();
	
	}
}