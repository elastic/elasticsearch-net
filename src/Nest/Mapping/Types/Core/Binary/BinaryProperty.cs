using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBinaryProperty : IProperty
	{
	}

	public class BinaryProperty : PropertyBase, IBinaryProperty
	{
		public BinaryProperty() : base("binary") { }
	}

	public class BinaryPropertyDescriptor<T> 
		: PropertyDescriptorBase<BinaryPropertyDescriptor<T>, IBinaryProperty, T>, IBinaryProperty
		where T : class
	{
		public BinaryPropertyDescriptor() : base("binary") { }
	}
}