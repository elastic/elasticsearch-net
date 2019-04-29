using System;
using System.Collections.Generic;
using Nest;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IIlmPutLifecycleRequest
	{
		[JsonProperty("policy")]
		IPolicy Policy { get; set; }
	}

	public partial class IlmPutLifecycleRequest
	{
		public IPolicy Policy { get; set; }
	}

	[DescriptorFor("IlmPutLifecycle")]
	public partial class IlmPutLifecycleDescriptor
	{
		IPolicy IIlmPutLifecycleRequest.Policy { get; set; }

		public IlmPutLifecycleDescriptor Policy(Func<PolicyDescriptor, IPolicy> selector) =>
			Assign(selector, (a, v) => a.Policy = v?.InvokeOrDefault(new PolicyDescriptor()));
	}
}
