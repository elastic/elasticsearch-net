// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// This action turns a ccr follower index into a regular index. This can be desired when moving follower indices into the
	/// next phase. Also certain actions like shrink and rollover can then be performed safely on follower indices.
	/// </summary>
	public interface IUnfollowLifecycleAction : ILifecycleAction { }

	public class UnfollowLifecycleAction : IUnfollowLifecycleAction { }

	public class UnfollowLifecycleActionDescriptor
		: DescriptorBase<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction>, IUnfollowLifecycleAction { }
}
