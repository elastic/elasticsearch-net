using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ilm.put_lifecycle")]
	public partial interface IPutLifecycleRequest
	{
		[DataMember(Name = "policy")]
		IPolicy Policy { get; set; }
	}

	public partial class PutLifecycleRequest
	{
		public IPolicy Policy { get; set; }
	}

	public partial class PutLifecycleDescriptor
	{
		IPolicy IPutLifecycleRequest.Policy { get; set; }

		public PutLifecycleDescriptor Policy(Func<PolicyDescriptor, IPolicy> selector) =>
			Assign(selector, (a, v) => a.Policy = v?.InvokeOrDefault(new PolicyDescriptor()));
	}
}
