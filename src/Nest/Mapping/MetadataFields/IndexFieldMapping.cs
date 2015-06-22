using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<IndexFieldMapping>))]
	public interface IIndexFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

	}

	public class IndexFieldMapping : IIndexFieldMapping
	{
		public bool? Enabled { get; set; }
		public bool? Store { get; set; }
	}

	public class IndexFieldMappingDescriptor : IIndexFieldMapping
	{
		private IIndexFieldMapping Self { get { return this; } }

		bool? IIndexFieldMapping.Enabled { get; set; }

		bool? IIndexFieldMapping.Store { get; set; }

		public IndexFieldMappingDescriptor Store(bool store = true)
		{
			Self.Store = store;
			return this;
		}

		public IndexFieldMappingDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
	}
}