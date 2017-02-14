using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBinaryProperty : IDocValuesProperty
	{
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BinaryProperty : DocValuesPropertyBase, IBinaryProperty
	{
		public BinaryProperty() : base(FieldType.Binary) { }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BinaryPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<BinaryPropertyDescriptor<T>, IBinaryProperty, T>, IBinaryProperty
		where T : class
	{
		public BinaryPropertyDescriptor() : base(FieldType.Binary) { }
	}
}
