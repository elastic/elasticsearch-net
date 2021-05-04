// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The Force Merge Action force merges the index into at most a specific number of segments.
	/// </summary>
	/// <remarks>
	/// Phases allowed: hot, warm.
	///
	/// NOTE: If the action is used in the `hot` phase, the `rollover` action *must* be present.
	/// ILM validates this predicate and will refuse a policy with a forcemerge in the hot phase without a rollover action.
	/// </remarks>
	public interface IForceMergeLifecycleAction : ILifecycleAction
	{
		/// <summary>
		/// The number of segments to merge to. To fully merge the index, set it to 1
		/// </summary>
		[DataMember(Name = "max_num_segments")]
		int? MaximumNumberOfSegments { get; set; }
	}

	public class ForceMergeLifecycleAction : IForceMergeLifecycleAction
	{
		/// <inheritdoc />
		public int? MaximumNumberOfSegments { get; set; }
	}

	public class ForceMergeLifecycleActionDescriptor
		: DescriptorBase<ForceMergeLifecycleActionDescriptor, IForceMergeLifecycleAction>, IForceMergeLifecycleAction
	{
		/// <inheritdoc cref="IForceMergeLifecycleAction.MaximumNumberOfSegments" />
		int? IForceMergeLifecycleAction.MaximumNumberOfSegments { get; set; }

		/// <inheritdoc cref="IForceMergeLifecycleAction.MaximumNumberOfSegments" />
		public ForceMergeLifecycleActionDescriptor MaximumNumberOfSegments(int? maximumNumberOfSegments)
			=> Assign(maximumNumberOfSegments, (a, v) => a.MaximumNumberOfSegments = maximumNumberOfSegments);
	}
}
