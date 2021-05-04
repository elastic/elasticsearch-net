// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
