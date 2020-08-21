// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Restores a snapshot
	/// </summary>
	[MapsApi("snapshot.restore.json")]
	public partial interface IRestoreRequest
	{
		/// <summary>
		/// The index settings to ignore as part of the restore operation
		/// </summary>
		[DataMember(Name ="ignore_index_settings")]
		List<string> IgnoreIndexSettings { get; set; }

		/// <summary>
		/// Whether indices specified that do not exist
		/// should be ignored.
		/// </summary>
		[DataMember(Name ="ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		/// <summary>
		/// Whether to include aliases as part of the restore
		/// </summary>
		[DataMember(Name ="include_aliases")]
		bool? IncludeAliases { get; set; }

		/// <summary>
		/// Whether the cluster global state should be included
		/// </summary>
		[DataMember(Name ="include_global_state")]
		bool? IncludeGlobalState { get; set; }

		/// <summary>
		/// The index settings that should be applied as part of
		/// the restore operation. Some settings cannot be changed
		/// as part of a restore operation, for example, the number
		/// of shards.
		/// </summary>
		[DataMember(Name ="index_settings")]
		IUpdateIndexSettingsRequest IndexSettings { get; set; }

		/// <summary>
		/// The indices to restore
		/// </summary>
		[DataMember(Name ="indices")]
		Indices Indices { get; set; }

		/// <summary>
		/// Allow partial restore for indices that don't have snapshots of all shards available.
		/// <para />
		/// By default, the entire restore operation will fail if one or more indices participating
		/// in the operation don’t have snapshots of all shards available. It can occur if some
		/// shards failed to snapshot for example. It is still possible to restore such indices
		/// by setting <see cref="Partial" /> to <c>true</c>. Only successfully snapshotted shards
		/// will be restored in this case and all missing shards will be recreated empty.
		/// </summary>
		[DataMember(Name ="partial")]
		bool? Partial { get; set; }

		/// <summary>
		/// A pattern to use to rename restored indices. The pattern
		/// can be used to capture parts of the original index name
		/// and used within <see cref="RenameReplacement" />
		/// </summary>
		[DataMember(Name ="rename_pattern")]
		string RenamePattern { get; set; }

		/// <summary>
		/// A replacement to use to rename restored indices. Used
		/// in conjunction with <see cref="RenamePattern" />.
		/// </summary>
		[DataMember(Name ="rename_replacement")]
		string RenameReplacement { get; set; }
	}

	/// <inheritdoc cref="IRestoreRequest" />
	public partial class RestoreRequest
	{
		/// <inheritdoc />
		public List<string> IgnoreIndexSettings { get; set; }

		/// <inheritdoc />
		public bool? IgnoreUnavailable { get; set; }

		/// <inheritdoc />
		public bool? IncludeAliases { get; set; }

		/// <inheritdoc />
		public bool? IncludeGlobalState { get; set; }

		/// <inheritdoc />
		public IUpdateIndexSettingsRequest IndexSettings { get; set; }

		/// <inheritdoc />
		public Indices Indices { get; set; }

		/// <inheritdoc />
		public bool? Partial { get; set; }

		/// <inheritdoc />
		public string RenamePattern { get; set; }

		/// <inheritdoc />
		public string RenameReplacement { get; set; }
	}

	/// <inheritdoc cref="IRestoreRequest" />
	public partial class RestoreDescriptor
	{
		List<string> IRestoreRequest.IgnoreIndexSettings { get; set; }
		bool? IRestoreRequest.IgnoreUnavailable { get; set; }
		bool? IRestoreRequest.IncludeAliases { get; set; }
		bool? IRestoreRequest.IncludeGlobalState { get; set; }
		IUpdateIndexSettingsRequest IRestoreRequest.IndexSettings { get; set; }
		Indices IRestoreRequest.Indices { get; set; }
		bool? IRestoreRequest.Partial { get; set; }
		string IRestoreRequest.RenamePattern { get; set; }
		string IRestoreRequest.RenameReplacement { get; set; }

		/// <inheritdoc cref="IRestoreRequest.Indices" />
		public RestoreDescriptor Index(IndexName index) => Indices(index);

		/// <inheritdoc cref="IRestoreRequest.Indices" />
		public RestoreDescriptor Index<T>() where T : class => Indices(typeof(T));

		/// <inheritdoc cref="IRestoreRequest.Indices" />
		public RestoreDescriptor Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		/// <inheritdoc cref="IRestoreRequest.IgnoreUnavailable" />
		public RestoreDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		/// <inheritdoc cref="IRestoreRequest.IncludeGlobalState" />
		public RestoreDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(includeGlobalState, (a, v) => a.IncludeGlobalState = v);

		/// <inheritdoc cref="IRestoreRequest.RenamePattern" />
		public RestoreDescriptor RenamePattern(string renamePattern) => Assign(renamePattern, (a, v) => a.RenamePattern = v);

		/// <inheritdoc cref="IRestoreRequest.RenameReplacement" />
		public RestoreDescriptor RenameReplacement(string renameReplacement) => Assign(renameReplacement, (a, v) => a.RenameReplacement = v);

		/// <inheritdoc cref="IRestoreRequest.IndexSettings" />
		public RestoreDescriptor IndexSettings(Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> settingsSelector) =>
			Assign(settingsSelector, (a, v) => a.IndexSettings = v?.Invoke(new UpdateIndexSettingsDescriptor()));

		/// <inheritdoc cref="IRestoreRequest.IgnoreIndexSettings" />
		public RestoreDescriptor IgnoreIndexSettings(List<string> ignoreIndexSettings) => Assign(ignoreIndexSettings, (a, v) => a.IgnoreIndexSettings = v);

		/// <inheritdoc cref="IRestoreRequest.IgnoreIndexSettings" />
		public RestoreDescriptor IgnoreIndexSettings(params string[] ignoreIndexSettings) =>
			Assign(ignoreIndexSettings.ToListOrNullIfEmpty(), (a, v) => a.IgnoreIndexSettings = v);

		/// <inheritdoc cref="IRestoreRequest.IncludeAliases" />
		public RestoreDescriptor IncludeAliases(bool? includeAliases = true) => Assign(includeAliases, (a, v) => a.IncludeAliases = v);

		/// <inheritdoc cref="IRestoreRequest.Partial" />
		public RestoreDescriptor Partial(bool? partial = true) => Assign(partial, (a, v) => a.Partial = v);
	}
}
