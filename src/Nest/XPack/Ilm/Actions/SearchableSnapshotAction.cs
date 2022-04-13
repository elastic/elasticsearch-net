// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// This action takes a snapshot of the managed index in the configured repository and mounts it as a
	/// searchable snapshot. If the index is part of a data stream, the mounted index replaces the original
	/// index in the stream.
	/// </summary>
	public interface ISearchableSnapshotAction : ILifecycleAction
	{
		/// <summary>
		/// The repository used to store the snapshot.
		/// </summary>
		[DataMember(Name = "snapshot_repository")]
		string SnapshotRepository { get; set; }

		/// <summary>
		/// Force merges the managed index to one segment.
		/// </summary>
		[DataMember(Name = "force_merge_index")]
		bool? ForceMergeIndex { get; set; }
	}

	public class SearchableSnapshotAction : ISearchableSnapshotAction
	{
		/// <inheritdoc />
		public string SnapshotRepository { get; set; }

		/// <inheritdoc />
		public bool? ForceMergeIndex { get; set; }
	}

	public class SearchableSnapshotActionDescriptor : DescriptorBase<SearchableSnapshotActionDescriptor, ISearchableSnapshotAction>, ISearchableSnapshotAction
	{
		/// <inheritdoc cref="ISearchableSnapshotAction.SnapshotRepository" />
		string ISearchableSnapshotAction.SnapshotRepository { get; set; }

		/// <inheritdoc cref="ISearchableSnapshotAction.ForceMergeIndex" />
		bool? ISearchableSnapshotAction.ForceMergeIndex { get; set; }

		/// <inheritdoc cref="ISearchableSnapshotAction.SnapshotRepository" />
		public SearchableSnapshotActionDescriptor SnapshotRepository(string snapshotRespository) =>
			Assign(snapshotRespository, (a, v) => a.SnapshotRepository = snapshotRespository);

		/// <inheritdoc cref="ISearchableSnapshotAction.ForceMergeIndex" />
		public SearchableSnapshotActionDescriptor ForceMergeIndex(bool? forceMergeIndex = true) =>
			Assign(forceMergeIndex, (a, v) => a.ForceMergeIndex = forceMergeIndex);
	}
}
