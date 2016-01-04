using System;
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
		Fields Fields { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		Type ClrType { get; }

		bool CanBeFlattened { get; }
	}
}