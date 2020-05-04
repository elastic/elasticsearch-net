// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// The Delete Action deletes the index.
	/// </summary>
	/// <remarks>
	/// Phases allowed: delete.
	/// </remarks>
	public interface IDeleteLifecycleAction : ILifecycleAction { }

	public class DeleteLifecycleAction : IDeleteLifecycleAction { }

	public class DeleteLifecycleActionDescriptor : DescriptorBase<DeleteLifecycleActionDescriptor, IDeleteLifecycleAction>, IDeleteLifecycleAction { }
}
