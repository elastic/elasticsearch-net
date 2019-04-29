using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPolicy
	{
		[JsonProperty("phases")]
		IPhases Phases { get; set; }
	}

	public class Policy : IPolicy
	{
		public IPhases Phases { get; set; }
	}

	public class PolicyDescriptor : IDescriptor, IPolicy
	{
		IPhases IPolicy.Phases { get; set; }

		private PolicyDescriptor Assign<TValue>(TValue value, Action<IPolicy, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public PolicyDescriptor Phases(Func<PhasesDescriptor, IPhases> selector) =>
			Assign(selector, (a, v) => a.Phases = v?.InvokeOrDefault(new PhasesDescriptor()));
	}
}
