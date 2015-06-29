using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<SizeFieldMapping>))]
	public interface ISizeFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

	}

	public class SizeFieldMapping : ISizeFieldMapping
	{
		public bool? Enabled { get; set; }

		public bool? Store { get; set; }
	}

	public class SizeFieldMappingDescriptor : ISizeFieldMapping
	{
		private ISizeFieldMapping Self => this;

		bool? ISizeFieldMapping.Enabled { get; set; }

		bool? ISizeFieldMapping.Store { get; set; }

		public SizeFieldMappingDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}

		public SizeFieldMappingDescriptor Store(bool store = true)
		{
			Self.Store = store;
			return this;
		}

	}
}