using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRestoreRequest : IRepositorySnapshotPath<RestoreRequestParameters>
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
		IUpdateSettingsRequest IndexSettings { get; set; }
		[JsonProperty("ignore_index_settings")]
		List<string> IgnoreIndexSettings { get; set; }
	}

	public partial class RestoreRequest : RepositorySnapshotPathBase<RestoreRequestParameters>, IRestoreRequest
	{
		public RestoreRequest(string repository, string snapshot) : base(repository, snapshot) { }

		public IEnumerable<IndexName> Indices { get; set; }
		
		public bool? IgnoreUnavailable { get; set; }
		
		public bool? IncludeGlobalState { get; set; }
		
		public string RenamePattern { get; set; }
		
		public string RenameReplacement { get; set; }
		public IUpdateSettingsRequest IndexSettings { get; set; }
		public List<string> IgnoreIndexSettings { get; set; }
	}

	[DescriptorFor("SnapshotRestore")]
	public partial class RestoreDescriptor : RepositorySnapshotPathDescriptor<RestoreDescriptor, RestoreRequestParameters>, IRestoreRequest
	{
		private IRestoreRequest Self => this;

		IEnumerable<IndexName> IRestoreRequest.Indices { get; set; }
		bool? IRestoreRequest.IgnoreUnavailable { get; set; }
		bool? IRestoreRequest.IncludeGlobalState { get; set; }
		string IRestoreRequest.RenamePattern { get; set; }
		string IRestoreRequest.RenameReplacement { get; set; }
		IUpdateSettingsRequest IRestoreRequest.IndexSettings { get; set; }
		List<string> IRestoreRequest.IgnoreIndexSettings { get; set; }

		public RestoreDescriptor Index(string index)
		{
			return this.Indices(index);
		}
	
		public RestoreDescriptor Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}
			
		public RestoreDescriptor Indices(params string[] indices)
		{
			Self.Indices = indices.Select(s=>(IndexName)s);
			return this;
		}

		public RestoreDescriptor Indices(params Type[] indicesTypes)
		{
			Self.Indices = indicesTypes.Select(s=>(IndexName)s);
			return this;
		}
		public RestoreDescriptor IgnoreUnavailable(bool ignoreUnavailable = true)
		{
			Self.IgnoreUnavailable = ignoreUnavailable;
			return this;
		}
		public RestoreDescriptor IncludeGlobalState(bool includeGlobalState = true)
		{
			Self.IncludeGlobalState = includeGlobalState;
			return this;
		}
		public RestoreDescriptor RenamePattern(string renamePattern)
		{
			Self.RenamePattern = renamePattern;
			return this;
		}
		public RestoreDescriptor RenameReplacement(string renameReplacement)
		{
			Self.RenameReplacement = renameReplacement;
			return this;
		}

		public RestoreDescriptor IndexSettings(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settings");
			Self.IndexSettings = settingsSelector(new UpdateSettingsDescriptor());
			return this;
		}

		public RestoreDescriptor IgnoreIndexSettings(List<string> ignoreIndexSettings)
		{
			ignoreIndexSettings.ThrowIfNull("ignoreIndexSettings");
			Self.IgnoreIndexSettings = ignoreIndexSettings;
			return this;
		}

		public RestoreDescriptor IgnoreIndexSettings(params string[] ignoreIndexSettings)
		{
			ignoreIndexSettings.ThrowIfNull("ignoreIndexSettings");
			this.IgnoreIndexSettings(ignoreIndexSettings.ToList());
			return this;
		}

	}
}
