using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Phase>))]
	public interface IPhase
	{
		[JsonProperty("min_age")]
		Time MinimumAge { get; set; }

		[JsonProperty("actions")]
		ILifecycleActions Actions { get; set; }
	}

	public class Phase : IPhase
	{
		public Time MinimumAge { get; set; }

		public ILifecycleActions Actions { get; set; }
	}

	public class PhaseDescriptor : DescriptorBase<PhaseDescriptor, IPhase>, IPhase
	{
		Time IPhase.MinimumAge { get; set; }

		ILifecycleActions IPhase.Actions { get; set; }

		public PhaseDescriptor MinimumAge(string minimumAge) => Assign(minimumAge, (a, v) => a.MinimumAge = v);

		public PhaseDescriptor Actions(Func<LifecycleActionsDescriptor, IPromise<ILifecycleActions>> selector) =>
			Assign(selector, (a, v) => a.Actions = v?.Invoke(new LifecycleActionsDescriptor())?.Value);
	}
}
