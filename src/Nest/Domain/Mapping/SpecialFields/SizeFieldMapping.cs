using Newtonsoft.Json;

namespace Nest
{
	public interface ISizeFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }
	}

	public class SizeFieldMapping : ISizeFieldMapping
	{
		public bool? Enabled { get; set; }
	}

	public class SizeFieldMappingDescriptor : ISizeFieldMapping
	{
		private ISizeFieldMapping Self { get { return this; } }

		bool? ISizeFieldMapping.Enabled { get; set; }

		public SizeFieldMappingDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
	}
}