// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
