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
	
		
		internal ConnectionStatus BulkDispatch(ElasticsearchPathInfo<BulkQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> BulkDispatchAsync(ElasticsearchPathInfo<BulkQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus CatAliasesDispatch(ElasticsearchPathInfo<CatAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/aliases/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.CatAliasesGet(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_cat/aliases
					return this.Raw.CatAliasesGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatAliases() into any of the following paths: \r\n - /_cat/aliases\r\n - /_cat/aliases/{name}");
		}
		
		
		internal Task<ConnectionStatus> CatAliasesDispatchAsync(ElasticsearchPathInfo<CatAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/aliases/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.CatAliasesGetAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_cat/aliases
					return this.Raw.CatAliasesGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatAliases() into any of the following paths: \r\n - /_cat/aliases\r\n - /_cat/aliases/{name}");
		}
		
		
		internal ConnectionStatus CatAllocationDispatch(ElasticsearchPathInfo<CatAllocationQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/allocation/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.CatAllocationGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cat/allocation
					return this.Raw.CatAllocationGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatAllocation() into any of the following paths: \r\n - /_cat/allocation\r\n - /_cat/allocation/{node_id}");
		}
		
		
		internal Task<ConnectionStatus> CatAllocationDispatchAsync(ElasticsearchPathInfo<CatAllocationQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/allocation/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.CatAllocationGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cat/allocation
					return this.Raw.CatAllocationGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatAllocation() into any of the following paths: \r\n - /_cat/allocation\r\n - /_cat/allocation/{node_id}");
		}
		
		
		internal ConnectionStatus CatCountDispatch(ElasticsearchPathInfo<CatCountQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/count/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatCountGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/count
					return this.Raw.CatCountGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatCount() into any of the following paths: \r\n - /_cat/count\r\n - /_cat/count/{index}");
		}
		
		
		internal Task<ConnectionStatus> CatCountDispatchAsync(ElasticsearchPathInfo<CatCountQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/count/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatCountGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/count
					return this.Raw.CatCountGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatCount() into any of the following paths: \r\n - /_cat/count\r\n - /_cat/count/{index}");
		}
		
		
		internal ConnectionStatus CatHealthDispatch(ElasticsearchPathInfo<CatHealthQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/health
					return this.Raw.CatHealthGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatHealth() into any of the following paths: \r\n - /_cat/health");
		}
		
		
		internal Task<ConnectionStatus> CatHealthDispatchAsync(ElasticsearchPathInfo<CatHealthQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/health
					return this.Raw.CatHealthGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatHealth() into any of the following paths: \r\n - /_cat/health");
		}
		
		
		internal ConnectionStatus CatHelpDispatch(ElasticsearchPathInfo<CatHelpQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat
					return this.Raw.CatHelpGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatHelp() into any of the following paths: \r\n - /_cat");
		}
		
		
		internal Task<ConnectionStatus> CatHelpDispatchAsync(ElasticsearchPathInfo<CatHelpQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat
					return this.Raw.CatHelpGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatHelp() into any of the following paths: \r\n - /_cat");
		}
		
		
		internal ConnectionStatus CatIndicesDispatch(ElasticsearchPathInfo<CatIndicesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/indices/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatIndicesGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/indices
					return this.Raw.CatIndicesGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatIndices() into any of the following paths: \r\n - /_cat/indices\r\n - /_cat/indices/{index}");
		}
		
		
		internal Task<ConnectionStatus> CatIndicesDispatchAsync(ElasticsearchPathInfo<CatIndicesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/indices/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatIndicesGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/indices
					return this.Raw.CatIndicesGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatIndices() into any of the following paths: \r\n - /_cat/indices\r\n - /_cat/indices/{index}");
		}
		
		
		internal ConnectionStatus CatMasterDispatch(ElasticsearchPathInfo<CatMasterQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/master
					return this.Raw.CatMasterGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatMaster() into any of the following paths: \r\n - /_cat/master");
		}
		
		
		internal Task<ConnectionStatus> CatMasterDispatchAsync(ElasticsearchPathInfo<CatMasterQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/master
					return this.Raw.CatMasterGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatMaster() into any of the following paths: \r\n - /_cat/master");
		}
		
		
		internal ConnectionStatus CatNodesDispatch(ElasticsearchPathInfo<CatNodesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/nodes
					return this.Raw.CatNodesGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatNodes() into any of the following paths: \r\n - /_cat/nodes");
		}
		
		
		internal Task<ConnectionStatus> CatNodesDispatchAsync(ElasticsearchPathInfo<CatNodesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/nodes
					return this.Raw.CatNodesGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatNodes() into any of the following paths: \r\n - /_cat/nodes");
		}
		
		
		internal ConnectionStatus CatPendingTasksDispatch(ElasticsearchPathInfo<CatPendingTasksQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/pending_tasks
					return this.Raw.CatPendingTasksGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatPendingTasks() into any of the following paths: \r\n - /_cat/pending_tasks");
		}
		
		
		internal Task<ConnectionStatus> CatPendingTasksDispatchAsync(ElasticsearchPathInfo<CatPendingTasksQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/pending_tasks
					return this.Raw.CatPendingTasksGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatPendingTasks() into any of the following paths: \r\n - /_cat/pending_tasks");
		}
		
		
		internal ConnectionStatus CatRecoveryDispatch(ElasticsearchPathInfo<CatRecoveryQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/recovery/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatRecoveryGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/recovery
					return this.Raw.CatRecoveryGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatRecovery() into any of the following paths: \r\n - /_cat/recovery\r\n - /_cat/recovery/{index}");
		}
		
		
		internal Task<ConnectionStatus> CatRecoveryDispatchAsync(ElasticsearchPathInfo<CatRecoveryQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/recovery/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatRecoveryGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/recovery
					return this.Raw.CatRecoveryGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatRecovery() into any of the following paths: \r\n - /_cat/recovery\r\n - /_cat/recovery/{index}");
		}
		
		
		internal ConnectionStatus CatShardsDispatch(ElasticsearchPathInfo<CatShardsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/shards/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatShardsGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/shards
					return this.Raw.CatShardsGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatShards() into any of the following paths: \r\n - /_cat/shards\r\n - /_cat/shards/{index}");
		}
		
		
		internal Task<ConnectionStatus> CatShardsDispatchAsync(ElasticsearchPathInfo<CatShardsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/shards/{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.CatShardsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cat/shards
					return this.Raw.CatShardsGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatShards() into any of the following paths: \r\n - /_cat/shards\r\n - /_cat/shards/{index}");
		}
		
		
		internal ConnectionStatus CatThreadPoolDispatch(ElasticsearchPathInfo<CatThreadPoolQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/thread_pool
					return this.Raw.CatThreadPoolGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatThreadPool() into any of the following paths: \r\n - /_cat/thread_pool");
		}
		
		
		internal Task<ConnectionStatus> CatThreadPoolDispatchAsync(ElasticsearchPathInfo<CatThreadPoolQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cat/thread_pool
					return this.Raw.CatThreadPoolGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.CatThreadPool() into any of the following paths: \r\n - /_cat/thread_pool");
		}
		
		
		internal ConnectionStatus ClearScrollDispatch(ElasticsearchPathInfo<ClearScrollQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> ClearScrollDispatchAsync(ElasticsearchPathInfo<ClearScrollQueryString> pathInfo )
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
		
		
		internal ConnectionStatus ClusterGetSettingsDispatch(ElasticsearchPathInfo<ClusterGetSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/settings
					return this.Raw.ClusterGetSettings(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterGetSettings() into any of the following paths: \r\n - /_cluster/settings");
		}
		
		
		internal Task<ConnectionStatus> ClusterGetSettingsDispatchAsync(ElasticsearchPathInfo<ClusterGetSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/settings
					return this.Raw.ClusterGetSettingsAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterGetSettings() into any of the following paths: \r\n - /_cluster/settings");
		}
		
		
		internal ConnectionStatus ClusterHealthDispatch(ElasticsearchPathInfo<ClusterHealthQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> ClusterHealthDispatchAsync(ElasticsearchPathInfo<ClusterHealthQueryString> pathInfo )
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
		
		
		internal ConnectionStatus ClusterPendingTasksDispatch(ElasticsearchPathInfo<ClusterPendingTasksQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/pending_tasks
					return this.Raw.ClusterPendingTasksGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterPendingTasks() into any of the following paths: \r\n - /_cluster/pending_tasks");
		}
		
		
		internal Task<ConnectionStatus> ClusterPendingTasksDispatchAsync(ElasticsearchPathInfo<ClusterPendingTasksQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/pending_tasks
					return this.Raw.ClusterPendingTasksGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterPendingTasks() into any of the following paths: \r\n - /_cluster/pending_tasks");
		}
		
		
		internal ConnectionStatus ClusterPutSettingsDispatch(ElasticsearchPathInfo<ClusterPutSettingsQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> ClusterPutSettingsDispatchAsync(ElasticsearchPathInfo<ClusterPutSettingsQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus ClusterRerouteDispatch(ElasticsearchPathInfo<ClusterRerouteQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> ClusterRerouteDispatchAsync(ElasticsearchPathInfo<ClusterRerouteQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus ClusterStateDispatch(ElasticsearchPathInfo<ClusterStateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/state/{metric}/{index}
					if (!pathInfo.Metric.IsNullOrEmpty() && !pathInfo.Index.IsNullOrEmpty())
						return this.Raw.ClusterStateGet(pathInfo.Metric,pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cluster/state/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.ClusterStateGet(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_cluster/state
					return this.Raw.ClusterStateGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterState() into any of the following paths: \r\n - /_cluster/state\r\n - /_cluster/state/{metric}\r\n - /_cluster/state/{metric}/{index}");
		}
		
		
		internal Task<ConnectionStatus> ClusterStateDispatchAsync(ElasticsearchPathInfo<ClusterStateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/state/{metric}/{index}
					if (!pathInfo.Metric.IsNullOrEmpty() && !pathInfo.Index.IsNullOrEmpty())
						return this.Raw.ClusterStateGetAsync(pathInfo.Metric,pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cluster/state/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.ClusterStateGetAsync(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_cluster/state
					return this.Raw.ClusterStateGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterState() into any of the following paths: \r\n - /_cluster/state\r\n - /_cluster/state/{metric}\r\n - /_cluster/state/{metric}/{index}");
		}
		
		
		internal ConnectionStatus ClusterStatsDispatch(ElasticsearchPathInfo<ClusterStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/stats/nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterStatsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/stats
					return this.Raw.ClusterStatsGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterStats() into any of the following paths: \r\n - /_cluster/stats\r\n - /_cluster/stats/nodes/{node_id}");
		}
		
		
		internal Task<ConnectionStatus> ClusterStatsDispatchAsync(ElasticsearchPathInfo<ClusterStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/stats/nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.ClusterStatsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/stats
					return this.Raw.ClusterStatsGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.ClusterStats() into any of the following paths: \r\n - /_cluster/stats\r\n - /_cluster/stats/nodes/{node_id}");
		}
		
		
		internal ConnectionStatus CountDispatch(ElasticsearchPathInfo<CountQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> CountDispatchAsync(ElasticsearchPathInfo<CountQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus CountPercolateDispatch(ElasticsearchPathInfo<CountPercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.CountPercolateGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					//GET /{index}/{type}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.CountPercolateGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CountPercolatePost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CountPercolatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.CountPercolate() into any of the following paths: \r\n - /{index}/{type}/_percolate/count\r\n - /{index}/{type}/{id}/_percolate/count");
		}
		
		
		internal Task<ConnectionStatus> CountPercolateDispatchAsync(ElasticsearchPathInfo<CountPercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.CountPercolateGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					//GET /{index}/{type}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.CountPercolateGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.CountPercolatePostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}/_percolate/count
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.CountPercolatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.CountPercolate() into any of the following paths: \r\n - /{index}/{type}/_percolate/count\r\n - /{index}/{type}/{id}/_percolate/count");
		}
		
		
		internal ConnectionStatus DeleteDispatch(ElasticsearchPathInfo<DeleteQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> DeleteDispatchAsync(ElasticsearchPathInfo<DeleteQueryString> pathInfo )
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
		
		
		internal ConnectionStatus DeleteByQueryDispatch(ElasticsearchPathInfo<DeleteByQueryQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> DeleteByQueryDispatchAsync(ElasticsearchPathInfo<DeleteByQueryQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus ExistsDispatch(ElasticsearchPathInfo<ExistsQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> ExistsDispatchAsync(ElasticsearchPathInfo<ExistsQueryString> pathInfo )
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
		
		
		internal ConnectionStatus ExplainDispatch(ElasticsearchPathInfo<ExplainQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> ExplainDispatchAsync(ElasticsearchPathInfo<ExplainQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus GetDispatch(ElasticsearchPathInfo<GetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.Get(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Get() into any of the following paths: \r\n - /{index}/{type}/{id}");
		}
		
		
		internal Task<ConnectionStatus> GetDispatchAsync(ElasticsearchPathInfo<GetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.GetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Get() into any of the following paths: \r\n - /{index}/{type}/{id}");
		}
		
		
		internal ConnectionStatus GetSourceDispatch(ElasticsearchPathInfo<GetSourceQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> GetSourceDispatchAsync(ElasticsearchPathInfo<GetSourceQueryString> pathInfo )
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
		
		
		internal ConnectionStatus IndexDispatch(ElasticsearchPathInfo<IndexQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> IndexDispatchAsync(ElasticsearchPathInfo<IndexQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus IndicesAnalyzeDispatch(ElasticsearchPathInfo<AnalyzeQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesAnalyzeGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_analyze
					return this.Raw.IndicesAnalyzeGetForAll(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesAnalyzePost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_analyze
					if (body != null)
						return this.Raw.IndicesAnalyzePostForAll(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesAnalyze() into any of the following paths: \r\n - /_analyze\r\n - /{index}/_analyze");
		}
		
		
		internal Task<ConnectionStatus> IndicesAnalyzeDispatchAsync(ElasticsearchPathInfo<AnalyzeQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesAnalyzeGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_analyze
					return this.Raw.IndicesAnalyzeGetForAllAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/_analyze
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesAnalyzePostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_analyze
					if (body != null)
						return this.Raw.IndicesAnalyzePostForAllAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesAnalyze() into any of the following paths: \r\n - /_analyze\r\n - /{index}/_analyze");
		}
		
		
		internal ConnectionStatus IndicesClearCacheDispatch(ElasticsearchPathInfo<ClearCacheQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCachePost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_cache/clear
					return this.Raw.IndicesClearCachePostForAll(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCacheGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cache/clear
					return this.Raw.IndicesClearCacheGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesClearCache() into any of the following paths: \r\n - /_cache/clear\r\n - /{index}/_cache/clear");
		}
		
		
		internal Task<ConnectionStatus> IndicesClearCacheDispatchAsync(ElasticsearchPathInfo<ClearCacheQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCachePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_cache/clear
					return this.Raw.IndicesClearCachePostForAllAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_cache/clear
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesClearCacheGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_cache/clear
					return this.Raw.IndicesClearCacheGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesClearCache() into any of the following paths: \r\n - /_cache/clear\r\n - /{index}/_cache/clear");
		}
		
		
		internal ConnectionStatus IndicesCloseDispatch(ElasticsearchPathInfo<CloseIndexQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> IndicesCloseDispatchAsync(ElasticsearchPathInfo<CloseIndexQueryString> pathInfo )
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
		
		
		internal ConnectionStatus IndicesCreateDispatch(ElasticsearchPathInfo<CreateIndexQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> IndicesCreateDispatchAsync(ElasticsearchPathInfo<CreateIndexQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus IndicesDeleteDispatch(ElasticsearchPathInfo<DeleteIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDelete(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDelete() into any of the following paths: \r\n - /{index}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteDispatchAsync(ElasticsearchPathInfo<DeleteIndexQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDelete() into any of the following paths: \r\n - /{index}");
		}
		
		
		internal ConnectionStatus IndicesDeleteAliasDispatch(ElasticsearchPathInfo<IndicesDeleteAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAlias(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteAlias() into any of the following paths: \r\n - /{index}/_alias/{name}\r\n - /{index}/_aliases/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteAliasDispatchAsync(ElasticsearchPathInfo<IndicesDeleteAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteAliasAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteAlias() into any of the following paths: \r\n - /{index}/_alias/{name}\r\n - /{index}/_aliases/{name}");
		}
		
		
		internal ConnectionStatus IndicesDeleteMappingDispatch(ElasticsearchPathInfo<DeleteMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping\r\n - /{index}/{type}\r\n - /{index}/_mapping/{type}\r\n - /{index}/{type}/_mappings\r\n - /{index}/_mappings/{type}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteMappingDispatchAsync(ElasticsearchPathInfo<DeleteMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesDeleteMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping\r\n - /{index}/{type}\r\n - /{index}/_mapping/{type}\r\n - /{index}/{type}/_mappings\r\n - /{index}/_mappings/{type}");
		}
		
		
		internal ConnectionStatus IndicesDeleteTemplateDispatch(ElasticsearchPathInfo<DeleteTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteTemplateForAll(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteTemplateDispatchAsync(ElasticsearchPathInfo<DeleteTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteTemplateForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesDeleteWarmerDispatch(ElasticsearchPathInfo<DeleteWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmer(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteWarmer() into any of the following paths: \r\n - /{index}/_warmer/{name}\r\n - /{index}/_warmers/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesDeleteWarmerDispatchAsync(ElasticsearchPathInfo<DeleteWarmerQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesDeleteWarmerAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesDeleteWarmer() into any of the following paths: \r\n - /{index}/_warmer/{name}\r\n - /{index}/_warmers/{name}");
		}
		
		
		internal ConnectionStatus IndicesExistsDispatch(ElasticsearchPathInfo<IndexExistsQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> IndicesExistsDispatchAsync(ElasticsearchPathInfo<IndexExistsQueryString> pathInfo )
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
		
		
		internal ConnectionStatus IndicesExistsAliasDispatch(ElasticsearchPathInfo<IndicesExistsAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHead(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//HEAD /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadForAll(pathInfo.Name,u => pathInfo.QueryString);
					//HEAD /{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHead(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsAlias() into any of the following paths: \r\n - /_alias/{name}\r\n - /{index}/_alias/{name}\r\n - /{index}/_alias");
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsAliasDispatchAsync(ElasticsearchPathInfo<IndicesExistsAliasQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//HEAD /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					//HEAD /{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesExistsAliasHeadAsync(pathInfo.Index,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsAlias() into any of the following paths: \r\n - /_alias/{name}\r\n - /{index}/_alias/{name}\r\n - /{index}/_alias");
		}
		
		
		internal ConnectionStatus IndicesExistsTemplateDispatch(ElasticsearchPathInfo<IndicesExistsTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsTemplateHeadForAll(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesExistsTemplateDispatchAsync(ElasticsearchPathInfo<IndicesExistsTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesExistsTemplateHeadForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesExistsTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesExistsTypeDispatch(ElasticsearchPathInfo<IndicesExistsTypeQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> IndicesExistsTypeDispatchAsync(ElasticsearchPathInfo<IndicesExistsTypeQueryString> pathInfo )
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
		
		
		internal ConnectionStatus IndicesFlushDispatch(ElasticsearchPathInfo<FlushQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushPost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_flush
					return this.Raw.IndicesFlushPostForAll(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_flush
					return this.Raw.IndicesFlushGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesFlush() into any of the following paths: \r\n - /_flush\r\n - /{index}/_flush");
		}
		
		
		internal Task<ConnectionStatus> IndicesFlushDispatchAsync(ElasticsearchPathInfo<FlushQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_flush
					return this.Raw.IndicesFlushPostForAllAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_flush
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesFlushGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_flush
					return this.Raw.IndicesFlushGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesFlush() into any of the following paths: \r\n - /_flush\r\n - /{index}/_flush");
		}
		
		
		internal ConnectionStatus IndicesGetAliasDispatch(ElasticsearchPathInfo<GetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAlias(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasForAll(pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAlias(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_alias
					return this.Raw.IndicesGetAliasForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAlias() into any of the following paths: \r\n - /_alias\r\n - /_alias/{name}\r\n - /{index}/_alias/{name}\r\n - /{index}/_alias");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetAliasDispatchAsync(ElasticsearchPathInfo<GetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_alias
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_alias
					return this.Raw.IndicesGetAliasForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAlias() into any of the following paths: \r\n - /_alias\r\n - /_alias/{name}\r\n - /{index}/_alias/{name}\r\n - /{index}/_alias");
		}
		
		
		internal ConnectionStatus IndicesGetAliasesDispatch(ElasticsearchPathInfo<IndicesGetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_aliases/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliases(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_aliases
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliases(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_aliases/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasesForAll(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_aliases
					return this.Raw.IndicesGetAliasesForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAliases() into any of the following paths: \r\n - /_aliases\r\n - /{index}/_aliases\r\n - /{index}/_aliases/{name}\r\n - /_aliases/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetAliasesDispatchAsync(ElasticsearchPathInfo<IndicesGetAliasesQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_aliases/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasesAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_aliases
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasesAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_aliases/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetAliasesForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_aliases
					return this.Raw.IndicesGetAliasesForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetAliases() into any of the following paths: \r\n - /_aliases\r\n - /{index}/_aliases\r\n - /{index}/_aliases/{name}\r\n - /_aliases/{name}");
		}
		
		
		internal ConnectionStatus IndicesGetFieldMappingDispatch(ElasticsearchPathInfo<IndicesGetFieldMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_mapping/{type}/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					//GET /{index}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMapping(pathInfo.Index,pathInfo.Field,u => pathInfo.QueryString);
					//GET /_mapping/{type}/field/{field}
					if (!pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingForAll(pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					//GET /_mapping/field/{field}
					if (!pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingForAll(pathInfo.Field,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetFieldMapping() into any of the following paths: \r\n - /_mapping/field/{field}\r\n - /{index}/_mapping/field/{field}\r\n - /_mapping/{type}/field/{field}\r\n - /{index}/_mapping/{type}/field/{field}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetFieldMappingDispatchAsync(ElasticsearchPathInfo<IndicesGetFieldMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_mapping/{type}/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					//GET /{index}/_mapping/field/{field}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingAsync(pathInfo.Index,pathInfo.Field,u => pathInfo.QueryString);
					//GET /_mapping/{type}/field/{field}
					if (!pathInfo.Type.IsNullOrEmpty() && !pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingForAllAsync(pathInfo.Type,pathInfo.Field,u => pathInfo.QueryString);
					//GET /_mapping/field/{field}
					if (!pathInfo.Field.IsNullOrEmpty())
						return this.Raw.IndicesGetFieldMappingForAllAsync(pathInfo.Field,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetFieldMapping() into any of the following paths: \r\n - /_mapping/field/{field}\r\n - /{index}/_mapping/field/{field}\r\n - /_mapping/{type}/field/{field}\r\n - /{index}/_mapping/{type}/field/{field}");
		}
		
		
		internal ConnectionStatus IndicesGetMappingDispatch(ElasticsearchPathInfo<GetMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_mapping/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMapping(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetMapping(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mapping/{type}
					if (!pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingForAll(pathInfo.Type,u => pathInfo.QueryString);
					//GET /_mapping
					return this.Raw.IndicesGetMappingForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetMapping() into any of the following paths: \r\n - /_mapping\r\n - /{index}/_mapping\r\n - /_mapping/{type}\r\n - /{index}/_mapping/{type}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetMappingDispatchAsync(ElasticsearchPathInfo<GetMappingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_mapping/{type}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mapping/{type}
					if (!pathInfo.Type.IsNullOrEmpty())
						return this.Raw.IndicesGetMappingForAllAsync(pathInfo.Type,u => pathInfo.QueryString);
					//GET /_mapping
					return this.Raw.IndicesGetMappingForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetMapping() into any of the following paths: \r\n - /_mapping\r\n - /{index}/_mapping\r\n - /_mapping/{type}\r\n - /{index}/_mapping/{type}");
		}
		
		
		internal ConnectionStatus IndicesGetSettingsDispatch(ElasticsearchPathInfo<GetIndexSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_settings/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetSettings(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetSettings(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_settings/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetSettingsForAll(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_settings
					return this.Raw.IndicesGetSettingsForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings\r\n - /{index}/_settings/{name}\r\n - /_settings/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetSettingsDispatchAsync(ElasticsearchPathInfo<GetIndexSettingsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_settings/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetSettingsAsync(pathInfo.Index,pathInfo.Name,u => pathInfo.QueryString);
					//GET /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesGetSettingsAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_settings/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetSettingsForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_settings
					return this.Raw.IndicesGetSettingsForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings\r\n - /{index}/_settings/{name}\r\n - /_settings/{name}");
		}
		
		
		internal ConnectionStatus IndicesGetTemplateDispatch(ElasticsearchPathInfo<GetTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetTemplateForAll(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_template
					return this.Raw.IndicesGetTemplateForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetTemplate() into any of the following paths: \r\n - /_template\r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetTemplateDispatchAsync(ElasticsearchPathInfo<GetTemplateQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetTemplateForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_template
					return this.Raw.IndicesGetTemplateForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetTemplate() into any of the following paths: \r\n - /_template\r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesGetWarmerDispatch(ElasticsearchPathInfo<GetWarmerQueryString> pathInfo )
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
					//GET /_warmer/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerForAll(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_warmer
					return this.Raw.IndicesGetWarmerForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetWarmer() into any of the following paths: \r\n - /_warmer\r\n - /{index}/_warmer\r\n - /{index}/_warmer/{name}\r\n - /_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesGetWarmerDispatchAsync(ElasticsearchPathInfo<GetWarmerQueryString> pathInfo )
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
					//GET /_warmer/{name}
					if (!pathInfo.Name.IsNullOrEmpty())
						return this.Raw.IndicesGetWarmerForAllAsync(pathInfo.Name,u => pathInfo.QueryString);
					//GET /_warmer
					return this.Raw.IndicesGetWarmerForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesGetWarmer() into any of the following paths: \r\n - /_warmer\r\n - /{index}/_warmer\r\n - /{index}/_warmer/{name}\r\n - /_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}");
		}
		
		
		internal ConnectionStatus IndicesOpenDispatch(ElasticsearchPathInfo<OpenIndexQueryString> pathInfo )
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
		
		
		internal Task<ConnectionStatus> IndicesOpenDispatchAsync(ElasticsearchPathInfo<OpenIndexQueryString> pathInfo )
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
		
		
		internal ConnectionStatus IndicesOptimizeDispatch(ElasticsearchPathInfo<OptimizeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizePost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_optimize
					return this.Raw.IndicesOptimizePostForAll(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizeGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_optimize
					return this.Raw.IndicesOptimizeGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesOptimize() into any of the following paths: \r\n - /_optimize\r\n - /{index}/_optimize");
		}
		
		
		internal Task<ConnectionStatus> IndicesOptimizeDispatchAsync(ElasticsearchPathInfo<OptimizeQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizePostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_optimize
					return this.Raw.IndicesOptimizePostForAllAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_optimize
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesOptimizeGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_optimize
					return this.Raw.IndicesOptimizeGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesOptimize() into any of the following paths: \r\n - /_optimize\r\n - /{index}/_optimize");
		}
		
		
		internal ConnectionStatus IndicesPutAliasDispatch(ElasticsearchPathInfo<IndicesPutAliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAlias(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasForAll(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasPost(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//POST /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasPostForAll(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutAlias() into any of the following paths: \r\n - /{index}/_alias/{name}\r\n - /_alias/{name}\r\n - /{index}/_aliases/{name}\r\n - /_aliases/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutAliasDispatchAsync(ElasticsearchPathInfo<IndicesPutAliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//PUT /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasForAllAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/_alias/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasPostAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//POST /_alias/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutAliasPostForAllAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutAlias() into any of the following paths: \r\n - /{index}/_alias/{name}\r\n - /_alias/{name}\r\n - /{index}/_aliases/{name}\r\n - /_aliases/{name}");
		}
		
		
		internal ConnectionStatus IndicesPutMappingDispatch(ElasticsearchPathInfo<PutMappingQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMapping(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//PUT /_mapping/{type}
					if (!pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingForAll(pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /_mapping/{type}
					if (!pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPostForAll(pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping\r\n - /{index}/_mapping/{type}\r\n - /_mapping/{type}\r\n - /{index}/{type}/_mappings\r\n - /{index}/_mappings/{type}\r\n - /_mappings/{type}");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutMappingDispatchAsync(ElasticsearchPathInfo<PutMappingQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//PUT /_mapping/{type}
					if (!pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingForAllAsync(pathInfo.Type,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mapping
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /_mapping/{type}
					if (!pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutMappingPostForAllAsync(pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutMapping() into any of the following paths: \r\n - /{index}/{type}/_mapping\r\n - /{index}/_mapping/{type}\r\n - /_mapping/{type}\r\n - /{index}/{type}/_mappings\r\n - /{index}/_mappings/{type}\r\n - /_mappings/{type}");
		}
		
		
		internal ConnectionStatus IndicesPutSettingsDispatch(ElasticsearchPathInfo<UpdateSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutSettings(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_settings
					if (body != null)
						return this.Raw.IndicesPutSettingsForAll(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutSettingsDispatchAsync(ElasticsearchPathInfo<UpdateSettingsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /{index}/_settings
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutSettingsAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//PUT /_settings
					if (body != null)
						return this.Raw.IndicesPutSettingsForAllAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutSettings() into any of the following paths: \r\n - /_settings\r\n - /{index}/_settings");
		}
		
		
		internal ConnectionStatus IndicesPutTemplateDispatch(ElasticsearchPathInfo<PutTemplateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplateForAll(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplatePostForAll(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutTemplateDispatchAsync(ElasticsearchPathInfo<PutTemplateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplateForAllAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_template/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutTemplatePostForAllAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutTemplate() into any of the following paths: \r\n - /_template/{name}");
		}
		
		
		internal ConnectionStatus IndicesPutWarmerDispatch(ElasticsearchPathInfo<PutWarmerQueryString> pathInfo , object body)
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
					//PUT /_warmer/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerForAll(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerPost(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.QueryString);
					//POST /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerPost(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//POST /_warmer/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerPostForAll(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutWarmer() into any of the following paths: \r\n - /_warmer/{name}\r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}\r\n - /_warmers/{name}\r\n - /{index}/_warmers/{name}\r\n - /{index}/{type}/_warmers/{name}");
		}
		
		
		internal Task<ConnectionStatus> IndicesPutWarmerDispatchAsync(ElasticsearchPathInfo<PutWarmerQueryString> pathInfo , object body)
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
					//PUT /_warmer/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerForAllAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.QueryString);
					//POST /{index}/_warmer/{name}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerPostAsync(pathInfo.Index,pathInfo.Name,body,u => pathInfo.QueryString);
					//POST /_warmer/{name}
					if (!pathInfo.Name.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesPutWarmerPostForAllAsync(pathInfo.Name,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesPutWarmer() into any of the following paths: \r\n - /_warmer/{name}\r\n - /{index}/_warmer/{name}\r\n - /{index}/{type}/_warmer/{name}\r\n - /_warmers/{name}\r\n - /{index}/_warmers/{name}\r\n - /{index}/{type}/_warmers/{name}");
		}
		
		
		internal ConnectionStatus IndicesRefreshDispatch(ElasticsearchPathInfo<RefreshQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshPost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_refresh
					return this.Raw.IndicesRefreshPostForAll(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_refresh
					return this.Raw.IndicesRefreshGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesRefresh() into any of the following paths: \r\n - /_refresh\r\n - /{index}/_refresh");
		}
		
		
		internal Task<ConnectionStatus> IndicesRefreshDispatchAsync(ElasticsearchPathInfo<RefreshQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_refresh
					return this.Raw.IndicesRefreshPostForAllAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.GET:
					//GET /{index}/_refresh
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesRefreshGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_refresh
					return this.Raw.IndicesRefreshGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesRefresh() into any of the following paths: \r\n - /_refresh\r\n - /{index}/_refresh");
		}
		
		
		internal ConnectionStatus IndicesSegmentsDispatch(ElasticsearchPathInfo<SegmentsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_segments
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSegmentsGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_segments
					return this.Raw.IndicesSegmentsGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSegments() into any of the following paths: \r\n - /_segments\r\n - /{index}/_segments");
		}
		
		
		internal Task<ConnectionStatus> IndicesSegmentsDispatchAsync(ElasticsearchPathInfo<SegmentsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_segments
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSegmentsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_segments
					return this.Raw.IndicesSegmentsGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSegments() into any of the following paths: \r\n - /_segments\r\n - /{index}/_segments");
		}
		
		
		internal ConnectionStatus IndicesSnapshotIndexDispatch(ElasticsearchPathInfo<SnapshotQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_gateway/snapshot
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSnapshotIndexPost(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_gateway/snapshot
					return this.Raw.IndicesSnapshotIndexPostForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSnapshotIndex() into any of the following paths: \r\n - /_gateway/snapshot\r\n - /{index}/_gateway/snapshot");
		}
		
		
		internal Task<ConnectionStatus> IndicesSnapshotIndexDispatchAsync(ElasticsearchPathInfo<SnapshotQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /{index}/_gateway/snapshot
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesSnapshotIndexPostAsync(pathInfo.Index,u => pathInfo.QueryString);
					//POST /_gateway/snapshot
					return this.Raw.IndicesSnapshotIndexPostForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesSnapshotIndex() into any of the following paths: \r\n - /_gateway/snapshot\r\n - /{index}/_gateway/snapshot");
		}
		
		
		internal ConnectionStatus IndicesStatsDispatch(ElasticsearchPathInfo<IndicesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_stats/{metric}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.IndicesStatsGet(pathInfo.Index,pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_stats/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.IndicesStatsGetForAll(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /{index}/_stats
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatsGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_stats
					return this.Raw.IndicesStatsGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStats() into any of the following paths: \r\n - /_stats\r\n - /_stats/{metric}\r\n - /{index}/_stats\r\n - /{index}/_stats/{metric}");
		}
		
		
		internal Task<ConnectionStatus> IndicesStatsDispatchAsync(ElasticsearchPathInfo<IndicesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_stats/{metric}
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.IndicesStatsGetAsync(pathInfo.Index,pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_stats/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.IndicesStatsGetForAllAsync(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /{index}/_stats
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_stats
					return this.Raw.IndicesStatsGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStats() into any of the following paths: \r\n - /_stats\r\n - /_stats/{metric}\r\n - /{index}/_stats\r\n - /{index}/_stats/{metric}");
		}
		
		
		internal ConnectionStatus IndicesStatusDispatch(ElasticsearchPathInfo<IndicesStatusQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_status
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatusGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_status
					return this.Raw.IndicesStatusGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStatus() into any of the following paths: \r\n - /_status\r\n - /{index}/_status");
		}
		
		
		internal Task<ConnectionStatus> IndicesStatusDispatchAsync(ElasticsearchPathInfo<IndicesStatusQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/_status
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.IndicesStatusGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_status
					return this.Raw.IndicesStatusGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesStatus() into any of the following paths: \r\n - /_status\r\n - /{index}/_status");
		}
		
		
		internal ConnectionStatus IndicesUpdateAliasesDispatch(ElasticsearchPathInfo<AliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_aliases
					if (body != null)
						return this.Raw.IndicesUpdateAliasesPostForAll(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesUpdateAliases() into any of the following paths: \r\n - /_aliases");
		}
		
		
		internal Task<ConnectionStatus> IndicesUpdateAliasesDispatchAsync(ElasticsearchPathInfo<AliasQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_aliases
					if (body != null)
						return this.Raw.IndicesUpdateAliasesPostForAllAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesUpdateAliases() into any of the following paths: \r\n - /_aliases");
		}
		
		
		internal ConnectionStatus IndicesValidateQueryDispatch(ElasticsearchPathInfo<ValidateQueryQueryString> pathInfo , object body)
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
					return this.Raw.IndicesValidateQueryGetForAll(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_validate/query
					if (body != null)
						return this.Raw.IndicesValidateQueryPostForAll(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesValidateQuery() into any of the following paths: \r\n - /_validate/query\r\n - /{index}/_validate/query\r\n - /{index}/{type}/_validate/query");
		}
		
		
		internal Task<ConnectionStatus> IndicesValidateQueryDispatchAsync(ElasticsearchPathInfo<ValidateQueryQueryString> pathInfo , object body)
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
					return this.Raw.IndicesValidateQueryGetForAllAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_validate/query
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.IndicesValidateQueryPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_validate/query
					if (body != null)
						return this.Raw.IndicesValidateQueryPostForAllAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.IndicesValidateQuery() into any of the following paths: \r\n - /_validate/query\r\n - /{index}/_validate/query\r\n - /{index}/{type}/_validate/query");
		}
		
		
		internal ConnectionStatus InfoDispatch(ElasticsearchPathInfo<InfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /
					return this.Raw.InfoGet(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Info() into any of the following paths: \r\n - /");
		}
		
		
		internal Task<ConnectionStatus> InfoDispatchAsync(ElasticsearchPathInfo<InfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /
					return this.Raw.InfoGetAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Info() into any of the following paths: \r\n - /");
		}
		
		
		internal ConnectionStatus MgetDispatch(ElasticsearchPathInfo<MultiGetQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> MgetDispatchAsync(ElasticsearchPathInfo<MultiGetQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus MltDispatch(ElasticsearchPathInfo<MoreLikeThisQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> MltDispatchAsync(ElasticsearchPathInfo<MoreLikeThisQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus MpercolateDispatch(ElasticsearchPathInfo<MpercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MpercolateGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MpercolateGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mpercolate
					return this.Raw.MpercolateGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MpercolatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MpercolatePost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_mpercolate
					if (body != null)
						return this.Raw.MpercolatePost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mpercolate() into any of the following paths: \r\n - /_mpercolate\r\n - /{index}/_mpercolate\r\n - /{index}/{type}/_mpercolate");
		}
		
		
		internal Task<ConnectionStatus> MpercolateDispatchAsync(ElasticsearchPathInfo<MpercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MpercolateGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MpercolateGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mpercolate
					return this.Raw.MpercolateGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MpercolatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_mpercolate
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MpercolatePostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_mpercolate
					if (body != null)
						return this.Raw.MpercolatePostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mpercolate() into any of the following paths: \r\n - /_mpercolate\r\n - /{index}/_mpercolate\r\n - /{index}/{type}/_mpercolate");
		}
		
		
		internal ConnectionStatus MsearchDispatch(ElasticsearchPathInfo<MultiSearchQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> MsearchDispatchAsync(ElasticsearchPathInfo<MultiSearchQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus MtermvectorsDispatch(ElasticsearchPathInfo<MtermvectorsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MtermvectorsGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MtermvectorsGet(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mtermvectors
					return this.Raw.MtermvectorsGet(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MtermvectorsPost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MtermvectorsPost(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_mtermvectors
					if (body != null)
						return this.Raw.MtermvectorsPost(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mtermvectors() into any of the following paths: \r\n - /_mtermvectors\r\n - /{index}/_mtermvectors\r\n - /{index}/{type}/_mtermvectors");
		}
		
		
		internal Task<ConnectionStatus> MtermvectorsDispatchAsync(ElasticsearchPathInfo<MtermvectorsQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.MtermvectorsGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					//GET /{index}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty())
						return this.Raw.MtermvectorsGetAsync(pathInfo.Index,u => pathInfo.QueryString);
					//GET /_mtermvectors
					return this.Raw.MtermvectorsGetAsync(u => pathInfo.QueryString);

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.MtermvectorsPostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					//POST /{index}/_mtermvectors
					if (!pathInfo.Index.IsNullOrEmpty() && body != null)
						return this.Raw.MtermvectorsPostAsync(pathInfo.Index,body,u => pathInfo.QueryString);
					//POST /_mtermvectors
					if (body != null)
						return this.Raw.MtermvectorsPostAsync(body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Mtermvectors() into any of the following paths: \r\n - /_mtermvectors\r\n - /{index}/_mtermvectors\r\n - /{index}/{type}/_mtermvectors");
		}
		
		
		internal ConnectionStatus NodesHotThreadsDispatch(ElasticsearchPathInfo<NodesHotThreadsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesHotThreadsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes/hotthreads
					return this.Raw.NodesHotThreadsGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesHotThreads() into any of the following paths: \r\n - /_cluster/nodes/hotthreads\r\n - /_cluster/nodes/hot_threads\r\n - /_cluster/nodes/{node_id}/hotthreads\r\n - /_cluster/nodes/{node_id}/hot_threads\r\n - /_nodes/hotthreads\r\n - /_nodes/hot_threads\r\n - /_nodes/{node_id}/hotthreads\r\n - /_nodes/{node_id}/hot_threads");
		}
		
		
		internal Task<ConnectionStatus> NodesHotThreadsDispatchAsync(ElasticsearchPathInfo<NodesHotThreadsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_cluster/nodes/{node_id}/hotthreads
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesHotThreadsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_cluster/nodes/hotthreads
					return this.Raw.NodesHotThreadsGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesHotThreads() into any of the following paths: \r\n - /_cluster/nodes/hotthreads\r\n - /_cluster/nodes/hot_threads\r\n - /_cluster/nodes/{node_id}/hotthreads\r\n - /_cluster/nodes/{node_id}/hot_threads\r\n - /_nodes/hotthreads\r\n - /_nodes/hot_threads\r\n - /_nodes/{node_id}/hotthreads\r\n - /_nodes/{node_id}/hot_threads");
		}
		
		
		internal ConnectionStatus NodesInfoDispatch(ElasticsearchPathInfo<NodesInfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_nodes/{node_id}/{metric}
					if (!pathInfo.NodeId.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesInfoGet(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesInfoGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_nodes/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesInfoGetForAll(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes
					return this.Raw.NodesInfoGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesInfo() into any of the following paths: \r\n - /_nodes\r\n - /_nodes/{node_id}\r\n - /_nodes/{metric}\r\n - /_nodes/{node_id}/{metric}");
		}
		
		
		internal Task<ConnectionStatus> NodesInfoDispatchAsync(ElasticsearchPathInfo<NodesInfoQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_nodes/{node_id}/{metric}
					if (!pathInfo.NodeId.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesInfoGetAsync(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes/{node_id}
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesInfoGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_nodes/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesInfoGetForAllAsync(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes
					return this.Raw.NodesInfoGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesInfo() into any of the following paths: \r\n - /_nodes\r\n - /_nodes/{node_id}\r\n - /_nodes/{metric}\r\n - /_nodes/{node_id}/{metric}");
		}
		
		
		internal ConnectionStatus NodesShutdownDispatch(ElasticsearchPathInfo<NodesShutdownQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_cluster/nodes/{node_id}/_shutdown
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesShutdownPost(pathInfo.NodeId,u => pathInfo.QueryString);
					//POST /_shutdown
					return this.Raw.NodesShutdownPostForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesShutdown() into any of the following paths: \r\n - /_shutdown\r\n - /_cluster/nodes/_shutdown\r\n - /_cluster/nodes/{node_id}/_shutdown");
		}
		
		
		internal Task<ConnectionStatus> NodesShutdownDispatchAsync(ElasticsearchPathInfo<NodesShutdownQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_cluster/nodes/{node_id}/_shutdown
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesShutdownPostAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//POST /_shutdown
					return this.Raw.NodesShutdownPostForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesShutdown() into any of the following paths: \r\n - /_shutdown\r\n - /_cluster/nodes/_shutdown\r\n - /_cluster/nodes/{node_id}/_shutdown");
		}
		
		
		internal ConnectionStatus NodesStatsDispatch(ElasticsearchPathInfo<NodesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_nodes/{node_id}/stats/{metric}/{index_metric}
					if (!pathInfo.NodeId.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty() && !pathInfo.IndexMetric.IsNullOrEmpty())
						return this.Raw.NodesStatsGet(pathInfo.NodeId,pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.QueryString);
					//GET /_nodes/{node_id}/stats/{metric}
					if (!pathInfo.NodeId.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesStatsGet(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes/stats/{metric}/{index_metric}
					if (!pathInfo.Metric.IsNullOrEmpty() && !pathInfo.IndexMetric.IsNullOrEmpty())
						return this.Raw.NodesStatsGetForAll(pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.QueryString);
					//GET /_nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesStatsGet(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_nodes/stats/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesStatsGetForAll(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes/stats
					return this.Raw.NodesStatsGetForAll(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesStats() into any of the following paths: \r\n - /_nodes/stats\r\n - /_nodes/{node_id}/stats\r\n - /_nodes/stats/{metric}\r\n - /_nodes/{node_id}/stats/{metric}\r\n - /_nodes/stats/{metric}/{index_metric}\r\n - /_nodes/{node_id}/stats/{metric}/{index_metric}");
		}
		
		
		internal Task<ConnectionStatus> NodesStatsDispatchAsync(ElasticsearchPathInfo<NodesStatsQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_nodes/{node_id}/stats/{metric}/{index_metric}
					if (!pathInfo.NodeId.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty() && !pathInfo.IndexMetric.IsNullOrEmpty())
						return this.Raw.NodesStatsGetAsync(pathInfo.NodeId,pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.QueryString);
					//GET /_nodes/{node_id}/stats/{metric}
					if (!pathInfo.NodeId.IsNullOrEmpty() && !pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesStatsGetAsync(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes/stats/{metric}/{index_metric}
					if (!pathInfo.Metric.IsNullOrEmpty() && !pathInfo.IndexMetric.IsNullOrEmpty())
						return this.Raw.NodesStatsGetForAllAsync(pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.QueryString);
					//GET /_nodes/{node_id}/stats
					if (!pathInfo.NodeId.IsNullOrEmpty())
						return this.Raw.NodesStatsGetAsync(pathInfo.NodeId,u => pathInfo.QueryString);
					//GET /_nodes/stats/{metric}
					if (!pathInfo.Metric.IsNullOrEmpty())
						return this.Raw.NodesStatsGetForAllAsync(pathInfo.Metric,u => pathInfo.QueryString);
					//GET /_nodes/stats
					return this.Raw.NodesStatsGetForAllAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.NodesStats() into any of the following paths: \r\n - /_nodes/stats\r\n - /_nodes/{node_id}/stats\r\n - /_nodes/stats/{metric}\r\n - /_nodes/{node_id}/stats/{metric}\r\n - /_nodes/stats/{metric}/{index_metric}\r\n - /_nodes/{node_id}/stats/{metric}/{index_metric}");
		}
		
		
		internal ConnectionStatus PercolateDispatch(ElasticsearchPathInfo<PercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.PercolateGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					//GET /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.PercolateGet(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePost(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Percolate() into any of the following paths: \r\n - /{index}/{type}/_percolate\r\n - /{index}/{type}/{id}/_percolate");
		}
		
		
		internal Task<ConnectionStatus> PercolateDispatchAsync(ElasticsearchPathInfo<PercolateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.PercolateGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					//GET /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
						return this.Raw.PercolateGetAsync(pathInfo.Index,pathInfo.Type,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					//POST /{index}/{type}/_percolate
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && body != null)
						return this.Raw.PercolatePostAsync(pathInfo.Index,pathInfo.Type,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Percolate() into any of the following paths: \r\n - /{index}/{type}/_percolate\r\n - /{index}/{type}/{id}/_percolate");
		}
		
		
		internal ConnectionStatus PingDispatch(ElasticsearchPathInfo<PingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /
					return this.Raw.PingHead(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Ping() into any of the following paths: \r\n - /");
		}
		
		
		internal Task<ConnectionStatus> PingDispatchAsync(ElasticsearchPathInfo<PingQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					//HEAD /
					return this.Raw.PingHeadAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.Ping() into any of the following paths: \r\n - /");
		}
		
		
		internal ConnectionStatus ScrollDispatch(ElasticsearchPathInfo<ScrollQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> ScrollDispatchAsync(ElasticsearchPathInfo<ScrollQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus SearchDispatch(ElasticsearchPathInfo<SearchQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> SearchDispatchAsync(ElasticsearchPathInfo<SearchQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus SnapshotCreateDispatch(ElasticsearchPathInfo<SnapshotCreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreatePut(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreatePost(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotCreate() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}\r\n - /_snapshot/{repository}/{snapshot}/_create");
		}
		
		
		internal Task<ConnectionStatus> SnapshotCreateDispatchAsync(ElasticsearchPathInfo<SnapshotCreateQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreatePutAsync(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreatePostAsync(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotCreate() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}\r\n - /_snapshot/{repository}/{snapshot}/_create");
		}
		
		
		internal ConnectionStatus SnapshotCreateRepositoryDispatch(ElasticsearchPathInfo<SnapshotCreateRepositoryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreateRepositoryPut(pathInfo.Repository,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreateRepositoryPost(pathInfo.Repository,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotCreateRepository() into any of the following paths: \r\n - /_snapshot/{repository}");
		}
		
		
		internal Task<ConnectionStatus> SnapshotCreateRepositoryDispatchAsync(ElasticsearchPathInfo<SnapshotCreateRepositoryQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					//PUT /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreateRepositoryPutAsync(pathInfo.Repository,body,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotCreateRepositoryPostAsync(pathInfo.Repository,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotCreateRepository() into any of the following paths: \r\n - /_snapshot/{repository}");
		}
		
		
		internal ConnectionStatus SnapshotDeleteDispatch(ElasticsearchPathInfo<SnapshotDeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty())
						return this.Raw.SnapshotDelete(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotDelete() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}");
		}
		
		
		internal Task<ConnectionStatus> SnapshotDeleteDispatchAsync(ElasticsearchPathInfo<SnapshotDeleteQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty())
						return this.Raw.SnapshotDeleteAsync(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotDelete() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}");
		}
		
		
		internal ConnectionStatus SnapshotDeleteRepositoryDispatch(ElasticsearchPathInfo<SnapshotDeleteRepositoryQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty())
						return this.Raw.SnapshotDeleteRepository(pathInfo.Repository,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotDeleteRepository() into any of the following paths: \r\n - /_snapshot/{repository}");
		}
		
		
		internal Task<ConnectionStatus> SnapshotDeleteRepositoryDispatchAsync(ElasticsearchPathInfo<SnapshotDeleteRepositoryQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					//DELETE /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty())
						return this.Raw.SnapshotDeleteRepositoryAsync(pathInfo.Repository,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotDeleteRepository() into any of the following paths: \r\n - /_snapshot/{repository}");
		}
		
		
		internal ConnectionStatus SnapshotGetDispatch(ElasticsearchPathInfo<SnapshotGetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty())
						return this.Raw.SnapshotGet(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotGet() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}");
		}
		
		
		internal Task<ConnectionStatus> SnapshotGetDispatchAsync(ElasticsearchPathInfo<SnapshotGetQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_snapshot/{repository}/{snapshot}
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty())
						return this.Raw.SnapshotGetAsync(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotGet() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}");
		}
		
		
		internal ConnectionStatus SnapshotGetRepositoryDispatch(ElasticsearchPathInfo<SnapshotGetRepositoryQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty())
						return this.Raw.SnapshotGetRepository(pathInfo.Repository,u => pathInfo.QueryString);
					//GET /_snapshot
					return this.Raw.SnapshotGetRepository(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotGetRepository() into any of the following paths: \r\n - /_snapshot\r\n - /_snapshot/{repository}");
		}
		
		
		internal Task<ConnectionStatus> SnapshotGetRepositoryDispatchAsync(ElasticsearchPathInfo<SnapshotGetRepositoryQueryString> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /_snapshot/{repository}
					if (!pathInfo.Repository.IsNullOrEmpty())
						return this.Raw.SnapshotGetRepositoryAsync(pathInfo.Repository,u => pathInfo.QueryString);
					//GET /_snapshot
					return this.Raw.SnapshotGetRepositoryAsync(u => pathInfo.QueryString);

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotGetRepository() into any of the following paths: \r\n - /_snapshot\r\n - /_snapshot/{repository}");
		}
		
		
		internal ConnectionStatus SnapshotRestoreDispatch(ElasticsearchPathInfo<SnapshotRestoreQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_snapshot/{repository}/{snapshot}/_restore
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotRestorePost(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotRestore() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}/_restore");
		}
		
		
		internal Task<ConnectionStatus> SnapshotRestoreDispatchAsync(ElasticsearchPathInfo<SnapshotRestoreQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					//POST /_snapshot/{repository}/{snapshot}/_restore
					if (!pathInfo.Repository.IsNullOrEmpty() && !pathInfo.Snapshot.IsNullOrEmpty() && body != null)
						return this.Raw.SnapshotRestorePostAsync(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.SnapshotRestore() into any of the following paths: \r\n - /_snapshot/{repository}/{snapshot}/_restore");
		}
		
		
		internal ConnectionStatus SuggestDispatch(ElasticsearchPathInfo<SuggestQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> SuggestDispatchAsync(ElasticsearchPathInfo<SuggestQueryString> pathInfo , object body)
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
		
		
		internal ConnectionStatus TermvectorDispatch(ElasticsearchPathInfo<TermvectorQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_termvector
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.TermvectorGet(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_termvector
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.TermvectorPost(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Termvector() into any of the following paths: \r\n - /{index}/{type}/{id}/_termvector");
		}
		
		
		internal Task<ConnectionStatus> TermvectorDispatchAsync(ElasticsearchPathInfo<TermvectorQueryString> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					//GET /{index}/{type}/{id}/_termvector
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty())
						return this.Raw.TermvectorGetAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.QueryString);
					break;

				case PathInfoHttpMethod.POST:
					//POST /{index}/{type}/{id}/_termvector
					if (!pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty() && !pathInfo.Id.IsNullOrEmpty() && body != null)
						return this.Raw.TermvectorPostAsync(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.QueryString);
					break;

			}
			throw new DispatchException("Could not dispatch IElasticClient.Termvector() into any of the following paths: \r\n - /{index}/{type}/{id}/_termvector");
		}
		
		
		internal ConnectionStatus UpdateDispatch(ElasticsearchPathInfo<UpdateQueryString> pathInfo , object body)
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
		
		
		internal Task<ConnectionStatus> UpdateDispatchAsync(ElasticsearchPathInfo<UpdateQueryString> pathInfo , object body)
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
