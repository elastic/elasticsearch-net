using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface ITypeFieldMapping : ISpecialField
	{
		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		NonStringIndexOption? Index { get; set;  }

		[JsonProperty("store")]
		bool? Store { get; set; }
	}

	public class TypeFieldMapping : ITypeFieldMapping
	{
		public NonStringIndexOption? Index { get; set; }
		public bool? Store { get; set; }
	}

	public class TypeFieldMappingDescriptor : ITypeFieldMapping
	{
		private ITypeFieldMapping Self { get { return this; } } 

		NonStringIndexOption? ITypeFieldMapping.Index { get; set; }

		bool? ITypeFieldMapping.Store { get; set; }

		public TypeFieldMappingDescriptor Index(NonStringIndexOption index = NonStringIndexOption.Analyzed)
		{
			Self.Index = index;
			return this;
		}
		public TypeFieldMappingDescriptor Store(bool stored = true)
		{
			Self.Store = stored;
			return this;
		}
    }
}