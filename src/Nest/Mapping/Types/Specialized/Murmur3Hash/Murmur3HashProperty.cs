using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IMurmur3HashProperty : IDocValuesProperty { }

	[DebuggerDisplay("{DebugDisplay}")]
	public class Murmur3HashProperty : DocValuesPropertyBase, IMurmur3HashProperty
	{
		public Murmur3HashProperty() : base(FieldType.Murmur3Hash) { }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class Murmur3HashPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty, T>, IMurmur3HashProperty
		where T : class
	{
		public Murmur3HashPropertyDescriptor() : base(FieldType.Murmur3Hash) { }
	}
}
