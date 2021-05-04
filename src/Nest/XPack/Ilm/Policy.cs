// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
