using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SizeField>))]
	public interface ISizeField : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

	}

	public class SizeField : ISizeField
	{
		public bool? Enabled { get; set; }

		public bool? Store { get; set; }
	}

	public class SizeFieldDescriptor 
		: DescriptorBase<SizeFieldDescriptor, ISizeField>, ISizeField
	{
		bool? ISizeField.Enabled { get; set; }
		bool? ISizeField.Store { get; set; }

		public SizeFieldDescriptor Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);

		public SizeFieldDescriptor Store(bool store = true) => Assign(a => a.Store = store);
	}
}