using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IRestoreRequest 
	{
		[JsonProperty("indices")]
		IEnumerable<IndexName> Indices { get; set; }
		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }
		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }
		[JsonProperty("rename_pattern")]
		string RenamePattern { get; set; }
		[JsonProperty("rename_replacement")]
		string RenameReplacement { get; set; }
		[JsonProperty("index_settings")]
		IUpdateIndexSettingsRequest IndexSettings { get; set; }
		[JsonProperty("ignore_index_settings")]
		List<string> IgnoreIndexSettings { get; set; }
	}

	public partial class RestoreRequest 
	{
		public IEnumerable<IndexName> Indices { get; set; }
		
		public bool? IgnoreUnavailable { get; set; }
		
		public bool? IncludeGlobalState { get; set; }
		
		public string RenamePattern { get; set; }
		
		public string RenameReplacement { get; set; }
		public IUpdateIndexSettingsRequest IndexSettings { get; set; }
		public List<string> IgnoreIndexSettings { get; set; }
	}

	[DescriptorFor("SnapshotRestore")]
	public partial class RestoreDescriptor 
	{
		IEnumerable<IndexName> IRestoreRequest.Indices { get; set; }
		bool? IRestoreRequest.IgnoreUnavailable { get; set; }
		bool? IRestoreRequest.IncludeGlobalState { get; set; }
		string IRestoreRequest.RenamePattern { get; set; }
		string IRestoreRequest.RenameReplacement { get; set; }
		IUpdateIndexSettingsRequest IRestoreRequest.IndexSettings { get; set; }
		List<string> IRestoreRequest.IgnoreIndexSettings { get; set; }

		public RestoreDescriptor Index(string index)
		{
			return this.Indices(index);
		}
	
		public RestoreDescriptor Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}

		public RestoreDescriptor Indices(params string[] indices) => Assign(a => a.Indices = indices.Select(s => (IndexName) s));

		public RestoreDescriptor Indices(params Type[] indicesTypes) => Assign(a => a.Indices = indicesTypes.Select(s => (IndexName) s));

		public RestoreDescriptor IgnoreUnavailable(bool ignoreUnavailable = true) => Assign(a => a.IgnoreUnavailable = ignoreUnavailable);

		public RestoreDescriptor IncludeGlobalState(bool includeGlobalState = true) => Assign(a => a.IncludeGlobalState = includeGlobalState);

		public RestoreDescriptor RenamePattern(string renamePattern) => Assign(a => a.RenamePattern = renamePattern);

		public RestoreDescriptor RenameReplacement(string renameReplacement) => Assign(a => a.RenameReplacement = renameReplacement);

		public RestoreDescriptor IndexSettings(Func<UpdateIndexSettingsDescriptor, UpdateIndexSettingsDescriptor> settingsSelector) =>
				Assign(a => a.IndexSettings = settingsSelector?.Invoke(new UpdateIndexSettingsDescriptor()));

		public RestoreDescriptor IgnoreIndexSettings(List<string> ignoreIndexSettings) => Assign(a => a.IgnoreIndexSettings = ignoreIndexSettings);

		public RestoreDescriptor IgnoreIndexSettings(params string[] ignoreIndexSettings) =>Assign(a => a.IgnoreIndexSettings = ignoreIndexSettings.ToListOrNullIfEmpty());

	}
}
