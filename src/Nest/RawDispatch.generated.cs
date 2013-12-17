using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Generated File Please Do Not Edit Manually


namespace Nest
{
	///<summary>
	///Raw operations with elasticsearch
	///</summary>
	internal partial class RawDispatch
	{
	
		
		internal ConnectionStatus BulkDispatch(ElasticSearchPathInfo<BulkQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_bulk
					if (body != null)
						return this.Raw.BulkPost(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					///{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPut(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPut(pathInfo.Index,body,u => pathInfo.QueryString);
					///_bulk
					if (body != null)
						return this.Raw.BulkPut(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> BulkDispatchAsync(ElasticSearchPathInfo<BulkQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_bulk
					if (body != null)
						return this.Raw.BulkPostAsync(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					///{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPutAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPutAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_bulk
					if (body != null)
						return this.Raw.BulkPutAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ClearScrollDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ClearScrollDelete(pathInfo.ScrollId,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClearScrollDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ClearScrollDeleteAsync(pathInfo.ScrollId,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterGetSettingsDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/settings
					return this.Raw.ClusterGetSettings(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterGetSettingsDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/settings
					return this.Raw.ClusterGetSettingsAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterHealthDispatch(ElasticSearchPathInfo<ClusterHealthQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/health/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.ClusterHealthGet(pathInfo.Index,u => pathInfo.QueryString);
					///_cluster/health
					return this.Raw.ClusterHealthGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterHealthDispatchAsync(ElasticSearchPathInfo<ClusterHealthQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/health/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.ClusterHealthGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_cluster/health
					return this.Raw.ClusterHealthGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterNodeHotThreadsDispatch(ElasticSearchPathInfo<ClusterNodeHotThreadsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeHotThreadsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeHotThreadsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_cluster/nodes/hotthreads
					return this.Raw.ClusterNodeHotThreadsGet(u => pathInfo.QueryString);
					///_nodes/hotthreads
					return this.Raw.ClusterNodeHotThreadsGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeHotThreadsDispatchAsync(ElasticSearchPathInfo<ClusterNodeHotThreadsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeHotThreadsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeHotThreadsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_cluster/nodes/hotthreads
					return this.Raw.ClusterNodeHotThreadsGetAsync(u => pathInfo.QueryString);
					///_nodes/hotthreads
					return this.Raw.ClusterNodeHotThreadsGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterNodeInfoDispatch(ElasticSearchPathInfo<ClusterNodeInfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/settings
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/os
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/process
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/jvm
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/thread_pool
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/network
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/transport
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/http
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/plugin
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_cluster/nodes
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/settings
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/os
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/process
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/jvm
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/thread_pool
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/network
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/transport
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/http
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);
					///_nodes/plugin
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeInfoDispatchAsync(ElasticSearchPathInfo<ClusterNodeInfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/settings
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/os
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/process
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/jvm
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/thread_pool
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/network
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/transport
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/http
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/plugin
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_cluster/nodes
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/settings
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/os
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/process
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/jvm
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/thread_pool
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/network
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/transport
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/http
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);
					///_nodes/plugin
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterNodeShutdownDispatch(ElasticSearchPathInfo<ClusterNodeShutdownQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///_cluster/nodes/{node_id}/_shutdown
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeShutdownPost(pathInfo.NodeId,u => pathInfo.QueryString);
					///_shutdown
					return this.Raw.ClusterNodeShutdownPost(u => pathInfo.QueryString);
					///_cluster/nodes/_shutdown
					return this.Raw.ClusterNodeShutdownPost(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeShutdownDispatchAsync(ElasticSearchPathInfo<ClusterNodeShutdownQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///_cluster/nodes/{node_id}/_shutdown
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeShutdownPostAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_shutdown
					return this.Raw.ClusterNodeShutdownPostAsync(u => pathInfo.QueryString);
					///_cluster/nodes/_shutdown
					return this.Raw.ClusterNodeShutdownPostAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterNodeStatsDispatch(ElasticSearchPathInfo<ClusterNodeStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_nodes/{node_id}/stats/indices/{metric}/{fields}
					if (!pathInfo.NodeId.IsNullOrEmpty() && pathInfo.Metric != null && !pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGet(pathInfo.NodeId,pathInfo.Metric,pathInfo.Fields,u => pathInfo.QueryString);
					///_nodes/{node_id}/stats/{metric_family}
					if (!pathInfo.NodeId.IsNullOrEmpty() && pathInfo.MetricFamily != null)
						return this.Raw.ClusterNodeStatsGet(pathInfo.NodeId,pathInfo.MetricFamily,u => pathInfo.QueryString);
					///_nodes/stats/indices/{metric}/{fields}
					if (pathInfo.Metric != null && !pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGet(pathInfo.Metric,pathInfo.Fields,u => pathInfo.QueryString);
					///_cluster/nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/stats/{metric_family}
					if (pathInfo.MetricFamily != null)
						return this.Raw.ClusterNodeStatsGet(pathInfo.MetricFamily,u => pathInfo.QueryString);
					///_cluster/nodes/stats
					return this.Raw.ClusterNodeStatsGet(u => pathInfo.QueryString);
					///_nodes/stats
					return this.Raw.ClusterNodeStatsGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeStatsDispatchAsync(ElasticSearchPathInfo<ClusterNodeStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_nodes/{node_id}/stats/indices/{metric}/{fields}
					if (!pathInfo.NodeId.IsNullOrEmpty() && pathInfo.Metric != null && !pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.NodeId,pathInfo.Metric,pathInfo.Fields,u => pathInfo.QueryString);
					///_nodes/{node_id}/stats/{metric_family}
					if (!pathInfo.NodeId.IsNullOrEmpty() && pathInfo.MetricFamily != null)
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.NodeId,pathInfo.MetricFamily,u => pathInfo.QueryString);
					///_nodes/stats/indices/{metric}/{fields}
					if (pathInfo.Metric != null && !pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.Metric,pathInfo.Fields,u => pathInfo.QueryString);
					///_cluster/nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					///_nodes/stats/{metric_family}
					if (pathInfo.MetricFamily != null)
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.MetricFamily,u => pathInfo.QueryString);
					///_cluster/nodes/stats
					return this.Raw.ClusterNodeStatsGetAsync(u => pathInfo.QueryString);
					///_nodes/stats
					return this.Raw.ClusterNodeStatsGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterPutSettingsDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///_cluster/settings
					if (body != null)
						return this.Raw.ClusterPutSettings(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterPutSettingsDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///_cluster/settings
					if (body != null)
						return this.Raw.ClusterPutSettingsAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterRerouteDispatch(ElasticSearchPathInfo<ClusterRerouteQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///_cluster/reroute
					if (body != null)
						return this.Raw.ClusterReroutePost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterRerouteDispatchAsync(ElasticSearchPathInfo<ClusterRerouteQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///_cluster/reroute
					if (body != null)
						return this.Raw.ClusterReroutePostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ClusterStateDispatch(ElasticSearchPathInfo<ClusterStateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/state
					return this.Raw.ClusterStateGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ClusterStateDispatchAsync(ElasticSearchPathInfo<ClusterStateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_cluster/state
					return this.Raw.ClusterStateGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus CountDispatch(ElasticSearchPathInfo<CountQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CountPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.CountPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_count
					if (body != null)
						return this.Raw.CountPost(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					///{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.CountGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CountGet(pathInfo.Index,u => pathInfo.QueryString);
					///_count
					return this.Raw.CountGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> CountDispatchAsync(ElasticSearchPathInfo<CountQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CountPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.CountPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_count
					if (body != null)
						return this.Raw.CountPostAsync(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					///{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.CountGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CountGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_count
					return this.Raw.CountGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus CreateDispatch(ElasticSearchPathInfo<CreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					///{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePut(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePut(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> CreateDispatchAsync(ElasticSearchPathInfo<CreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					///{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePutAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePutAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus DeleteDispatch(ElasticSearchPathInfo<DeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.Delete(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> DeleteDispatchAsync(ElasticSearchPathInfo<DeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.DeleteAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus DeleteByQueryDispatch(ElasticSearchPathInfo<DeleteByQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQuery(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQuery(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> DeleteByQueryDispatchAsync(ElasticSearchPathInfo<DeleteByQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQueryAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQueryAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ExistsDispatch(ElasticSearchPathInfo<ExistsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExistsHead(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ExistsDispatchAsync(ElasticSearchPathInfo<ExistsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExistsHeadAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ExplainDispatch(ElasticSearchPathInfo<ExplainQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExplainGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.ExplainPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ExplainDispatchAsync(ElasticSearchPathInfo<ExplainQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExplainGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.ExplainPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus GetDispatch(ElasticSearchPathInfo<GetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.Get(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					///{index}/{type}/{id}/_source
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.Get(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> GetDispatchAsync(ElasticSearchPathInfo<GetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					///{index}/{type}/{id}/_source
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus GetSourceDispatch(ElasticSearchPathInfo<GetSourceQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}/_source
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetSource(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> GetSourceDispatchAsync(ElasticSearchPathInfo<GetSourceQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}/_source
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetSourceAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndexDispatch(ElasticSearchPathInfo<IndexQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPut(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPut(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndexDispatchAsync(ElasticSearchPathInfo<IndexQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					///{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesAnalyzeDispatch(ElasticSearchPathInfo<IndicesAnalyzeQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesAnalyzeGet(pathInfo.Index,u => pathInfo.QueryString);
					///_analyze
					return this.Raw.IndicesAnalyzeGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesAnalyzePost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_analyze
					if (body != null)
						return this.Raw.IndicesAnalyzePost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesAnalyzeDispatchAsync(ElasticSearchPathInfo<IndicesAnalyzeQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesAnalyzeGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_analyze
					return this.Raw.IndicesAnalyzeGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesAnalyzePostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_analyze
					if (body != null)
						return this.Raw.IndicesAnalyzePostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesClearCacheDispatch(ElasticSearchPathInfo<IndicesClearCacheQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCachePost(pathInfo.Index,u => pathInfo.QueryString);
					///_cache/clear
					return this.Raw.IndicesClearCachePost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCacheGet(pathInfo.Index,u => pathInfo.QueryString);
					///_cache/clear
					return this.Raw.IndicesClearCacheGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesClearCacheDispatchAsync(ElasticSearchPathInfo<IndicesClearCacheQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCachePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_cache/clear
					return this.Raw.IndicesClearCachePostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCacheGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_cache/clear
					return this.Raw.IndicesClearCacheGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesCloseDispatch(ElasticSearchPathInfo<IndicesCloseQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_close
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClosePost(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesCloseDispatchAsync(ElasticSearchPathInfo<IndicesCloseQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_close
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClosePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesCreateDispatch(ElasticSearchPathInfo<IndicesCreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePut(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePost(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesCreateDispatchAsync(ElasticSearchPathInfo<IndicesCreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePutAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesDeleteDispatch(ElasticSearchPathInfo<IndicesDeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDelete(pathInfo.Index,u => pathInfo.QueryString);
					///
					return this.Raw.IndicesDelete(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteDispatchAsync(ElasticSearchPathInfo<IndicesDeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAsync(pathInfo.Index,u => pathInfo.QueryString);
					///
					return this.Raw.IndicesDeleteAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesDeleteAliasDispatch(ElasticSearchPathInfo<IndicesDeleteAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAlias(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteAliasDispatchAsync(ElasticSearchPathInfo<IndicesDeleteAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAliasAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesDeleteMappingDispatch(ElasticSearchPathInfo<IndicesDeleteMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteMappingDispatchAsync(ElasticSearchPathInfo<IndicesDeleteMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesDeleteTemplateDispatch(ElasticSearchPathInfo<IndicesDeleteTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteTemplate(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteTemplateDispatchAsync(ElasticSearchPathInfo<IndicesDeleteTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteTemplateAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesDeleteWarmerDispatch(ElasticSearchPathInfo<IndicesDeleteWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteWarmerDispatchAsync(ElasticSearchPathInfo<IndicesDeleteWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					///{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesExistsDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesExistsHead(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesExistsHeadAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesExistsAliasDispatch(ElasticSearchPathInfo<IndicesExistsAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHead(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHead(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsAliasDispatchAsync(ElasticSearchPathInfo<IndicesExistsAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesExistsTypeDispatch(ElasticSearchPathInfo<IndicesExistsTypeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesExistsTypeHead(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsTypeDispatchAsync(ElasticSearchPathInfo<IndicesExistsTypeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					///{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesExistsTypeHeadAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesFlushDispatch(ElasticSearchPathInfo<IndicesFlushQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushPost(pathInfo.Index,u => pathInfo.QueryString);
					///_flush
					return this.Raw.IndicesFlushPost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushGet(pathInfo.Index,u => pathInfo.QueryString);
					///_flush
					return this.Raw.IndicesFlushGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesFlushDispatchAsync(ElasticSearchPathInfo<IndicesFlushQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_flush
					return this.Raw.IndicesFlushPostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_flush
					return this.Raw.IndicesFlushGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetAliasDispatch(ElasticSearchPathInfo<IndicesGetAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAlias(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAlias(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetAliasDispatchAsync(ElasticSearchPathInfo<IndicesGetAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetAliasesDispatch(ElasticSearchPathInfo<IndicesGetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_aliases
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliases(pathInfo.Index,u => pathInfo.QueryString);
					///_aliases
					return this.Raw.IndicesGetAliases(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetAliasesDispatchAsync(ElasticSearchPathInfo<IndicesGetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_aliases
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasesAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_aliases
					return this.Raw.IndicesGetAliasesAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetFieldMappingDispatch(ElasticSearchPathInfo<IndicesGetFieldMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					///{index}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Index,pathInfo.Field,u => pathInfo.QueryString);
					///_mapping/field/{field}
					if (!pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Field,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetFieldMappingDispatchAsync(ElasticSearchPathInfo<IndicesGetFieldMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					///{index}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Index,pathInfo.Field,u => pathInfo.QueryString);
					///_mapping/field/{field}
					if (!pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Field,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetMappingDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetMapping(pathInfo.Index,u => pathInfo.QueryString);
					///_mapping
					return this.Raw.IndicesGetMapping(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetMappingDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_mapping
					return this.Raw.IndicesGetMappingAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetSettingsDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetSettings(pathInfo.Index,u => pathInfo.QueryString);
					///_settings
					return this.Raw.IndicesGetSettings(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetSettingsDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetSettingsAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_settings
					return this.Raw.IndicesGetSettingsAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetTemplateDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetTemplate(pathInfo.Name,u => pathInfo.QueryString);
					///_template
					return this.Raw.IndicesGetTemplate(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetTemplateDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetTemplateAsync(pathInfo.Name,u => pathInfo.QueryString);
					///_template
					return this.Raw.IndicesGetTemplateAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesGetWarmerDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmer(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmer(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmer(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesGetWarmerDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					///{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesOpenDispatch(ElasticSearchPathInfo<IndicesOpenQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_open
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOpenPost(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesOpenDispatchAsync(ElasticSearchPathInfo<IndicesOpenQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_open
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOpenPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesOptimizeDispatch(ElasticSearchPathInfo<IndicesOptimizeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizePost(pathInfo.Index,u => pathInfo.QueryString);
					///_optimize
					return this.Raw.IndicesOptimizePost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizeGet(pathInfo.Index,u => pathInfo.QueryString);
					///_optimize
					return this.Raw.IndicesOptimizeGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesOptimizeDispatchAsync(ElasticSearchPathInfo<IndicesOptimizeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_optimize
					return this.Raw.IndicesOptimizePostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizeGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_optimize
					return this.Raw.IndicesOptimizeGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesPutAliasDispatch(ElasticSearchPathInfo<IndicesPutAliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAlias(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					///_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAlias(pathInfo.Name,body,u => pathInfo.QueryString);
					///{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAlias(pathInfo.Index,body,u => pathInfo.QueryString);
					///_alias
					if (body != null)
						return this.Raw.IndicesPutAlias(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesPutAliasDispatchAsync(ElasticSearchPathInfo<IndicesPutAliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAliasAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					///_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					///{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAliasAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_alias
					if (body != null)
						return this.Raw.IndicesPutAliasAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesPutMappingDispatch(ElasticSearchPathInfo<IndicesPutMappingQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMapping(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesPutMappingDispatchAsync(ElasticSearchPathInfo<IndicesPutMappingQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesPutSettingsDispatch(ElasticSearchPathInfo<IndicesPutSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutSettings(pathInfo.Index,body,u => pathInfo.QueryString);
					///_settings
					if (body != null)
						return this.Raw.IndicesPutSettings(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesPutSettingsDispatchAsync(ElasticSearchPathInfo<IndicesPutSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutSettingsAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_settings
					if (body != null)
						return this.Raw.IndicesPutSettingsAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesPutTemplateDispatch(ElasticSearchPathInfo<IndicesPutTemplateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplate(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplatePost(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesPutTemplateDispatchAsync(ElasticSearchPathInfo<IndicesPutTemplateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplateAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplatePostAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesPutWarmerDispatch(ElasticSearchPathInfo<IndicesPutWarmerQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmer(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.QueryString);
					///{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmer(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesPutWarmerDispatchAsync(ElasticSearchPathInfo<IndicesPutWarmerQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					///{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.QueryString);
					///{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesRefreshDispatch(ElasticSearchPathInfo<IndicesRefreshQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshPost(pathInfo.Index,u => pathInfo.QueryString);
					///_refresh
					return this.Raw.IndicesRefreshPost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshGet(pathInfo.Index,u => pathInfo.QueryString);
					///_refresh
					return this.Raw.IndicesRefreshGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesRefreshDispatchAsync(ElasticSearchPathInfo<IndicesRefreshQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_refresh
					return this.Raw.IndicesRefreshPostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					///{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_refresh
					return this.Raw.IndicesRefreshGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesSegmentsDispatch(ElasticSearchPathInfo<IndicesSegmentsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_segments
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSegmentsGet(pathInfo.Index,u => pathInfo.QueryString);
					///_segments
					return this.Raw.IndicesSegmentsGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesSegmentsDispatchAsync(ElasticSearchPathInfo<IndicesSegmentsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_segments
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSegmentsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_segments
					return this.Raw.IndicesSegmentsGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesSnapshotIndexDispatch(ElasticSearchPathInfo<IndicesSnapshotIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_gateway/snapshot
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSnapshotIndexPost(pathInfo.Index,u => pathInfo.QueryString);
					///_gateway/snapshot
					return this.Raw.IndicesSnapshotIndexPost(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesSnapshotIndexDispatchAsync(ElasticSearchPathInfo<IndicesSnapshotIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_gateway/snapshot
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSnapshotIndexPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_gateway/snapshot
					return this.Raw.IndicesSnapshotIndexPostAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesStatsDispatch(ElasticSearchPathInfo<IndicesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_stats/{metric_family}
					if (!pathInfo.Index.IsNullOrEmpty() && pathInfo.MetricFamily != null)
						return this.Raw.IndexStatsGet(pathInfo.Index,pathInfo.MetricFamily,u => pathInfo.QueryString);
					///{index}/_stats/search/{search_groups}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.SearchGroups.IsNullOrEmpty())
						return this.Raw.IndexSearchStatsGet(pathInfo.Index,pathInfo.SearchGroups,u => pathInfo.QueryString);
					///{index}/_stats/fielddata/{fields}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.IndexFieldDataStatsGet(pathInfo.Index,pathInfo.Fields,u => pathInfo.QueryString);
					///{index}/_stats
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndexStatsGet(pathInfo.Index,u => pathInfo.QueryString);
					//_stats/{metric_family}
					if (pathInfo.MetricFamily != null)
						return this.Raw.IndicesStatsGet(pathInfo.MetricFamily,u => pathInfo.QueryString);
					///{index}/_stats/indexing
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndexStatsGet(pathInfo.Index,u => pathInfo.QueryString);
					///_stats/indexing/{indexing_types}
					if (!pathInfo.IndexingTypes.IsNullOrEmpty())
						return this.Raw.IndicesIndexingStatsGet(pathInfo.IndexingTypes,u => pathInfo.QueryString);
					///_stats/search/{search_groups}
					if (!pathInfo.SearchGroups.IsNullOrEmpty())
						return this.Raw.IndicesSearchStatsGet(pathInfo.SearchGroups,u => pathInfo.QueryString);
					///_stats/fielddata/{fields}
					if (!pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.IndicesFieldDataStatsGet(pathInfo.Fields,u => pathInfo.QueryString);
					///_stats
					return this.Raw.IndicesStatsGet(u => pathInfo.QueryString);
					///_stats/indexing
					return this.Raw.IndicesStatsGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesStatsDispatchAsync(ElasticSearchPathInfo<IndicesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_stats/{metric_family}
					if (!pathInfo.Index.IsNullOrEmpty() && pathInfo.MetricFamily != null)
						return this.Raw.IndexStatsGetAsync(pathInfo.Index,pathInfo.MetricFamily,u => pathInfo.QueryString);
					///{index}/_stats/search/{search_groups}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.SearchGroups.IsNullOrEmpty())
						return this.Raw.IndexSearchStatsGetAsync(pathInfo.Index,pathInfo.SearchGroups,u => pathInfo.QueryString);
					///{index}/_stats/fielddata/{fields}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.IndexFieldDataStatsGetAsync(pathInfo.Index,pathInfo.Fields,u => pathInfo.QueryString);
					///{index}/_stats
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndexStatsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//_stats/{metric_family}
					if (pathInfo.MetricFamily != null)
						return this.Raw.IndicesStatsGetAsync(pathInfo.MetricFamily,u => pathInfo.QueryString);
					///{index}/_stats/indexing
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndexStatsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_stats/indexing/{indexing_types}
					if (!pathInfo.IndexingTypes.IsNullOrEmpty())
						return this.Raw.IndicesIndexingStatsGetAsync(pathInfo.IndexingTypes,u => pathInfo.QueryString);
					///_stats/search/{search_groups}
					if (!pathInfo.SearchGroups.IsNullOrEmpty())
						return this.Raw.IndicesSearchStatsGetAsync(pathInfo.SearchGroups,u => pathInfo.QueryString);
					///_stats/fielddata/{fields}
					if (!pathInfo.Fields.IsNullOrEmpty())
						return this.Raw.IndicesFieldDataStatsGetAsync(pathInfo.Fields,u => pathInfo.QueryString);
					///_stats
					return this.Raw.IndicesStatsGetAsync(u => pathInfo.QueryString);
					///_stats/indexing
					return this.Raw.IndicesStatsGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesStatusDispatch(ElasticSearchPathInfo<IndicesStatusQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_status
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatusGet(pathInfo.Index,u => pathInfo.QueryString);
					///_status
					return this.Raw.IndicesStatusGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesStatusDispatchAsync(ElasticSearchPathInfo<IndicesStatusQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/_status
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatusGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_status
					return this.Raw.IndicesStatusGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesUpdateAliasesDispatch(ElasticSearchPathInfo<IndicesUpdateAliasesQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///_aliases
					if (body != null)
						return this.Raw.IndicesUpdateAliasesPost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesUpdateAliasesDispatchAsync(ElasticSearchPathInfo<IndicesUpdateAliasesQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///_aliases
					if (body != null)
						return this.Raw.IndicesUpdateAliasesPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus IndicesValidateQueryDispatch(ElasticSearchPathInfo<ValidateQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGet(pathInfo.Index,u => pathInfo.QueryString);
					///_validate/query
					return this.Raw.IndicesValidateQueryGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_validate/query
					if (body != null)
						return this.Raw.IndicesValidateQueryPost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> IndicesValidateQueryDispatchAsync(ElasticSearchPathInfo<ValidateQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_validate/query
					return this.Raw.IndicesValidateQueryGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_validate/query
					if (body != null)
						return this.Raw.IndicesValidateQueryPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus InfoDispatch(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///
					return this.Raw.InfoGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.HEAD:
					///
					return this.Raw.InfoHead(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> InfoDispatchAsync(ElasticSearchPathInfo<FluentQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///
					return this.Raw.InfoGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.HEAD:
					///
					return this.Raw.InfoHeadAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus MgetDispatch(ElasticSearchPathInfo<MgetQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MgetGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MgetGet(pathInfo.Index,u => pathInfo.QueryString);
					///_mget
					return this.Raw.MgetGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_mget
					if (body != null)
						return this.Raw.MgetPost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> MgetDispatchAsync(ElasticSearchPathInfo<MgetQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MgetGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MgetGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_mget
					return this.Raw.MgetGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_mget
					if (body != null)
						return this.Raw.MgetPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus MltDispatch(ElasticSearchPathInfo<MltQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.MltGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.MltPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> MltDispatchAsync(ElasticSearchPathInfo<MltQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.MltGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.MltPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus MsearchDispatch(ElasticSearchPathInfo<MsearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MsearchGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MsearchGet(pathInfo.Index,u => pathInfo.QueryString);
					///_msearch
					return this.Raw.MsearchGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_msearch
					if (body != null)
						return this.Raw.MsearchPost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> MsearchDispatchAsync(ElasticSearchPathInfo<MsearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MsearchGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MsearchGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_msearch
					return this.Raw.MsearchGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_msearch
					if (body != null)
						return this.Raw.MsearchPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus PercolateDispatch(ElasticSearchPathInfo<PercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.PercolateGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> PercolateDispatchAsync(ElasticSearchPathInfo<PercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.PercolateGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus ScrollDispatch(ElasticSearchPathInfo<ScrollQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ScrollGet(pathInfo.ScrollId,u => pathInfo.QueryString);
					///_search/scroll
					return this.Raw.ScrollGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty() && body != null)
						return this.Raw.ScrollPost(pathInfo.ScrollId,body,u => pathInfo.QueryString);
					///_search/scroll
					if (body != null)
						return this.Raw.ScrollPost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> ScrollDispatchAsync(ElasticSearchPathInfo<ScrollQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ScrollGetAsync(pathInfo.ScrollId,u => pathInfo.QueryString);
					///_search/scroll
					return this.Raw.ScrollGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty() && body != null)
						return this.Raw.ScrollPostAsync(pathInfo.ScrollId,body,u => pathInfo.QueryString);
					///_search/scroll
					if (body != null)
						return this.Raw.ScrollPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus SearchDispatch(ElasticSearchPathInfo<SearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.SearchGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SearchGet(pathInfo.Index,u => pathInfo.QueryString);
					///_search
					return this.Raw.SearchGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_search
					if (body != null)
						return this.Raw.SearchPost(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> SearchDispatchAsync(ElasticSearchPathInfo<SearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					///{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.SearchGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					///{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SearchGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_search
					return this.Raw.SearchGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					///{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					///{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_search
					if (body != null)
						return this.Raw.SearchPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal ConnectionStatus SuggestDispatch(ElasticSearchPathInfo<SuggestQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SuggestPost(pathInfo.Index,body,u => pathInfo.QueryString);
					///_suggest
					if (body != null)
						return this.Raw.SuggestPost(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					///{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SuggestGet(pathInfo.Index,u => pathInfo.QueryString);
					///_suggest
					return this.Raw.SuggestGet(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> SuggestDispatchAsync(ElasticSearchPathInfo<SuggestQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SuggestPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					///_suggest
					if (body != null)
						return this.Raw.SuggestPostAsync(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					///{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SuggestGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					///_suggest
					return this.Raw.SuggestGetAsync(u => pathInfo.QueryString);

			}
			return null;
		}
		
		
		internal ConnectionStatus UpdateDispatch(ElasticSearchPathInfo<UpdateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_update
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.UpdatePost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
		
		internal Task<ConnectionStatus> UpdateDispatchAsync(ElasticSearchPathInfo<UpdateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					///{index}/{type}/{id}/_update
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.UpdatePostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			return null;
		}
		
	}	
}
