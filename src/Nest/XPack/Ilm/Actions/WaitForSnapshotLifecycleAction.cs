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
	/// The Wait For Snapshot Action waits for defined SLM policy to be executed to ensure that snapshot of index exists before deletion.
	/// <para></para>
	/// Available in Elasticsearch 7.6.0+
	/// </summary>
	public interface IWaitForSnapshotLifecycleAction : ILifecycleAction
	{
		/// <summary>
		/// The Snapshot Lifecycle Management (SLM) policy name that this action should wait for
		/// </summary>
		[DataMember(Name = "policy")]
		string Policy { get; set; }
	}

	/// <inheritdoc />
	public class WaitForSnapshotLifecycleAction : IWaitForSnapshotLifecycleAction
	{
		/// <inheritdoc />
		public string Policy { get; set; }
	}

	/// <inheritdoc cref="IWaitForSnapshotLifecycleAction"/>
	public class WaitForSnapshotLifecycleActionDescriptor
		: DescriptorBase<WaitForSnapshotLifecycleActionDescriptor, IWaitForSnapshotLifecycleAction>, IWaitForSnapshotLifecycleAction
	{
		string IWaitForSnapshotLifecycleAction.Policy { get; set; }

		/// <inheritdoc cref="IWaitForSnapshotLifecycleAction.Policy"/>
		public WaitForSnapshotLifecycleActionDescriptor Policy(string policy) => Assign(policy, (a, v) => a.Policy = policy);
	}
}
