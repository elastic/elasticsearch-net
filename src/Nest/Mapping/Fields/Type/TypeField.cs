using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TypeField>))]
	public interface ITypeField : ISpecialField
	{
		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		NonStringIndexOption? Index { get; set;  }

		[JsonProperty("store")]
		bool? Store { get; set; }
	}

	public class TypeField : ITypeField
	{
		public NonStringIndexOption? Index { get; set; }

		public bool? Store { get; set; }
	}

	public class TypeFieldDescriptor 
		: DescriptorBase<TypeFieldDescriptor, ITypeField>, ITypeField
	{
		NonStringIndexOption? ITypeField.Index { get; set; }

		bool? ITypeField.Store { get; set; }

		public TypeFieldDescriptor Index(NonStringIndexOption index = NonStringIndexOption.No) => Assign(a => a.Index = index);

		public TypeFieldDescriptor Store(bool store = true) => Assign(a => a.Store = store);
    }
}