using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using static Elasticsearch.Net.HttpMethod;
using static Nest.DispatchException;

//Generated File Please Do Not Edit Manually


namespace Nest
{
	///<summary>This dispatches highlevel requests into the proper lowlevel client overload method</summary>
	internal partial class LowLevelDispatch
	{
		internal ElasticsearchResponse<T> BulkDispatch<T>(RequestPath<BulkRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Bulk<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Bulk<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Bulk<T>(body,u => p.RequestParameters);

				case PUT:
					if (AllSet(p.Index, p.Type)) return _lowLevel.BulkPut<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.BulkPut<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.BulkPut<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Bulk", p, new [] { POST, PUT }, "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk");
		}
		
		internal Task<ElasticsearchResponse<T>> BulkDispatchAsync<T>(RequestPath<BulkRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.BulkAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.BulkAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.BulkAsync<T>(body,u => p.RequestParameters);

				case PUT:
					if (AllSet(p.Index, p.Type)) return _lowLevel.BulkPutAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.BulkPutAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.BulkPutAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Bulk", p, new [] { POST, PUT }, "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk");
		}
		
		internal ElasticsearchResponse<T> CatAliasesDispatch<T>(RequestPath<CatAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Name)) return _lowLevel.CatAliases<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.CatAliases<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAliases", p, new [] { GET }, "/_cat/aliases", "/_cat/aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatAliasesDispatchAsync<T>(RequestPath<CatAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Name)) return _lowLevel.CatAliasesAsync<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.CatAliasesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAliases", p, new [] { GET }, "/_cat/aliases", "/_cat/aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> CatAllocationDispatch<T>(RequestPath<CatAllocationRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId)) return _lowLevel.CatAllocation<T>(p.NodeId,u => p.RequestParameters);
					return _lowLevel.CatAllocation<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAllocation", p, new [] { GET }, "/_cat/allocation", "/_cat/allocation/{node_id}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatAllocationDispatchAsync<T>(RequestPath<CatAllocationRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId)) return _lowLevel.CatAllocationAsync<T>(p.NodeId,u => p.RequestParameters);
					return _lowLevel.CatAllocationAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAllocation", p, new [] { GET }, "/_cat/allocation", "/_cat/allocation/{node_id}");
		}
		
		internal ElasticsearchResponse<T> CatCountDispatch<T>(RequestPath<CatCountRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatCount<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatCount<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatCount", p, new [] { GET }, "/_cat/count", "/_cat/count/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatCountDispatchAsync<T>(RequestPath<CatCountRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatCountAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatCountAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatCount", p, new [] { GET }, "/_cat/count", "/_cat/count/{index}");
		}
		
		internal ElasticsearchResponse<T> CatFielddataDispatch<T>(RequestPath<CatFielddataRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatFielddata<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatFielddata", p, new [] { GET }, "/_cat/fielddata");
		}
		
		internal Task<ElasticsearchResponse<T>> CatFielddataDispatchAsync<T>(RequestPath<CatFielddataRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatFielddataAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatFielddata", p, new [] { GET }, "/_cat/fielddata");
		}
		
		internal ElasticsearchResponse<T> CatHealthDispatch<T>(RequestPath<CatHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHealth<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHealth", p, new [] { GET }, "/_cat/health");
		}
		
		internal Task<ElasticsearchResponse<T>> CatHealthDispatchAsync<T>(RequestPath<CatHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHealthAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHealth", p, new [] { GET }, "/_cat/health");
		}
		
		internal ElasticsearchResponse<T> CatHelpDispatch<T>(RequestPath<CatHelpRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHelp<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHelp", p, new [] { GET }, "/_cat");
		}
		
		internal Task<ElasticsearchResponse<T>> CatHelpDispatchAsync<T>(RequestPath<CatHelpRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHelpAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHelp", p, new [] { GET }, "/_cat");
		}
		
		internal ElasticsearchResponse<T> CatIndicesDispatch<T>(RequestPath<CatIndicesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatIndices<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatIndices<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatIndices", p, new [] { GET }, "/_cat/indices", "/_cat/indices/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatIndicesDispatchAsync<T>(RequestPath<CatIndicesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatIndicesAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatIndicesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatIndices", p, new [] { GET }, "/_cat/indices", "/_cat/indices/{index}");
		}
		
		internal ElasticsearchResponse<T> CatMasterDispatch<T>(RequestPath<CatMasterRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatMaster<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatMaster", p, new [] { GET }, "/_cat/master");
		}
		
		internal Task<ElasticsearchResponse<T>> CatMasterDispatchAsync<T>(RequestPath<CatMasterRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatMasterAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatMaster", p, new [] { GET }, "/_cat/master");
		}
		
		internal ElasticsearchResponse<T> CatNodeattrsDispatch<T>(RequestPath<CatNodeattrsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodeattrs<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodeattrs", p, new [] { GET }, "/_cat/nodeattrs");
		}
		
		internal Task<ElasticsearchResponse<T>> CatNodeattrsDispatchAsync<T>(RequestPath<CatNodeattrsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodeattrsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodeattrs", p, new [] { GET }, "/_cat/nodeattrs");
		}
		
		internal ElasticsearchResponse<T> CatNodesDispatch<T>(RequestPath<CatNodesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodes<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodes", p, new [] { GET }, "/_cat/nodes");
		}
		
		internal Task<ElasticsearchResponse<T>> CatNodesDispatchAsync<T>(RequestPath<CatNodesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodes", p, new [] { GET }, "/_cat/nodes");
		}
		
		internal ElasticsearchResponse<T> CatPendingTasksDispatch<T>(RequestPath<CatPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPendingTasks<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPendingTasks", p, new [] { GET }, "/_cat/pending_tasks");
		}
		
		internal Task<ElasticsearchResponse<T>> CatPendingTasksDispatchAsync<T>(RequestPath<CatPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPendingTasksAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPendingTasks", p, new [] { GET }, "/_cat/pending_tasks");
		}
		
		internal ElasticsearchResponse<T> CatPluginsDispatch<T>(RequestPath<CatPluginsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPlugins<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPlugins", p, new [] { GET }, "/_cat/plugins");
		}
		
		internal Task<ElasticsearchResponse<T>> CatPluginsDispatchAsync<T>(RequestPath<CatPluginsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPluginsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPlugins", p, new [] { GET }, "/_cat/plugins");
		}
		
		internal ElasticsearchResponse<T> CatRecoveryDispatch<T>(RequestPath<CatRecoveryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatRecovery<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatRecovery<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatRecovery", p, new [] { GET }, "/_cat/recovery", "/_cat/recovery/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatRecoveryDispatchAsync<T>(RequestPath<CatRecoveryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatRecoveryAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatRecoveryAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatRecovery", p, new [] { GET }, "/_cat/recovery", "/_cat/recovery/{index}");
		}
		
		internal ElasticsearchResponse<T> CatSegmentsDispatch<T>(RequestPath<CatSegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatSegments<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatSegments<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatSegments", p, new [] { GET }, "/_cat/segments", "/_cat/segments/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatSegmentsDispatchAsync<T>(RequestPath<CatSegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatSegmentsAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatSegmentsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatSegments", p, new [] { GET }, "/_cat/segments", "/_cat/segments/{index}");
		}
		
		internal ElasticsearchResponse<T> CatShardsDispatch<T>(RequestPath<CatShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatShards<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatShards<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatShards", p, new [] { GET }, "/_cat/shards", "/_cat/shards/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatShardsDispatchAsync<T>(RequestPath<CatShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.CatShardsAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CatShardsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatShards", p, new [] { GET }, "/_cat/shards", "/_cat/shards/{index}");
		}
		
		internal ElasticsearchResponse<T> CatThreadPoolDispatch<T>(RequestPath<CatThreadPoolRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatThreadPool<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatThreadPool", p, new [] { GET }, "/_cat/thread_pool");
		}
		
		internal Task<ElasticsearchResponse<T>> CatThreadPoolDispatchAsync<T>(RequestPath<CatThreadPoolRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatThreadPoolAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatThreadPool", p, new [] { GET }, "/_cat/thread_pool");
		}
		
		internal ElasticsearchResponse<T> ClearScrollDispatch<T>(RequestPath<ClearScrollRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.ScrollId)) return _lowLevel.ClearScroll<T>(p.ScrollId,body,u => p.RequestParameters);
					return _lowLevel.ClearScroll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClearScroll", p, new [] { DELETE }, "/_search/scroll/{scroll_id}", "/_search/scroll");
		}
		
		internal Task<ElasticsearchResponse<T>> ClearScrollDispatchAsync<T>(RequestPath<ClearScrollRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.ScrollId)) return _lowLevel.ClearScrollAsync<T>(p.ScrollId,body,u => p.RequestParameters);
					return _lowLevel.ClearScrollAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClearScroll", p, new [] { DELETE }, "/_search/scroll/{scroll_id}", "/_search/scroll");
		}
		
		internal ElasticsearchResponse<T> ClusterGetSettingsDispatch<T>(RequestPath<ClusterGetSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterGetSettings<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterGetSettings", p, new [] { GET }, "/_cluster/settings");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterGetSettingsDispatchAsync<T>(RequestPath<ClusterGetSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterGetSettingsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterGetSettings", p, new [] { GET }, "/_cluster/settings");
		}
		
		internal ElasticsearchResponse<T> ClusterHealthDispatch<T>(RequestPath<ClusterHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.ClusterHealth<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.ClusterHealth<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterHealth", p, new [] { GET }, "/_cluster/health", "/_cluster/health/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterHealthDispatchAsync<T>(RequestPath<ClusterHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.ClusterHealthAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.ClusterHealthAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterHealth", p, new [] { GET }, "/_cluster/health", "/_cluster/health/{index}");
		}
		
		internal ElasticsearchResponse<T> ClusterPendingTasksDispatch<T>(RequestPath<ClusterPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterPendingTasks<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPendingTasks", p, new [] { GET }, "/_cluster/pending_tasks");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPendingTasksDispatchAsync<T>(RequestPath<ClusterPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterPendingTasksAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPendingTasks", p, new [] { GET }, "/_cluster/pending_tasks");
		}
		
		internal ElasticsearchResponse<T> ClusterPutSettingsDispatch<T>(RequestPath<ClusterSettingsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					return _lowLevel.ClusterPutSettings<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPutSettings", p, new [] { PUT }, "/_cluster/settings");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPutSettingsDispatchAsync<T>(RequestPath<ClusterSettingsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					return _lowLevel.ClusterPutSettingsAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPutSettings", p, new [] { PUT }, "/_cluster/settings");
		}
		
		internal ElasticsearchResponse<T> ClusterRerouteDispatch<T>(RequestPath<ClusterRerouteRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.ClusterReroute<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterReroute", p, new [] { POST }, "/_cluster/reroute");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterRerouteDispatchAsync<T>(RequestPath<ClusterRerouteRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.ClusterRerouteAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterReroute", p, new [] { POST }, "/_cluster/reroute");
		}
		
		internal ElasticsearchResponse<T> ClusterStateDispatch<T>(RequestPath<ClusterStateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Metric, p.Index)) return _lowLevel.ClusterState<T>(p.Metric,p.Index,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.ClusterState<T>(p.Metric,u => p.RequestParameters);
					return _lowLevel.ClusterState<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterState", p, new [] { GET }, "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStateDispatchAsync<T>(RequestPath<ClusterStateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Metric, p.Index)) return _lowLevel.ClusterStateAsync<T>(p.Metric,p.Index,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.ClusterStateAsync<T>(p.Metric,u => p.RequestParameters);
					return _lowLevel.ClusterStateAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterState", p, new [] { GET }, "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}");
		}
		
		internal ElasticsearchResponse<T> ClusterStatsDispatch<T>(RequestPath<ClusterStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId)) return _lowLevel.ClusterStats<T>(p.NodeId,u => p.RequestParameters);
					return _lowLevel.ClusterStats<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterStats", p, new [] { GET }, "/_cluster/stats", "/_cluster/stats/nodes/{node_id}");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStatsDispatchAsync<T>(RequestPath<ClusterStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId)) return _lowLevel.ClusterStatsAsync<T>(p.NodeId,u => p.RequestParameters);
					return _lowLevel.ClusterStatsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterStats", p, new [] { GET }, "/_cluster/stats", "/_cluster/stats/nodes/{node_id}");
		}
		
		internal ElasticsearchResponse<T> CountDispatch<T>(RequestPath<CountRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Count<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Count<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Count<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.CountGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CountGet<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Count", p, new [] { POST, GET }, "/_count", "/{index}/_count", "/{index}/{type}/_count");
		}
		
		internal Task<ElasticsearchResponse<T>> CountDispatchAsync<T>(RequestPath<CountRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.CountAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.CountAsync<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.CountGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.CountGetAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Count", p, new [] { POST, GET }, "/_count", "/{index}/_count", "/{index}/{type}/_count");
		}
		
		internal ElasticsearchResponse<T> CountPercolateDispatch<T>(RequestPath<PercolateCountRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.CountPercolateGet<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountPercolateGet<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.CountPercolate<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountPercolate<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("CountPercolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate/count", "/{index}/{type}/{id}/_percolate/count");
		}
		
		internal Task<ElasticsearchResponse<T>> CountPercolateDispatchAsync<T>(RequestPath<PercolateCountRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.CountPercolateGetAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountPercolateGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.CountPercolateAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.CountPercolateAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("CountPercolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate/count", "/{index}/{type}/{id}/_percolate/count");
		}
		
		internal ElasticsearchResponse<T> DeleteDispatch<T>(RequestPath<DeleteRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Delete<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Delete", p, new [] { DELETE }, "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteDispatchAsync<T>(RequestPath<DeleteRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.DeleteAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Delete", p, new [] { DELETE }, "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> DeleteByQueryDispatch<T>(RequestPath<DeleteByQueryRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Type)) return _lowLevel.DeleteByQuery<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.DeleteByQuery<T>(p.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteByQuery", p, new [] { DELETE }, "/{index}/_query", "/{index}/{type}/_query");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteByQueryDispatchAsync<T>(RequestPath<DeleteByQueryRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Type)) return _lowLevel.DeleteByQueryAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.DeleteByQueryAsync<T>(p.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteByQuery", p, new [] { DELETE }, "/{index}/_query", "/{index}/{type}/_query");
		}
		
		internal ElasticsearchResponse<T> DeleteScriptDispatch<T>(RequestPath<DeleteScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.DeleteScript<T>(p.Lang,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteScript", p, new [] { DELETE }, "/_scripts/{lang}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteScriptDispatchAsync<T>(RequestPath<DeleteScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.DeleteScriptAsync<T>(p.Lang,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteScript", p, new [] { DELETE }, "/_scripts/{lang}/{id}");
		}
		
		internal ElasticsearchResponse<T> DeleteTemplateDispatch<T>(RequestPath<DeleteTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Id)) return _lowLevel.DeleteTemplate<T>(p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteTemplate", p, new [] { DELETE }, "/_search/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteTemplateDispatchAsync<T>(RequestPath<DeleteTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Id)) return _lowLevel.DeleteTemplateAsync<T>(p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteTemplate", p, new [] { DELETE }, "/_search/template/{id}");
		}
		
		internal ElasticsearchResponse<T> ExistsDispatch<T>(RequestPath<DocumentExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Exists<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Exists", p, new [] { HEAD }, "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> ExistsDispatchAsync<T>(RequestPath<DocumentExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.ExistsAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Exists", p, new [] { HEAD }, "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> ExplainDispatch<T>(RequestPath<ExplainRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.ExplainGet<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Explain<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Explain", p, new [] { GET, POST }, "/{index}/{type}/{id}/_explain");
		}
		
		internal Task<ElasticsearchResponse<T>> ExplainDispatchAsync<T>(RequestPath<ExplainRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.ExplainGetAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.ExplainAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Explain", p, new [] { GET, POST }, "/{index}/{type}/{id}/_explain");
		}
		
		internal ElasticsearchResponse<T> FieldStatsDispatch<T>(RequestPath<FieldStatsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.FieldStatsGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.FieldStatsGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index)) return _lowLevel.FieldStats<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.FieldStats<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("FieldStats", p, new [] { GET, POST }, "/_field_stats", "/{index}/_field_stats");
		}
		
		internal Task<ElasticsearchResponse<T>> FieldStatsDispatchAsync<T>(RequestPath<FieldStatsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.FieldStatsGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.FieldStatsGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index)) return _lowLevel.FieldStatsAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.FieldStatsAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("FieldStats", p, new [] { GET, POST }, "/_field_stats", "/{index}/_field_stats");
		}
		
		internal ElasticsearchResponse<T> GetDispatch<T>(RequestPath<GetRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Get<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Get", p, new [] { GET }, "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> GetDispatchAsync<T>(RequestPath<GetRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.GetAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Get", p, new [] { GET }, "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> GetScriptDispatch<T>(RequestPath<GetScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.GetScript<T>(p.Lang,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetScript", p, new [] { GET }, "/_scripts/{lang}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> GetScriptDispatchAsync<T>(RequestPath<GetScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.GetScriptAsync<T>(p.Lang,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetScript", p, new [] { GET }, "/_scripts/{lang}/{id}");
		}
		
		internal ElasticsearchResponse<T> GetSourceDispatch<T>(RequestPath<SourceRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.GetSource<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetSource", p, new [] { GET }, "/{index}/{type}/{id}/_source");
		}
		
		internal Task<ElasticsearchResponse<T>> GetSourceDispatchAsync<T>(RequestPath<SourceRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.GetSourceAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetSource", p, new [] { GET }, "/{index}/{type}/{id}/_source");
		}
		
		internal ElasticsearchResponse<T> GetTemplateDispatch<T>(RequestPath<GetTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Id)) return _lowLevel.GetTemplate<T>(p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetTemplate", p, new [] { GET }, "/_search/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> GetTemplateDispatchAsync<T>(RequestPath<GetTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Id)) return _lowLevel.GetTemplateAsync<T>(p.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetTemplate", p, new [] { GET }, "/_search/template/{id}");
		}
		
		internal ElasticsearchResponse<T> IndexDispatch<T>(RequestPath<IndexRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Index<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.Index<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

				case PUT:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.IndexPut<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndexPut<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Index", p, new [] { POST, PUT }, "/{index}/{type}", "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndexDispatchAsync<T>(RequestPath<IndexRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.IndexAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndexAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

				case PUT:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.IndexPutAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndexPutAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Index", p, new [] { POST, PUT }, "/{index}/{type}", "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> IndicesAnalyzeDispatch<T>(RequestPath<AnalyzeRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesAnalyzeGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeGetForAll<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesAnalyze<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesAnalyze", p, new [] { GET, POST }, "/_analyze", "/{index}/_analyze");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesAnalyzeDispatchAsync<T>(RequestPath<AnalyzeRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesAnalyzeGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeGetForAllAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesAnalyzeAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesAnalyze", p, new [] { GET, POST }, "/_analyze", "/{index}/_analyze");
		}
		
		internal ElasticsearchResponse<T> IndicesClearCacheDispatch<T>(RequestPath<ClearCacheRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesClearCache<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesClearCacheGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesClearCache", p, new [] { POST, GET }, "/_cache/clear", "/{index}/_cache/clear");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesClearCacheDispatchAsync<T>(RequestPath<ClearCacheRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesClearCacheAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesClearCacheGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesClearCache", p, new [] { POST, GET }, "/_cache/clear", "/{index}/_cache/clear");
		}
		
		internal ElasticsearchResponse<T> IndicesCloseDispatch<T>(RequestPath<CloseIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesClose<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesClose", p, new [] { POST }, "/{index}/_close");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesCloseDispatchAsync<T>(RequestPath<CloseIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesCloseAsync<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesClose", p, new [] { POST }, "/{index}/_close");
		}
		
		internal ElasticsearchResponse<T> IndicesCreateDispatch<T>(RequestPath<CreateIndexRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index)) return _lowLevel.IndicesCreate<T>(p.Index,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesCreatePost<T>(p.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesCreate", p, new [] { PUT, POST }, "/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesCreateDispatchAsync<T>(RequestPath<CreateIndexRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index)) return _lowLevel.IndicesCreateAsync<T>(p.Index,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesCreatePostAsync<T>(p.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesCreate", p, new [] { PUT, POST }, "/{index}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteDispatch<T>(RequestPath<DeleteIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index)) return _lowLevel.IndicesDelete<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDelete", p, new [] { DELETE }, "/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteDispatchAsync<T>(RequestPath<DeleteIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index)) return _lowLevel.IndicesDeleteAsync<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDelete", p, new [] { DELETE }, "/{index}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteAliasDispatch<T>(RequestPath<DeleteAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesDeleteAlias<T>(p.Index,p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteAlias", p, new [] { DELETE }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteAliasDispatchAsync<T>(RequestPath<DeleteAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesDeleteAliasAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteAlias", p, new [] { DELETE }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteTemplateDispatch<T>(RequestPath<DeleteTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Name)) return _lowLevel.IndicesDeleteTemplateForAll<T>(p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteTemplate", p, new [] { DELETE }, "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteTemplateDispatchAsync<T>(RequestPath<DeleteTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Name)) return _lowLevel.IndicesDeleteTemplateForAllAsync<T>(p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteTemplate", p, new [] { DELETE }, "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteWarmerDispatch<T>(RequestPath<DeleteWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesDeleteWarmer<T>(p.Index,p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteWarmer", p, new [] { DELETE }, "/{index}/_warmer/{name}", "/{index}/_warmers/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteWarmerDispatchAsync<T>(RequestPath<DeleteWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesDeleteWarmerAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteWarmer", p, new [] { DELETE }, "/{index}/_warmer/{name}", "/{index}/_warmers/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsDispatch<T>(RequestPath<IndexExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index)) return _lowLevel.IndicesExists<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExists", p, new [] { HEAD }, "/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsDispatchAsync<T>(RequestPath<IndexExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index)) return _lowLevel.IndicesExistsAsync<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExists", p, new [] { HEAD }, "/{index}");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsAliasDispatch<T>(RequestPath<AliasExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesExistsAlias<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesExistsAliasForAll<T>(p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesExistsAlias<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsAlias", p, new [] { HEAD }, "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsAliasDispatchAsync<T>(RequestPath<AliasExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesExistsAliasAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesExistsAliasForAllAsync<T>(p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesExistsAliasAsync<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsAlias", p, new [] { HEAD }, "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsTemplateDispatch<T>(RequestPath<TemplateExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Name)) return _lowLevel.IndicesExistsTemplateForAll<T>(p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsTemplate", p, new [] { HEAD }, "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsTemplateDispatchAsync<T>(RequestPath<TemplateExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Name)) return _lowLevel.IndicesExistsTemplateForAllAsync<T>(p.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsTemplate", p, new [] { HEAD }, "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsTypeDispatch<T>(RequestPath<TypeExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesExistsType<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsType", p, new [] { HEAD }, "/{index}/{type}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsTypeDispatchAsync<T>(RequestPath<TypeExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesExistsTypeAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsType", p, new [] { HEAD }, "/{index}/{type}");
		}
		
		internal ElasticsearchResponse<T> IndicesFlushDispatch<T>(RequestPath<FlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlush<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlush", p, new [] { POST, GET }, "/_flush", "/{index}/_flush");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushDispatchAsync<T>(RequestPath<FlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlush", p, new [] { POST, GET }, "/_flush", "/{index}/_flush");
		}
		
		internal ElasticsearchResponse<T> IndicesFlushSyncedDispatch<T>(RequestPath<SyncedFlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushSynced<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushSyncedGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlushSynced", p, new [] { POST, GET }, "/_flush/synced", "/{index}/_flush/synced");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushSyncedDispatchAsync<T>(RequestPath<SyncedFlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushSyncedAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesFlushSyncedGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlushSynced", p, new [] { POST, GET }, "/_flush/synced", "/{index}/_flush/synced");
		}
		
		internal ElasticsearchResponse<T> IndicesGetDispatch<T>(RequestPath<GetIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Feature)) return _lowLevel.IndicesGet<T>(p.Index,p.Feature,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGet<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGet", p, new [] { GET }, "/{index}", "/{index}/{feature}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetDispatchAsync<T>(RequestPath<GetIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Feature)) return _lowLevel.IndicesGetAsync<T>(p.Index,p.Feature,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetAsync<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGet", p, new [] { GET }, "/{index}", "/{index}/{feature}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasDispatch<T>(RequestPath<GetAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetAlias<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetAliasForAll<T>(p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetAlias<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAlias", p, new [] { GET }, "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasDispatchAsync<T>(RequestPath<GetAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetAliasAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetAliasForAllAsync<T>(p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetAliasAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAlias", p, new [] { GET }, "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasesDispatch<T>(RequestPath<GetAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetAliases<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetAliases<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetAliasesForAll<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasesForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAliases", p, new [] { GET }, "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasesDispatchAsync<T>(RequestPath<GetAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetAliasesAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetAliasesAsync<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetAliasesForAllAsync<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasesForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAliases", p, new [] { GET }, "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetFieldMappingDispatch<T>(RequestPath<GetFieldMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Field)) return _lowLevel.IndicesGetFieldMapping<T>(p.Index,p.Type,p.Field,u => p.RequestParameters);
					if (AllSet(p.Index, p.Field)) return _lowLevel.IndicesGetFieldMapping<T>(p.Index,p.Field,u => p.RequestParameters);
					if (AllSet(p.Type, p.Field)) return _lowLevel.IndicesGetFieldMappingForAll<T>(p.Type,p.Field,u => p.RequestParameters);
					if (AllSet(p.Field)) return _lowLevel.IndicesGetFieldMappingForAll<T>(p.Field,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGetFieldMapping", p, new [] { GET }, "/_mapping/field/{field}", "/{index}/_mapping/field/{field}", "/_mapping/{type}/field/{field}", "/{index}/_mapping/{type}/field/{field}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetFieldMappingDispatchAsync<T>(RequestPath<GetFieldMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Field)) return _lowLevel.IndicesGetFieldMappingAsync<T>(p.Index,p.Type,p.Field,u => p.RequestParameters);
					if (AllSet(p.Index, p.Field)) return _lowLevel.IndicesGetFieldMappingAsync<T>(p.Index,p.Field,u => p.RequestParameters);
					if (AllSet(p.Type, p.Field)) return _lowLevel.IndicesGetFieldMappingForAllAsync<T>(p.Type,p.Field,u => p.RequestParameters);
					if (AllSet(p.Field)) return _lowLevel.IndicesGetFieldMappingForAllAsync<T>(p.Field,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGetFieldMapping", p, new [] { GET }, "/_mapping/field/{field}", "/{index}/_mapping/field/{field}", "/_mapping/{type}/field/{field}", "/{index}/_mapping/{type}/field/{field}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetMappingDispatch<T>(RequestPath<GetMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesGetMapping<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetMapping<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Type)) return _lowLevel.IndicesGetMappingForAll<T>(p.Type,u => p.RequestParameters);
					return _lowLevel.IndicesGetMappingForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetMapping", p, new [] { GET }, "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetMappingDispatchAsync<T>(RequestPath<GetMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesGetMappingAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetMappingAsync<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Type)) return _lowLevel.IndicesGetMappingForAllAsync<T>(p.Type,u => p.RequestParameters);
					return _lowLevel.IndicesGetMappingForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetMapping", p, new [] { GET }, "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetSettingsDispatch<T>(RequestPath<GetIndexSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetSettings<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetSettings<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetSettingsForAll<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetSettingsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetSettings", p, new [] { GET }, "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetSettingsDispatchAsync<T>(RequestPath<GetIndexSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetSettingsAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetSettingsAsync<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetSettingsForAllAsync<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetSettingsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetSettings", p, new [] { GET }, "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetTemplateDispatch<T>(RequestPath<GetTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Name)) return _lowLevel.IndicesGetTemplateForAll<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetTemplateForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetTemplate", p, new [] { GET }, "/_template", "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetTemplateDispatchAsync<T>(RequestPath<GetTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Name)) return _lowLevel.IndicesGetTemplateForAllAsync<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetTemplateForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetTemplate", p, new [] { GET }, "/_template", "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetUpgradeDispatch<T>(RequestPath<UpgradeStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesGetUpgrade<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetUpgradeForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetUpgrade", p, new [] { GET }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetUpgradeDispatchAsync<T>(RequestPath<UpgradeStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesGetUpgradeAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetUpgradeForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetUpgrade", p, new [] { GET }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal ElasticsearchResponse<T> IndicesGetWarmerDispatch<T>(RequestPath<GetWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Name)) return _lowLevel.IndicesGetWarmer<T>(p.Index,p.Type,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetWarmer<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetWarmer<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetWarmerForAll<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetWarmerForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetWarmer", p, new [] { GET }, "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetWarmerDispatchAsync<T>(RequestPath<GetWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Name)) return _lowLevel.IndicesGetWarmerAsync<T>(p.Index,p.Type,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesGetWarmerAsync<T>(p.Index,p.Name,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesGetWarmerAsync<T>(p.Index,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesGetWarmerForAllAsync<T>(p.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetWarmerForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetWarmer", p, new [] { GET }, "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesOpenDispatch<T>(RequestPath<OpenIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesOpen<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesOpen", p, new [] { POST }, "/{index}/_open");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOpenDispatchAsync<T>(RequestPath<OpenIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesOpenAsync<T>(p.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesOpen", p, new [] { POST }, "/{index}/_open");
		}
		
		internal ElasticsearchResponse<T> IndicesOptimizeDispatch<T>(RequestPath<OptimizeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesOptimize<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesOptimizeGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesOptimize", p, new [] { POST, GET }, "/_optimize", "/{index}/_optimize");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOptimizeDispatchAsync<T>(RequestPath<OptimizeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesOptimizeAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesOptimizeGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesOptimize", p, new [] { POST, GET }, "/_optimize", "/{index}/_optimize");
		}
		
		internal ElasticsearchResponse<T> IndicesPutAliasDispatch<T>(RequestPath<PutAliasRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutAlias<T>(p.Index,p.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutAliasPost<T>(p.Index,p.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutAlias", p, new [] { PUT, POST }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutAliasDispatchAsync<T>(RequestPath<PutAliasRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutAliasAsync<T>(p.Index,p.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutAliasPostAsync<T>(p.Index,p.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutAlias", p, new [] { PUT, POST }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesPutMappingDispatch<T>(RequestPath<PutMappingRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesPutMapping<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Type)) return _lowLevel.IndicesPutMappingForAll<T>(p.Type,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesPutMappingPost<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Type)) return _lowLevel.IndicesPutMappingPostForAll<T>(p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutMapping", p, new [] { PUT, POST }, "/{index}/{type}/_mapping", "/{index}/_mapping/{type}", "/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}", "/_mappings/{type}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutMappingDispatchAsync<T>(RequestPath<PutMappingRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesPutMappingAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Type)) return _lowLevel.IndicesPutMappingForAllAsync<T>(p.Type,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesPutMappingPostAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Type)) return _lowLevel.IndicesPutMappingPostForAllAsync<T>(p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutMapping", p, new [] { PUT, POST }, "/{index}/{type}/_mapping", "/{index}/_mapping/{type}", "/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}", "/_mappings/{type}");
		}
		
		internal ElasticsearchResponse<T> IndicesPutSettingsDispatch<T>(RequestPath<UpdateSettingsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index)) return _lowLevel.IndicesPutSettings<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesPutSettingsForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesPutSettings", p, new [] { PUT }, "/_settings", "/{index}/_settings");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutSettingsDispatchAsync<T>(RequestPath<UpdateSettingsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index)) return _lowLevel.IndicesPutSettingsAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesPutSettingsForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesPutSettings", p, new [] { PUT }, "/_settings", "/{index}/_settings");
		}
		
		internal ElasticsearchResponse<T> IndicesPutTemplateDispatch<T>(RequestPath<PutTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Name)) return _lowLevel.IndicesPutTemplateForAll<T>(p.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Name)) return _lowLevel.IndicesPutTemplatePostForAll<T>(p.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutTemplate", p, new [] { PUT, POST }, "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutTemplateDispatchAsync<T>(RequestPath<PutTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Name)) return _lowLevel.IndicesPutTemplateForAllAsync<T>(p.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Name)) return _lowLevel.IndicesPutTemplatePostForAllAsync<T>(p.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutTemplate", p, new [] { PUT, POST }, "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesPutWarmerDispatch<T>(RequestPath<PutWarmerRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index, p.Type, p.Name)) return _lowLevel.IndicesPutWarmer<T>(p.Index,p.Type,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutWarmer<T>(p.Index,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesPutWarmerForAll<T>(p.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Name)) return _lowLevel.IndicesPutWarmerPost<T>(p.Index,p.Type,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutWarmerPost<T>(p.Index,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesPutWarmerPostForAll<T>(p.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutWarmer", p, new [] { PUT, POST }, "/_warmer/{name}", "/{index}/_warmer/{name}", "/{index}/{type}/_warmer/{name}", "/_warmers/{name}", "/{index}/_warmers/{name}", "/{index}/{type}/_warmers/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutWarmerDispatchAsync<T>(RequestPath<PutWarmerRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Index, p.Type, p.Name)) return _lowLevel.IndicesPutWarmerAsync<T>(p.Index,p.Type,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutWarmerAsync<T>(p.Index,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesPutWarmerForAllAsync<T>(p.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Name)) return _lowLevel.IndicesPutWarmerPostAsync<T>(p.Index,p.Type,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Name)) return _lowLevel.IndicesPutWarmerPostAsync<T>(p.Index,p.Name,body,u => p.RequestParameters);
					if (AllSet(p.Name)) return _lowLevel.IndicesPutWarmerPostForAllAsync<T>(p.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutWarmer", p, new [] { PUT, POST }, "/_warmer/{name}", "/{index}/_warmer/{name}", "/{index}/{type}/_warmer/{name}", "/_warmers/{name}", "/{index}/_warmers/{name}", "/{index}/{type}/_warmers/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesRecoveryDispatch<T>(RequestPath<RecoveryStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesRecovery<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRecoveryForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRecovery", p, new [] { GET }, "/_recovery", "/{index}/_recovery");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRecoveryDispatchAsync<T>(RequestPath<RecoveryStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesRecoveryAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRecoveryForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRecovery", p, new [] { GET }, "/_recovery", "/{index}/_recovery");
		}
		
		internal ElasticsearchResponse<T> IndicesRefreshDispatch<T>(RequestPath<RefreshRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesRefresh<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesRefreshGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRefresh", p, new [] { POST, GET }, "/_refresh", "/{index}/_refresh");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRefreshDispatchAsync<T>(RequestPath<RefreshRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesRefreshAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesRefreshGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRefresh", p, new [] { POST, GET }, "/_refresh", "/{index}/_refresh");
		}
		
		internal ElasticsearchResponse<T> IndicesSegmentsDispatch<T>(RequestPath<SegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesSegments<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesSegmentsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesSegments", p, new [] { GET }, "/_segments", "/{index}/_segments");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesSegmentsDispatchAsync<T>(RequestPath<SegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesSegmentsAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesSegmentsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesSegments", p, new [] { GET }, "/_segments", "/{index}/_segments");
		}
		
		internal ElasticsearchResponse<T> IndicesShardStoresDispatch<T>(RequestPath<IndicesShardStoresRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesShardStores<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesShardStoresForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesShardStores", p, new [] { GET }, "/_shard_stores", "/{index}/_shard_stores");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesShardStoresDispatchAsync<T>(RequestPath<IndicesShardStoresRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index)) return _lowLevel.IndicesShardStoresAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesShardStoresForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesShardStores", p, new [] { GET }, "/_shard_stores", "/{index}/_shard_stores");
		}
		
		internal ElasticsearchResponse<T> IndicesStatsDispatch<T>(RequestPath<IndicesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Metric)) return _lowLevel.IndicesStats<T>(p.Index,p.Metric,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.IndicesStatsForAll<T>(p.Metric,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesStats<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesStatsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesStats", p, new [] { GET }, "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesStatsDispatchAsync<T>(RequestPath<IndicesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Metric)) return _lowLevel.IndicesStatsAsync<T>(p.Index,p.Metric,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.IndicesStatsForAllAsync<T>(p.Metric,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesStatsAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesStatsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesStats", p, new [] { GET }, "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}");
		}
		
		internal ElasticsearchResponse<T> IndicesUpdateAliasesDispatch<T>(RequestPath<BulkAliasRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.IndicesUpdateAliasesForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpdateAliases", p, new [] { POST }, "/_aliases");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpdateAliasesDispatchAsync<T>(RequestPath<BulkAliasRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.IndicesUpdateAliasesForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpdateAliases", p, new [] { POST }, "/_aliases");
		}
		
		internal ElasticsearchResponse<T> IndicesUpgradeDispatch<T>(RequestPath<UpgradeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesUpgrade<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesUpgradeForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpgrade", p, new [] { POST }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpgradeDispatchAsync<T>(RequestPath<UpgradeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.IndicesUpgradeAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesUpgradeForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpgrade", p, new [] { POST }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal ElasticsearchResponse<T> IndicesValidateQueryDispatch<T>(RequestPath<ValidateQueryRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesValidateQueryGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesValidateQueryGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryGetForAll<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesValidateQuery<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesValidateQuery<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesValidateQuery", p, new [] { GET, POST }, "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesValidateQueryDispatchAsync<T>(RequestPath<ValidateQueryRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesValidateQueryGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesValidateQueryGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryGetForAllAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.IndicesValidateQueryAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.IndicesValidateQueryAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesValidateQuery", p, new [] { GET, POST }, "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query");
		}
		
		internal ElasticsearchResponse<T> InfoDispatch<T>(RequestPath<InfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.Info<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Info", p, new [] { GET }, "/");
		}
		
		internal Task<ElasticsearchResponse<T>> InfoDispatchAsync<T>(RequestPath<InfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.InfoAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Info", p, new [] { GET }, "/");
		}
		
		internal ElasticsearchResponse<T> MgetDispatch<T>(RequestPath<MultiGetRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MgetGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MgetGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MgetGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Mget<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Mget<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Mget<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mget", p, new [] { GET, POST }, "/_mget", "/{index}/_mget", "/{index}/{type}/_mget");
		}
		
		internal Task<ElasticsearchResponse<T>> MgetDispatchAsync<T>(RequestPath<MultiGetRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MgetGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MgetGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MgetGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MgetAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MgetAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.MgetAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mget", p, new [] { GET, POST }, "/_mget", "/{index}/_mget", "/{index}/{type}/_mget");
		}
		
		internal ElasticsearchResponse<T> MpercolateDispatch<T>(RequestPath<MultiPercolateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MpercolateGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MpercolateGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MpercolateGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Mpercolate<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Mpercolate<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Mpercolate<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mpercolate", p, new [] { GET, POST }, "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate");
		}
		
		internal Task<ElasticsearchResponse<T>> MpercolateDispatchAsync<T>(RequestPath<MultiPercolateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MpercolateGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MpercolateGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MpercolateGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MpercolateAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MpercolateAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.MpercolateAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mpercolate", p, new [] { GET, POST }, "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate");
		}
		
		internal ElasticsearchResponse<T> MsearchDispatch<T>(RequestPath<MultiSearchRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MsearchGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MsearchGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MsearchGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Msearch<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Msearch<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Msearch<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Msearch", p, new [] { GET, POST }, "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch");
		}
		
		internal Task<ElasticsearchResponse<T>> MsearchDispatchAsync<T>(RequestPath<MultiSearchRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MsearchGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MsearchGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MsearchGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MsearchAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MsearchAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.MsearchAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Msearch", p, new [] { GET, POST }, "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch");
		}
		
		internal ElasticsearchResponse<T> MtermvectorsDispatch<T>(RequestPath<MultiTermVectorsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MtermvectorsGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MtermvectorsGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MtermvectorsGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Mtermvectors<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Mtermvectors<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Mtermvectors<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mtermvectors", p, new [] { GET, POST }, "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors");
		}
		
		internal Task<ElasticsearchResponse<T>> MtermvectorsDispatchAsync<T>(RequestPath<MultiTermVectorsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MtermvectorsGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MtermvectorsGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.MtermvectorsGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.MtermvectorsAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.MtermvectorsAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.MtermvectorsAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mtermvectors", p, new [] { GET, POST }, "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors");
		}
		
		internal ElasticsearchResponse<T> NodesHotThreadsDispatch<T>(RequestPath<NodesHotThreadsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId)) return _lowLevel.NodesHotThreads<T>(p.NodeId,u => p.RequestParameters);
					return _lowLevel.NodesHotThreadsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesHotThreads", p, new [] { GET }, "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads");
		}
		
		internal Task<ElasticsearchResponse<T>> NodesHotThreadsDispatchAsync<T>(RequestPath<NodesHotThreadsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId)) return _lowLevel.NodesHotThreadsAsync<T>(p.NodeId,u => p.RequestParameters);
					return _lowLevel.NodesHotThreadsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesHotThreads", p, new [] { GET }, "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads");
		}
		
		internal ElasticsearchResponse<T> NodesInfoDispatch<T>(RequestPath<NodesInfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId, p.Metric)) return _lowLevel.NodesInfo<T>(p.NodeId,p.Metric,u => p.RequestParameters);
					if (AllSet(p.NodeId)) return _lowLevel.NodesInfo<T>(p.NodeId,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.NodesInfoForAll<T>(p.Metric,u => p.RequestParameters);
					return _lowLevel.NodesInfoForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesInfo", p, new [] { GET }, "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}");
		}
		
		internal Task<ElasticsearchResponse<T>> NodesInfoDispatchAsync<T>(RequestPath<NodesInfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId, p.Metric)) return _lowLevel.NodesInfoAsync<T>(p.NodeId,p.Metric,u => p.RequestParameters);
					if (AllSet(p.NodeId)) return _lowLevel.NodesInfoAsync<T>(p.NodeId,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.NodesInfoForAllAsync<T>(p.Metric,u => p.RequestParameters);
					return _lowLevel.NodesInfoForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesInfo", p, new [] { GET }, "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}");
		}
		
		internal ElasticsearchResponse<T> NodesStatsDispatch<T>(RequestPath<NodesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId, p.Metric, p.IndexMetric)) return _lowLevel.NodesStats<T>(p.NodeId,p.Metric,p.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.NodeId, p.Metric)) return _lowLevel.NodesStats<T>(p.NodeId,p.Metric,u => p.RequestParameters);
					if (AllSet(p.Metric, p.IndexMetric)) return _lowLevel.NodesStatsForAll<T>(p.Metric,p.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.NodeId)) return _lowLevel.NodesStats<T>(p.NodeId,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.NodesStatsForAll<T>(p.Metric,u => p.RequestParameters);
					return _lowLevel.NodesStatsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesStats", p, new [] { GET }, "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}");
		}
		
		internal Task<ElasticsearchResponse<T>> NodesStatsDispatchAsync<T>(RequestPath<NodesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.NodeId, p.Metric, p.IndexMetric)) return _lowLevel.NodesStatsAsync<T>(p.NodeId,p.Metric,p.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.NodeId, p.Metric)) return _lowLevel.NodesStatsAsync<T>(p.NodeId,p.Metric,u => p.RequestParameters);
					if (AllSet(p.Metric, p.IndexMetric)) return _lowLevel.NodesStatsForAllAsync<T>(p.Metric,p.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.NodeId)) return _lowLevel.NodesStatsAsync<T>(p.NodeId,u => p.RequestParameters);
					if (AllSet(p.Metric)) return _lowLevel.NodesStatsForAllAsync<T>(p.Metric,u => p.RequestParameters);
					return _lowLevel.NodesStatsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesStats", p, new [] { GET }, "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}");
		}
		
		internal ElasticsearchResponse<T> PercolateDispatch<T>(RequestPath<PercolateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.PercolateGet<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.PercolateGet<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Percolate<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.Percolate<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Percolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate");
		}
		
		internal Task<ElasticsearchResponse<T>> PercolateDispatchAsync<T>(RequestPath<PercolateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.PercolateGetAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.PercolateGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.PercolateAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.PercolateAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Percolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate");
		}
		
		internal ElasticsearchResponse<T> PingDispatch<T>(RequestPath<PingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					return _lowLevel.Ping<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Ping", p, new [] { HEAD }, "/");
		}
		
		internal Task<ElasticsearchResponse<T>> PingDispatchAsync<T>(RequestPath<PingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					return _lowLevel.PingAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Ping", p, new [] { HEAD }, "/");
		}
		
		internal ElasticsearchResponse<T> PutScriptDispatch<T>(RequestPath<PutScriptRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.PutScript<T>(p.Lang,p.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.PutScriptPost<T>(p.Lang,p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutScript", p, new [] { PUT, POST }, "/_scripts/{lang}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> PutScriptDispatchAsync<T>(RequestPath<PutScriptRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.PutScriptAsync<T>(p.Lang,p.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Lang, p.Id)) return _lowLevel.PutScriptPostAsync<T>(p.Lang,p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutScript", p, new [] { PUT, POST }, "/_scripts/{lang}/{id}");
		}
		
		internal ElasticsearchResponse<T> PutTemplateDispatch<T>(RequestPath<PutTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Id)) return _lowLevel.PutTemplate<T>(p.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Id)) return _lowLevel.PutTemplatePost<T>(p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutTemplate", p, new [] { PUT, POST }, "/_search/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> PutTemplateDispatchAsync<T>(RequestPath<PutTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Id)) return _lowLevel.PutTemplateAsync<T>(p.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Id)) return _lowLevel.PutTemplatePostAsync<T>(p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutTemplate", p, new [] { PUT, POST }, "/_search/template/{id}");
		}
		
		internal ElasticsearchResponse<T> RenderSearchTemplateDispatch<T>(RequestPath<RenderSearchTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Id)) return _lowLevel.RenderSearchTemplateGet<T>(p.Id,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplateGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Id)) return _lowLevel.RenderSearchTemplate<T>(p.Id,body,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplate<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("RenderSearchTemplate", p, new [] { GET, POST }, "/_render/template", "/_render/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> RenderSearchTemplateDispatchAsync<T>(RequestPath<RenderSearchTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Id)) return _lowLevel.RenderSearchTemplateGetAsync<T>(p.Id,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplateGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Id)) return _lowLevel.RenderSearchTemplateAsync<T>(p.Id,body,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplateAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("RenderSearchTemplate", p, new [] { GET, POST }, "/_render/template", "/_render/template/{id}");
		}
		
		internal ElasticsearchResponse<T> ScrollDispatch<T>(RequestPath<ScrollRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.ScrollId)) return _lowLevel.ScrollGet<T>(p.ScrollId,u => p.RequestParameters);
					return _lowLevel.ScrollGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.ScrollId)) return _lowLevel.Scroll<T>(p.ScrollId,body,u => p.RequestParameters);
					return _lowLevel.Scroll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Scroll", p, new [] { GET, POST }, "/_search/scroll", "/_search/scroll/{scroll_id}");
		}
		
		internal Task<ElasticsearchResponse<T>> ScrollDispatchAsync<T>(RequestPath<ScrollRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.ScrollId)) return _lowLevel.ScrollGetAsync<T>(p.ScrollId,u => p.RequestParameters);
					return _lowLevel.ScrollGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.ScrollId)) return _lowLevel.ScrollAsync<T>(p.ScrollId,body,u => p.RequestParameters);
					return _lowLevel.ScrollAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Scroll", p, new [] { GET, POST }, "/_search/scroll", "/_search/scroll/{scroll_id}");
		}
		
		internal ElasticsearchResponse<T> SearchDispatch<T>(RequestPath<SearchRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.Search<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.Search<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Search<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Search", p, new [] { GET, POST }, "/_search", "/{index}/_search", "/{index}/{type}/_search");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchDispatchAsync<T>(RequestPath<SearchRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Search", p, new [] { GET, POST }, "/_search", "/{index}/_search", "/{index}/{type}/_search");
		}
		
		internal ElasticsearchResponse<T> SearchExistsDispatch<T>(RequestPath<SearchExistsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchExists<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchExists<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchExists<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchExistsGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchExistsGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchExistsGet<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchExists", p, new [] { POST, GET }, "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchExistsDispatchAsync<T>(RequestPath<SearchExistsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchExistsAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchExistsAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchExistsAsync<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchExistsGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchExistsGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchExistsGetAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchExists", p, new [] { POST, GET }, "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists");
		}
		
		internal ElasticsearchResponse<T> SearchShardsDispatch<T>(RequestPath<SearchShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchShardsGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchShardsGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchShardsGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchShards<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchShards<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchShards<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchShards", p, new [] { GET, POST }, "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchShardsDispatchAsync<T>(RequestPath<SearchShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchShardsGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchShardsGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchShardsGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchShardsAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchShardsAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchShardsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchShards", p, new [] { GET, POST }, "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards");
		}
		
		internal ElasticsearchResponse<T> SearchTemplateDispatch<T>(RequestPath<SearchTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchTemplateGet<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchTemplateGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchTemplateGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchTemplate<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchTemplate<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchTemplate<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchTemplate", p, new [] { GET, POST }, "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchTemplateDispatchAsync<T>(RequestPath<SearchTemplateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchTemplateGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchTemplateGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SearchTemplateGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.Index, p.Type)) return _lowLevel.SearchTemplateAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					if (AllSet(p.Index)) return _lowLevel.SearchTemplateAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchTemplateAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchTemplate", p, new [] { GET, POST }, "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template");
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateDispatch<T>(RequestPath<SnapshotRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotCreate<T>(p.Repository,p.Snapshot,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotCreatePost<T>(p.Repository,p.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreate", p, new [] { PUT, POST }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotCreateDispatchAsync<T>(RequestPath<SnapshotRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotCreateAsync<T>(p.Repository,p.Snapshot,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotCreatePostAsync<T>(p.Repository,p.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreate", p, new [] { PUT, POST }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateRepositoryDispatch<T>(RequestPath<CreateRepositoryRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotCreateRepository<T>(p.Repository,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotCreateRepositoryPost<T>(p.Repository,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreateRepository", p, new [] { PUT, POST }, "/_snapshot/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotCreateRepositoryDispatchAsync<T>(RequestPath<CreateRepositoryRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotCreateRepositoryAsync<T>(p.Repository,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotCreateRepositoryPostAsync<T>(p.Repository,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreateRepository", p, new [] { PUT, POST }, "/_snapshot/{repository}");
		}
		
		internal ElasticsearchResponse<T> SnapshotDeleteDispatch<T>(RequestPath<DeleteSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotDelete<T>(p.Repository,p.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDelete", p, new [] { DELETE }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotDeleteDispatchAsync<T>(RequestPath<DeleteSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotDeleteAsync<T>(p.Repository,p.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDelete", p, new [] { DELETE }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal ElasticsearchResponse<T> SnapshotDeleteRepositoryDispatch<T>(RequestPath<DeleteRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotDeleteRepository<T>(p.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDeleteRepository", p, new [] { DELETE }, "/_snapshot/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotDeleteRepositoryDispatchAsync<T>(RequestPath<DeleteRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotDeleteRepositoryAsync<T>(p.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDeleteRepository", p, new [] { DELETE }, "/_snapshot/{repository}");
		}
		
		internal ElasticsearchResponse<T> SnapshotGetDispatch<T>(RequestPath<GetSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotGet<T>(p.Repository,p.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotGet", p, new [] { GET }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetDispatchAsync<T>(RequestPath<GetSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotGetAsync<T>(p.Repository,p.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotGet", p, new [] { GET }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal ElasticsearchResponse<T> SnapshotGetRepositoryDispatch<T>(RequestPath<GetRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotGetRepository<T>(p.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotGetRepository<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotGetRepository", p, new [] { GET }, "/_snapshot", "/_snapshot/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetRepositoryDispatchAsync<T>(RequestPath<GetRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotGetRepositoryAsync<T>(p.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotGetRepositoryAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotGetRepository", p, new [] { GET }, "/_snapshot", "/_snapshot/{repository}");
		}
		
		internal ElasticsearchResponse<T> SnapshotRestoreDispatch<T>(RequestPath<RestoreRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotRestore<T>(p.Repository,p.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotRestore", p, new [] { POST }, "/_snapshot/{repository}/{snapshot}/_restore");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotRestoreDispatchAsync<T>(RequestPath<RestoreRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotRestoreAsync<T>(p.Repository,p.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotRestore", p, new [] { POST }, "/_snapshot/{repository}/{snapshot}/_restore");
		}
		
		internal ElasticsearchResponse<T> SnapshotStatusDispatch<T>(RequestPath<SnapshotStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotStatus<T>(p.Repository,p.Snapshot,u => p.RequestParameters);
					if (AllSet(p.Repository)) return _lowLevel.SnapshotStatus<T>(p.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotStatus<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotStatus", p, new [] { GET }, "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotStatusDispatchAsync<T>(RequestPath<SnapshotStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Repository, p.Snapshot)) return _lowLevel.SnapshotStatusAsync<T>(p.Repository,p.Snapshot,u => p.RequestParameters);
					if (AllSet(p.Repository)) return _lowLevel.SnapshotStatusAsync<T>(p.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotStatusAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotStatus", p, new [] { GET }, "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status");
		}
		
		internal ElasticsearchResponse<T> SnapshotVerifyRepositoryDispatch<T>(RequestPath<VerifyRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotVerifyRepository<T>(p.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotVerifyRepository", p, new [] { POST }, "/_snapshot/{repository}/_verify");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotVerifyRepositoryDispatchAsync<T>(RequestPath<VerifyRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Repository)) return _lowLevel.SnapshotVerifyRepositoryAsync<T>(p.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotVerifyRepository", p, new [] { POST }, "/_snapshot/{repository}/_verify");
		}
		
		internal ElasticsearchResponse<T> SuggestDispatch<T>(RequestPath<SuggestRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.Suggest<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.Suggest<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.SuggestGet<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SuggestGet<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Suggest", p, new [] { POST, GET }, "/_suggest", "/{index}/_suggest");
		}
		
		internal Task<ElasticsearchResponse<T>> SuggestDispatchAsync<T>(RequestPath<SuggestRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index)) return _lowLevel.SuggestAsync<T>(p.Index,body,u => p.RequestParameters);
					return _lowLevel.SuggestAsync<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.Index)) return _lowLevel.SuggestGetAsync<T>(p.Index,u => p.RequestParameters);
					return _lowLevel.SuggestGetAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Suggest", p, new [] { POST, GET }, "/_suggest", "/{index}/_suggest");
		}
		
		internal ElasticsearchResponse<T> TermvectorsDispatch<T>(RequestPath<TermVectorsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.TermvectorsGet<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.TermvectorsGet<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Termvectors<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.Termvectors<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Termvectors", p, new [] { GET, POST }, "/{index}/{type}/_termvectors", "/{index}/{type}/{id}/_termvectors");
		}
		
		internal Task<ElasticsearchResponse<T>> TermvectorsDispatchAsync<T>(RequestPath<TermVectorsRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.TermvectorsGetAsync<T>(p.Index,p.Type,p.Id,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.TermvectorsGetAsync<T>(p.Index,p.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.TermvectorsAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					if (AllSet(p.Index, p.Type)) return _lowLevel.TermvectorsAsync<T>(p.Index,p.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Termvectors", p, new [] { GET, POST }, "/{index}/{type}/_termvectors", "/{index}/{type}/{id}/_termvectors");
		}
		
		internal ElasticsearchResponse<T> UpdateDispatch<T>(RequestPath<UpdateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.Update<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Update", p, new [] { POST }, "/{index}/{type}/{id}/_update");
		}
		
		internal Task<ElasticsearchResponse<T>> UpdateDispatchAsync<T>(RequestPath<UpdateRequestParameters> p , object body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.Index, p.Type, p.Id)) return _lowLevel.UpdateAsync<T>(p.Index,p.Type,p.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Update", p, new [] { POST }, "/{index}/{type}/{id}/_update");
		}
		
	}	
}
