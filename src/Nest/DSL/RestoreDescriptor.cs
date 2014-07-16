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
		IEnumerable<IndexNameMarker> Indices { get; set; }
		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }
		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }
		[JsonProperty("rename_pattern")]
		string RenamePattern { get; set; }
		[JsonProperty("rename_replacement")]
		string RenameReplacement { get; set; }
		
	}

	internal static class RestorePathInfo
	{
		public static void Update(ElasticsearchPathInfo<RestoreRequestParameters> pathInfo, IRestoreRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class RestoreRequest : RepositorySnapshotPathBase<RestoreRequestParameters>, IRestoreRequest
	{
		public RestoreRequest(string repository, string snapshot) : base(repository, snapshot) { }

		public IEnumerable<IndexNameMarker> Indices { get; set; }
		
		public bool? IgnoreUnavailable { get; set; }
		
		public bool? IncludeGlobalState { get; set; }
		
		public string RenamePattern { get; set; }
		
		public string RenameReplacement { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RestoreRequestParameters> pathInfo)
		{
			RestorePathInfo.Update(pathInfo, this);
		}

	}

	[DescriptorFor("SnapshotRestore")]
	public partial class RestoreDescriptor : RepositorySnapshotPathDescriptor<RestoreDescriptor, RestoreRequestParameters>, IRestoreRequest
	{
		private IRestoreRequest Self { get { return this; } }

		IEnumerable<IndexNameMarker> IRestoreRequest.Indices { get; set; }
		bool? IRestoreRequest.IgnoreUnavailable { get; set; }
		bool? IRestoreRequest.IncludeGlobalState { get; set; }
		string IRestoreRequest.RenamePattern { get; set; }
		string IRestoreRequest.RenameReplacement { get; set; }
		
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
			Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return this;
		}

		public RestoreDescriptor Indices(params Type[] indicesTypes)
		{
			Self.Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<RestoreRequestParameters> pathInfo)
		{
			RestorePathInfo.Update(pathInfo, this);
		}

	}
}
