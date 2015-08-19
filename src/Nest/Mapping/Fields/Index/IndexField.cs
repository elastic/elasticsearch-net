using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndexField>))]
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

	public class IndexFieldDescriptor 
		: DescriptorBase<IndexFieldDescriptor, IIndexField>, IIndexField
	{
		bool? IIndexField.Enabled { get; set; }
		bool? IIndexField.Store { get; set; }

		public IndexFieldDescriptor Store(bool store = true) => Assign(a => a.Store = store);

		public IndexFieldDescriptor Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);
	}
}