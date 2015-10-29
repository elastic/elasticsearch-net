using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [JsonConverter(typeof(ReadAsTypeJsonConverter<MultiGetOperationDescriptor<object>>))]
	public interface IMultiGetOperation
	{
		[JsonProperty(PropertyName = "_index")]
		IndexName Index { get; set; }
	
		[JsonProperty(PropertyName = "_type")]
		TypeName Type { get; set; }
		
		[JsonProperty(PropertyName = "_id")]
		Id Id { get; set; }
		
		[JsonProperty(PropertyName = "fields")]
		IList<FieldName> Fields { get; set; }
		
		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }
		
		[JsonProperty(PropertyName = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

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
		IDictionary<FieldName, string> PerFieldAnalyzer { get; set; }

		Type ClrType { get; }

		bool CanBeFlattened { get; }
	}
}