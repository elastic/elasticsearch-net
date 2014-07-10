using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
    public interface ISnapshotRequest : IRepositorySnapshotPath<SnapshotRequestParameters>
    {
        [JsonProperty("indices")]
		IEnumerable<IndexNameMarker> Indices { get; set; }

		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }

		[JsonProperty("include_global_state")]
		bool? IncludeGlobalState { get; set; }

		[JsonProperty("partial")]
		bool? Partial { get; set; }
    }

    internal static class SnapshotPathInfo
    {
        public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<SnapshotRequestParameters> pathInfo)
        {
            pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
        }
    }

    public partial class SnapshotRequest : RepositorySnapshotPathBase<SnapshotRequestParameters>, ISnapshotRequest
    {
	    public SnapshotRequest(string repository, string snapshot) : base(repository, snapshot) { }

	    public IEnumerable<IndexNameMarker> Indices { get; set; }

        public bool? IgnoreUnavailable { get; set; }

        public bool? IncludeGlobalState { get; set; }

        public bool? Partial { get; set; }

        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SnapshotRequestParameters> pathInfo)
        {
            SnapshotPathInfo.Update(settings, pathInfo);
        }
    }


	[DescriptorFor("SnapshotCreate")]
	public partial class SnapshotDescriptor 
        : RepositorySnapshotPathDescriptor<SnapshotDescriptor, SnapshotRequestParameters>, ISnapshotRequest
	{
        private ISnapshotRequest Self { get { return this; } }

        IEnumerable<IndexNameMarker> ISnapshotRequest.Indices { get; set; }
        bool? ISnapshotRequest.IgnoreUnavailable { get; set; }

        bool? ISnapshotRequest.IncludeGlobalState { get; set; }

        bool? ISnapshotRequest.Partial { get; set; }

        string IRepositorySnapshotPath<SnapshotRequestParameters>.Repository { get; set; }

        string IRepositorySnapshotPath<SnapshotRequestParameters>.Snapshot { get; set; }

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
			this.Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return this;
		}

		public SnapshotDescriptor Indices(params Type[] indicesTypes)
		{
			this.Self.Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return this;
		}
		public SnapshotDescriptor IgnoreUnavailable(bool ignoreUnavailable = true)
		{
			this.Self.IgnoreUnavailable = ignoreUnavailable;
			return this;
		}
		public SnapshotDescriptor IncludeGlobalstate(bool includeGlobalState = true)
		{
			this.Self.IncludeGlobalState = includeGlobalState;
			return this;
		}
		public SnapshotDescriptor Partial(bool partial = true)
		{
			this.Self.Partial = partial;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SnapshotRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
		}
    }
}
