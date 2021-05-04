// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot lifecycle configuration
	/// </summary>
	[ReadAs(typeof(SnapshotLifecycleConfig))]
	[InterfaceDataContract]
	public interface ISnapshotLifecycleConfig
	{
		[DataMember(Name = "ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[DataMember(Name = "include_global_state")]
		bool? IncludeGlobalState { get; set; }

		/// <summary>
		/// The indices to snapshot
		/// </summary>
		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(IndicesMultiSyntaxFormatter))]
		Indices Indices { get; set; }
	}

	public class SnapshotLifecycleConfig : ISnapshotLifecycleConfig
	{
		/// <inheritdoc />
		public bool? IgnoreUnavailable { get; set; }

		/// <inheritdoc />
		public bool? IncludeGlobalState { get; set; }

		/// <inheritdoc />
		public Indices Indices { get; set; }
	}

	public class SnapshotLifecycleConfigDescriptor : DescriptorBase<SnapshotLifecycleConfigDescriptor, ISnapshotLifecycleConfig>, ISnapshotLifecycleConfig
	{
		bool? ISnapshotLifecycleConfig.IgnoreUnavailable { get; set; }
		bool? ISnapshotLifecycleConfig.IncludeGlobalState { get; set; }
		Indices ISnapshotLifecycleConfig.Indices { get; set; }

		public SnapshotLifecycleConfigDescriptor Indices(IndexName index) => Indices((Indices)index);

		public SnapshotLifecycleConfigDescriptor Indices<T>() where T : class => Indices(typeof(T));

		public SnapshotLifecycleConfigDescriptor Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		public SnapshotLifecycleConfigDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public SnapshotLifecycleConfigDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);
	}
}
