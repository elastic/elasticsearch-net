// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(Phase))]
	public interface IPhase
	{
		[DataMember(Name = "actions")]
		ILifecycleActions Actions { get; set; }

		[DataMember(Name = "min_age")]
		Time MinimumAge { get; set; }
	}

	public class Phase : IPhase
	{
		public ILifecycleActions Actions { get; set; }
		public Time MinimumAge { get; set; }
	}

	public class PhaseDescriptor : DescriptorBase<PhaseDescriptor, IPhase>, IPhase
	{
		ILifecycleActions IPhase.Actions { get; set; }
		Time IPhase.MinimumAge { get; set; }

		public PhaseDescriptor MinimumAge(string minimumAge) => Assign(minimumAge, (a, v) => a.MinimumAge = v);

		public PhaseDescriptor Actions(Func<LifecycleActionsDescriptor, IPromise<ILifecycleActions>> selector) =>
			Assign(selector, (a, v) => a.Actions = v?.Invoke(new LifecycleActionsDescriptor())?.Value);
	}
}
