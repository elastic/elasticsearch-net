using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using static Elasticsearch.Net.HttpMethod;

//Generated File Please Do Not Edit Manually


namespace Nest
{
	///<summary>This dispatches highlevel requests into the proper lowlevel client overload method</summary>
	internal partial class LowLevelDispatch
	{

		internal ElasticsearchResponse<T> BulkDispatch<T>(IRequest<BulkRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Bulk<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Bulk<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Bulk<T>(body,u => p.RequestParameters);

				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.BulkPut<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.BulkPut<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.BulkPut<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Bulk", p, new [] { POST, PUT }, "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk");
		}
		
		internal Task<ElasticsearchResponse<T>> BulkDispatchAsync<T>(IRequest<BulkRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.BulkAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.BulkAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.BulkAsync<T>(body,u => p.RequestParameters);

				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.BulkPutAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.BulkPutAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.BulkPutAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Bulk", p, new [] { POST, PUT }, "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk");
		}
		
		internal ElasticsearchResponse<T> CatAliasesDispatch<T>(IRequest<CatAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Name)) return _lowLevel.CatAliases<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.CatAliases<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAliases", p, new [] { GET }, "/_cat/aliases", "/_cat/aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatAliasesDispatchAsync<T>(IRequest<CatAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Name)) return _lowLevel.CatAliasesAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.CatAliasesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAliases", p, new [] { GET }, "/_cat/aliases", "/_cat/aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> CatAllocationDispatch<T>(IRequest<CatAllocationRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.CatAllocation<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					return _lowLevel.CatAllocation<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAllocation", p, new [] { GET }, "/_cat/allocation", "/_cat/allocation/{node_id}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatAllocationDispatchAsync<T>(IRequest<CatAllocationRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.CatAllocationAsync<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					return _lowLevel.CatAllocationAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatAllocation", p, new [] { GET }, "/_cat/allocation", "/_cat/allocation/{node_id}");
		}
		
		internal ElasticsearchResponse<T> CatCountDispatch<T>(IRequest<CatCountRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatCount<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatCount<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatCount", p, new [] { GET }, "/_cat/count", "/_cat/count/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatCountDispatchAsync<T>(IRequest<CatCountRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatCountAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatCountAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatCount", p, new [] { GET }, "/_cat/count", "/_cat/count/{index}");
		}
		
		internal ElasticsearchResponse<T> CatFielddataDispatch<T>(IRequest<CatFielddataRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Fields)) return _lowLevel.CatFielddata<T>(p.RouteValues.Fields,u => p.RequestParameters);
					return _lowLevel.CatFielddata<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatFielddata", p, new [] { GET }, "/_cat/fielddata", "/_cat/fielddata/{fields}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatFielddataDispatchAsync<T>(IRequest<CatFielddataRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Fields)) return _lowLevel.CatFielddataAsync<T>(p.RouteValues.Fields,u => p.RequestParameters);
					return _lowLevel.CatFielddataAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatFielddata", p, new [] { GET }, "/_cat/fielddata", "/_cat/fielddata/{fields}");
		}
		
		internal ElasticsearchResponse<T> CatHealthDispatch<T>(IRequest<CatHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHealth<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHealth", p, new [] { GET }, "/_cat/health");
		}
		
		internal Task<ElasticsearchResponse<T>> CatHealthDispatchAsync<T>(IRequest<CatHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHealthAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHealth", p, new [] { GET }, "/_cat/health");
		}
		
		internal ElasticsearchResponse<T> CatHelpDispatch<T>(IRequest<CatHelpRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHelp<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHelp", p, new [] { GET }, "/_cat");
		}
		
		internal Task<ElasticsearchResponse<T>> CatHelpDispatchAsync<T>(IRequest<CatHelpRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatHelpAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatHelp", p, new [] { GET }, "/_cat");
		}
		
		internal ElasticsearchResponse<T> CatIndicesDispatch<T>(IRequest<CatIndicesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatIndices<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatIndices<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatIndices", p, new [] { GET }, "/_cat/indices", "/_cat/indices/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatIndicesDispatchAsync<T>(IRequest<CatIndicesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatIndicesAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatIndicesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatIndices", p, new [] { GET }, "/_cat/indices", "/_cat/indices/{index}");
		}
		
		internal ElasticsearchResponse<T> CatMasterDispatch<T>(IRequest<CatMasterRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatMaster<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatMaster", p, new [] { GET }, "/_cat/master");
		}
		
		internal Task<ElasticsearchResponse<T>> CatMasterDispatchAsync<T>(IRequest<CatMasterRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatMasterAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatMaster", p, new [] { GET }, "/_cat/master");
		}
		
		internal ElasticsearchResponse<T> CatNodeattrsDispatch<T>(IRequest<CatNodeattrsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodeattrs<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodeattrs", p, new [] { GET }, "/_cat/nodeattrs");
		}
		
		internal Task<ElasticsearchResponse<T>> CatNodeattrsDispatchAsync<T>(IRequest<CatNodeattrsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodeattrsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodeattrs", p, new [] { GET }, "/_cat/nodeattrs");
		}
		
		internal ElasticsearchResponse<T> CatNodesDispatch<T>(IRequest<CatNodesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodes<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodes", p, new [] { GET }, "/_cat/nodes");
		}
		
		internal Task<ElasticsearchResponse<T>> CatNodesDispatchAsync<T>(IRequest<CatNodesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatNodesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatNodes", p, new [] { GET }, "/_cat/nodes");
		}
		
		internal ElasticsearchResponse<T> CatPendingTasksDispatch<T>(IRequest<CatPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPendingTasks<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPendingTasks", p, new [] { GET }, "/_cat/pending_tasks");
		}
		
		internal Task<ElasticsearchResponse<T>> CatPendingTasksDispatchAsync<T>(IRequest<CatPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPendingTasksAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPendingTasks", p, new [] { GET }, "/_cat/pending_tasks");
		}
		
		internal ElasticsearchResponse<T> CatPluginsDispatch<T>(IRequest<CatPluginsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPlugins<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPlugins", p, new [] { GET }, "/_cat/plugins");
		}
		
		internal Task<ElasticsearchResponse<T>> CatPluginsDispatchAsync<T>(IRequest<CatPluginsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatPluginsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatPlugins", p, new [] { GET }, "/_cat/plugins");
		}
		
		internal ElasticsearchResponse<T> CatRecoveryDispatch<T>(IRequest<CatRecoveryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatRecovery<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatRecovery<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatRecovery", p, new [] { GET }, "/_cat/recovery", "/_cat/recovery/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatRecoveryDispatchAsync<T>(IRequest<CatRecoveryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatRecoveryAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatRecoveryAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatRecovery", p, new [] { GET }, "/_cat/recovery", "/_cat/recovery/{index}");
		}
		
		internal ElasticsearchResponse<T> CatRepositoriesDispatch<T>(IRequest<CatRepositoriesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatRepositories<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatRepositories", p, new [] { GET }, "/_cat/repositories");
		}
		
		internal Task<ElasticsearchResponse<T>> CatRepositoriesDispatchAsync<T>(IRequest<CatRepositoriesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatRepositoriesAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatRepositories", p, new [] { GET }, "/_cat/repositories");
		}
		
		internal ElasticsearchResponse<T> CatSegmentsDispatch<T>(IRequest<CatSegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatSegments<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatSegments<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatSegments", p, new [] { GET }, "/_cat/segments", "/_cat/segments/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatSegmentsDispatchAsync<T>(IRequest<CatSegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatSegmentsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatSegmentsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatSegments", p, new [] { GET }, "/_cat/segments", "/_cat/segments/{index}");
		}
		
		internal ElasticsearchResponse<T> CatShardsDispatch<T>(IRequest<CatShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatShards<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatShards<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatShards", p, new [] { GET }, "/_cat/shards", "/_cat/shards/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatShardsDispatchAsync<T>(IRequest<CatShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CatShardsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CatShardsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatShards", p, new [] { GET }, "/_cat/shards", "/_cat/shards/{index}");
		}
		
		internal ElasticsearchResponse<T> CatSnapshotsDispatch<T>(IRequest<CatSnapshotsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.CatSnapshots<T>(p.RouteValues.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("CatSnapshots", p, new [] { GET }, "/_cat/snapshots/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> CatSnapshotsDispatchAsync<T>(IRequest<CatSnapshotsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.CatSnapshotsAsync<T>(p.RouteValues.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("CatSnapshots", p, new [] { GET }, "/_cat/snapshots/{repository}");
		}
		
		internal ElasticsearchResponse<T> CatThreadPoolDispatch<T>(IRequest<CatThreadPoolRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatThreadPool<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatThreadPool", p, new [] { GET }, "/_cat/thread_pool");
		}
		
		internal Task<ElasticsearchResponse<T>> CatThreadPoolDispatchAsync<T>(IRequest<CatThreadPoolRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.CatThreadPoolAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("CatThreadPool", p, new [] { GET }, "/_cat/thread_pool");
		}
		
		internal ElasticsearchResponse<T> ClearScrollDispatch<T>(IRequest<ClearScrollRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					return _lowLevel.ClearScroll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClearScroll", p, new [] { DELETE }, "/_search/scroll");
		}
		
		internal Task<ElasticsearchResponse<T>> ClearScrollDispatchAsync<T>(IRequest<ClearScrollRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					return _lowLevel.ClearScrollAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClearScroll", p, new [] { DELETE }, "/_search/scroll");
		}
		
		internal ElasticsearchResponse<T> ClusterGetSettingsDispatch<T>(IRequest<ClusterGetSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterGetSettings<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterGetSettings", p, new [] { GET }, "/_cluster/settings");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterGetSettingsDispatchAsync<T>(IRequest<ClusterGetSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterGetSettingsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterGetSettings", p, new [] { GET }, "/_cluster/settings");
		}
		
		internal ElasticsearchResponse<T> ClusterHealthDispatch<T>(IRequest<ClusterHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.ClusterHealth<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.ClusterHealth<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterHealth", p, new [] { GET }, "/_cluster/health", "/_cluster/health/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterHealthDispatchAsync<T>(IRequest<ClusterHealthRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.ClusterHealthAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.ClusterHealthAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterHealth", p, new [] { GET }, "/_cluster/health", "/_cluster/health/{index}");
		}
		
		internal ElasticsearchResponse<T> ClusterPendingTasksDispatch<T>(IRequest<ClusterPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterPendingTasks<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPendingTasks", p, new [] { GET }, "/_cluster/pending_tasks");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPendingTasksDispatchAsync<T>(IRequest<ClusterPendingTasksRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ClusterPendingTasksAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPendingTasks", p, new [] { GET }, "/_cluster/pending_tasks");
		}
		
		internal ElasticsearchResponse<T> ClusterPutSettingsDispatch<T>(IRequest<ClusterPutSettingsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					return _lowLevel.ClusterPutSettings<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPutSettings", p, new [] { PUT }, "/_cluster/settings");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPutSettingsDispatchAsync<T>(IRequest<ClusterPutSettingsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					return _lowLevel.ClusterPutSettingsAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterPutSettings", p, new [] { PUT }, "/_cluster/settings");
		}
		
		internal ElasticsearchResponse<T> ClusterRerouteDispatch<T>(IRequest<ClusterRerouteRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.ClusterReroute<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterReroute", p, new [] { POST }, "/_cluster/reroute");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterRerouteDispatchAsync<T>(IRequest<ClusterRerouteRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.ClusterRerouteAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterReroute", p, new [] { POST }, "/_cluster/reroute");
		}
		
		internal ElasticsearchResponse<T> ClusterStateDispatch<T>(IRequest<ClusterStateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Metric, p.RouteValues.Index)) return _lowLevel.ClusterState<T>(p.RouteValues.Metric,p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.ClusterState<T>(p.RouteValues.Metric,u => p.RequestParameters);
					return _lowLevel.ClusterState<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterState", p, new [] { GET }, "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStateDispatchAsync<T>(IRequest<ClusterStateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Metric, p.RouteValues.Index)) return _lowLevel.ClusterStateAsync<T>(p.RouteValues.Metric,p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.ClusterStateAsync<T>(p.RouteValues.Metric,u => p.RequestParameters);
					return _lowLevel.ClusterStateAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterState", p, new [] { GET }, "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}");
		}
		
		internal ElasticsearchResponse<T> ClusterStatsDispatch<T>(IRequest<ClusterStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.ClusterStats<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					return _lowLevel.ClusterStats<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterStats", p, new [] { GET }, "/_cluster/stats", "/_cluster/stats/nodes/{node_id}");
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStatsDispatchAsync<T>(IRequest<ClusterStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.ClusterStatsAsync<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					return _lowLevel.ClusterStatsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("ClusterStats", p, new [] { GET }, "/_cluster/stats", "/_cluster/stats/nodes/{node_id}");
		}
		
		internal ElasticsearchResponse<T> CountDispatch<T>(IRequest<CountRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Count<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Count<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Count<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CountGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CountGet<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Count", p, new [] { POST, GET }, "/_count", "/{index}/_count", "/{index}/{type}/_count");
		}
		
		internal Task<ElasticsearchResponse<T>> CountDispatchAsync<T>(IRequest<CountRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CountAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.CountAsync<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.CountGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.CountGetAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Count", p, new [] { POST, GET }, "/_count", "/{index}/_count", "/{index}/{type}/_count");
		}
		
		internal ElasticsearchResponse<T> CountPercolateDispatch<T>(IRequest<PercolateCountRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.CountPercolateGet<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountPercolateGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.CountPercolate<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountPercolate<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("CountPercolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate/count", "/{index}/{type}/{id}/_percolate/count");
		}
		
		internal Task<ElasticsearchResponse<T>> CountPercolateDispatchAsync<T>(IRequest<PercolateCountRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.CountPercolateGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountPercolateGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.CountPercolateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.CountPercolateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("CountPercolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate/count", "/{index}/{type}/{id}/_percolate/count");
		}
		
		internal ElasticsearchResponse<T> DeleteDispatch<T>(IRequest<DeleteRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Delete<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Delete", p, new [] { DELETE }, "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteDispatchAsync<T>(IRequest<DeleteRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.DeleteAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Delete", p, new [] { DELETE }, "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> DeleteScriptDispatch<T>(IRequest<DeleteScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.DeleteScript<T>(p.RouteValues.Lang,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteScript", p, new [] { DELETE }, "/_scripts/{lang}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteScriptDispatchAsync<T>(IRequest<DeleteScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.DeleteScriptAsync<T>(p.RouteValues.Lang,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteScript", p, new [] { DELETE }, "/_scripts/{lang}/{id}");
		}
		
		internal ElasticsearchResponse<T> DeleteTemplateDispatch<T>(IRequest<DeleteSearchTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.DeleteTemplate<T>(p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteTemplate", p, new [] { DELETE }, "/_search/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteTemplateDispatchAsync<T>(IRequest<DeleteSearchTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.DeleteTemplateAsync<T>(p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteTemplate", p, new [] { DELETE }, "/_search/template/{id}");
		}
		
		internal ElasticsearchResponse<T> ExistsDispatch<T>(IRequest<DocumentExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Exists<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Exists", p, new [] { HEAD }, "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> ExistsDispatchAsync<T>(IRequest<DocumentExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.ExistsAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Exists", p, new [] { HEAD }, "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> ExplainDispatch<T>(IRequest<ExplainRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.ExplainGet<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Explain<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Explain", p, new [] { GET, POST }, "/{index}/{type}/{id}/_explain");
		}
		
		internal Task<ElasticsearchResponse<T>> ExplainDispatchAsync<T>(IRequest<ExplainRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.ExplainGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.ExplainAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Explain", p, new [] { GET, POST }, "/{index}/{type}/{id}/_explain");
		}
		
		internal ElasticsearchResponse<T> FieldStatsDispatch<T>(IRequest<FieldStatsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.FieldStatsGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.FieldStatsGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.FieldStats<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.FieldStats<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("FieldStats", p, new [] { GET, POST }, "/_field_stats", "/{index}/_field_stats");
		}
		
		internal Task<ElasticsearchResponse<T>> FieldStatsDispatchAsync<T>(IRequest<FieldStatsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.FieldStatsGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.FieldStatsGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.FieldStatsAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.FieldStatsAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("FieldStats", p, new [] { GET, POST }, "/_field_stats", "/{index}/_field_stats");
		}
		
		internal ElasticsearchResponse<T> GetDispatch<T>(IRequest<GetRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Get<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Get", p, new [] { GET }, "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> GetDispatchAsync<T>(IRequest<GetRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.GetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Get", p, new [] { GET }, "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> GetScriptDispatch<T>(IRequest<GetScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.GetScript<T>(p.RouteValues.Lang,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetScript", p, new [] { GET }, "/_scripts/{lang}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> GetScriptDispatchAsync<T>(IRequest<GetScriptRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.GetScriptAsync<T>(p.RouteValues.Lang,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetScript", p, new [] { GET }, "/_scripts/{lang}/{id}");
		}
		
		internal ElasticsearchResponse<T> GetSourceDispatch<T>(IRequest<SourceRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.GetSource<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetSource", p, new [] { GET }, "/{index}/{type}/{id}/_source");
		}
		
		internal Task<ElasticsearchResponse<T>> GetSourceDispatchAsync<T>(IRequest<SourceRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.GetSourceAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetSource", p, new [] { GET }, "/{index}/{type}/{id}/_source");
		}
		
		internal ElasticsearchResponse<T> GetTemplateDispatch<T>(IRequest<GetSearchTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.GetTemplate<T>(p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetTemplate", p, new [] { GET }, "/_search/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> GetTemplateDispatchAsync<T>(IRequest<GetSearchTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.GetTemplateAsync<T>(p.RouteValues.Id,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GetTemplate", p, new [] { GET }, "/_search/template/{id}");
		}
		
		internal ElasticsearchResponse<T> IndexDispatch<T>(IRequest<IndexRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Index<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Index<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.IndexPut<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndexPut<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Index", p, new [] { POST, PUT }, "/{index}/{type}", "/{index}/{type}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndexDispatchAsync<T>(IRequest<IndexRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.IndexAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndexAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.IndexPutAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndexPutAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Index", p, new [] { POST, PUT }, "/{index}/{type}", "/{index}/{type}/{id}");
		}
		
		internal ElasticsearchResponse<T> IndicesAnalyzeDispatch<T>(IRequest<AnalyzeRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesAnalyzeGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeGetForAll<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesAnalyze<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesAnalyze", p, new [] { GET, POST }, "/_analyze", "/{index}/_analyze");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesAnalyzeDispatchAsync<T>(IRequest<AnalyzeRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesAnalyzeGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeGetForAllAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesAnalyzeAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesAnalyzeForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesAnalyze", p, new [] { GET, POST }, "/_analyze", "/{index}/_analyze");
		}
		
		internal ElasticsearchResponse<T> IndicesClearCacheDispatch<T>(IRequest<ClearCacheRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesClearCache<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesClearCacheGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesClearCache", p, new [] { POST, GET }, "/_cache/clear", "/{index}/_cache/clear");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesClearCacheDispatchAsync<T>(IRequest<ClearCacheRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesClearCacheAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesClearCacheGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesClearCacheGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesClearCache", p, new [] { POST, GET }, "/_cache/clear", "/{index}/_cache/clear");
		}
		
		internal ElasticsearchResponse<T> IndicesCloseDispatch<T>(IRequest<CloseIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesClose<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesClose", p, new [] { POST }, "/{index}/_close");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesCloseDispatchAsync<T>(IRequest<CloseIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesCloseAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesClose", p, new [] { POST }, "/{index}/_close");
		}
		
		internal ElasticsearchResponse<T> IndicesCreateDispatch<T>(IRequest<CreateIndexRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesCreate<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesCreatePost<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesCreate", p, new [] { PUT, POST }, "/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesCreateDispatchAsync<T>(IRequest<CreateIndexRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesCreateAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesCreatePostAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesCreate", p, new [] { PUT, POST }, "/{index}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteDispatch<T>(IRequest<DeleteIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesDelete<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDelete", p, new [] { DELETE }, "/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteDispatchAsync<T>(IRequest<DeleteIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesDeleteAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDelete", p, new [] { DELETE }, "/{index}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteAliasDispatch<T>(IRequest<DeleteAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesDeleteAlias<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteAlias", p, new [] { DELETE }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteAliasDispatchAsync<T>(IRequest<DeleteAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesDeleteAliasAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteAlias", p, new [] { DELETE }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteTemplateDispatch<T>(IRequest<DeleteIndexTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesDeleteTemplateForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteTemplate", p, new [] { DELETE }, "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteTemplateDispatchAsync<T>(IRequest<DeleteIndexTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesDeleteTemplateForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteTemplate", p, new [] { DELETE }, "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteWarmerDispatch<T>(IRequest<DeleteWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesDeleteWarmer<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteWarmer", p, new [] { DELETE }, "/{index}/_warmer/{name}", "/{index}/_warmers/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteWarmerDispatchAsync<T>(IRequest<DeleteWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesDeleteWarmerAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesDeleteWarmer", p, new [] { DELETE }, "/{index}/_warmer/{name}", "/{index}/_warmers/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsDispatch<T>(IRequest<IndexExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesExists<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExists", p, new [] { HEAD }, "/{index}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsDispatchAsync<T>(IRequest<IndexExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesExistsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExists", p, new [] { HEAD }, "/{index}");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsAliasDispatch<T>(IRequest<AliasExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesExistsAlias<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesExistsAliasForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesExistsAlias<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsAlias", p, new [] { HEAD }, "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsAliasDispatchAsync<T>(IRequest<AliasExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesExistsAliasAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesExistsAliasForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesExistsAliasAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsAlias", p, new [] { HEAD }, "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsTemplateDispatch<T>(IRequest<IndexTemplateExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesExistsTemplateForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsTemplate", p, new [] { HEAD }, "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsTemplateDispatchAsync<T>(IRequest<IndexTemplateExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesExistsTemplateForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsTemplate", p, new [] { HEAD }, "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesExistsTypeDispatch<T>(IRequest<TypeExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesExistsType<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsType", p, new [] { HEAD }, "/{index}/{type}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsTypeDispatchAsync<T>(IRequest<TypeExistsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesExistsTypeAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesExistsType", p, new [] { HEAD }, "/{index}/{type}");
		}
		
		internal ElasticsearchResponse<T> IndicesFlushDispatch<T>(IRequest<FlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlush<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlush", p, new [] { POST, GET }, "/_flush", "/{index}/_flush");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushDispatchAsync<T>(IRequest<FlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlush", p, new [] { POST, GET }, "/_flush", "/{index}/_flush");
		}
		
		internal ElasticsearchResponse<T> IndicesFlushSyncedDispatch<T>(IRequest<SyncedFlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushSynced<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushSyncedGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlushSynced", p, new [] { POST, GET }, "/_flush/synced", "/{index}/_flush/synced");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushSyncedDispatchAsync<T>(IRequest<SyncedFlushRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushSyncedAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesFlushSyncedGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesFlushSyncedGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesFlushSynced", p, new [] { POST, GET }, "/_flush/synced", "/{index}/_flush/synced");
		}
		
		internal ElasticsearchResponse<T> IndicesForcemergeDispatch<T>(IRequest<ForceMergeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesForcemerge<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesForcemergeForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesForcemergeGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesForcemergeGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesForcemerge", p, new [] { POST, GET }, "/_forcemerge", "/{index}/_forcemerge");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesForcemergeDispatchAsync<T>(IRequest<ForceMergeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesForcemergeAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesForcemergeForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesForcemergeGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesForcemergeGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesForcemerge", p, new [] { POST, GET }, "/_forcemerge", "/{index}/_forcemerge");
		}
		
		internal ElasticsearchResponse<T> IndicesGetDispatch<T>(IRequest<GetIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Feature)) return _lowLevel.IndicesGet<T>(p.RouteValues.Index,p.RouteValues.Feature,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGet", p, new [] { GET }, "/{index}", "/{index}/{feature}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetDispatchAsync<T>(IRequest<GetIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Feature)) return _lowLevel.IndicesGetAsync<T>(p.RouteValues.Index,p.RouteValues.Feature,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGet", p, new [] { GET }, "/{index}", "/{index}/{feature}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasDispatch<T>(IRequest<GetAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetAlias<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetAliasForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetAlias<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAlias", p, new [] { GET }, "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasDispatchAsync<T>(IRequest<GetAliasRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetAliasAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetAliasForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetAliasAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAlias", p, new [] { GET }, "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias");
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasesDispatch<T>(IRequest<GetAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetAliases<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetAliases<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetAliasesForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasesForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAliases", p, new [] { GET }, "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasesDispatchAsync<T>(IRequest<GetAliasesRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetAliasesAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetAliasesAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetAliasesForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetAliasesForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetAliases", p, new [] { GET }, "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetFieldMappingDispatch<T>(IRequest<GetFieldMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMapping<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Fields,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMapping<T>(p.RouteValues.Index,p.RouteValues.Fields,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Type, p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMappingForAll<T>(p.RouteValues.Type,p.RouteValues.Fields,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMappingForAll<T>(p.RouteValues.Fields,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGetFieldMapping", p, new [] { GET }, "/_mapping/field/{fields}", "/{index}/_mapping/field/{fields}", "/_mapping/{type}/field/{fields}", "/{index}/_mapping/{type}/field/{fields}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetFieldMappingDispatchAsync<T>(IRequest<GetFieldMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMappingAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Fields,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMappingAsync<T>(p.RouteValues.Index,p.RouteValues.Fields,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Type, p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMappingForAllAsync<T>(p.RouteValues.Type,p.RouteValues.Fields,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Fields)) return _lowLevel.IndicesGetFieldMappingForAllAsync<T>(p.RouteValues.Fields,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesGetFieldMapping", p, new [] { GET }, "/_mapping/field/{fields}", "/{index}/_mapping/field/{fields}", "/_mapping/{type}/field/{fields}", "/{index}/_mapping/{type}/field/{fields}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetMappingDispatch<T>(IRequest<GetMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesGetMapping<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetMapping<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Type)) return _lowLevel.IndicesGetMappingForAll<T>(p.RouteValues.Type,u => p.RequestParameters);
					return _lowLevel.IndicesGetMappingForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetMapping", p, new [] { GET }, "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetMappingDispatchAsync<T>(IRequest<GetMappingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesGetMappingAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetMappingAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Type)) return _lowLevel.IndicesGetMappingForAllAsync<T>(p.RouteValues.Type,u => p.RequestParameters);
					return _lowLevel.IndicesGetMappingForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetMapping", p, new [] { GET }, "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetSettingsDispatch<T>(IRequest<GetIndexSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetSettings<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetSettings<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetSettingsForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetSettingsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetSettings", p, new [] { GET }, "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetSettingsDispatchAsync<T>(IRequest<GetIndexSettingsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetSettingsAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetSettingsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetSettingsForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetSettingsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetSettings", p, new [] { GET }, "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetTemplateDispatch<T>(IRequest<GetIndexTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetTemplateForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetTemplateForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetTemplate", p, new [] { GET }, "/_template", "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetTemplateDispatchAsync<T>(IRequest<GetIndexTemplateRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetTemplateForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetTemplateForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetTemplate", p, new [] { GET }, "/_template", "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesGetUpgradeDispatch<T>(IRequest<UpgradeStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetUpgrade<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetUpgradeForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetUpgrade", p, new [] { GET }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetUpgradeDispatchAsync<T>(IRequest<UpgradeStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetUpgradeAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesGetUpgradeForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetUpgrade", p, new [] { GET }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal ElasticsearchResponse<T> IndicesGetWarmerDispatch<T>(IRequest<GetWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Name)) return _lowLevel.IndicesGetWarmer<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetWarmer<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetWarmer<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetWarmerForAll<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetWarmerForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetWarmer", p, new [] { GET }, "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetWarmerDispatchAsync<T>(IRequest<GetWarmerRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Name)) return _lowLevel.IndicesGetWarmerAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesGetWarmerAsync<T>(p.RouteValues.Index,p.RouteValues.Name,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesGetWarmerAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Name)) return _lowLevel.IndicesGetWarmerForAllAsync<T>(p.RouteValues.Name,u => p.RequestParameters);
					return _lowLevel.IndicesGetWarmerForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesGetWarmer", p, new [] { GET }, "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesOpenDispatch<T>(IRequest<OpenIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesOpen<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesOpen", p, new [] { POST }, "/{index}/_open");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOpenDispatchAsync<T>(IRequest<OpenIndexRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.IndicesOpenAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesOpen", p, new [] { POST }, "/{index}/_open");
		}
		
		internal ElasticsearchResponse<T> IndicesOptimizeDispatch<T>(IRequest<OptimizeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesOptimize<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesOptimizeGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesOptimize", p, new [] { POST, GET }, "/_optimize", "/{index}/_optimize");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOptimizeDispatchAsync<T>(IRequest<OptimizeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesOptimizeAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesOptimizeGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesOptimizeGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesOptimize", p, new [] { POST, GET }, "/_optimize", "/{index}/_optimize");
		}
		
		internal ElasticsearchResponse<T> IndicesPutAliasDispatch<T>(IRequest<PutAliasRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutAlias<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutAliasPost<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutAlias", p, new [] { PUT, POST }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutAliasDispatchAsync<T>(IRequest<PutAliasRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutAliasAsync<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutAliasPostAsync<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutAlias", p, new [] { PUT, POST }, "/{index}/_alias/{name}", "/{index}/_aliases/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesPutMappingDispatch<T>(IRequest<PutMappingRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesPutMapping<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Type)) return _lowLevel.IndicesPutMappingForAll<T>(p.RouteValues.Type,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesPutMappingPost<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Type)) return _lowLevel.IndicesPutMappingPostForAll<T>(p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutMapping", p, new [] { PUT, POST }, "/{index}/{type}/_mapping", "/{index}/_mapping/{type}", "/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}", "/_mappings/{type}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutMappingDispatchAsync<T>(IRequest<PutMappingRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesPutMappingAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Type)) return _lowLevel.IndicesPutMappingForAllAsync<T>(p.RouteValues.Type,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesPutMappingPostAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Type)) return _lowLevel.IndicesPutMappingPostForAllAsync<T>(p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutMapping", p, new [] { PUT, POST }, "/{index}/{type}/_mapping", "/{index}/_mapping/{type}", "/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}", "/_mappings/{type}");
		}
		
		internal ElasticsearchResponse<T> IndicesPutSettingsDispatch<T>(IRequest<UpdateIndexSettingsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesPutSettings<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesPutSettingsForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesPutSettings", p, new [] { PUT }, "/_settings", "/{index}/_settings");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutSettingsDispatchAsync<T>(IRequest<UpdateIndexSettingsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesPutSettingsAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesPutSettingsForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesPutSettings", p, new [] { PUT }, "/_settings", "/{index}/_settings");
		}
		
		internal ElasticsearchResponse<T> IndicesPutTemplateDispatch<T>(IRequest<PutIndexTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutTemplateForAll<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutTemplatePostForAll<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutTemplate", p, new [] { PUT, POST }, "/_template/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutTemplateDispatchAsync<T>(IRequest<PutIndexTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutTemplateForAllAsync<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutTemplatePostForAllAsync<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutTemplate", p, new [] { PUT, POST }, "/_template/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesPutWarmerDispatch<T>(IRequest<PutWarmerRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmer<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmer<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerForAll<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerPost<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerPost<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerPostForAll<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutWarmer", p, new [] { PUT, POST }, "/_warmer/{name}", "/{index}/_warmer/{name}", "/{index}/{type}/_warmer/{name}", "/_warmers/{name}", "/{index}/_warmers/{name}", "/{index}/{type}/_warmers/{name}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutWarmerDispatchAsync<T>(IRequest<PutWarmerRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerAsync<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerForAllAsync<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerPostAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index, p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerPostAsync<T>(p.RouteValues.Index,p.RouteValues.Name,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Name)) return _lowLevel.IndicesPutWarmerPostForAllAsync<T>(p.RouteValues.Name,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("IndicesPutWarmer", p, new [] { PUT, POST }, "/_warmer/{name}", "/{index}/_warmer/{name}", "/{index}/{type}/_warmer/{name}", "/_warmers/{name}", "/{index}/_warmers/{name}", "/{index}/{type}/_warmers/{name}");
		}
		
		internal ElasticsearchResponse<T> IndicesRecoveryDispatch<T>(IRequest<RecoveryStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesRecovery<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRecoveryForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRecovery", p, new [] { GET }, "/_recovery", "/{index}/_recovery");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRecoveryDispatchAsync<T>(IRequest<RecoveryStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesRecoveryAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRecoveryForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRecovery", p, new [] { GET }, "/_recovery", "/{index}/_recovery");
		}
		
		internal ElasticsearchResponse<T> IndicesRefreshDispatch<T>(IRequest<RefreshRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesRefresh<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshForAll<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesRefreshGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshGetForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRefresh", p, new [] { POST, GET }, "/_refresh", "/{index}/_refresh");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRefreshDispatchAsync<T>(IRequest<RefreshRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesRefreshAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshForAllAsync<T>(u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesRefreshGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesRefreshGetForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesRefresh", p, new [] { POST, GET }, "/_refresh", "/{index}/_refresh");
		}
		
		internal ElasticsearchResponse<T> IndicesSegmentsDispatch<T>(IRequest<SegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesSegments<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesSegmentsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesSegments", p, new [] { GET }, "/_segments", "/{index}/_segments");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesSegmentsDispatchAsync<T>(IRequest<SegmentsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesSegmentsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesSegmentsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesSegments", p, new [] { GET }, "/_segments", "/{index}/_segments");
		}
		
		internal ElasticsearchResponse<T> IndicesShardStoresDispatch<T>(IRequest<IndicesShardStoresRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesShardStores<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesShardStoresForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesShardStores", p, new [] { GET }, "/_shard_stores", "/{index}/_shard_stores");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesShardStoresDispatchAsync<T>(IRequest<IndicesShardStoresRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesShardStoresAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesShardStoresForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesShardStores", p, new [] { GET }, "/_shard_stores", "/{index}/_shard_stores");
		}
		
		internal ElasticsearchResponse<T> IndicesStatsDispatch<T>(IRequest<IndicesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Metric)) return _lowLevel.IndicesStats<T>(p.RouteValues.Index,p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.IndicesStatsForAll<T>(p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesStats<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesStatsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesStats", p, new [] { GET }, "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesStatsDispatchAsync<T>(IRequest<IndicesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Metric)) return _lowLevel.IndicesStatsAsync<T>(p.RouteValues.Index,p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.IndicesStatsForAllAsync<T>(p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesStatsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesStatsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesStats", p, new [] { GET }, "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}");
		}
		
		internal ElasticsearchResponse<T> IndicesUpdateAliasesDispatch<T>(IRequest<BulkAliasRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.IndicesUpdateAliasesForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpdateAliases", p, new [] { POST }, "/_aliases");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpdateAliasesDispatchAsync<T>(IRequest<BulkAliasRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.IndicesUpdateAliasesForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpdateAliases", p, new [] { POST }, "/_aliases");
		}
		
		internal ElasticsearchResponse<T> IndicesUpgradeDispatch<T>(IRequest<UpgradeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesUpgrade<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesUpgradeForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpgrade", p, new [] { POST }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpgradeDispatchAsync<T>(IRequest<UpgradeRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesUpgradeAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesUpgradeForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesUpgrade", p, new [] { POST }, "/_upgrade", "/{index}/_upgrade");
		}
		
		internal ElasticsearchResponse<T> IndicesValidateQueryDispatch<T>(IRequest<ValidateQueryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesValidateQueryGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesValidateQueryGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryGetForAll<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesValidateQuery<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesValidateQuery<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryForAll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesValidateQuery", p, new [] { GET, POST }, "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query");
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesValidateQueryDispatchAsync<T>(IRequest<ValidateQueryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesValidateQueryGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesValidateQueryGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryGetForAllAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.IndicesValidateQueryAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.IndicesValidateQueryAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.IndicesValidateQueryForAllAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("IndicesValidateQuery", p, new [] { GET, POST }, "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query");
		}
		
		internal ElasticsearchResponse<T> InfoDispatch<T>(IRequest<RootNodeInfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.Info<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Info", p, new [] { GET }, "/");
		}
		
		internal Task<ElasticsearchResponse<T>> InfoDispatchAsync<T>(IRequest<RootNodeInfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.InfoAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Info", p, new [] { GET }, "/");
		}
		
		internal ElasticsearchResponse<T> MgetDispatch<T>(IRequest<MultiGetRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MgetGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MgetGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MgetGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Mget<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Mget<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Mget<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mget", p, new [] { GET, POST }, "/_mget", "/{index}/_mget", "/{index}/{type}/_mget");
		}
		
		internal Task<ElasticsearchResponse<T>> MgetDispatchAsync<T>(IRequest<MultiGetRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MgetGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MgetGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MgetGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MgetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MgetAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.MgetAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mget", p, new [] { GET, POST }, "/_mget", "/{index}/_mget", "/{index}/{type}/_mget");
		}
		
		internal ElasticsearchResponse<T> MpercolateDispatch<T>(IRequest<MultiPercolateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MpercolateGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MpercolateGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MpercolateGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Mpercolate<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Mpercolate<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Mpercolate<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mpercolate", p, new [] { GET, POST }, "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate");
		}
		
		internal Task<ElasticsearchResponse<T>> MpercolateDispatchAsync<T>(IRequest<MultiPercolateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MpercolateGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MpercolateGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MpercolateGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MpercolateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MpercolateAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.MpercolateAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mpercolate", p, new [] { GET, POST }, "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate");
		}
		
		internal ElasticsearchResponse<T> MsearchDispatch<T>(IRequest<MultiSearchRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MsearchGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MsearchGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MsearchGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Msearch<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Msearch<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Msearch<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Msearch", p, new [] { GET, POST }, "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch");
		}
		
		internal Task<ElasticsearchResponse<T>> MsearchDispatchAsync<T>(IRequest<MultiSearchRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MsearchGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MsearchGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MsearchGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MsearchAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MsearchAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.MsearchAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Msearch", p, new [] { GET, POST }, "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch");
		}
		
		internal ElasticsearchResponse<T> MtermvectorsDispatch<T>(IRequest<MultiTermVectorsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MtermvectorsGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MtermvectorsGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MtermvectorsGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Mtermvectors<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Mtermvectors<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Mtermvectors<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mtermvectors", p, new [] { GET, POST }, "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors");
		}
		
		internal Task<ElasticsearchResponse<T>> MtermvectorsDispatchAsync<T>(IRequest<MultiTermVectorsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MtermvectorsGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MtermvectorsGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.MtermvectorsGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.MtermvectorsAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.MtermvectorsAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.MtermvectorsAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Mtermvectors", p, new [] { GET, POST }, "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors");
		}
		
		internal ElasticsearchResponse<T> NodesHotThreadsDispatch<T>(IRequest<NodesHotThreadsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.NodesHotThreads<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					return _lowLevel.NodesHotThreadsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesHotThreads", p, new [] { GET }, "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads");
		}
		
		internal Task<ElasticsearchResponse<T>> NodesHotThreadsDispatchAsync<T>(IRequest<NodesHotThreadsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.NodesHotThreadsAsync<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					return _lowLevel.NodesHotThreadsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesHotThreads", p, new [] { GET }, "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads");
		}
		
		internal ElasticsearchResponse<T> NodesInfoDispatch<T>(IRequest<NodesInfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId, p.RouteValues.Metric)) return _lowLevel.NodesInfo<T>(p.RouteValues.NodeId,p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.NodesInfo<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.NodesInfoForAll<T>(p.RouteValues.Metric,u => p.RequestParameters);
					return _lowLevel.NodesInfoForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesInfo", p, new [] { GET }, "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}");
		}
		
		internal Task<ElasticsearchResponse<T>> NodesInfoDispatchAsync<T>(IRequest<NodesInfoRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId, p.RouteValues.Metric)) return _lowLevel.NodesInfoAsync<T>(p.RouteValues.NodeId,p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.NodesInfoAsync<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.NodesInfoForAllAsync<T>(p.RouteValues.Metric,u => p.RequestParameters);
					return _lowLevel.NodesInfoForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesInfo", p, new [] { GET }, "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}");
		}
		
		internal ElasticsearchResponse<T> NodesStatsDispatch<T>(IRequest<NodesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId, p.RouteValues.Metric, p.RouteValues.IndexMetric)) return _lowLevel.NodesStats<T>(p.RouteValues.NodeId,p.RouteValues.Metric,p.RouteValues.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.NodeId, p.RouteValues.Metric)) return _lowLevel.NodesStats<T>(p.RouteValues.NodeId,p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric, p.RouteValues.IndexMetric)) return _lowLevel.NodesStatsForAll<T>(p.RouteValues.Metric,p.RouteValues.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.NodesStats<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.NodesStatsForAll<T>(p.RouteValues.Metric,u => p.RequestParameters);
					return _lowLevel.NodesStatsForAll<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesStats", p, new [] { GET }, "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}");
		}
		
		internal Task<ElasticsearchResponse<T>> NodesStatsDispatchAsync<T>(IRequest<NodesStatsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.NodeId, p.RouteValues.Metric, p.RouteValues.IndexMetric)) return _lowLevel.NodesStatsAsync<T>(p.RouteValues.NodeId,p.RouteValues.Metric,p.RouteValues.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.NodeId, p.RouteValues.Metric)) return _lowLevel.NodesStatsAsync<T>(p.RouteValues.NodeId,p.RouteValues.Metric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric, p.RouteValues.IndexMetric)) return _lowLevel.NodesStatsForAllAsync<T>(p.RouteValues.Metric,p.RouteValues.IndexMetric,u => p.RequestParameters);
					if (AllSet(p.RouteValues.NodeId)) return _lowLevel.NodesStatsAsync<T>(p.RouteValues.NodeId,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Metric)) return _lowLevel.NodesStatsForAllAsync<T>(p.RouteValues.Metric,u => p.RequestParameters);
					return _lowLevel.NodesStatsForAllAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("NodesStats", p, new [] { GET }, "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}");
		}
		
		internal ElasticsearchResponse<T> PercolateDispatch<T>(IRequest<PercolateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.PercolateGet<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.PercolateGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Percolate<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Percolate<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Percolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate");
		}
		
		internal Task<ElasticsearchResponse<T>> PercolateDispatchAsync<T>(IRequest<PercolateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.PercolateGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.PercolateGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.PercolateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.PercolateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Percolate", p, new [] { GET, POST }, "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate");
		}
		
		internal ElasticsearchResponse<T> PingDispatch<T>(IRequest<PingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					return _lowLevel.Ping<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Ping", p, new [] { HEAD }, "/");
		}
		
		internal Task<ElasticsearchResponse<T>> PingDispatchAsync<T>(IRequest<PingRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case HEAD:
					return _lowLevel.PingAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Ping", p, new [] { HEAD }, "/");
		}
		
		internal ElasticsearchResponse<T> PutScriptDispatch<T>(IRequest<PutScriptRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.PutScript<T>(p.RouteValues.Lang,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.PutScriptPost<T>(p.RouteValues.Lang,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutScript", p, new [] { PUT, POST }, "/_scripts/{lang}/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> PutScriptDispatchAsync<T>(IRequest<PutScriptRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.PutScriptAsync<T>(p.RouteValues.Lang,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Lang, p.RouteValues.Id)) return _lowLevel.PutScriptPostAsync<T>(p.RouteValues.Lang,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutScript", p, new [] { PUT, POST }, "/_scripts/{lang}/{id}");
		}
		
		internal ElasticsearchResponse<T> PutTemplateDispatch<T>(IRequest<PutSearchTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.PutTemplate<T>(p.RouteValues.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.PutTemplatePost<T>(p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutTemplate", p, new [] { PUT, POST }, "/_search/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> PutTemplateDispatchAsync<T>(IRequest<PutSearchTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.PutTemplateAsync<T>(p.RouteValues.Id,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Id)) return _lowLevel.PutTemplatePostAsync<T>(p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("PutTemplate", p, new [] { PUT, POST }, "/_search/template/{id}");
		}
		
		internal ElasticsearchResponse<T> ReindexDispatch<T>(IRequest<ReindexOnServerRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.Reindex<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Reindex", p, new [] { POST }, "/_reindex");
		}
		
		internal Task<ElasticsearchResponse<T>> ReindexDispatchAsync<T>(IRequest<ReindexOnServerRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					return _lowLevel.ReindexAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Reindex", p, new [] { POST }, "/_reindex");
		}
		
		internal ElasticsearchResponse<T> RenderSearchTemplateDispatch<T>(IRequest<RenderSearchTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Id)) return _lowLevel.RenderSearchTemplateGet<T>(p.RouteValues.Id,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplateGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Id)) return _lowLevel.RenderSearchTemplate<T>(p.RouteValues.Id,body,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplate<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("RenderSearchTemplate", p, new [] { GET, POST }, "/_render/template", "/_render/template/{id}");
		}
		
		internal Task<ElasticsearchResponse<T>> RenderSearchTemplateDispatchAsync<T>(IRequest<RenderSearchTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Id)) return _lowLevel.RenderSearchTemplateGetAsync<T>(p.RouteValues.Id,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplateGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Id)) return _lowLevel.RenderSearchTemplateAsync<T>(p.RouteValues.Id,body,u => p.RequestParameters);
					return _lowLevel.RenderSearchTemplateAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("RenderSearchTemplate", p, new [] { GET, POST }, "/_render/template", "/_render/template/{id}");
		}
		
		internal ElasticsearchResponse<T> ScrollDispatch<T>(IRequest<ScrollRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ScrollGet<T>(u => p.RequestParameters);

				case POST:
					return _lowLevel.Scroll<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Scroll", p, new [] { GET, POST }, "/_search/scroll");
		}
		
		internal Task<ElasticsearchResponse<T>> ScrollDispatchAsync<T>(IRequest<ScrollRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					return _lowLevel.ScrollGetAsync<T>(u => p.RequestParameters);

				case POST:
					return _lowLevel.ScrollAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Scroll", p, new [] { GET, POST }, "/_search/scroll");
		}
		
		internal ElasticsearchResponse<T> SearchDispatch<T>(IRequest<SearchRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Search<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Search<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Search<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Search", p, new [] { GET, POST }, "/_search", "/{index}/_search", "/{index}/{type}/_search");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchDispatchAsync<T>(IRequest<SearchRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("Search", p, new [] { GET, POST }, "/_search", "/{index}/_search", "/{index}/{type}/_search");
		}
		
		internal ElasticsearchResponse<T> SearchExistsDispatch<T>(IRequest<SearchExistsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchExists<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchExists<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchExists<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchExistsGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchExistsGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchExistsGet<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchExists", p, new [] { POST, GET }, "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchExistsDispatchAsync<T>(IRequest<SearchExistsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchExistsAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchExistsAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchExistsAsync<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchExistsGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchExistsGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchExistsGetAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchExists", p, new [] { POST, GET }, "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists");
		}
		
		internal ElasticsearchResponse<T> SearchShardsDispatch<T>(IRequest<SearchShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchShardsGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchShardsGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchShardsGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchShards<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchShards<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchShards<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchShards", p, new [] { GET, POST }, "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchShardsDispatchAsync<T>(IRequest<SearchShardsRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchShardsGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchShardsGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchShardsGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchShardsAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchShardsAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchShardsAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchShards", p, new [] { GET, POST }, "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards");
		}
		
		internal ElasticsearchResponse<T> SearchTemplateDispatch<T>(IRequest<SearchTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchTemplateGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchTemplateGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchTemplateGet<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchTemplate<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchTemplate<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchTemplate<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchTemplate", p, new [] { GET, POST }, "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template");
		}
		
		internal Task<ElasticsearchResponse<T>> SearchTemplateDispatchAsync<T>(IRequest<SearchTemplateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchTemplateGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchTemplateGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SearchTemplateGetAsync<T>(u => p.RequestParameters);

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.SearchTemplateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SearchTemplateAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.SearchTemplateAsync<T>(body,u => p.RequestParameters);

			}
			throw InvalidDispatch("SearchTemplate", p, new [] { GET, POST }, "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template");
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateDispatch<T>(IRequest<SnapshotRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotCreate<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotCreatePost<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreate", p, new [] { PUT, POST }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotCreateDispatchAsync<T>(IRequest<SnapshotRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotCreateAsync<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotCreatePostAsync<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreate", p, new [] { PUT, POST }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateRepositoryDispatch<T>(IRequest<CreateRepositoryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotCreateRepository<T>(p.RouteValues.Repository,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotCreateRepositoryPost<T>(p.RouteValues.Repository,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreateRepository", p, new [] { PUT, POST }, "/_snapshot/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotCreateRepositoryDispatchAsync<T>(IRequest<CreateRepositoryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case PUT:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotCreateRepositoryAsync<T>(p.RouteValues.Repository,body,u => p.RequestParameters);
					break;

				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotCreateRepositoryPostAsync<T>(p.RouteValues.Repository,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotCreateRepository", p, new [] { PUT, POST }, "/_snapshot/{repository}");
		}
		
		internal ElasticsearchResponse<T> SnapshotDeleteDispatch<T>(IRequest<DeleteSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotDelete<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDelete", p, new [] { DELETE }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotDeleteDispatchAsync<T>(IRequest<DeleteSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotDeleteAsync<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDelete", p, new [] { DELETE }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal ElasticsearchResponse<T> SnapshotDeleteRepositoryDispatch<T>(IRequest<DeleteRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotDeleteRepository<T>(p.RouteValues.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDeleteRepository", p, new [] { DELETE }, "/_snapshot/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotDeleteRepositoryDispatchAsync<T>(IRequest<DeleteRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotDeleteRepositoryAsync<T>(p.RouteValues.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotDeleteRepository", p, new [] { DELETE }, "/_snapshot/{repository}");
		}
		
		internal ElasticsearchResponse<T> SnapshotGetDispatch<T>(IRequest<GetSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotGet<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotGet", p, new [] { GET }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetDispatchAsync<T>(IRequest<GetSnapshotRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotGetAsync<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotGet", p, new [] { GET }, "/_snapshot/{repository}/{snapshot}");
		}
		
		internal ElasticsearchResponse<T> SnapshotGetRepositoryDispatch<T>(IRequest<GetRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Repository)) return _lowLevel.SnapshotGetRepository<T>(p.RouteValues.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotGetRepository<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotGetRepository", p, new [] { GET }, "/_snapshot", "/_snapshot/{repository}");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetRepositoryDispatchAsync<T>(IRequest<GetRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Repository)) return _lowLevel.SnapshotGetRepositoryAsync<T>(p.RouteValues.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotGetRepositoryAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotGetRepository", p, new [] { GET }, "/_snapshot", "/_snapshot/{repository}");
		}
		
		internal ElasticsearchResponse<T> SnapshotRestoreDispatch<T>(IRequest<RestoreRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotRestore<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotRestore", p, new [] { POST }, "/_snapshot/{repository}/{snapshot}/_restore");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotRestoreDispatchAsync<T>(IRequest<RestoreRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotRestoreAsync<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotRestore", p, new [] { POST }, "/_snapshot/{repository}/{snapshot}/_restore");
		}
		
		internal ElasticsearchResponse<T> SnapshotStatusDispatch<T>(IRequest<SnapshotStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotStatus<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Repository)) return _lowLevel.SnapshotStatus<T>(p.RouteValues.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotStatus<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotStatus", p, new [] { GET }, "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotStatusDispatchAsync<T>(IRequest<SnapshotStatusRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Repository, p.RouteValues.Snapshot)) return _lowLevel.SnapshotStatusAsync<T>(p.RouteValues.Repository,p.RouteValues.Snapshot,u => p.RequestParameters);
					if (AllSet(p.RouteValues.Repository)) return _lowLevel.SnapshotStatusAsync<T>(p.RouteValues.Repository,u => p.RequestParameters);
					return _lowLevel.SnapshotStatusAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("SnapshotStatus", p, new [] { GET }, "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status");
		}
		
		internal ElasticsearchResponse<T> SnapshotVerifyRepositoryDispatch<T>(IRequest<VerifyRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotVerifyRepository<T>(p.RouteValues.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotVerifyRepository", p, new [] { POST }, "/_snapshot/{repository}/_verify");
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotVerifyRepositoryDispatchAsync<T>(IRequest<VerifyRepositoryRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Repository)) return _lowLevel.SnapshotVerifyRepositoryAsync<T>(p.RouteValues.Repository,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("SnapshotVerifyRepository", p, new [] { POST }, "/_snapshot/{repository}/_verify");
		}
		
		internal ElasticsearchResponse<T> SuggestDispatch<T>(IRequest<SuggestRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.Suggest<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.Suggest<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SuggestGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SuggestGet<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Suggest", p, new [] { POST, GET }, "/_suggest", "/{index}/_suggest");
		}
		
		internal Task<ElasticsearchResponse<T>> SuggestDispatchAsync<T>(IRequest<SuggestRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SuggestAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					return _lowLevel.SuggestAsync<T>(body,u => p.RequestParameters);

				case GET:
					if (AllSet(p.RouteValues.Index)) return _lowLevel.SuggestGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					return _lowLevel.SuggestGetAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("Suggest", p, new [] { POST, GET }, "/_suggest", "/{index}/_suggest");
		}
		
		internal ElasticsearchResponse<T> TasksCancelDispatch<T>(IRequest<TasksCancelRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.TaskId)) return _lowLevel.TasksCancel<T>(p.RouteValues.TaskId,u => p.RequestParameters);
					return _lowLevel.TasksCancel<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("TasksCancel", p, new [] { POST }, "/_tasks/_cancel", "/_tasks/{task_id}/_cancel");
		}
		
		internal Task<ElasticsearchResponse<T>> TasksCancelDispatchAsync<T>(IRequest<TasksCancelRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.TaskId)) return _lowLevel.TasksCancelAsync<T>(p.RouteValues.TaskId,u => p.RequestParameters);
					return _lowLevel.TasksCancelAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("TasksCancel", p, new [] { POST }, "/_tasks/_cancel", "/_tasks/{task_id}/_cancel");
		}
		
		internal ElasticsearchResponse<T> TasksListDispatch<T>(IRequest<TasksListRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.TaskId)) return _lowLevel.TasksList<T>(p.RouteValues.TaskId,u => p.RequestParameters);
					return _lowLevel.TasksList<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("TasksList", p, new [] { GET }, "/_tasks", "/_tasks/{task_id}");
		}
		
		internal Task<ElasticsearchResponse<T>> TasksListDispatchAsync<T>(IRequest<TasksListRequestParameters> p ) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.TaskId)) return _lowLevel.TasksListAsync<T>(p.RouteValues.TaskId,u => p.RequestParameters);
					return _lowLevel.TasksListAsync<T>(u => p.RequestParameters);

			}
			throw InvalidDispatch("TasksList", p, new [] { GET }, "/_tasks", "/_tasks/{task_id}");
		}
		
		internal ElasticsearchResponse<T> TermvectorsDispatch<T>(IRequest<TermVectorsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.TermvectorsGet<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.TermvectorsGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Termvectors<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.Termvectors<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Termvectors", p, new [] { GET, POST }, "/{index}/{type}/_termvectors", "/{index}/{type}/{id}/_termvectors");
		}
		
		internal Task<ElasticsearchResponse<T>> TermvectorsDispatchAsync<T>(IRequest<TermVectorsRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.TermvectorsGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.TermvectorsGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.TermvectorsAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.TermvectorsAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Termvectors", p, new [] { GET, POST }, "/{index}/{type}/_termvectors", "/{index}/{type}/{id}/_termvectors");
		}
		
		internal ElasticsearchResponse<T> UpdateDispatch<T>(IRequest<UpdateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.Update<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Update", p, new [] { POST }, "/{index}/{type}/{id}/_update");
		}
		
		internal Task<ElasticsearchResponse<T>> UpdateDispatchAsync<T>(IRequest<UpdateRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSetNoFallback(p.RouteValues.Index, p.RouteValues.Type, p.RouteValues.Id)) return _lowLevel.UpdateAsync<T>(p.RouteValues.Index,p.RouteValues.Type,p.RouteValues.Id,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("Update", p, new [] { POST }, "/{index}/{type}/{id}/_update");
		}
		
		internal ElasticsearchResponse<T> UpdateByQueryDispatch<T>(IRequest<UpdateByQueryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.UpdateByQuery<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.UpdateByQuery<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("UpdateByQuery", p, new [] { POST }, "/{index}/_update_by_query", "/{index}/{type}/_update_by_query");
		}
		
		internal Task<ElasticsearchResponse<T>> UpdateByQueryDispatchAsync<T>(IRequest<UpdateByQueryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.UpdateByQueryAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.UpdateByQueryAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("UpdateByQuery", p, new [] { POST }, "/{index}/_update_by_query", "/{index}/{type}/_update_by_query");
		}
		
		internal ElasticsearchResponse<T> DeleteByQueryDispatch<T>(IRequest<DeleteByQueryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.DeleteByQuery<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.DeleteByQuery<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteByQuery", p, new [] { DELETE }, "/{index}/_query", "/{index}/{type}/_query");
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteByQueryDispatchAsync<T>(IRequest<DeleteByQueryRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case DELETE:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.DeleteByQueryAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.DeleteByQueryAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("DeleteByQuery", p, new [] { DELETE }, "/{index}/_query", "/{index}/{type}/_query");
		}
		
		internal ElasticsearchResponse<T> GraphExploreDispatch<T>(IRequest<GraphExploreRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.GraphExploreGet<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.GraphExploreGet<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.GraphExplore<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.GraphExplore<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GraphExplore", p, new [] { GET, POST }, "/{index}/_graph/explore", "/{index}/{type}/_graph/explore");
		}
		
		internal Task<ElasticsearchResponse<T>> GraphExploreDispatchAsync<T>(IRequest<GraphExploreRequestParameters> p , PostData<object> body) where T : class
		{
			switch(p.HttpMethod)
			{
				case GET:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.GraphExploreGetAsync<T>(p.RouteValues.Index,p.RouteValues.Type,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.GraphExploreGetAsync<T>(p.RouteValues.Index,u => p.RequestParameters);
					break;

				case POST:
					if (AllSet(p.RouteValues.Index, p.RouteValues.Type)) return _lowLevel.GraphExploreAsync<T>(p.RouteValues.Index,p.RouteValues.Type,body,u => p.RequestParameters);
					if (AllSetNoFallback(p.RouteValues.Index)) return _lowLevel.GraphExploreAsync<T>(p.RouteValues.Index,body,u => p.RequestParameters);
					break;

			}
			throw InvalidDispatch("GraphExplore", p, new [] { GET, POST }, "/{index}/_graph/explore", "/{index}/{type}/_graph/explore");
		}
		
	}	
}
