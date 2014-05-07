using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("SnapshotRestore")]
	public partial class RestoreDescriptor :
		RepositorySnapshotPathDescriptor<RestoreDescriptor, RestoreRequestParameters>
		, IPathInfo<RestoreRequestParameters>
	{
		[JsonProperty("indices")]
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		[JsonProperty("ignore_unavailable")]
		internal bool? _IgnoreUnavailable { get; set; }
		[JsonProperty("include_global_state")]
		internal bool? _IncludeGlobalState { get; set; }
		[JsonProperty("rename_pattern")]
		internal string _RenamePattern { get; set; }
		[JsonProperty("rename_replacement")]
		internal string _RenameReplacement { get; set; }
		
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
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return this;
		}

		public RestoreDescriptor Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return this;
		}
		public RestoreDescriptor IgnoreUnavailable(bool ignoreUnavailable = true)
		{
			this._IgnoreUnavailable = ignoreUnavailable;
			return this;
		}
		public RestoreDescriptor IncludeGlobalstate(bool includeGlobalState = true)
		{
			this._IncludeGlobalState = includeGlobalState;
			return this;
		}
		public RestoreDescriptor RenamePattern(string renamePattern)
		{
			this._RenamePattern = renamePattern;
			return this;
		}
		public RestoreDescriptor RenameReplacement(string renameReplacement)
		{
			this._RenameReplacement = renameReplacement;
			return this;
		}

		ElasticsearchPathInfo<RestoreRequestParameters> IPathInfo<RestoreRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			
			return pathInfo;
		}

	}
}
