/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
