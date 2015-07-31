using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<TypeField>))]
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

	public class TypeFieldDescriptor : ITypeField
	{
		private ITypeField Self => this; 

		NonStringIndexOption? ITypeField.Index { get; set; }

		bool? ITypeField.Store { get; set; }

		public TypeFieldDescriptor Index(NonStringIndexOption index = NonStringIndexOption.No)
		{
			Self.Index = index;
			return this;
		}
		public TypeFieldDescriptor Store(bool stored = true)
		{
			Self.Store = stored;
			return this;
		}
    }
}