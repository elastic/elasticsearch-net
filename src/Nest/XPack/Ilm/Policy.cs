using System;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(Policy))]
	public interface IPolicy
	{
		[DataMember(Name = "phases")]
		IPhases Phases { get; set; }
	}

	public class Policy : IPolicy
	{
		public IPhases Phases { get; set; }
	}

	public class PolicyDescriptor : DescriptorBase<PolicyDescriptor, IPolicy>, IPolicy
	{
		IPhases IPolicy.Phases { get; set; }

		public PolicyDescriptor Phases(Func<PhasesDescriptor, IPhases> selector) =>
			Assign(selector, (a, v) => a.Phases = v?.InvokeOrDefault(new PhasesDescriptor()));
	}
}
