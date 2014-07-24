using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<IndexFieldMapping>))]
	public interface IIndexFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }
	}

	public class IndexFieldMapping : IIndexFieldMapping
	{
		public bool? Enabled { get; set; }
	}

	public class IndexFieldMappingDescriptor : IIndexFieldMapping
	{
		private IIndexFieldMapping Self { get { return this; } }

		bool? IIndexFieldMapping.Enabled { get; set; }

		public IndexFieldMappingDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
	}
}