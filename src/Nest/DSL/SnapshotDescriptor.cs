using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("SnapshotCreate")]
	public partial class SnapshotDescriptor :
		RepositorySnapshotPathDescriptor<SnapshotDescriptor, SnapshotRequestParameters>
		, IPathInfo<SnapshotRequestParameters>
	{
		[JsonProperty("indices")]
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		[JsonProperty("ignore_unavailable")]
		internal bool? _IgnoreUnavailable { get; set; }
		[JsonProperty("include_global_state")]
		internal bool? _IncludeGlobalState { get; set; }
		[JsonProperty("partial")]
		internal bool? _Partial { get; set; }
		
		public SnapshotDescriptor Index(string index)
		{
			return this.Indices(index);
		}
	
		public SnapshotDescriptor Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}
			
		public SnapshotDescriptor Indices(params string[] indices)
		{
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return this;
		}

		public SnapshotDescriptor Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return this;
		}
		public SnapshotDescriptor IgnoreUnavailable(bool ignoreUnavailable = true)
		{
			this._IgnoreUnavailable = ignoreUnavailable;
			return this;
		}
		public SnapshotDescriptor IncludeGlobalstate(bool includeGlobalState = true)
		{
			this._IncludeGlobalState = includeGlobalState;
			return this;
		}
		public SnapshotDescriptor Partial(bool partial = true)
		{
			this._Partial = partial;
			return this;
		}

		ElasticsearchPathInfo<SnapshotRequestParameters> IPathInfo<SnapshotRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
			
			return pathInfo;
		}

	}
}
