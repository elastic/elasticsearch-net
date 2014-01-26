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
					//POST /{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_bulk
					if (body != null)
						return this.Raw.BulkPost(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPut(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//PUT /{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPut(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_bulk
					if (body != null)
						return this.Raw.BulkPut(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Bulk() into any of the following paths: \r\n - /_bulk\r\n - /{index}/_bulk\r\n - /{index}/{type}/_bulk");
		}
		
		
		internal Task<ConnectionStatus> BulkDispatchAsync(ElasticSearchPathInfo<BulkQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_bulk
					if (body != null)
						return this.Raw.BulkPostAsync(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPutAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//PUT /{index}/_bulk
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.BulkPutAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_bulk
					if (body != null)
						return this.Raw.BulkPutAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Bulk() into any of the following paths: \r\n - /_bulk\r\n - /{index}/_bulk\r\n - /{index}/{type}/_bulk");
		}
		
		
		internal ConnectionStatus ClearScrollDispatch(ElasticSearchPathInfo<ClearScrollQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ClearScrollDelete(pathInfo.ScrollId,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClearScroll() into any of the following paths: \r\n - /_search/scroll/{scroll_id}");
		}
		
		
		internal Task<ConnectionStatus> ClearScrollDispatchAsync(ElasticSearchPathInfo<ClearScrollQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ClearScrollDeleteAsync(pathInfo.ScrollId,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClearScroll() into any of the following paths: \r\n - /_search/scroll/{scroll_id}");
		}
		
		
		internal ConnectionStatus ClusterGetSettingsDispatch(ElasticSearchPathInfo<ClusterGetSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/settings
					return this.Raw.ClusterGetSettings(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterGetSettings() into any of the following paths: \r\n - /_cluster/settings");
		}
		
		
		internal Task<ConnectionStatus> ClusterGetSettingsDispatchAsync(ElasticSearchPathInfo<ClusterGetSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/settings
					return this.Raw.ClusterGetSettingsAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterGetSettings() into any of the following paths: \r\n - /_cluster/settings");
		}
		
		
		internal ConnectionStatus ClusterHealthDispatch(ElasticSearchPathInfo<ClusterHealthQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/health/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.ClusterHealthGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cluster/health
					return this.Raw.ClusterHealthGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterHealth() into any of the following paths: \r\n - /_cluster/health\r\n - /_cluster/health/{index}");
		}
		
		
		internal Task<ConnectionStatus> ClusterHealthDispatchAsync(ElasticSearchPathInfo<ClusterHealthQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/health/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.ClusterHealthGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cluster/health
					return this.Raw.ClusterHealthGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterHealth() into any of the following paths: \r\n - /_cluster/health\r\n - /_cluster/health/{index}");
		}
		
		
		internal ConnectionStatus ClusterNodeHotThreadsDispatch(ElasticSearchPathInfo<ClusterNodeHotThreadsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeHotThreadsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes/hotthreads
					return this.Raw.ClusterNodeHotThreadsGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeHotThreads() into any of the following paths: \r\n - /_cluster/nodes/hotthreads\r\n - /_cluster/nodes/hot_threads\r\n - /_cluster/nodes/{node_id}/hotthreads\r\n - /_cluster/nodes/{node_id}/hot_threads\r\n - /_nodes/hotthreads\r\n - /_nodes/hot_threads\r\n - /_nodes/{node_id}/hotthreads\r\n - /_nodes/{node_id}/hot_threads");
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeHotThreadsDispatchAsync(ElasticSearchPathInfo<ClusterNodeHotThreadsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeHotThreadsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes/hotthreads
					return this.Raw.ClusterNodeHotThreadsGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeHotThreads() into any of the following paths: \r\n - /_cluster/nodes/hotthreads\r\n - /_cluster/nodes/hot_threads\r\n - /_cluster/nodes/{node_id}/hotthreads\r\n - /_cluster/nodes/{node_id}/hot_threads\r\n - /_nodes/hotthreads\r\n - /_nodes/hot_threads\r\n - /_nodes/{node_id}/hotthreads\r\n - /_nodes/{node_id}/hot_threads");
		}
		
		
		internal ConnectionStatus ClusterNodeInfoDispatch(ElasticSearchPathInfo<ClusterNodeInfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes
					return this.Raw.ClusterNodeInfoGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeInfo() into any of the following paths: \r\n - /_cluster/nodes\r\n - /_cluster/nodes/{node_id}\r\n - /_nodes\r\n - /_nodes/{node_id}\r\n - /_nodes/settings\r\n - /_nodes/{node_id}/settings\r\n - /_nodes/os\r\n - /_nodes/{node_id}/os\r\n - /_nodes/process\r\n - /_nodes/{node_id}/process\r\n - /_nodes/jvm\r\n - /_nodes/{node_id}/jvm\r\n - /_nodes/thread_pool\r\n - /_nodes/{node_id}/thread_pool\r\n - /_nodes/network\r\n - /_nodes/{node_id}/network\r\n - /_nodes/transport\r\n - /_nodes/{node_id}/transport\r\n - /_nodes/http\r\n - /_nodes/{node_id}/http\r\n - /_nodes/plugin\r\n - /_nodes/{node_id}/plugin");
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeInfoDispatchAsync(ElasticSearchPathInfo<ClusterNodeInfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes
					return this.Raw.ClusterNodeInfoGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeInfo() into any of the following paths: \r\n - /_cluster/nodes\r\n - /_cluster/nodes/{node_id}\r\n - /_nodes\r\n - /_nodes/{node_id}\r\n - /_nodes/settings\r\n - /_nodes/{node_id}/settings\r\n - /_nodes/os\r\n - /_nodes/{node_id}/os\r\n - /_nodes/process\r\n - /_nodes/{node_id}/process\r\n - /_nodes/jvm\r\n - /_nodes/{node_id}/jvm\r\n - /_nodes/thread_pool\r\n - /_nodes/{node_id}/thread_pool\r\n - /_nodes/network\r\n - /_nodes/{node_id}/network\r\n - /_nodes/transport\r\n - /_nodes/{node_id}/transport\r\n - /_nodes/http\r\n - /_nodes/{node_id}/http\r\n - /_nodes/plugin\r\n - /_nodes/{node_id}/plugin");
		}
		
		
		internal ConnectionStatus ClusterNodeShutdownDispatch(ElasticSearchPathInfo<ClusterNodeShutdownQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_cluster/nodes/{node_id}/_shutdown
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeShutdownPost(pathInfo.NodeId,u => pathInfo.QueryString);
					//POST /_shutdown
					return this.Raw.ClusterNodeShutdownPost(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeShutdown() into any of the following paths: \r\n - /_shutdown\r\n - /_cluster/nodes/_shutdown\r\n - /_cluster/nodes/{node_id}/_shutdown");
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeShutdownDispatchAsync(ElasticSearchPathInfo<ClusterNodeShutdownQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_cluster/nodes/{node_id}/_shutdown
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeShutdownPostAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//POST /_shutdown
					return this.Raw.ClusterNodeShutdownPostAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeShutdown() into any of the following paths: \r\n - /_shutdown\r\n - /_cluster/nodes/_shutdown\r\n - /_cluster/nodes/{node_id}/_shutdown");
		}
		
		
		internal ConnectionStatus ClusterNodeStatsDispatch(ElasticSearchPathInfo<ClusterNodeStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes/stats
					return this.Raw.ClusterNodeStatsGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeStats() into any of the following paths: \r\n - /_cluster/nodes/stats\r\n - /_cluster/nodes/{node_id}/stats\r\n - /_nodes/stats\r\n - /_nodes/{node_id}/stats");
		}
		
		
		internal Task<ConnectionStatus> ClusterNodeStatsDispatchAsync(ElasticSearchPathInfo<ClusterNodeStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterNodeStatsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes/stats
					return this.Raw.ClusterNodeStatsGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterNodeStats() into any of the following paths: \r\n - /_cluster/nodes/stats\r\n - /_cluster/nodes/{node_id}/stats\r\n - /_nodes/stats\r\n - /_nodes/{node_id}/stats");
		}
		
		
		internal ConnectionStatus ClusterPutSettingsDispatch(ElasticSearchPathInfo<ClusterPutSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_cluster/settings
					if (body != null)
						return this.Raw.ClusterPutSettings(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterPutSettings() into any of the following paths: \r\n - /_cluster/settings");
		}
		
		
		internal Task<ConnectionStatus> ClusterPutSettingsDispatchAsync(ElasticSearchPathInfo<ClusterPutSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_cluster/settings
					if (body != null)
						return this.Raw.ClusterPutSettingsAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterPutSettings() into any of the following paths: \r\n - /_cluster/settings");
		}
		
		
		internal ConnectionStatus ClusterRerouteDispatch(ElasticSearchPathInfo<ClusterRerouteQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_cluster/reroute
					if (body != null)
						return this.Raw.ClusterReroutePost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterReroute() into any of the following paths: \r\n - /_cluster/reroute");
		}
		
		
		internal Task<ConnectionStatus> ClusterRerouteDispatchAsync(ElasticSearchPathInfo<ClusterRerouteQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_cluster/reroute
					if (body != null)
						return this.Raw.ClusterReroutePostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterReroute() into any of the following paths: \r\n - /_cluster/reroute");
		}
		
		
		internal ConnectionStatus ClusterStateDispatch(ElasticSearchPathInfo<ClusterStateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/state
					return this.Raw.ClusterStateGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterState() into any of the following paths: \r\n - /_cluster/state");
		}
		
		
		internal Task<ConnectionStatus> ClusterStateDispatchAsync(ElasticSearchPathInfo<ClusterStateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/state
					return this.Raw.ClusterStateGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterState() into any of the following paths: \r\n - /_cluster/state");
		}
		
		
		internal ConnectionStatus CountDispatch(ElasticSearchPathInfo<CountQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CountPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.CountPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_count
					if (body != null)
						return this.Raw.CountPost(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.CountGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CountGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_count
					return this.Raw.CountGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Count() into any of the following paths: \r\n - /_count\r\n - /{index}/_count\r\n - /{index}/{type}/_count");
		}
		
		
		internal Task<ConnectionStatus> CountDispatchAsync(ElasticSearchPathInfo<CountQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CountPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.CountPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_count
					if (body != null)
						return this.Raw.CountPostAsync(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.CountGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_count
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CountGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_count
					return this.Raw.CountGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Count() into any of the following paths: \r\n - /_count\r\n - /{index}/_count\r\n - /{index}/{type}/_count");
		}
		
		
		internal ConnectionStatus CreateDispatch(ElasticSearchPathInfo<CreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePut(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//PUT /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePut(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Create() into any of the following paths: \r\n - /{index}/{type}\r\n - /{index}/{type}/{id}/_create");
		}
		
		
		internal Task<ConnectionStatus> CreateDispatchAsync(ElasticSearchPathInfo<CreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/{id}/_create
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePutAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//PUT /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CreatePutAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Create() into any of the following paths: \r\n - /{index}/{type}\r\n - /{index}/{type}/{id}/_create");
		}
		
		
		internal ConnectionStatus DeleteDispatch(ElasticSearchPathInfo<DeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.Delete(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Delete() into any of the following paths: \r\n - /{index}/{type}/{id}");
		}
		
		
		internal Task<ConnectionStatus> DeleteDispatchAsync(ElasticSearchPathInfo<DeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.DeleteAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Delete() into any of the following paths: \r\n - /{index}/{type}/{id}");
		}
		
		
		internal ConnectionStatus DeleteByQueryDispatch(ElasticSearchPathInfo<DeleteByQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQuery(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//DELETE /{index}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQuery(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.DeleteByQuery() into any of the following paths: \r\n - /{index}/_query\r\n - /{index}/{type}/_query");
		}
		
		
		internal Task<ConnectionStatus> DeleteByQueryDispatchAsync(ElasticSearchPathInfo<DeleteByQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQueryAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//DELETE /{index}/_query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.DeleteByQueryAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.DeleteByQuery() into any of the following paths: \r\n - /{index}/_query\r\n - /{index}/{type}/_query");
		}
		
		
		internal ConnectionStatus ExistsDispatch(ElasticSearchPathInfo<ExistsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExistsHead(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Exists() into any of the following paths: \r\n - /{index}/{type}/{id}");
		}
		
		
		internal Task<ConnectionStatus> ExistsDispatchAsync(ElasticSearchPathInfo<ExistsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExistsHeadAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Exists() into any of the following paths: \r\n - /{index}/{type}/{id}");
		}
		
		
		internal ConnectionStatus ExplainDispatch(ElasticSearchPathInfo<ExplainQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExplainGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.ExplainPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Explain() into any of the following paths: \r\n - /{index}/{type}/{id}/_explain");
		}
		
		
		internal Task<ConnectionStatus> ExplainDispatchAsync(ElasticSearchPathInfo<ExplainQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.ExplainGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_explain
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.ExplainPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Explain() into any of the following paths: \r\n - /{index}/{type}/{id}/_explain");
		}
		
		
		internal ConnectionStatus GetDispatch(ElasticSearchPathInfo<GetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.Get(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Get() into any of the following paths: \r\n - /{index}/{type}/{id}\r\n - /{index}/{type}/{id}/_source");
		}
		
		
		internal Task<ConnectionStatus> GetDispatchAsync(ElasticSearchPathInfo<GetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Get() into any of the following paths: \r\n - /{index}/{type}/{id}\r\n - /{index}/{type}/{id}/_source");
		}
		
		
		internal ConnectionStatus GetSourceDispatch(ElasticSearchPathInfo<GetSourceQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_source
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetSource(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.GetSource() into any of the following paths: \r\n - /{index}/{type}/{id}/_source");
		}
		
		
		internal Task<ConnectionStatus> GetSourceDispatchAsync(ElasticSearchPathInfo<GetSourceQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_source
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetSourceAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.GetSource() into any of the following paths: \r\n - /{index}/{type}/{id}/_source");
		}
		
		
		internal ConnectionStatus IndexDispatch(ElasticSearchPathInfo<IndexQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPut(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//PUT /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPut(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Index() into any of the following paths: \r\n - /{index}/{type}\r\n - /{index}/{type}/{id}");
		}
		
		
		internal Task<ConnectionStatus> IndexDispatchAsync(ElasticSearchPathInfo<IndexQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//PUT /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Index() into any of the following paths: \r\n - /{index}/{type}\r\n - /{index}/{type}/{id}");
		}
		
		
		internal ConnectionStatus IndicesAnalyzeDispatch(ElasticSearchPathInfo<AnalyzeQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesAnalyzeGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_analyze
					return this.Raw.IndicesAnalyzeGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesAnalyzePost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_analyze
					if (body != null)
						return this.Raw.IndicesAnalyzePost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesAnalyze() into any of the following paths: \r\n - /_analyze\r\n - /{index}/_analyze");
		}
		
		
		internal Task<ConnectionStatus> IndicesAnalyzeDispatchAsync(ElasticSearchPathInfo<AnalyzeQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesAnalyzeGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_analyze
					return this.Raw.IndicesAnalyzeGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesAnalyzePostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_analyze
					if (body != null)
						return this.Raw.IndicesAnalyzePostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesAnalyze() into any of the following paths: \r\n - /_analyze\r\n - /{index}/_analyze");
		}
		
		
		internal ConnectionStatus IndicesClearCacheDispatch(ElasticSearchPathInfo<ClearCacheQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCachePost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_cache/clear
					return this.Raw.IndicesClearCachePost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCacheGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cache/clear
					return this.Raw.IndicesClearCacheGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesClearCache() into any of the following paths: \r\n - /_cache/clear\r\n - /{index}/_cache/clear");
		}
		
		
		internal Task<ConnectionStatus> IndicesClearCacheDispatchAsync(ElasticSearchPathInfo<ClearCacheQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCachePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_cache/clear
					return this.Raw.IndicesClearCachePostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCacheGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cache/clear
					return this.Raw.IndicesClearCacheGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesClearCache() into any of the following paths: \r\n - /_cache/clear\r\n - /{index}/_cache/clear");
		}
		
		
		internal ConnectionStatus IndicesCloseDispatch(ElasticSearchPathInfo<CloseIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_close
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClosePost(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesClose() into any of the following paths: \r\n - /{index}/_close");
		}
		
		
		internal Task<ConnectionStatus> IndicesCloseDispatchAsync(ElasticSearchPathInfo<CloseIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_close
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClosePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesClose() into any of the following paths: \r\n - /{index}/_close");
		}
		
		
		internal ConnectionStatus IndicesCreateDispatch(ElasticSearchPathInfo<CreateIndexQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePut(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePost(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesCreate() into any of the following paths: \r\n - /{index}");
		}
		
		
		internal Task<ConnectionStatus> IndicesCreateDispatchAsync(ElasticSearchPathInfo<CreateIndexQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePutAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesCreatePostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesCreate() into any of the following paths: \r\n - /{index}");
		}
		
		
		internal ConnectionStatus IndicesDeleteDispatch(ElasticSearchPathInfo<DeleteIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDelete(pathInfo.Index,u => pathInfo.QueryString);
					//DELETE /
					return this.Raw.IndicesDelete(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDelete() into any of the following paths: \r\n - /\r\n - /{index}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteDispatchAsync(ElasticSearchPathInfo<DeleteIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAsync(pathInfo.Index,u => pathInfo.QueryString);
					//DELETE /
					return this.Raw.IndicesDeleteAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDelete() into any of the following paths: \r\n - /\r\n - /{index}");
		}
		
		
		internal ConnectionStatus IndicesDeleteAliasDispatch(ElasticSearchPathInfo<IndicesDeleteAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAlias(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteAlias() into any of the following paths: \r\n - /{index}/_alias/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteAliasDispatchAsync(ElasticSearchPathInfo<IndicesDeleteAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAliasAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteAlias() into any of the following paths: \r\n - /{index}/_alias/{name}");
		}
		
		
		internal ConnectionStatus IndicesDeleteMappingDispatch(ElasticSearchPathInfo<DeleteMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping\r\n - /{index}/{type}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteMappingDispatchAsync(ElasticSearchPathInfo<DeleteMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping\r\n - /{index}/{type}");
		}
		
		
		internal ConnectionStatus IndicesDeleteTemplateDispatch(ElasticSearchPathInfo<DeleteTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteTemplate(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteTemplateDispatchAsync(ElasticSearchPathInfo<DeleteTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteTemplateAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesDeleteWarmerDispatch(ElasticSearchPathInfo<DeleteWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					//DELETE /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//DELETE /{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteWarmer() into any of the following paths: \r\n - /{index}/_warmer\r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteWarmerDispatchAsync(ElasticSearchPathInfo<DeleteWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					//DELETE /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//DELETE /{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteWarmer() into any of the following paths: \r\n - /{index}/_warmer\r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal ConnectionStatus IndicesExistsDispatch(ElasticSearchPathInfo<IndexExistsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesExistsHead(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExists() into any of the following paths: \r\n - /{index}");
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsDispatchAsync(ElasticSearchPathInfo<IndexExistsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesExistsHeadAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExists() into any of the following paths: \r\n - /{index}");
		}
		
		
		internal ConnectionStatus IndicesExistsAliasDispatch(ElasticSearchPathInfo<IndicesExistsAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHead(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//HEAD /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHead(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsAlias() into any of the following paths: \r\n - /_alias/{name}\r\n - /{index}/_alias/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsAliasDispatchAsync(ElasticSearchPathInfo<IndicesExistsAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//HEAD /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsAlias() into any of the following paths: \r\n - /_alias/{name}\r\n - /{index}/_alias/{name}");
		}
		
		
		internal ConnectionStatus IndicesExistsTypeDispatch(ElasticSearchPathInfo<IndicesExistsTypeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesExistsTypeHead(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsType() into any of the following paths: \r\n - /{index}/{type}");
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsTypeDispatchAsync(ElasticSearchPathInfo<IndicesExistsTypeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesExistsTypeHeadAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsType() into any of the following paths: \r\n - /{index}/{type}");
		}
		
		
		internal ConnectionStatus IndicesFlushDispatch(ElasticSearchPathInfo<FlushQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushPost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_flush
					return this.Raw.IndicesFlushPost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_flush
					return this.Raw.IndicesFlushGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesFlush() into any of the following paths: \r\n - /_flush\r\n - /{index}/_flush");
		}
		
		
		internal Task<ConnectionStatus> IndicesFlushDispatchAsync(ElasticSearchPathInfo<FlushQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_flush
					return this.Raw.IndicesFlushPostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_flush
					return this.Raw.IndicesFlushGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesFlush() into any of the following paths: \r\n - /_flush\r\n - /{index}/_flush");
		}
		
		
		internal ConnectionStatus IndicesGetAliasDispatch(ElasticSearchPathInfo<IndicesGetAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAlias(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAlias(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAlias() into any of the following paths: \r\n - /_alias/{name}\r\n - /{index}/_alias/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetAliasDispatchAsync(ElasticSearchPathInfo<IndicesGetAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAlias() into any of the following paths: \r\n - /_alias/{name}\r\n - /{index}/_alias/{name}");
		}
		
		
		internal ConnectionStatus IndicesGetAliasesDispatch(ElasticSearchPathInfo<IndicesGetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_aliases
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliases(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_aliases
					return this.Raw.IndicesGetAliases(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAliases() into any of the following paths: \r\n - /_aliases\r\n - /{index}/_aliases");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetAliasesDispatchAsync(ElasticSearchPathInfo<IndicesGetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_aliases
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasesAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_aliases
					return this.Raw.IndicesGetAliasesAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAliases() into any of the following paths: \r\n - /_aliases\r\n - /{index}/_aliases");
		}
		
		
		internal ConnectionStatus IndicesGetFieldMappingDispatch(ElasticSearchPathInfo<IndicesGetFieldMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					//GET /{index}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Index,pathInfo.Field,u => pathInfo.QueryString);
					//GET /_mapping/field/{field}
					if (!pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Field,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetFieldMapping() into any of the following paths: \r\n - /_mapping/field/{field}\r\n - /{index}/_mapping/field/{field}\r\n - /{index}/{type}/_mapping/field/{field}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetFieldMappingDispatchAsync(ElasticSearchPathInfo<IndicesGetFieldMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					//GET /{index}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Index,pathInfo.Field,u => pathInfo.QueryString);
					//GET /_mapping/field/{field}
					if (!pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Field,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetFieldMapping() into any of the following paths: \r\n - /_mapping/field/{field}\r\n - /{index}/_mapping/field/{field}\r\n - /{index}/{type}/_mapping/field/{field}");
		}
		
		
		internal ConnectionStatus IndicesGetMappingDispatch(ElasticSearchPathInfo<GetMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetMapping(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mapping
					return this.Raw.IndicesGetMapping(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetMapping() into any of the following paths: \r\n - /_mapping\r\n - /{index}/_mapping\r\n - /{index}/{type}/_mapping");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetMappingDispatchAsync(ElasticSearchPathInfo<GetMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mapping
					return this.Raw.IndicesGetMappingAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetMapping() into any of the following paths: \r\n - /_mapping\r\n - /{index}/_mapping\r\n - /{index}/{type}/_mapping");
		}
		
		
		internal ConnectionStatus IndicesGetSettingsDispatch(ElasticSearchPathInfo<GetIndexSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetSettings(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_settings
					return this.Raw.IndicesGetSettings(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetSettingsDispatchAsync(ElasticSearchPathInfo<GetIndexSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetSettingsAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_settings
					return this.Raw.IndicesGetSettingsAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings");
		}
		
		
		internal ConnectionStatus IndicesGetTemplateDispatch(ElasticSearchPathInfo<GetTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetTemplate(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_template
					return this.Raw.IndicesGetTemplate(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetTemplate() into any of the following paths: \r\n - /_template\r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetTemplateDispatchAsync(ElasticSearchPathInfo<GetTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetTemplateAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_template
					return this.Raw.IndicesGetTemplateAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetTemplate() into any of the following paths: \r\n - /_template\r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesGetWarmerDispatch(ElasticSearchPathInfo<GetWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmer(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmer(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmer(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetWarmer() into any of the following paths: \r\n - /{index}/_warmer\r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetWarmerDispatchAsync(ElasticSearchPathInfo<GetWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_warmer
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetWarmer() into any of the following paths: \r\n - /{index}/_warmer\r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal ConnectionStatus IndicesOpenDispatch(ElasticSearchPathInfo<OpenIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_open
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOpenPost(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesOpen() into any of the following paths: \r\n - /{index}/_open");
		}
		
		
		internal Task<ConnectionStatus> IndicesOpenDispatchAsync(ElasticSearchPathInfo<OpenIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_open
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOpenPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesOpen() into any of the following paths: \r\n - /{index}/_open");
		}
		
		
		internal ConnectionStatus IndicesOptimizeDispatch(ElasticSearchPathInfo<OptimizeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizePost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_optimize
					return this.Raw.IndicesOptimizePost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizeGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_optimize
					return this.Raw.IndicesOptimizeGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesOptimize() into any of the following paths: \r\n - /_optimize\r\n - /{index}/_optimize");
		}
		
		
		internal Task<ConnectionStatus> IndicesOptimizeDispatchAsync(ElasticSearchPathInfo<OptimizeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_optimize
					return this.Raw.IndicesOptimizePostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizeGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_optimize
					return this.Raw.IndicesOptimizeGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesOptimize() into any of the following paths: \r\n - /_optimize\r\n - /{index}/_optimize");
		}
		
		
		internal ConnectionStatus IndicesPutAliasDispatch(ElasticSearchPathInfo<IndicesPutAliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAlias(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAlias(pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAlias(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_alias
					if (body != null)
						return this.Raw.IndicesPutAlias(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutAlias() into any of the following paths: \r\n - /{index}/_alias/{name}\r\n - /_alias/{name}\r\n - /{index}/_alias\r\n - /_alias");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutAliasDispatchAsync(ElasticSearchPathInfo<IndicesPutAliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAliasAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndexPutAliasAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_alias
					if (body != null)
						return this.Raw.IndicesPutAliasAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutAlias() into any of the following paths: \r\n - /{index}/_alias/{name}\r\n - /_alias/{name}\r\n - /{index}/_alias\r\n - /_alias");
		}
		
		
		internal ConnectionStatus IndicesPutMappingDispatch(ElasticSearchPathInfo<PutMappingQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMapping(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutMappingDispatchAsync(ElasticSearchPathInfo<PutMappingQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping");
		}
		
		
		internal ConnectionStatus IndicesPutSettingsDispatch(ElasticSearchPathInfo<UpdateSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutSettings(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_settings
					if (body != null)
						return this.Raw.IndicesPutSettings(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutSettingsDispatchAsync(ElasticSearchPathInfo<UpdateSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutSettingsAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_settings
					if (body != null)
						return this.Raw.IndicesPutSettingsAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings");
		}
		
		
		internal ConnectionStatus IndicesPutTemplateDispatch(ElasticSearchPathInfo<PutTemplateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplate(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplatePost(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutTemplateDispatchAsync(ElasticSearchPathInfo<PutTemplateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplateAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplatePostAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesPutWarmerDispatch(ElasticSearchPathInfo<PutWarmerQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmer(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmer(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutWarmer() into any of the following paths: \r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutWarmerDispatchAsync(ElasticSearchPathInfo<PutWarmerQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutWarmer() into any of the following paths: \r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal ConnectionStatus IndicesRefreshDispatch(ElasticSearchPathInfo<RefreshQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshPost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_refresh
					return this.Raw.IndicesRefreshPost(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_refresh
					return this.Raw.IndicesRefreshGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesRefresh() into any of the following paths: \r\n - /_refresh\r\n - /{index}/_refresh");
		}
		
		
		internal Task<ConnectionStatus> IndicesRefreshDispatchAsync(ElasticSearchPathInfo<RefreshQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_refresh
					return this.Raw.IndicesRefreshPostAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_refresh
					return this.Raw.IndicesRefreshGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesRefresh() into any of the following paths: \r\n - /_refresh\r\n - /{index}/_refresh");
		}
		
		
		internal ConnectionStatus IndicesSegmentsDispatch(ElasticSearchPathInfo<SegmentsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_segments
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSegmentsGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_segments
					return this.Raw.IndicesSegmentsGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSegments() into any of the following paths: \r\n - /_segments\r\n - /{index}/_segments");
		}
		
		
		internal Task<ConnectionStatus> IndicesSegmentsDispatchAsync(ElasticSearchPathInfo<SegmentsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_segments
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSegmentsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_segments
					return this.Raw.IndicesSegmentsGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSegments() into any of the following paths: \r\n - /_segments\r\n - /{index}/_segments");
		}
		
		
		internal ConnectionStatus IndicesSnapshotIndexDispatch(ElasticSearchPathInfo<SnapshotQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_gateway/snapshot
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSnapshotIndexPost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_gateway/snapshot
					return this.Raw.IndicesSnapshotIndexPost(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSnapshotIndex() into any of the following paths: \r\n - /_gateway/snapshot\r\n - /{index}/_gateway/snapshot");
		}
		
		
		internal Task<ConnectionStatus> IndicesSnapshotIndexDispatchAsync(ElasticSearchPathInfo<SnapshotQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_gateway/snapshot
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSnapshotIndexPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_gateway/snapshot
					return this.Raw.IndicesSnapshotIndexPostAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSnapshotIndex() into any of the following paths: \r\n - /_gateway/snapshot\r\n - /{index}/_gateway/snapshot");
		}
		
		
		internal ConnectionStatus IndicesStatsDispatch(ElasticSearchPathInfo<IndicesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_stats
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndexStatsGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_stats
					return this.Raw.IndicesStatsGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStats() into any of the following paths: \r\n - /_stats\r\n - /{index}/_stats\r\n - /_stats/indexing\r\n - /{index}/_stats/indexing");
		}
		
		
		internal Task<ConnectionStatus> IndicesStatsDispatchAsync(ElasticSearchPathInfo<IndicesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_stats
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndexStatsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_stats
					return this.Raw.IndicesStatsGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStats() into any of the following paths: \r\n - /_stats\r\n - /{index}/_stats\r\n - /_stats/indexing\r\n - /{index}/_stats/indexing");
		}
		
		
		internal ConnectionStatus IndicesStatusDispatch(ElasticSearchPathInfo<IndicesStatusQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_status
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatusGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_status
					return this.Raw.IndicesStatusGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStatus() into any of the following paths: \r\n - /_status\r\n - /{index}/_status");
		}
		
		
		internal Task<ConnectionStatus> IndicesStatusDispatchAsync(ElasticSearchPathInfo<IndicesStatusQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_status
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatusGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_status
					return this.Raw.IndicesStatusGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStatus() into any of the following paths: \r\n - /_status\r\n - /{index}/_status");
		}
		
		
		internal ConnectionStatus IndicesUpdateAliasesDispatch(ElasticSearchPathInfo<IndicesUpdateAliasesQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_aliases
					if (body != null)
						return this.Raw.IndicesUpdateAliasesPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesUpdateAliases() into any of the following paths: \r\n - /_aliases");
		}
		
		
		internal Task<ConnectionStatus> IndicesUpdateAliasesDispatchAsync(ElasticSearchPathInfo<IndicesUpdateAliasesQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_aliases
					if (body != null)
						return this.Raw.IndicesUpdateAliasesPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesUpdateAliases() into any of the following paths: \r\n - /_aliases");
		}
		
		
		internal ConnectionStatus IndicesValidateQueryDispatch(ElasticSearchPathInfo<ValidateQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_validate/query
					return this.Raw.IndicesValidateQueryGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_validate/query
					if (body != null)
						return this.Raw.IndicesValidateQueryPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesValidateQuery() into any of the following paths: \r\n - /_validate/query\r\n - /{index}/_validate/query\r\n - /{index}/{type}/_validate/query");
		}
		
		
		internal Task<ConnectionStatus> IndicesValidateQueryDispatchAsync(ElasticSearchPathInfo<ValidateQueryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesValidateQueryGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_validate/query
					return this.Raw.IndicesValidateQueryGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_validate/query
					if (body != null)
						return this.Raw.IndicesValidateQueryPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesValidateQuery() into any of the following paths: \r\n - /_validate/query\r\n - /{index}/_validate/query\r\n - /{index}/{type}/_validate/query");
		}
		
		
		internal ConnectionStatus InfoDispatch(ElasticSearchPathInfo<InfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /
					return this.Raw.InfoGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.HEAD:
					//HEAD /
					return this.Raw.InfoHead(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Info() into any of the following paths: \r\n - /");
		}
		
		
		internal Task<ConnectionStatus> InfoDispatchAsync(ElasticSearchPathInfo<InfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /
					return this.Raw.InfoGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.HEAD:
					//HEAD /
					return this.Raw.InfoHeadAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Info() into any of the following paths: \r\n - /");
		}
		
		
		internal ConnectionStatus MgetDispatch(ElasticSearchPathInfo<MgetQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MgetGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MgetGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mget
					return this.Raw.MgetGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_mget
					if (body != null)
						return this.Raw.MgetPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mget() into any of the following paths: \r\n - /_mget\r\n - /{index}/_mget\r\n - /{index}/{type}/_mget");
		}
		
		
		internal Task<ConnectionStatus> MgetDispatchAsync(ElasticSearchPathInfo<MgetQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MgetGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MgetGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mget
					return this.Raw.MgetGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_mget
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MgetPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_mget
					if (body != null)
						return this.Raw.MgetPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mget() into any of the following paths: \r\n - /_mget\r\n - /{index}/_mget\r\n - /{index}/{type}/_mget");
		}
		
		
		internal ConnectionStatus MltDispatch(ElasticSearchPathInfo<MoreLikeThisQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.MltGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.MltPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mlt() into any of the following paths: \r\n - /{index}/{type}/{id}/_mlt");
		}
		
		
		internal Task<ConnectionStatus> MltDispatchAsync(ElasticSearchPathInfo<MoreLikeThisQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.MltGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_mlt
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.MltPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mlt() into any of the following paths: \r\n - /{index}/{type}/{id}/_mlt");
		}
		
		
		internal ConnectionStatus MsearchDispatch(ElasticSearchPathInfo<MsearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MsearchGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MsearchGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_msearch
					return this.Raw.MsearchGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_msearch
					if (body != null)
						return this.Raw.MsearchPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Msearch() into any of the following paths: \r\n - /_msearch\r\n - /{index}/_msearch\r\n - /{index}/{type}/_msearch");
		}
		
		
		internal Task<ConnectionStatus> MsearchDispatchAsync(ElasticSearchPathInfo<MsearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MsearchGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MsearchGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_msearch
					return this.Raw.MsearchGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_msearch
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MsearchPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_msearch
					if (body != null)
						return this.Raw.MsearchPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Msearch() into any of the following paths: \r\n - /_msearch\r\n - /{index}/_msearch\r\n - /{index}/{type}/_msearch");
		}
		
		
		internal ConnectionStatus PercolateDispatch(ElasticSearchPathInfo<PercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.PercolateGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Percolate() into any of the following paths: \r\n - /{index}/{type}/_percolate");
		}
		
		
		internal Task<ConnectionStatus> PercolateDispatchAsync(ElasticSearchPathInfo<PercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.PercolateGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Percolate() into any of the following paths: \r\n - /{index}/{type}/_percolate");
		}
		
		
		internal ConnectionStatus ScrollDispatch(ElasticSearchPathInfo<ScrollQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ScrollGet(pathInfo.ScrollId,u => pathInfo.QueryString);
					//GET /_search/scroll
					return this.Raw.ScrollGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty() && body != null)
						return this.Raw.ScrollPost(pathInfo.ScrollId,body,u => pathInfo.QueryString);
					//POST /_search/scroll
					if (body != null)
						return this.Raw.ScrollPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Scroll() into any of the following paths: \r\n - /_search/scroll\r\n - /_search/scroll/{scroll_id}");
		}
		
		
		internal Task<ConnectionStatus> ScrollDispatchAsync(ElasticSearchPathInfo<ScrollQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty())
						return this.Raw.ScrollGetAsync(pathInfo.ScrollId,u => pathInfo.QueryString);
					//GET /_search/scroll
					return this.Raw.ScrollGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /_search/scroll/{scroll_id}
					if (!pathInfo.ScrollId.IsNullOrEmpty() && body != null)
						return this.Raw.ScrollPostAsync(pathInfo.ScrollId,body,u => pathInfo.QueryString);
					//POST /_search/scroll
					if (body != null)
						return this.Raw.ScrollPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Scroll() into any of the following paths: \r\n - /_search/scroll\r\n - /_search/scroll/{scroll_id}");
		}
		
		
		internal ConnectionStatus SearchDispatch(ElasticSearchPathInfo<SearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.SearchGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SearchGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_search
					return this.Raw.SearchGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_search
					if (body != null)
						return this.Raw.SearchPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Search() into any of the following paths: \r\n - /_search\r\n - /{index}/_search\r\n - /{index}/{type}/_search");
		}
		
		
		internal Task<ConnectionStatus> SearchDispatchAsync(ElasticSearchPathInfo<SearchQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.SearchGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SearchGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_search
					return this.Raw.SearchGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_search
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SearchPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_search
					if (body != null)
						return this.Raw.SearchPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Search() into any of the following paths: \r\n - /_search\r\n - /{index}/_search\r\n - /{index}/{type}/_search");
		}
		
		
		internal ConnectionStatus SuggestDispatch(ElasticSearchPathInfo<SuggestQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SuggestPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_suggest
					if (body != null)
						return this.Raw.SuggestPost(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					//GET /{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SuggestGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_suggest
					return this.Raw.SuggestGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Suggest() into any of the following paths: \r\n - /_suggest\r\n - /{index}/_suggest");
		}
		
		
		internal Task<ConnectionStatus> SuggestDispatchAsync(ElasticSearchPathInfo<SuggestQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.SuggestPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_suggest
					if (body != null)
						return this.Raw.SuggestPostAsync(body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.GET:
					//GET /{index}/_suggest
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.SuggestGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_suggest
					return this.Raw.SuggestGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Suggest() into any of the following paths: \r\n - /_suggest\r\n - /{index}/_suggest");
		}
		
		
		internal ConnectionStatus UpdateDispatch(ElasticSearchPathInfo<UpdateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_update
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.UpdatePost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Update() into any of the following paths: \r\n - /{index}/{type}/{id}/_update");
		}
		
		
		internal Task<ConnectionStatus> UpdateDispatchAsync(ElasticSearchPathInfo<UpdateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_update
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.UpdatePostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Update() into any of the following paths: \r\n - /{index}/{type}/{id}/_update");
		}
		
	}	
}
