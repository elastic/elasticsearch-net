using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TtlField>))]
	public interface ITtlField : IFieldMapping
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("default")]
		Time Default { get; set; }
	}

	public class TtlField : ITtlField
	{
		public bool? Enabled { get; set; }
		public Time Default { get; set; }
	}

	public class TtlFieldDescriptor 
		: DescriptorBase<TtlFieldDescriptor, ITtlField>, ITtlField
	{
		bool? ITtlField.Enabled { get; set; }
		Time ITtlField.Default { get; set; }

		public TtlFieldDescriptor Enable(bool enable = true) => Assign(a => a.Enabled = enable);

		public TtlFieldDescriptor Default(Time defaultTtl) => Assign(a => a.Default = defaultTtl);
	}
}