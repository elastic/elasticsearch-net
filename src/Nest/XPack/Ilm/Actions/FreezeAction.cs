// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// This action will freeze the index by calling the Freeze Index API.
	/// Note: Freezing an index will close the index and reopen it within the same API call.
	/// This causes primaries to not be allocated for a short amount of time and
	/// causes the cluster to go red until the primaries are allocated again.
	/// This limitation might be removed in the future.
	/// </summary>
	/// <remarks>
	/// Phases allowed: cold.
	/// </remarks>
	public interface IFreezeLifecycleAction : ILifecycleAction { }

	public class FreezeLifecycleAction : IFreezeLifecycleAction { }

	public class FreezeLifecycleActionDescriptor : DescriptorBase<FreezeLifecycleActionDescriptor, IFreezeLifecycleAction>, IFreezeLifecycleAction { }
}
