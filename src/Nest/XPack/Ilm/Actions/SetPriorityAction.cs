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

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// This action sets the index priority on the index as soon as the policy enters the hot, warm, or cold phase. Indices
	/// with a higher priority will be recovered before indices with lower priorities following a node restart. Generally,
	/// indexes in the hot phase should have the highest value and indexes in the cold phase should have the lowest values. For
	/// example: 100 for the hot phase, 50 for the warm phase, and 0 for the cold phase. Indicies that don’t set this value
	/// have an implicit default priority of 1.
	/// </summary>
	/// <remarks>
	/// Phases allowed: hot, warm, cold.
	/// </remarks>
	public interface ISetPriorityLifecycleAction : ILifecycleAction
	{
		/// <summary>
		/// The priority for the index. Must be 0 or greater. The value may also be set to null to remove the priority.
		/// </summary>
		[DataMember(Name = "priority")]
		int? Priority { get; set; }
	}

	public class SetPriorityLifecycleAction : ISetPriorityLifecycleAction
	{
		/// <inheritdoc />
		public int? Priority { get; set; }
	}

	public class SetPriorityLifecycleActionDescriptor
		: DescriptorBase<SetPriorityLifecycleActionDescriptor, ISetPriorityLifecycleAction>, ISetPriorityLifecycleAction
	{
		/// <inheritdoc cref="ISetPriorityLifecycleAction.Priority" />
		int? ISetPriorityLifecycleAction.Priority { get; set; }

		/// <inheritdoc cref="ISetPriorityLifecycleAction.Priority" />
		public SetPriorityLifecycleActionDescriptor Priority(int? priority) => Assign(priority, (a, v) => a.Priority = priority);
	}
}
