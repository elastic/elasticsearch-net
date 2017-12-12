using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface IDocValuesProperty : ICoreProperty
	{
		[JsonProperty("doc_values")]
		bool? DocValues { get; set; }
	}

	public abstract class DocValuesPropertyBase : CorePropertyBase, IDocValuesProperty
	{
		protected DocValuesPropertyBase(FieldType type) : base(type) { }

		public bool? DocValues { get; set; }
	}
}
