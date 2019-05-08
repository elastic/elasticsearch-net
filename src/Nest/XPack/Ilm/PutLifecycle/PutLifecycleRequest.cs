using System;
using System.Collections.Generic;
using Nest;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutLifecycleRequest
	{
		[JsonProperty("policy")]
		IPolicy Policy { get; set; }
	}

	public partial class PutLifecycleRequest
	{
		public IPolicy Policy { get; set; }
	}

	[DescriptorFor("XpackIlmPutLifecycle")]
	public partial class PutLifecycleDescriptor
	{
		IPolicy IPutLifecycleRequest.Policy { get; set; }

		public PutLifecycleDescriptor Policy(Func<PolicyDescriptor, IPolicy> selector) =>
			Assign(selector, (a, v) => a.Policy = v?.InvokeOrDefault(new PolicyDescriptor()));
	}
}
