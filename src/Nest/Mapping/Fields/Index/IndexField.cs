using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<IndexField>))]
	public interface IIndexField : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

	}

	public class IndexField : IIndexField
	{
		public bool? Enabled { get; set; }
		public bool? Store { get; set; }
	}

	public class IndexFieldDescriptor : IIndexField
	{
		private IIndexField Self => this;

		bool? IIndexField.Enabled { get; set; }

		bool? IIndexField.Store { get; set; }

		public IndexFieldDescriptor Store(bool store = true)
		{
			Self.Store = store;
			return this;
		}

		public IndexFieldDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
	}
}