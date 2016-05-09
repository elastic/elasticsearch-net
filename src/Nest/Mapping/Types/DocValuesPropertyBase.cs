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
		protected DocValuesPropertyBase(TypeName typeName) : base(typeName)
		{
		}

		public bool? DocValues { get; set; }
	}
}
