using Newtonsoft.Json;

namespace Nest
{
	public interface IRolloverLifecycleAction : ILifecycleAction
	{
		[JsonProperty("max_size")]
		long? MaximumSize { get; set; }

		[JsonProperty("max_age")]
		Time MaximumAge { get; set; }

		[JsonProperty("max_docs")]
		long? MaximumDocuments { get; set; }
	}

	public class RolloverLifecycleAction : IRolloverLifecycleAction
	{
		public long? MaximumSize { get; set; }

		public Time MaximumAge { get; set; }

		public long? MaximumDocuments { get; set; }
	}

	public class RolloverLifecycleActionDescriptor : DescriptorBase<RolloverLifecycleActionDescriptor, IRolloverLifecycleAction>, IRolloverLifecycleAction
	{
		long? IRolloverLifecycleAction.MaximumSize { get; set; }

		Time IRolloverLifecycleAction.MaximumAge { get; set; }

		long? IRolloverLifecycleAction.MaximumDocuments { get; set; }

		public RolloverLifecycleActionDescriptor MaximumSize(long? maximumSize) => Assign(maximumSize, (a, v) => a.MaximumSize = maximumSize);

		public RolloverLifecycleActionDescriptor MaximumAge(Time maximumAge) => Assign(maximumAge, (a, v) => a.MaximumAge = maximumAge);

		public RolloverLifecycleActionDescriptor MaximumDocuments(long? maximumDocuments)
			=> Assign(maximumDocuments, (a, v) => a.MaximumDocuments = maximumDocuments);
	}
}
