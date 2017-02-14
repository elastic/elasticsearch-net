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
		[Obsolete("Please use overload taking FieldType")]
		protected DocValuesPropertyBase(TypeName typeName) : base(typeName) { }

#pragma warning disable 618
		protected DocValuesPropertyBase(FieldType type) : this(type.GetStringValue()) { }
#pragma warning restore 618

		public bool? DocValues { get; set; }
	}
}
