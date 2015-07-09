using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

//Generated File Please Do Not Edit Manually


namespace Nest
{
	///<summary>This dispatches highlevel requests into the proper lowlevel client overload method</summary>
	internal partial class RawDispatch
	{
		internal ElasticsearchResponse<T> AbortBenchmarkDispatch<T>(ElasticsearchPathInfo<AbortBenchmarkRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.Raw.AbortBenchmark<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "AbortBenchmark", new [] { "/_bench/abort/{name}" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> AbortBenchmarkDispatchAsync<T>(ElasticsearchPathInfo<AbortBenchmarkRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.Raw.AbortBenchmarkAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "AbortBenchmark", new [] { "/_bench/abort/{name}" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> BulkDispatch<T>(ElasticsearchPathInfo<BulkRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Bulk<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Bulk<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Bulk<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.BulkPut<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.BulkPut<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.BulkPut<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Bulk", new [] { "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> BulkDispatchAsync<T>(ElasticsearchPathInfo<BulkRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.BulkAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.BulkAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.BulkAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.BulkPutAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.BulkPutAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.BulkPutAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Bulk", new [] { "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> CatAliasesDispatch<T>(ElasticsearchPathInfo<CatAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.Raw.CatAliases<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.CatAliases<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAliases", new [] { "/_cat/aliases", "/_cat/aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatAliasesDispatchAsync<T>(ElasticsearchPathInfo<CatAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.Raw.CatAliasesAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.CatAliasesAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAliases", new [] { "/_cat/aliases", "/_cat/aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatAllocationDispatch<T>(ElasticsearchPathInfo<CatAllocationRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.CatAllocation<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.CatAllocation<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAllocation", new [] { "/_cat/allocation", "/_cat/allocation/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatAllocationDispatchAsync<T>(ElasticsearchPathInfo<CatAllocationRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.CatAllocationAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.CatAllocationAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAllocation", new [] { "/_cat/allocation", "/_cat/allocation/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatCountDispatch<T>(ElasticsearchPathInfo<CatCountRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatCount<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatCount<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatCount", new [] { "/_cat/count", "/_cat/count/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatCountDispatchAsync<T>(ElasticsearchPathInfo<CatCountRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatCountAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatCountAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatCount", new [] { "/_cat/count", "/_cat/count/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatFielddataDispatch<T>(ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatFielddata<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatFielddata", new [] { "/_cat/fielddata" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatFielddataDispatchAsync<T>(ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatFielddataAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatFielddata", new [] { "/_cat/fielddata" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatHealthDispatch<T>(ElasticsearchPathInfo<CatHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatHealth<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHealth", new [] { "/_cat/health" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatHealthDispatchAsync<T>(ElasticsearchPathInfo<CatHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatHealthAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHealth", new [] { "/_cat/health" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatHelpDispatch<T>(ElasticsearchPathInfo<CatHelpRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatHelp<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHelp", new [] { "/_cat" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatHelpDispatchAsync<T>(ElasticsearchPathInfo<CatHelpRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatHelpAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHelp", new [] { "/_cat" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatIndicesDispatch<T>(ElasticsearchPathInfo<CatIndicesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatIndices<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatIndices<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatIndices", new [] { "/_cat/indices", "/_cat/indices/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatIndicesDispatchAsync<T>(ElasticsearchPathInfo<CatIndicesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatIndicesAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatIndicesAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatIndices", new [] { "/_cat/indices", "/_cat/indices/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatMasterDispatch<T>(ElasticsearchPathInfo<CatMasterRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatMaster<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatMaster", new [] { "/_cat/master" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatMasterDispatchAsync<T>(ElasticsearchPathInfo<CatMasterRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatMasterAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatMaster", new [] { "/_cat/master" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatNodesDispatch<T>(ElasticsearchPathInfo<CatNodesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatNodes<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatNodes", new [] { "/_cat/nodes" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatNodesDispatchAsync<T>(ElasticsearchPathInfo<CatNodesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatNodesAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatNodes", new [] { "/_cat/nodes" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatPendingTasksDispatch<T>(ElasticsearchPathInfo<CatPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatPendingTasks<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPendingTasks", new [] { "/_cat/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatPendingTasksDispatchAsync<T>(ElasticsearchPathInfo<CatPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatPendingTasksAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPendingTasks", new [] { "/_cat/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatPluginsDispatch<T>(ElasticsearchPathInfo<CatPluginsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatPlugins<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPlugins", new [] { "/_cat/plugins" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatPluginsDispatchAsync<T>(ElasticsearchPathInfo<CatPluginsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatPluginsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPlugins", new [] { "/_cat/plugins" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatRecoveryDispatch<T>(ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatRecovery<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatRecovery<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatRecovery", new [] { "/_cat/recovery", "/_cat/recovery/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatRecoveryDispatchAsync<T>(ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatRecoveryAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatRecoveryAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatRecovery", new [] { "/_cat/recovery", "/_cat/recovery/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatSegmentsDispatch<T>(ElasticsearchPathInfo<CatSegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatSegments<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatSegments<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatSegments", new [] { "/_cat/segments", "/_cat/segments/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatSegmentsDispatchAsync<T>(ElasticsearchPathInfo<CatSegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatSegmentsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatSegmentsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatSegments", new [] { "/_cat/segments", "/_cat/segments/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatShardsDispatch<T>(ElasticsearchPathInfo<CatShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatShards<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatShards<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatShards", new [] { "/_cat/shards", "/_cat/shards/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatShardsDispatchAsync<T>(ElasticsearchPathInfo<CatShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.CatShardsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CatShardsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatShards", new [] { "/_cat/shards", "/_cat/shards/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatThreadPoolDispatch<T>(ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatThreadPool<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatThreadPool", new [] { "/_cat/thread_pool" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatThreadPoolDispatchAsync<T>(ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.CatThreadPoolAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatThreadPool", new [] { "/_cat/thread_pool" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClearScrollDispatch<T>(ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.ScrollId))
						return this.Raw.ClearScroll<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.Raw.ClearScroll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClearScroll", new [] { "/_search/scroll/{scroll_id}", "/_search/scroll" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> ClearScrollDispatchAsync<T>(ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.ScrollId))
						return this.Raw.ClearScrollAsync<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.Raw.ClearScrollAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClearScroll", new [] { "/_search/scroll/{scroll_id}", "/_search/scroll" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> ClusterGetSettingsDispatch<T>(ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.ClusterGetSettings<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterGetSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterGetSettingsDispatchAsync<T>(ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.ClusterGetSettingsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterGetSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterHealthDispatch<T>(ElasticsearchPathInfo<ClusterHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.ClusterHealth<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.ClusterHealth<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterHealth", new [] { "/_cluster/health", "/_cluster/health/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterHealthDispatchAsync<T>(ElasticsearchPathInfo<ClusterHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.ClusterHealthAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.ClusterHealthAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterHealth", new [] { "/_cluster/health", "/_cluster/health/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterPendingTasksDispatch<T>(ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.ClusterPendingTasks<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPendingTasks", new [] { "/_cluster/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPendingTasksDispatchAsync<T>(ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.ClusterPendingTasksAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPendingTasks", new [] { "/_cluster/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterPutSettingsDispatch<T>(ElasticsearchPathInfo<ClusterSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					return this.Raw.ClusterPutSettings<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPutSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPutSettingsDispatchAsync<T>(ElasticsearchPathInfo<ClusterSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					return this.Raw.ClusterPutSettingsAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPutSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> ClusterRerouteDispatch<T>(ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.Raw.ClusterReroute<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterReroute", new [] { "/_cluster/reroute" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterRerouteDispatchAsync<T>(ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.Raw.ClusterRerouteAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterReroute", new [] { "/_cluster/reroute" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> ClusterStateDispatch<T>(ElasticsearchPathInfo<ClusterStateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Metric, pathInfo.Index))
						return this.Raw.ClusterState<T>(pathInfo.Metric,pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.ClusterState<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.Raw.ClusterState<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterState", new [] { "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStateDispatchAsync<T>(ElasticsearchPathInfo<ClusterStateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Metric, pathInfo.Index))
						return this.Raw.ClusterStateAsync<T>(pathInfo.Metric,pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.ClusterStateAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.Raw.ClusterStateAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterState", new [] { "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterStatsDispatch<T>(ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.ClusterStats<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.ClusterStats<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterStats", new [] { "/_cluster/stats", "/_cluster/stats/nodes/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStatsDispatchAsync<T>(ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.ClusterStatsAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.ClusterStatsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterStats", new [] { "/_cluster/stats", "/_cluster/stats/nodes/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CountDispatch<T>(ElasticsearchPathInfo<CountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Count<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Count<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Count<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.CountGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CountGet<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Count", new [] { "/_count", "/{index}/_count", "/{index}/{type}/_count" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CountDispatchAsync<T>(ElasticsearchPathInfo<CountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.CountAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.CountAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.CountGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.CountGetAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Count", new [] { "/_count", "/{index}/_count", "/{index}/{type}/_count" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CountPercolateDispatch<T>(ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.CountPercolateGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountPercolateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.CountPercolate<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountPercolate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "CountPercolate", new [] { "/{index}/{type}/_percolate/count", "/{index}/{type}/{id}/_percolate/count" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> CountPercolateDispatchAsync<T>(ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.CountPercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountPercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.CountPercolateAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.CountPercolateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "CountPercolate", new [] { "/{index}/{type}/_percolate/count", "/{index}/{type}/{id}/_percolate/count" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> DeleteDispatch<T>(ElasticsearchPathInfo<DeleteRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Delete<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Delete", new [] { "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteDispatchAsync<T>(ElasticsearchPathInfo<DeleteRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.DeleteAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Delete", new [] { "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> DeleteByQueryDispatch<T>(ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.DeleteByQuery<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.DeleteByQuery<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "DeleteByQuery", new [] { "/{index}/_query", "/{index}/{type}/_query" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteByQueryDispatchAsync<T>(ElasticsearchPathInfo<DeleteByQueryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.DeleteByQueryAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.DeleteByQueryAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "DeleteByQuery", new [] { "/{index}/_query", "/{index}/{type}/_query" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> DeleteScriptDispatch<T>(ElasticsearchPathInfo<DeleteScriptRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.DeleteScript<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "DeleteScript", new [] { "/_scripts/{lang}/{id}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteScriptDispatchAsync<T>(ElasticsearchPathInfo<DeleteScriptRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.DeleteScriptAsync<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "DeleteScript", new [] { "/_scripts/{lang}/{id}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> DeleteTemplateDispatch<T>(ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Id))
						return this.Raw.DeleteTemplate<T>(pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "DeleteTemplate", new [] { "/_search/template/{id}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> DeleteTemplateDispatchAsync<T>(ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Id))
						return this.Raw.DeleteTemplateAsync<T>(pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "DeleteTemplate", new [] { "/_search/template/{id}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> ExistsDispatch<T>(ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Exists<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Exists", new [] { "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> ExistsDispatchAsync<T>(ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.ExistsAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Exists", new [] { "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> ExplainDispatch<T>(ElasticsearchPathInfo<ExplainRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.ExplainGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Explain<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Explain", new [] { "/{index}/{type}/{id}/_explain" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> ExplainDispatchAsync<T>(ElasticsearchPathInfo<ExplainRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.ExplainGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.ExplainAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Explain", new [] { "/{index}/{type}/{id}/_explain" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> FieldStatsDispatch<T>(ElasticsearchPathInfo<FieldStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.FieldStatsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.FieldStatsGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.FieldStats<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.FieldStats<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "FieldStats", new [] { "/_field_stats", "/{index}/_field_stats" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> FieldStatsDispatchAsync<T>(ElasticsearchPathInfo<FieldStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.FieldStatsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.FieldStatsGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.FieldStatsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.FieldStatsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "FieldStats", new [] { "/_field_stats", "/{index}/_field_stats" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> GetDispatch<T>(ElasticsearchPathInfo<GetRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Get<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Get", new [] { "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> GetDispatchAsync<T>(ElasticsearchPathInfo<GetRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.GetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Get", new [] { "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> GetScriptDispatch<T>(ElasticsearchPathInfo<GetScriptRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.GetScript<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "GetScript", new [] { "/_scripts/{lang}/{id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> GetScriptDispatchAsync<T>(ElasticsearchPathInfo<GetScriptRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.GetScriptAsync<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "GetScript", new [] { "/_scripts/{lang}/{id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> GetSourceDispatch<T>(ElasticsearchPathInfo<SourceRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.GetSource<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "GetSource", new [] { "/{index}/{type}/{id}/_source" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> GetSourceDispatchAsync<T>(ElasticsearchPathInfo<SourceRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.GetSourceAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "GetSource", new [] { "/{index}/{type}/{id}/_source" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> GetTemplateDispatch<T>(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Id))
						return this.Raw.GetTemplate<T>(pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "GetTemplate", new [] { "/_search/template/{id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> GetTemplateDispatchAsync<T>(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Id))
						return this.Raw.GetTemplateAsync<T>(pathInfo.Id,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "GetTemplate", new [] { "/_search/template/{id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndexDispatch<T>(ElasticsearchPathInfo<IndexRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Index<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Index<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.IndexPut<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndexPut<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Index", new [] { "/{index}/{type}", "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> IndexDispatchAsync<T>(ElasticsearchPathInfo<IndexRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.IndexAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndexAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.IndexPutAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndexPutAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Index", new [] { "/{index}/{type}", "/{index}/{type}/{id}" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> IndicesAnalyzeDispatch<T>(ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesAnalyzeGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesAnalyzeGetForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesAnalyze<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.IndicesAnalyzeForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesAnalyze", new [] { "/_analyze", "/{index}/_analyze" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesAnalyzeDispatchAsync<T>(ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesAnalyzeGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesAnalyzeGetForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesAnalyzeAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.IndicesAnalyzeForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesAnalyze", new [] { "/_analyze", "/{index}/_analyze" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesClearCacheDispatch<T>(ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesClearCache<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesClearCacheForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesClearCacheGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesClearCacheGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesClearCache", new [] { "/_cache/clear", "/{index}/_cache/clear" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesClearCacheDispatchAsync<T>(ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesClearCacheAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesClearCacheForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesClearCacheGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesClearCacheGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesClearCache", new [] { "/_cache/clear", "/{index}/_cache/clear" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesCloseDispatch<T>(ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesClose<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesClose", new [] { "/{index}/_close" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesCloseDispatchAsync<T>(ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesCloseAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesClose", new [] { "/{index}/_close" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesCreateDispatch<T>(ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesCreate<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesCreatePost<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesCreate", new [] { "/{index}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesCreateDispatchAsync<T>(ElasticsearchPathInfo<CreateIndexRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesCreateAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesCreatePostAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesCreate", new [] { "/{index}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteDispatch<T>(ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesDelete<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDelete", new [] { "/{index}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteDispatchAsync<T>(ElasticsearchPathInfo<DeleteIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesDeleteAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDelete", new [] { "/{index}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteAliasDispatch<T>(ElasticsearchPathInfo<DeleteAliasRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesDeleteAlias<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteAlias", new [] { "/{index}/_alias/{name}", "/{index}/_aliases/{name}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteAliasDispatchAsync<T>(ElasticsearchPathInfo<DeleteAliasRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesDeleteAliasAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteAlias", new [] { "/{index}/_alias/{name}", "/{index}/_aliases/{name}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteMappingDispatch<T>(ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesDeleteMapping<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteMapping", new [] { "/{index}/{type}/_mapping", "/{index}/{type}", "/{index}/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteMappingDispatchAsync<T>(ElasticsearchPathInfo<DeleteMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesDeleteMappingAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteMapping", new [] { "/{index}/{type}/_mapping", "/{index}/{type}", "/{index}/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteTemplateDispatch<T>(ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesDeleteTemplateForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteTemplate", new [] { "/_template/{name}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteTemplateDispatchAsync<T>(ElasticsearchPathInfo<DeleteTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesDeleteTemplateForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteTemplate", new [] { "/_template/{name}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> IndicesDeleteWarmerDispatch<T>(ElasticsearchPathInfo<DeleteWarmerRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesDeleteWarmer<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteWarmer", new [] { "/{index}/_warmer/{name}", "/{index}/_warmers/{name}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesDeleteWarmerDispatchAsync<T>(ElasticsearchPathInfo<DeleteWarmerRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesDeleteWarmerAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesDeleteWarmer", new [] { "/{index}/_warmer/{name}", "/{index}/_warmers/{name}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> IndicesExistsDispatch<T>(ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesExists<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExists", new [] { "/{index}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsDispatchAsync<T>(ElasticsearchPathInfo<IndexExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesExistsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExists", new [] { "/{index}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> IndicesExistsAliasDispatch<T>(ElasticsearchPathInfo<AliasExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesExistsAlias<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesExistsAliasForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesExistsAlias<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExistsAlias", new [] { "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsAliasDispatchAsync<T>(ElasticsearchPathInfo<AliasExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesExistsAliasAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesExistsAliasForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesExistsAliasAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExistsAlias", new [] { "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> IndicesExistsTemplateDispatch<T>(ElasticsearchPathInfo<TemplateExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesExistsTemplateForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExistsTemplate", new [] { "/_template/{name}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsTemplateDispatchAsync<T>(ElasticsearchPathInfo<TemplateExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesExistsTemplateForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExistsTemplate", new [] { "/_template/{name}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> IndicesExistsTypeDispatch<T>(ElasticsearchPathInfo<TypeExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesExistsType<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExistsType", new [] { "/{index}/{type}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesExistsTypeDispatchAsync<T>(ElasticsearchPathInfo<TypeExistsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesExistsTypeAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesExistsType", new [] { "/{index}/{type}" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> IndicesFlushDispatch<T>(ElasticsearchPathInfo<FlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlush<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlush", new [] { "/_flush", "/{index}/_flush" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushDispatchAsync<T>(ElasticsearchPathInfo<FlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlush", new [] { "/_flush", "/{index}/_flush" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesFlushSyncedDispatch<T>(ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushSynced<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushSyncedForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushSyncedGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushSyncedGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlushSynced", new [] { "/_flush/synced", "/{index}/_flush/synced" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushSyncedDispatchAsync<T>(ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushSyncedAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushSyncedForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesFlushSyncedGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesFlushSyncedGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlushSynced", new [] { "/_flush/synced", "/{index}/_flush/synced" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetDispatch<T>(ElasticsearchPathInfo<GetIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Feature))
						return this.Raw.IndicesGet<T>(pathInfo.Index,pathInfo.Feature,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesGet", new [] { "/{index}", "/{index}/{feature}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetDispatchAsync<T>(ElasticsearchPathInfo<GetIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Feature))
						return this.Raw.IndicesGetAsync<T>(pathInfo.Index,pathInfo.Feature,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesGet", new [] { "/{index}", "/{index}/{feature}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasDispatch<T>(ElasticsearchPathInfo<GetAliasRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetAlias<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetAliasForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetAlias<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetAliasForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAlias", new [] { "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasDispatchAsync<T>(ElasticsearchPathInfo<GetAliasRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetAliasAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetAliasForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetAliasAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetAliasForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAlias", new [] { "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasesDispatch<T>(ElasticsearchPathInfo<GetAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetAliases<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetAliases<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetAliasesForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetAliasesForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAliases", new [] { "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasesDispatchAsync<T>(ElasticsearchPathInfo<GetAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetAliasesAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetAliasesAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetAliasesForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetAliasesForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAliases", new [] { "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetFieldMappingDispatch<T>(ElasticsearchPathInfo<GetFieldMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Field))
						return this.Raw.IndicesGetFieldMapping<T>(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Field))
						return this.Raw.IndicesGetFieldMapping<T>(pathInfo.Index,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type, pathInfo.Field))
						return this.Raw.IndicesGetFieldMappingForAll<T>(pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Field))
						return this.Raw.IndicesGetFieldMappingForAll<T>(pathInfo.Field,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesGetFieldMapping", new [] { "/_mapping/field/{field}", "/{index}/_mapping/field/{field}", "/_mapping/{type}/field/{field}", "/{index}/_mapping/{type}/field/{field}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetFieldMappingDispatchAsync<T>(ElasticsearchPathInfo<GetFieldMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Field))
						return this.Raw.IndicesGetFieldMappingAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Field))
						return this.Raw.IndicesGetFieldMappingAsync<T>(pathInfo.Index,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type, pathInfo.Field))
						return this.Raw.IndicesGetFieldMappingForAllAsync<T>(pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Field))
						return this.Raw.IndicesGetFieldMappingForAllAsync<T>(pathInfo.Field,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesGetFieldMapping", new [] { "/_mapping/field/{field}", "/{index}/_mapping/field/{field}", "/_mapping/{type}/field/{field}", "/{index}/_mapping/{type}/field/{field}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetMappingDispatch<T>(ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesGetMapping<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetMapping<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.Raw.IndicesGetMappingForAll<T>(pathInfo.Type,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetMappingForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetMapping", new [] { "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetMappingDispatchAsync<T>(ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesGetMappingAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetMappingAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.Raw.IndicesGetMappingForAllAsync<T>(pathInfo.Type,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetMappingForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetMapping", new [] { "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetSettingsDispatch<T>(ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetSettings<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetSettings<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetSettingsForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetSettingsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetSettings", new [] { "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetSettingsDispatchAsync<T>(ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetSettingsAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetSettingsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetSettingsForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetSettingsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetSettings", new [] { "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetTemplateDispatch<T>(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetTemplateForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetTemplateForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetTemplate", new [] { "/_template", "/_template/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetTemplateDispatchAsync<T>(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetTemplateForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetTemplateForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetTemplate", new [] { "/_template", "/_template/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetUpgradeDispatch<T>(ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetUpgrade<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetUpgradeForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetUpgradeDispatchAsync<T>(ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetUpgradeAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetUpgradeForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetWarmerDispatch<T>(ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.Raw.IndicesGetWarmer<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetWarmer<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetWarmer<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetWarmerForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetWarmerForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetWarmer", new [] { "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetWarmerDispatchAsync<T>(ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.Raw.IndicesGetWarmerAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesGetWarmerAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesGetWarmerAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesGetWarmerForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.Raw.IndicesGetWarmerForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetWarmer", new [] { "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesOpenDispatch<T>(ElasticsearchPathInfo<OpenIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesOpen<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesOpen", new [] { "/{index}/_open" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOpenDispatchAsync<T>(ElasticsearchPathInfo<OpenIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesOpenAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesOpen", new [] { "/{index}/_open" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesOptimizeDispatch<T>(ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesOptimize<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesOptimizeForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesOptimizeGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesOptimizeGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesOptimize", new [] { "/_optimize", "/{index}/_optimize" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOptimizeDispatchAsync<T>(ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesOptimizeAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesOptimizeForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesOptimizeGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesOptimizeGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesOptimize", new [] { "/_optimize", "/{index}/_optimize" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesPutAliasDispatch<T>(ElasticsearchPathInfo<PutAliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutAlias<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutAliasPost<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutAlias", new [] { "/{index}/_alias/{name}", "/{index}/_aliases/{name}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutAliasDispatchAsync<T>(ElasticsearchPathInfo<PutAliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutAliasAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutAliasPostAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutAlias", new [] { "/{index}/_alias/{name}", "/{index}/_aliases/{name}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesPutMappingDispatch<T>(ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesPutMapping<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.Raw.IndicesPutMappingForAll<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesPutMappingPost<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.Raw.IndicesPutMappingPostForAll<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutMapping", new [] { "/{index}/{type}/_mapping", "/{index}/_mapping/{type}", "/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}", "/_mappings/{type}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutMappingDispatchAsync<T>(ElasticsearchPathInfo<PutMappingRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesPutMappingAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.Raw.IndicesPutMappingForAllAsync<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesPutMappingPostAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.Raw.IndicesPutMappingPostForAllAsync<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutMapping", new [] { "/{index}/{type}/_mapping", "/{index}/_mapping/{type}", "/_mapping/{type}", "/{index}/{type}/_mappings", "/{index}/_mappings/{type}", "/_mappings/{type}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesPutSettingsDispatch<T>(ElasticsearchPathInfo<UpdateSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesPutSettings<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.IndicesPutSettingsForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesPutSettings", new [] { "/_settings", "/{index}/_settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutSettingsDispatchAsync<T>(ElasticsearchPathInfo<UpdateSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesPutSettingsAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.IndicesPutSettingsForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesPutSettings", new [] { "/_settings", "/{index}/_settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> IndicesPutTemplateDispatch<T>(ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutTemplateForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutTemplatePostForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutTemplate", new [] { "/_template/{name}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutTemplateDispatchAsync<T>(ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutTemplateForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutTemplatePostForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutTemplate", new [] { "/_template/{name}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesPutWarmerDispatch<T>(ElasticsearchPathInfo<PutWarmerRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.Raw.IndicesPutWarmer<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutWarmer<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutWarmerForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.Raw.IndicesPutWarmerPost<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutWarmerPost<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutWarmerPostForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutWarmer", new [] { "/_warmer/{name}", "/{index}/_warmer/{name}", "/{index}/{type}/_warmer/{name}", "/_warmers/{name}", "/{index}/_warmers/{name}", "/{index}/{type}/_warmers/{name}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutWarmerDispatchAsync<T>(ElasticsearchPathInfo<PutWarmerRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.Raw.IndicesPutWarmerAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutWarmerAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutWarmerForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.Raw.IndicesPutWarmerPostAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.Raw.IndicesPutWarmerPostAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.Raw.IndicesPutWarmerPostForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "IndicesPutWarmer", new [] { "/_warmer/{name}", "/{index}/_warmer/{name}", "/{index}/{type}/_warmer/{name}", "/_warmers/{name}", "/{index}/_warmers/{name}", "/{index}/{type}/_warmers/{name}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesRecoveryDispatch<T>(ElasticsearchPathInfo<RecoveryStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesRecovery<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesRecoveryForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRecovery", new [] { "/_recovery", "/{index}/_recovery" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRecoveryDispatchAsync<T>(ElasticsearchPathInfo<RecoveryStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesRecoveryAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesRecoveryForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRecovery", new [] { "/_recovery", "/{index}/_recovery" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesRefreshDispatch<T>(ElasticsearchPathInfo<RefreshRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesRefresh<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesRefreshForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesRefreshGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesRefreshGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRefresh", new [] { "/_refresh", "/{index}/_refresh" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRefreshDispatchAsync<T>(ElasticsearchPathInfo<RefreshRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesRefreshAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesRefreshForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesRefreshGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesRefreshGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRefresh", new [] { "/_refresh", "/{index}/_refresh" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesSegmentsDispatch<T>(ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesSegments<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesSegmentsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesSegments", new [] { "/_segments", "/{index}/_segments" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesSegmentsDispatchAsync<T>(ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesSegmentsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesSegmentsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesSegments", new [] { "/_segments", "/{index}/_segments" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesStatsDispatch<T>(ElasticsearchPathInfo<IndicesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Metric))
						return this.Raw.IndicesStats<T>(pathInfo.Index,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.IndicesStatsForAll<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesStats<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesStatsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStats", new [] { "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesStatsDispatchAsync<T>(ElasticsearchPathInfo<IndicesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Metric))
						return this.Raw.IndicesStatsAsync<T>(pathInfo.Index,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.IndicesStatsForAllAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesStatsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesStatsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStats", new [] { "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesStatusDispatch<T>(ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesStatus<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesStatusForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStatus", new [] { "/_status", "/{index}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesStatusDispatchAsync<T>(ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesStatusAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesStatusForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStatus", new [] { "/_status", "/{index}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesUpdateAliasesDispatch<T>(ElasticsearchPathInfo<AliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.Raw.IndicesUpdateAliasesForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpdateAliases", new [] { "/_aliases" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpdateAliasesDispatchAsync<T>(ElasticsearchPathInfo<AliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.Raw.IndicesUpdateAliasesForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpdateAliases", new [] { "/_aliases" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesUpgradeDispatch<T>(ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesUpgrade<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesUpgradeForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpgradeDispatchAsync<T>(ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesUpgradeAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesUpgradeForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesValidateQueryDispatch<T>(ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesValidateQueryGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesValidateQueryGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesValidateQueryGetForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesValidateQuery<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesValidateQuery<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.IndicesValidateQueryForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesValidateQuery", new [] { "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesValidateQueryDispatchAsync<T>(ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesValidateQueryGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesValidateQueryGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.IndicesValidateQueryGetForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.IndicesValidateQueryAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.IndicesValidateQueryAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.IndicesValidateQueryForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesValidateQuery", new [] { "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> InfoDispatch<T>(ElasticsearchPathInfo<InfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.Info<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Info", new [] { "/" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> InfoDispatchAsync<T>(ElasticsearchPathInfo<InfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.Raw.InfoAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Info", new [] { "/" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ListBenchmarksDispatch<T>(ElasticsearchPathInfo<ListBenchmarksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.ListBenchmarks<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.ListBenchmarks<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.ListBenchmarks<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ListBenchmarks", new [] { "/_bench", "/{index}/_bench", "/{index}/{type}/_bench" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ListBenchmarksDispatchAsync<T>(ElasticsearchPathInfo<ListBenchmarksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.ListBenchmarksAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.ListBenchmarksAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.ListBenchmarksAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ListBenchmarks", new [] { "/_bench", "/{index}/_bench", "/{index}/{type}/_bench" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> MgetDispatch<T>(ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MgetGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MgetGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MgetGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Mget<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Mget<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Mget<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mget", new [] { "/_mget", "/{index}/_mget", "/{index}/{type}/_mget" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MgetDispatchAsync<T>(ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MgetGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MgetGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MgetGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MgetAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MgetAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.MgetAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mget", new [] { "/_mget", "/{index}/_mget", "/{index}/{type}/_mget" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MltDispatch<T>(ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.MltGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Mlt<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Mlt", new [] { "/{index}/{type}/{id}/_mlt" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MltDispatchAsync<T>(ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.MltGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.MltAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Mlt", new [] { "/{index}/{type}/{id}/_mlt" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MpercolateDispatch<T>(ElasticsearchPathInfo<MultiPercolateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MpercolateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MpercolateGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MpercolateGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Mpercolate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Mpercolate<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Mpercolate<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mpercolate", new [] { "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MpercolateDispatchAsync<T>(ElasticsearchPathInfo<MultiPercolateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MpercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MpercolateGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MpercolateGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MpercolateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MpercolateAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.MpercolateAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mpercolate", new [] { "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MsearchDispatch<T>(ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MsearchGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MsearchGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MsearchGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Msearch<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Msearch<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Msearch<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Msearch", new [] { "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MsearchDispatchAsync<T>(ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MsearchGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MsearchGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MsearchGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MsearchAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MsearchAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.MsearchAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Msearch", new [] { "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MtermvectorsDispatch<T>(ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MtermvectorsGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MtermvectorsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MtermvectorsGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Mtermvectors<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Mtermvectors<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Mtermvectors<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mtermvectors", new [] { "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MtermvectorsDispatchAsync<T>(ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MtermvectorsGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MtermvectorsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.MtermvectorsGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.MtermvectorsAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.MtermvectorsAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.MtermvectorsAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mtermvectors", new [] { "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> NodesHotThreadsDispatch<T>(ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesHotThreads<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.NodesHotThreadsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesHotThreads", new [] { "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesHotThreadsDispatchAsync<T>(ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesHotThreadsAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.NodesHotThreadsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesHotThreads", new [] { "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> NodesInfoDispatch<T>(ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.Raw.NodesInfo<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesInfo<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.NodesInfoForAll<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.Raw.NodesInfoForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesInfo", new [] { "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesInfoDispatchAsync<T>(ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.Raw.NodesInfoAsync<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesInfoAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.NodesInfoForAllAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.Raw.NodesInfoForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesInfo", new [] { "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> NodesShutdownDispatch<T>(ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesShutdown<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.NodesShutdownForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesShutdown", new [] { "/_shutdown", "/_cluster/nodes/_shutdown", "/_cluster/nodes/{node_id}/_shutdown" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesShutdownDispatchAsync<T>(ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesShutdownAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.Raw.NodesShutdownForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesShutdown", new [] { "/_shutdown", "/_cluster/nodes/_shutdown", "/_cluster/nodes/{node_id}/_shutdown" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> NodesStatsDispatch<T>(ElasticsearchPathInfo<NodesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric, pathInfo.IndexMetric))
						return this.Raw.NodesStats<T>(pathInfo.NodeId,pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.Raw.NodesStats<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric, pathInfo.IndexMetric))
						return this.Raw.NodesStatsForAll<T>(pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesStats<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.NodesStatsForAll<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.Raw.NodesStatsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesStats", new [] { "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesStatsDispatchAsync<T>(ElasticsearchPathInfo<NodesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric, pathInfo.IndexMetric))
						return this.Raw.NodesStatsAsync<T>(pathInfo.NodeId,pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.Raw.NodesStatsAsync<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric, pathInfo.IndexMetric))
						return this.Raw.NodesStatsForAllAsync<T>(pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.Raw.NodesStatsAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.Raw.NodesStatsForAllAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.Raw.NodesStatsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesStats", new [] { "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> PercolateDispatch<T>(ElasticsearchPathInfo<PercolateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.PercolateGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.PercolateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Percolate<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Percolate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Percolate", new [] { "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> PercolateDispatchAsync<T>(ElasticsearchPathInfo<PercolateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.PercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.PercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.PercolateAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.PercolateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Percolate", new [] { "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> PingDispatch<T>(ElasticsearchPathInfo<PingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					return this.Raw.Ping<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Ping", new [] { "/" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> PingDispatchAsync<T>(ElasticsearchPathInfo<PingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					return this.Raw.PingAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Ping", new [] { "/" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> PutScriptDispatch<T>(ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.PutScript<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.PutScriptPost<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "PutScript", new [] { "/_scripts/{lang}/{id}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> PutScriptDispatchAsync<T>(ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.PutScriptAsync<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.Raw.PutScriptPostAsync<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "PutScript", new [] { "/_scripts/{lang}/{id}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> PutTemplateDispatch<T>(ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Id))
						return this.Raw.PutTemplate<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Id))
						return this.Raw.PutTemplatePost<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "PutTemplate", new [] { "/_search/template/{id}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> PutTemplateDispatchAsync<T>(ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Id))
						return this.Raw.PutTemplateAsync<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Id))
						return this.Raw.PutTemplatePostAsync<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "PutTemplate", new [] { "/_search/template/{id}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> ScrollDispatch<T>(ElasticsearchPathInfo<ScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.ScrollId))
						return this.Raw.ScrollGet<T>(pathInfo.ScrollId,u => pathInfo.RequestParameters);
					return this.Raw.ScrollGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.ScrollId))
						return this.Raw.Scroll<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.Raw.Scroll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Scroll", new [] { "/_search/scroll", "/_search/scroll/{scroll_id}" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> ScrollDispatchAsync<T>(ElasticsearchPathInfo<ScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.ScrollId))
						return this.Raw.ScrollGetAsync<T>(pathInfo.ScrollId,u => pathInfo.RequestParameters);
					return this.Raw.ScrollGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.ScrollId))
						return this.Raw.ScrollAsync<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.Raw.ScrollAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Scroll", new [] { "/_search/scroll", "/_search/scroll/{scroll_id}" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SearchDispatch<T>(ElasticsearchPathInfo<SearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Search<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.Search<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Search<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Search", new [] { "/_search", "/{index}/_search", "/{index}/{type}/_search" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchDispatchAsync<T>(ElasticsearchPathInfo<SearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.SearchAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Search", new [] { "/_search", "/{index}/_search", "/{index}/{type}/_search" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SearchExistsDispatch<T>(ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchExists<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchExists<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.SearchExists<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchExistsGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchExistsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchExistsGet<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchExists", new [] { "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchExistsDispatchAsync<T>(ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchExistsAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchExistsAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.SearchExistsAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchExistsGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchExistsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchExistsGetAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchExists", new [] { "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SearchShardsDispatch<T>(ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchShardsGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchShardsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchShardsGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchShards<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchShards<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchShards<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchShards", new [] { "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchShardsDispatchAsync<T>(ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchShardsGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchShardsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchShardsGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchShardsAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchShardsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchShardsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchShards", new [] { "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SearchTemplateDispatch<T>(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchTemplateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchTemplateGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchTemplateGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchTemplate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchTemplate<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.SearchTemplate<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchTemplate", new [] { "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchTemplateDispatchAsync<T>(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchTemplateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchTemplateGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SearchTemplateGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.SearchTemplateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.Raw.SearchTemplateAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.SearchTemplateAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchTemplate", new [] { "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateDispatch<T>(ElasticsearchPathInfo<SnapshotRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotCreate<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotCreatePost<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotCreate", new [] { "/_snapshot/{repository}/{snapshot}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotCreateDispatchAsync<T>(ElasticsearchPathInfo<SnapshotRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotCreateAsync<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotCreatePostAsync<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotCreate", new [] { "/_snapshot/{repository}/{snapshot}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateRepositoryDispatch<T>(ElasticsearchPathInfo<CreateRepositoryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotCreateRepository<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotCreateRepositoryPost<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotCreateRepository", new [] { "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotCreateRepositoryDispatchAsync<T>(ElasticsearchPathInfo<CreateRepositoryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotCreateRepositoryAsync<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotCreateRepositoryPostAsync<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotCreateRepository", new [] { "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.PUT, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SnapshotDeleteDispatch<T>(ElasticsearchPathInfo<DeleteSnapshotRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotDelete<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotDelete", new [] { "/_snapshot/{repository}/{snapshot}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotDeleteDispatchAsync<T>(ElasticsearchPathInfo<DeleteSnapshotRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotDeleteAsync<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotDelete", new [] { "/_snapshot/{repository}/{snapshot}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> SnapshotDeleteRepositoryDispatch<T>(ElasticsearchPathInfo<DeleteRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotDeleteRepository<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotDeleteRepository", new [] { "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotDeleteRepositoryDispatchAsync<T>(ElasticsearchPathInfo<DeleteRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotDeleteRepositoryAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotDeleteRepository", new [] { "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> SnapshotGetDispatch<T>(ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotGet<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotGet", new [] { "/_snapshot/{repository}/{snapshot}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetDispatchAsync<T>(ElasticsearchPathInfo<GetSnapshotRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotGetAsync<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotGet", new [] { "/_snapshot/{repository}/{snapshot}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SnapshotGetRepositoryDispatch<T>(ElasticsearchPathInfo<GetRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotGetRepository<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.Raw.SnapshotGetRepository<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotGetRepository", new [] { "/_snapshot", "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetRepositoryDispatchAsync<T>(ElasticsearchPathInfo<GetRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotGetRepositoryAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.Raw.SnapshotGetRepositoryAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotGetRepository", new [] { "/_snapshot", "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SnapshotRestoreDispatch<T>(ElasticsearchPathInfo<RestoreRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotRestore<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotRestore", new [] { "/_snapshot/{repository}/{snapshot}/_restore" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotRestoreDispatchAsync<T>(ElasticsearchPathInfo<RestoreRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotRestoreAsync<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotRestore", new [] { "/_snapshot/{repository}/{snapshot}/_restore" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SnapshotStatusDispatch<T>(ElasticsearchPathInfo<SnapshotStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotStatus<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotStatus<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.Raw.SnapshotStatus<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotStatus", new [] { "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotStatusDispatchAsync<T>(ElasticsearchPathInfo<SnapshotStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.Raw.SnapshotStatusAsync<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotStatusAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.Raw.SnapshotStatusAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotStatus", new [] { "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SnapshotVerifyRepositoryDispatch<T>(ElasticsearchPathInfo<VerifyRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotVerifyRepository<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotVerifyRepository", new [] { "/_snapshot/{repository}/_verify" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotVerifyRepositoryDispatchAsync<T>(ElasticsearchPathInfo<VerifyRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.Raw.SnapshotVerifyRepositoryAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "SnapshotVerifyRepository", new [] { "/_snapshot/{repository}/_verify" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SuggestDispatch<T>(ElasticsearchPathInfo<SuggestRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.Suggest<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.Suggest<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.SuggestGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SuggestGet<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Suggest", new [] { "/_suggest", "/{index}/_suggest" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SuggestDispatchAsync<T>(ElasticsearchPathInfo<SuggestRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.Raw.SuggestAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.Raw.SuggestAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.Raw.SuggestGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.Raw.SuggestGetAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Suggest", new [] { "/_suggest", "/{index}/_suggest" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> TermvectorDispatch<T>(ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.TermvectorGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.TermvectorGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Termvector<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.Termvector<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Termvector", new [] { "/{index}/{type}/_termvector", "/{index}/{type}/{id}/_termvector" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> TermvectorDispatchAsync<T>(ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.TermvectorGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.TermvectorGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.TermvectorAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.Raw.TermvectorAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Termvector", new [] { "/{index}/{type}/_termvector", "/{index}/{type}/{id}/_termvector" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> UpdateDispatch<T>(ElasticsearchPathInfo<UpdateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.Update<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Update", new [] { "/{index}/{type}/{id}/_update" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> UpdateDispatchAsync<T>(ElasticsearchPathInfo<UpdateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.Raw.UpdateAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Update", new [] { "/{index}/{type}/{id}/_update" }, new [] { PathInfoHttpMethod.POST });
		}
		
	}	
}
