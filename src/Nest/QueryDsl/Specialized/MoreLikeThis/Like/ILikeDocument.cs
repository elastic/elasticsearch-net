using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [JsonConverter(typeof(ReadAsTypeJsonConverter<MultiGetOperationDescriptor<object>>))]
	public interface ILikeDocument
	{
		[JsonProperty(PropertyName = "_index")]
		IndexName Index { get; set; }
	
		[JsonProperty(PropertyName = "_type")]
		TypeName Type { get; set; }
		
		[JsonProperty(PropertyName = "_id")]
		Id Id { get; set; }
		
		[JsonProperty(PropertyName = "fields")]
		Fields Fields { get; set; }
		
		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "doc")]
		object Document { get; set; }

		[JsonProperty(PropertyName = "per_field_analyzer")]
		IDictionary<Field, string> PerFieldAnalyzer { get; set; }

		Type ClrType { get; }

		bool CanBeFlattened { get; }
	}
}