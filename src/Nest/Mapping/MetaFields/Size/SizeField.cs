using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SizeField>))]
	public interface ISizeField : IFieldMapping
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }
	}

	public class SizeField : ISizeField
	{
		public bool? Enabled { get; set; }
	}

	public class SizeFieldDescriptor 
		: DescriptorBase<SizeFieldDescriptor, ISizeField>, ISizeField
	{
		bool? ISizeField.Enabled { get; set; }

		public SizeFieldDescriptor Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);
	}
}