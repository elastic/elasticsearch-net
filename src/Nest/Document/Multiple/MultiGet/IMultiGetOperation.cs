using System;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [JsonConverter(typeof(ReadAsTypeConverter<MultiGetOperationDescriptor<object>>))]
	public interface IMultiGetOperation
	{
		[JsonProperty(PropertyName = "_index")]
		IndexName Index { get; set; }
	
		[JsonProperty(PropertyName = "_type")]
		TypeName Type { get; set; }
		
		[JsonProperty(PropertyName = "_id")]
		string Id { get; set; }
		
		[JsonProperty(PropertyName = "fields")]
		IList<PropertyPath> Fields { get; set; }
		
		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }
		
		[JsonProperty(PropertyName = "_source")]
		ISourceFilter Source { get; set; }

		// Only used for the MLT query for specifying an artificial document.
		// TODO: For 2.0, we should consider decoupling IMultiGetOperation from 
		// MoreLikeThisQuery and have a dedicatd MoreLikeThisDocument object.
		[JsonProperty(PropertyName = "doc")]
		object Document { get; set; }

		// Only used for the MLT query for providing a different analyzer per
		// artificial document field.
		// TODO: For 2.0, we should consider decoupling IMultiGetOperation from 
		// MoreLikeThisQuery and have a dedicatd MoreLikeThisDocument object.
		[JsonProperty(PropertyName = "per_field_analyzer")]
		IDictionary<PropertyPath, string> PerFieldAnalyzer { get; set; }

		Type ClrType { get; }
	}
}