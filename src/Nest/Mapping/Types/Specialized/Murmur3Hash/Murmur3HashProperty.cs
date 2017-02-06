using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization.OptIn)]
	public interface IMurmur3HashProperty : IDocValuesProperty
	{
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class Murmur3HashProperty : DocValuesPropertyBase, IMurmur3HashProperty
	{
		public Murmur3HashProperty() : base("murmur3") { }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class Murmur3HashPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty, T>, IMurmur3HashProperty
		where T : class
	{
		public Murmur3HashPropertyDescriptor() : base("murmur3") { }
	}
}
