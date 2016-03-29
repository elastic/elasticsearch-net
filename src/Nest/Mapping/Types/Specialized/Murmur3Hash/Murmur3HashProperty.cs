using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization.OptIn)]
	public interface IMurmur3HashProperty : IProperty
	{
	}

	public class Murmur3HashProperty : PropertyBase, IMurmur3HashProperty
	{
		public Murmur3HashProperty() : base("murmur3") { }
	}

	public class Murmur3HashPropertyDescriptor<T> 
		: PropertyDescriptorBase<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty, T>, IMurmur3HashProperty
		where T : class
	{
		public Murmur3HashPropertyDescriptor() : base("murmur3") { }
	}
}
