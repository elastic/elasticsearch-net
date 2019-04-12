using Newtonsoft.Json;

namespace Nest
{
	public interface IRolloverAction : ILifecycleAction
	{
		[JsonProperty("max_size")]
		long? MaximumSize { get; set; }

		[JsonProperty("max_age")]
		Time MaximumAge { get; set; }

		[JsonProperty("max_docs")]
		long? MaximumDocuments { get; set; }
	}

	public class RolloverAction : LifecycleActionBase, IRolloverAction
	{
		public RolloverAction() : base("rollover"){ }

		public long? MaximumSize { get; set; }

		public Time MaximumAge { get; set; }

		public long? MaximumDocuments { get; set; }
	}

	public class RolloverActionDescriptor : LifecycleActionDescriptorBase<RolloverActionDescriptor, IRolloverAction>, IRolloverAction
	{
		public RolloverActionDescriptor() : base("rollover") { }

		long? IRolloverAction.MaximumSize { get; set; }

		Time IRolloverAction.MaximumAge { get; set; }

		long? IRolloverAction.MaximumDocuments { get; set; }

		public RolloverActionDescriptor MaximumSize(long? maximumSize) => Assign(maximumSize, (a, v) => a.MaximumSize = maximumSize);

		public RolloverActionDescriptor MaximumAge(Time maximumAge) => Assign(maximumAge, (a, v) => a.MaximumAge = maximumAge);

		public RolloverActionDescriptor MaximumDocuments(long? maximumDocuments)
			=> Assign(maximumDocuments, (a, v) => a.MaximumDocuments = maximumDocuments);
	}
}
