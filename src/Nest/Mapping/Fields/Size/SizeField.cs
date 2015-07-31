using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<SizeField>))]
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

	public class SizeFieldDescriptor : ISizeField
	{
		private ISizeField Self => this;

		bool? ISizeField.Enabled { get; set; }

		bool? ISizeField.Store { get; set; }

		public SizeFieldDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}

		public SizeFieldDescriptor Store(bool store = true)
		{
			Self.Store = store;
			return this;
		}
	}
}