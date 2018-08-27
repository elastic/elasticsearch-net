using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Restores a snapshot
	/// </summary>
	public partial interface IRestoreRequest
	{
		/// <summary>
		/// The indices to restore
		/// </summary>
		[JsonProperty("indices")]
		Indices Indices { get; set; }

		/// <summary>
		/// Whether indices specified that do not exist
		/// should be ignored.
		/// </summary>
		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		/// <summary>
		/// Whether the cluster global state should be included
		/// </summary>
		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }

		/// <summary>
		/// A pattern to use to rename restored indices. The pattern
		/// can be used to capture parts of the original index name
		/// and used within <see cref="RenameReplacement"/>
		/// </summary>
		[JsonProperty("rename_pattern")]
		string RenamePattern { get; set; }

		/// <summary>
		/// A replacement to use to rename restored indices. Used
		/// in conjunction with <see cref="RenamePattern"/>.
		/// </summary>
		[JsonProperty("rename_replacement")]
		string RenameReplacement { get; set; }

		/// <summary>
		/// The index settings that should be applied as part of
		/// the restore operation. Some settings cannot be changed
		/// as part of a restore operation, for example, the number
		/// of shards.
		/// </summary>
		[JsonProperty("index_settings")]
		IUpdateIndexSettingsRequest IndexSettings { get; set; }

		/// <summary>
		/// The index settings to ignore as part of the restore operation
		/// </summary>
		[JsonProperty("ignore_index_settings")]
		List<string> IgnoreIndexSettings { get; set; }

		/// <summary>
		/// Whether to include aliases as part of the restore
		/// </summary>
		[JsonProperty("include_aliases")]
		bool? IncludeAliases { get; set; }

		/// <summary>
		/// Allow partial restore for indices that don't have snapshots of all shards available.
		/// <para />
		/// By default, the entire restore operation will fail if one or more indices participating
		/// in the operation don’t have snapshots of all shards available. It can occur if some
		/// shards failed to snapshot for example. It is still possible to restore such indices
		/// by setting <see cref="Partial"/> to <c>true</c>. Only successfully snapshotted shards
		/// will be restored in this case and all missing shards will be recreated empty.
		/// </summary>
		[JsonProperty("partial")]
		bool? Partial { get; set; }
 	}

	/// <inheritdoc cref="IRestoreRequest"/>
	public partial class RestoreRequest
	{
		/// <inheritdoc />
		public Indices Indices { get; set; }

		/// <inheritdoc />
		public bool? IgnoreUnavailable { get; set; }

		/// <inheritdoc />
		public bool? IncludeGlobalState { get; set; }

		/// <inheritdoc />
		public string RenamePattern { get; set; }

		/// <inheritdoc />
		public string RenameReplacement { get; set; }

		/// <inheritdoc />
		public IUpdateIndexSettingsRequest IndexSettings { get; set; }

		/// <inheritdoc />
		public List<string> IgnoreIndexSettings { get; set; }

		/// <inheritdoc />
		public bool? IncludeAliases { get; set; }

		/// <inheritdoc />
		public bool? Partial { get; set; }
	}

	/// <inheritdoc cref="IRestoreRequest"/>
	[DescriptorFor("SnapshotRestore")]
	public partial class RestoreDescriptor
	{
		Indices IRestoreRequest.Indices { get; set; }
		bool? IRestoreRequest.IgnoreUnavailable { get; set; }
		bool? IRestoreRequest.IncludeGlobalState { get; set; }
		string IRestoreRequest.RenamePattern { get; set; }
		string IRestoreRequest.RenameReplacement { get; set; }
		IUpdateIndexSettingsRequest IRestoreRequest.IndexSettings { get; set; }
		List<string> IRestoreRequest.IgnoreIndexSettings { get; set; }
		bool? IRestoreRequest.IncludeAliases { get; set; }
		bool? IRestoreRequest.Partial { get; set; }

		/// <inheritdoc cref="IRestoreRequest.Indices"/>
		public RestoreDescriptor Index(IndexName index) => this.Indices(index);

		/// <inheritdoc cref="IRestoreRequest.Indices"/>
		public RestoreDescriptor Index<T>() where T : class => this.Indices(typeof(T));

		/// <inheritdoc cref="IRestoreRequest.Indices"/>
		public RestoreDescriptor Indices(Indices indices) => Assign(a => a.Indices = indices);

		/// <inheritdoc cref="IRestoreRequest.IgnoreUnavailable"/>
		public RestoreDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Assign(a => a.IgnoreUnavailable = ignoreUnavailable);

		/// <inheritdoc cref="IRestoreRequest.IncludeGlobalState"/>
		public RestoreDescriptor IncludeGlobalState(bool? includeGlobalState = true) => Assign(a => a.IncludeGlobalState = includeGlobalState);

		/// <inheritdoc cref="IRestoreRequest.RenamePattern"/>
		public RestoreDescriptor RenamePattern(string renamePattern) => Assign(a => a.RenamePattern = renamePattern);

		/// <inheritdoc cref="IRestoreRequest.RenameReplacement"/>
		public RestoreDescriptor RenameReplacement(string renameReplacement) => Assign(a => a.RenameReplacement = renameReplacement);

		/// <inheritdoc cref="IRestoreRequest.IndexSettings"/>
		public RestoreDescriptor IndexSettings(Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> settingsSelector) =>
				Assign(a => a.IndexSettings = settingsSelector?.Invoke(new UpdateIndexSettingsDescriptor()));

		/// <inheritdoc cref="IRestoreRequest.IgnoreIndexSettings"/>
		public RestoreDescriptor IgnoreIndexSettings(List<string> ignoreIndexSettings) => Assign(a => a.IgnoreIndexSettings = ignoreIndexSettings);

		/// <inheritdoc cref="IRestoreRequest.IgnoreIndexSettings"/>
		public RestoreDescriptor IgnoreIndexSettings(params string[] ignoreIndexSettings) => Assign(a => a.IgnoreIndexSettings = ignoreIndexSettings.ToListOrNullIfEmpty());

		/// <inheritdoc cref="IRestoreRequest.IncludeAliases"/>
		public RestoreDescriptor IncludeAliases(bool? includeAliases = true) => Assign(a => a.IncludeAliases = includeAliases);

		/// <inheritdoc cref="IRestoreRequest.Partial"/>
		public RestoreDescriptor Partial(bool? partial = true) => Assign(a => a.Partial = partial);
	}
}
