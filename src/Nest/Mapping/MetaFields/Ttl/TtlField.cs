using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TtlField>))]
	[Obsolete("will be replaced with a different implementation in a future version of Elasticsearch")]
	public interface ITtlField : IFieldMapping
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("default")]
		Time Default { get; set; }
	}

	[Obsolete("will be replaced with a different implementation in a future version of Elasticsearch")]
	public class TtlField : ITtlField
	{
		public bool? Enabled { get; set; }
		public Time Default { get; set; }
	}

	[Obsolete("will be replaced with a different implementation in a future version of Elasticsearch")]
	public class TtlFieldDescriptor
		: DescriptorBase<TtlFieldDescriptor, ITtlField>, ITtlField
	{
		bool? ITtlField.Enabled { get; set; }
		Time ITtlField.Default { get; set; }

		public TtlFieldDescriptor Enable(bool enable = true) => Assign(a => a.Enabled = enable);

		public TtlFieldDescriptor Default(Time defaultTtl) => Assign(a => a.Default = defaultTtl);
	}
}
