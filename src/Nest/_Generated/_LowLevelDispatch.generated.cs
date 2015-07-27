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
	internal partial class LowLevelDispatch
	{
		internal ElasticsearchResponse<T> AbortBenchmarkDispatch<T>(ElasticsearchPathInfo<AbortBenchmarkRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.AbortBenchmark<T>(pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.AbortBenchmarkAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.Bulk<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Bulk<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Bulk<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.BulkPut<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.BulkPut<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.BulkPut<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Bulk", new [] { "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> BulkDispatchAsync<T>(ElasticsearchPathInfo<BulkRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.BulkAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.BulkAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.BulkAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.BulkPutAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.BulkPutAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.BulkPutAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Bulk", new [] { "/_bulk", "/{index}/_bulk", "/{index}/{type}/_bulk" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> CatAliasesDispatch<T>(ElasticsearchPathInfo<CatAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.CatAliases<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatAliases<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAliases", new [] { "/_cat/aliases", "/_cat/aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatAliasesDispatchAsync<T>(ElasticsearchPathInfo<CatAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.CatAliasesAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatAliasesAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAliases", new [] { "/_cat/aliases", "/_cat/aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatAllocationDispatch<T>(ElasticsearchPathInfo<CatAllocationRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.CatAllocation<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatAllocation<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAllocation", new [] { "/_cat/allocation", "/_cat/allocation/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatAllocationDispatchAsync<T>(ElasticsearchPathInfo<CatAllocationRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.CatAllocationAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatAllocationAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatAllocation", new [] { "/_cat/allocation", "/_cat/allocation/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatCountDispatch<T>(ElasticsearchPathInfo<CatCountRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatCount<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatCount<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatCount", new [] { "/_cat/count", "/_cat/count/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatCountDispatchAsync<T>(ElasticsearchPathInfo<CatCountRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatCountAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatCountAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatCount", new [] { "/_cat/count", "/_cat/count/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatFielddataDispatch<T>(ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatFielddata<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatFielddata", new [] { "/_cat/fielddata" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatFielddataDispatchAsync<T>(ElasticsearchPathInfo<CatFielddataRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatFielddataAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatFielddata", new [] { "/_cat/fielddata" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatHealthDispatch<T>(ElasticsearchPathInfo<CatHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatHealth<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHealth", new [] { "/_cat/health" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatHealthDispatchAsync<T>(ElasticsearchPathInfo<CatHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatHealthAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHealth", new [] { "/_cat/health" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatHelpDispatch<T>(ElasticsearchPathInfo<CatHelpRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatHelp<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHelp", new [] { "/_cat" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatHelpDispatchAsync<T>(ElasticsearchPathInfo<CatHelpRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatHelpAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatHelp", new [] { "/_cat" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatIndicesDispatch<T>(ElasticsearchPathInfo<CatIndicesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatIndices<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatIndices<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatIndices", new [] { "/_cat/indices", "/_cat/indices/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatIndicesDispatchAsync<T>(ElasticsearchPathInfo<CatIndicesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatIndicesAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatIndicesAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatIndices", new [] { "/_cat/indices", "/_cat/indices/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatMasterDispatch<T>(ElasticsearchPathInfo<CatMasterRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatMaster<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatMaster", new [] { "/_cat/master" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatMasterDispatchAsync<T>(ElasticsearchPathInfo<CatMasterRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatMasterAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatMaster", new [] { "/_cat/master" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatNodesDispatch<T>(ElasticsearchPathInfo<CatNodesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatNodes<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatNodes", new [] { "/_cat/nodes" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatNodesDispatchAsync<T>(ElasticsearchPathInfo<CatNodesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatNodesAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatNodes", new [] { "/_cat/nodes" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatPendingTasksDispatch<T>(ElasticsearchPathInfo<CatPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatPendingTasks<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPendingTasks", new [] { "/_cat/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatPendingTasksDispatchAsync<T>(ElasticsearchPathInfo<CatPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatPendingTasksAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPendingTasks", new [] { "/_cat/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatPluginsDispatch<T>(ElasticsearchPathInfo<CatPluginsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatPlugins<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPlugins", new [] { "/_cat/plugins" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatPluginsDispatchAsync<T>(ElasticsearchPathInfo<CatPluginsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatPluginsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatPlugins", new [] { "/_cat/plugins" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatRecoveryDispatch<T>(ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatRecovery<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatRecovery<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatRecovery", new [] { "/_cat/recovery", "/_cat/recovery/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatRecoveryDispatchAsync<T>(ElasticsearchPathInfo<CatRecoveryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatRecoveryAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatRecoveryAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatRecovery", new [] { "/_cat/recovery", "/_cat/recovery/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatSegmentsDispatch<T>(ElasticsearchPathInfo<CatSegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatSegments<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatSegments<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatSegments", new [] { "/_cat/segments", "/_cat/segments/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatSegmentsDispatchAsync<T>(ElasticsearchPathInfo<CatSegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatSegmentsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatSegmentsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatSegments", new [] { "/_cat/segments", "/_cat/segments/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatShardsDispatch<T>(ElasticsearchPathInfo<CatShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatShards<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatShards<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatShards", new [] { "/_cat/shards", "/_cat/shards/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatShardsDispatchAsync<T>(ElasticsearchPathInfo<CatShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CatShardsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CatShardsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatShards", new [] { "/_cat/shards", "/_cat/shards/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CatThreadPoolDispatch<T>(ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatThreadPool<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatThreadPool", new [] { "/_cat/thread_pool" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CatThreadPoolDispatchAsync<T>(ElasticsearchPathInfo<CatThreadPoolRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.CatThreadPoolAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "CatThreadPool", new [] { "/_cat/thread_pool" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClearScrollDispatch<T>(ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.ScrollId))
						return this.LowLevelClient.ClearScroll<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClearScroll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClearScroll", new [] { "/_search/scroll/{scroll_id}", "/_search/scroll" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal Task<ElasticsearchResponse<T>> ClearScrollDispatchAsync<T>(ElasticsearchPathInfo<ClearScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.DELETE:
					if (AllSet(pathInfo.ScrollId))
						return this.LowLevelClient.ClearScrollAsync<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClearScrollAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClearScroll", new [] { "/_search/scroll/{scroll_id}", "/_search/scroll" }, new [] { PathInfoHttpMethod.DELETE });
		}
		
		internal ElasticsearchResponse<T> ClusterGetSettingsDispatch<T>(ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.ClusterGetSettings<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterGetSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterGetSettingsDispatchAsync<T>(ElasticsearchPathInfo<ClusterGetSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.ClusterGetSettingsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterGetSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterHealthDispatch<T>(ElasticsearchPathInfo<ClusterHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.ClusterHealth<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClusterHealth<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterHealth", new [] { "/_cluster/health", "/_cluster/health/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterHealthDispatchAsync<T>(ElasticsearchPathInfo<ClusterHealthRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.ClusterHealthAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClusterHealthAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterHealth", new [] { "/_cluster/health", "/_cluster/health/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterPendingTasksDispatch<T>(ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.ClusterPendingTasks<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPendingTasks", new [] { "/_cluster/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPendingTasksDispatchAsync<T>(ElasticsearchPathInfo<ClusterPendingTasksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.ClusterPendingTasksAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPendingTasks", new [] { "/_cluster/pending_tasks" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterPutSettingsDispatch<T>(ElasticsearchPathInfo<ClusterSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					return this.LowLevelClient.ClusterPutSettings<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPutSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterPutSettingsDispatchAsync<T>(ElasticsearchPathInfo<ClusterSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					return this.LowLevelClient.ClusterPutSettingsAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterPutSettings", new [] { "/_cluster/settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> ClusterRerouteDispatch<T>(ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.LowLevelClient.ClusterReroute<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterReroute", new [] { "/_cluster/reroute" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterRerouteDispatchAsync<T>(ElasticsearchPathInfo<ClusterRerouteRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.LowLevelClient.ClusterRerouteAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterReroute", new [] { "/_cluster/reroute" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> ClusterStateDispatch<T>(ElasticsearchPathInfo<ClusterStateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Metric, pathInfo.Index))
						return this.LowLevelClient.ClusterState<T>(pathInfo.Metric,pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.ClusterState<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClusterState<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterState", new [] { "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStateDispatchAsync<T>(ElasticsearchPathInfo<ClusterStateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Metric, pathInfo.Index))
						return this.LowLevelClient.ClusterStateAsync<T>(pathInfo.Metric,pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.ClusterStateAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClusterStateAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterState", new [] { "/_cluster/state", "/_cluster/state/{metric}", "/_cluster/state/{metric}/{index}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ClusterStatsDispatch<T>(ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.ClusterStats<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClusterStats<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterStats", new [] { "/_cluster/stats", "/_cluster/stats/nodes/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ClusterStatsDispatchAsync<T>(ElasticsearchPathInfo<ClusterStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.ClusterStatsAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ClusterStatsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ClusterStats", new [] { "/_cluster/stats", "/_cluster/stats/nodes/{node_id}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CountDispatch<T>(ElasticsearchPathInfo<CountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Count<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Count<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Count<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CountGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CountGet<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Count", new [] { "/_count", "/{index}/_count", "/{index}/{type}/_count" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> CountDispatchAsync<T>(ElasticsearchPathInfo<CountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CountAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CountAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.CountGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.CountGetAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Count", new [] { "/_count", "/{index}/_count", "/{index}/{type}/_count" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> CountPercolateDispatch<T>(ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.CountPercolateGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountPercolateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.CountPercolate<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountPercolate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.CountPercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountPercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.CountPercolateAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.CountPercolateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.Delete<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteByQuery<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.DeleteByQuery<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteByQueryAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.DeleteByQueryAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteScript<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteScriptAsync<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteTemplate<T>(pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.DeleteTemplateAsync<T>(pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.Exists<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.ExistsAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.ExplainGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.Explain<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.ExplainGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.ExplainAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.FieldStatsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.FieldStatsGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.FieldStats<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.FieldStats<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "FieldStats", new [] { "/_field_stats", "/{index}/_field_stats" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> FieldStatsDispatchAsync<T>(ElasticsearchPathInfo<FieldStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.FieldStatsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.FieldStatsGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.FieldStatsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.FieldStatsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "FieldStats", new [] { "/_field_stats", "/{index}/_field_stats" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> GetDispatch<T>(ElasticsearchPathInfo<GetRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.Get<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetScript<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetScriptAsync<T>(pathInfo.Lang,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetSource<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetSourceAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetTemplate<T>(pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.GetTemplateAsync<T>(pathInfo.Id,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.Index<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Index<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.IndexPut<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndexPut<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndexAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndexAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.IndexPutAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndexPutAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesAnalyzeGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesAnalyzeGetForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesAnalyze<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesAnalyzeForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesAnalyze", new [] { "/_analyze", "/{index}/_analyze" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesAnalyzeDispatchAsync<T>(ElasticsearchPathInfo<AnalyzeRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesAnalyzeGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesAnalyzeGetForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesAnalyzeAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesAnalyzeForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesAnalyze", new [] { "/_analyze", "/{index}/_analyze" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesClearCacheDispatch<T>(ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesClearCache<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesClearCacheForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesClearCacheGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesClearCacheGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesClearCache", new [] { "/_cache/clear", "/{index}/_cache/clear" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesClearCacheDispatchAsync<T>(ElasticsearchPathInfo<ClearCacheRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesClearCacheAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesClearCacheForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesClearCacheGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesClearCacheGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesClearCache", new [] { "/_cache/clear", "/{index}/_cache/clear" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesCloseDispatch<T>(ElasticsearchPathInfo<CloseIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesClose<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesCloseAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesCreate<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesCreatePost<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesCreateAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesCreatePostAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDelete<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteAlias<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteAliasAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteMapping<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteMappingAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteTemplateForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteTemplateForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteWarmer<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesDeleteWarmerAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExists<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsAlias<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesExistsAliasForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesExistsAlias<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsAliasAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesExistsAliasForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesExistsAliasAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsTemplateForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsTemplateForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsType<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesExistsTypeAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesFlush<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlush", new [] { "/_flush", "/{index}/_flush" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushDispatchAsync<T>(ElasticsearchPathInfo<FlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlush", new [] { "/_flush", "/{index}/_flush" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesFlushSyncedDispatch<T>(ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushSynced<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushSyncedForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushSyncedGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushSyncedGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlushSynced", new [] { "/_flush/synced", "/{index}/_flush/synced" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesFlushSyncedDispatchAsync<T>(ElasticsearchPathInfo<SyncedFlushRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushSyncedAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushSyncedForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesFlushSyncedGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesFlushSyncedGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesFlushSynced", new [] { "/_flush/synced", "/{index}/_flush/synced" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetDispatch<T>(ElasticsearchPathInfo<GetIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Feature))
						return this.LowLevelClient.IndicesGet<T>(pathInfo.Index,pathInfo.Feature,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesGetAsync<T>(pathInfo.Index,pathInfo.Feature,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesGetAlias<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliasForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetAlias<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetAliasForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAlias", new [] { "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasDispatchAsync<T>(ElasticsearchPathInfo<GetAliasRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliasAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliasForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetAliasAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetAliasForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAlias", new [] { "/_alias", "/_alias/{name}", "/{index}/_alias/{name}", "/{index}/_alias" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetAliasesDispatch<T>(ElasticsearchPathInfo<GetAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliases<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetAliases<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliasesForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetAliasesForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAliases", new [] { "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetAliasesDispatchAsync<T>(ElasticsearchPathInfo<GetAliasesRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliasesAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetAliasesAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetAliasesForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetAliasesForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetAliases", new [] { "/_aliases", "/{index}/_aliases", "/{index}/_aliases/{name}", "/_aliases/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetFieldMappingDispatch<T>(ElasticsearchPathInfo<GetFieldMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMapping<T>(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMapping<T>(pathInfo.Index,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type, pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMappingForAll<T>(pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMappingForAll<T>(pathInfo.Field,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesGetFieldMappingAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMappingAsync<T>(pathInfo.Index,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type, pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMappingForAllAsync<T>(pathInfo.Type,pathInfo.Field,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Field))
						return this.LowLevelClient.IndicesGetFieldMappingForAllAsync<T>(pathInfo.Field,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesGetMapping<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetMapping<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.LowLevelClient.IndicesGetMappingForAll<T>(pathInfo.Type,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetMappingForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetMapping", new [] { "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetMappingDispatchAsync<T>(ElasticsearchPathInfo<GetMappingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesGetMappingAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetMappingAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.LowLevelClient.IndicesGetMappingForAllAsync<T>(pathInfo.Type,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetMappingForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetMapping", new [] { "/_mapping", "/{index}/_mapping", "/_mapping/{type}", "/{index}/_mapping/{type}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetSettingsDispatch<T>(ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetSettings<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetSettings<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetSettingsForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetSettingsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetSettings", new [] { "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetSettingsDispatchAsync<T>(ElasticsearchPathInfo<GetIndexSettingsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetSettingsAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetSettingsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetSettingsForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetSettingsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetSettings", new [] { "/_settings", "/{index}/_settings", "/{index}/_settings/{name}", "/_settings/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetTemplateDispatch<T>(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetTemplateForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetTemplateForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetTemplate", new [] { "/_template", "/_template/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetTemplateDispatchAsync<T>(ElasticsearchPathInfo<GetTemplateRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetTemplateForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetTemplateForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetTemplate", new [] { "/_template", "/_template/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetUpgradeDispatch<T>(ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetUpgrade<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetUpgradeForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetUpgradeDispatchAsync<T>(ElasticsearchPathInfo<UpgradeStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetUpgradeAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetUpgradeForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesGetWarmerDispatch<T>(ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.LowLevelClient.IndicesGetWarmer<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetWarmer<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetWarmer<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetWarmerForAll<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetWarmerForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetWarmer", new [] { "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesGetWarmerDispatchAsync<T>(ElasticsearchPathInfo<GetWarmerRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.LowLevelClient.IndicesGetWarmerAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesGetWarmerAsync<T>(pathInfo.Index,pathInfo.Name,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesGetWarmerAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesGetWarmerForAllAsync<T>(pathInfo.Name,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesGetWarmerForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesGetWarmer", new [] { "/_warmer", "/{index}/_warmer", "/{index}/_warmer/{name}", "/_warmer/{name}", "/{index}/{type}/_warmer/{name}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesOpenDispatch<T>(ElasticsearchPathInfo<OpenIndexRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesOpen<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesOpenAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesOptimize<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesOptimizeForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesOptimizeGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesOptimizeGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesOptimize", new [] { "/_optimize", "/{index}/_optimize" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesOptimizeDispatchAsync<T>(ElasticsearchPathInfo<OptimizeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesOptimizeAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesOptimizeForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesOptimizeGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesOptimizeGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesOptimize", new [] { "/_optimize", "/{index}/_optimize" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesPutAliasDispatch<T>(ElasticsearchPathInfo<PutAliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutAlias<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutAliasPost<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutAliasAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutAliasPostAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutMapping<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.LowLevelClient.IndicesPutMappingForAll<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesPutMappingPost<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.LowLevelClient.IndicesPutMappingPostForAll<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutMappingAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.LowLevelClient.IndicesPutMappingForAllAsync<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesPutMappingPostAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Type))
						return this.LowLevelClient.IndicesPutMappingPostForAllAsync<T>(pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutSettings<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesPutSettingsForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesPutSettings", new [] { "/_settings", "/{index}/_settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesPutSettingsDispatchAsync<T>(ElasticsearchPathInfo<UpdateSettingsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesPutSettingsAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesPutSettingsForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesPutSettings", new [] { "/_settings", "/{index}/_settings" }, new [] { PathInfoHttpMethod.PUT });
		}
		
		internal ElasticsearchResponse<T> IndicesPutTemplateDispatch<T>(ElasticsearchPathInfo<PutTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutTemplateForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutTemplatePostForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutTemplateForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutTemplatePostForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutWarmer<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmer<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerPost<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerPost<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerPostForAll<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesPutWarmerAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerPostAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerPostAsync<T>(pathInfo.Index,pathInfo.Name,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Name))
						return this.LowLevelClient.IndicesPutWarmerPostForAllAsync<T>(pathInfo.Name,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.IndicesRecovery<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesRecoveryForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRecovery", new [] { "/_recovery", "/{index}/_recovery" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRecoveryDispatchAsync<T>(ElasticsearchPathInfo<RecoveryStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesRecoveryAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesRecoveryForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRecovery", new [] { "/_recovery", "/{index}/_recovery" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesRefreshDispatch<T>(ElasticsearchPathInfo<RefreshRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesRefresh<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesRefreshForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesRefreshGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesRefreshGetForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRefresh", new [] { "/_refresh", "/{index}/_refresh" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesRefreshDispatchAsync<T>(ElasticsearchPathInfo<RefreshRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesRefreshAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesRefreshForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesRefreshGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesRefreshGetForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesRefresh", new [] { "/_refresh", "/{index}/_refresh" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesSegmentsDispatch<T>(ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesSegments<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesSegmentsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesSegments", new [] { "/_segments", "/{index}/_segments" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesSegmentsDispatchAsync<T>(ElasticsearchPathInfo<SegmentsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesSegmentsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesSegmentsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesSegments", new [] { "/_segments", "/{index}/_segments" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesStatsDispatch<T>(ElasticsearchPathInfo<IndicesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Metric))
						return this.LowLevelClient.IndicesStats<T>(pathInfo.Index,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.IndicesStatsForAll<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesStats<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesStatsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStats", new [] { "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesStatsDispatchAsync<T>(ElasticsearchPathInfo<IndicesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Metric))
						return this.LowLevelClient.IndicesStatsAsync<T>(pathInfo.Index,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.IndicesStatsForAllAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesStatsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesStatsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStats", new [] { "/_stats", "/_stats/{metric}", "/{index}/_stats", "/{index}/_stats/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesStatusDispatch<T>(ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesStatus<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesStatusForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStatus", new [] { "/_status", "/{index}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesStatusDispatchAsync<T>(ElasticsearchPathInfo<IndicesStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesStatusAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesStatusForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesStatus", new [] { "/_status", "/{index}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> IndicesUpdateAliasesDispatch<T>(ElasticsearchPathInfo<AliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.LowLevelClient.IndicesUpdateAliasesForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpdateAliases", new [] { "/_aliases" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpdateAliasesDispatchAsync<T>(ElasticsearchPathInfo<AliasRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					return this.LowLevelClient.IndicesUpdateAliasesForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpdateAliases", new [] { "/_aliases" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesUpgradeDispatch<T>(ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesUpgrade<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesUpgradeForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesUpgradeDispatchAsync<T>(ElasticsearchPathInfo<UpgradeRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesUpgradeAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesUpgradeForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesUpgrade", new [] { "/_upgrade", "/{index}/_upgrade" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> IndicesValidateQueryDispatch<T>(ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesValidateQueryGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesValidateQueryGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesValidateQueryGetForAll<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesValidateQuery<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesValidateQuery<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesValidateQueryForAll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesValidateQuery", new [] { "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> IndicesValidateQueryDispatchAsync<T>(ElasticsearchPathInfo<ValidateQueryRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesValidateQueryGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesValidateQueryGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesValidateQueryGetForAllAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.IndicesValidateQueryAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.IndicesValidateQueryAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.IndicesValidateQueryForAllAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "IndicesValidateQuery", new [] { "/_validate/query", "/{index}/_validate/query", "/{index}/{type}/_validate/query" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> InfoDispatch<T>(ElasticsearchPathInfo<InfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.Info<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Info", new [] { "/" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> InfoDispatchAsync<T>(ElasticsearchPathInfo<InfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					return this.LowLevelClient.InfoAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Info", new [] { "/" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> ListBenchmarksDispatch<T>(ElasticsearchPathInfo<ListBenchmarksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.ListBenchmarks<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.ListBenchmarks<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ListBenchmarks<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ListBenchmarks", new [] { "/_bench", "/{index}/_bench", "/{index}/{type}/_bench" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> ListBenchmarksDispatchAsync<T>(ElasticsearchPathInfo<ListBenchmarksRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.ListBenchmarksAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.ListBenchmarksAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ListBenchmarksAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "ListBenchmarks", new [] { "/_bench", "/{index}/_bench", "/{index}/{type}/_bench" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> MgetDispatch<T>(ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MgetGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MgetGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MgetGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Mget<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Mget<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Mget<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mget", new [] { "/_mget", "/{index}/_mget", "/{index}/{type}/_mget" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MgetDispatchAsync<T>(ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MgetGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MgetGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MgetGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MgetAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MgetAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MgetAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mget", new [] { "/_mget", "/{index}/_mget", "/{index}/{type}/_mget" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MltDispatch<T>(ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.MltGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.Mlt<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.MltGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.MltAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.MpercolateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MpercolateGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MpercolateGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Mpercolate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Mpercolate<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Mpercolate<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mpercolate", new [] { "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MpercolateDispatchAsync<T>(ElasticsearchPathInfo<MultiPercolateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MpercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MpercolateGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MpercolateGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MpercolateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MpercolateAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MpercolateAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mpercolate", new [] { "/_mpercolate", "/{index}/_mpercolate", "/{index}/{type}/_mpercolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MsearchDispatch<T>(ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MsearchGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MsearchGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MsearchGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Msearch<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Msearch<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Msearch<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Msearch", new [] { "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MsearchDispatchAsync<T>(ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MsearchGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MsearchGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MsearchGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MsearchAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MsearchAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MsearchAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Msearch", new [] { "/_msearch", "/{index}/_msearch", "/{index}/{type}/_msearch" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> MtermvectorsDispatch<T>(ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MtermvectorsGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MtermvectorsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MtermvectorsGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Mtermvectors<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Mtermvectors<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Mtermvectors<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mtermvectors", new [] { "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> MtermvectorsDispatchAsync<T>(ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MtermvectorsGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MtermvectorsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MtermvectorsGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.MtermvectorsAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.MtermvectorsAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.MtermvectorsAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Mtermvectors", new [] { "/_mtermvectors", "/{index}/_mtermvectors", "/{index}/{type}/_mtermvectors" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> NodesHotThreadsDispatch<T>(ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesHotThreads<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesHotThreadsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesHotThreads", new [] { "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesHotThreadsDispatchAsync<T>(ElasticsearchPathInfo<NodesHotThreadsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesHotThreadsAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesHotThreadsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesHotThreads", new [] { "/_cluster/nodes/hotthreads", "/_cluster/nodes/hot_threads", "/_cluster/nodes/{node_id}/hotthreads", "/_cluster/nodes/{node_id}/hot_threads", "/_nodes/hotthreads", "/_nodes/hot_threads", "/_nodes/{node_id}/hotthreads", "/_nodes/{node_id}/hot_threads" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> NodesInfoDispatch<T>(ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.LowLevelClient.NodesInfo<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesInfo<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.NodesInfoForAll<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesInfoForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesInfo", new [] { "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesInfoDispatchAsync<T>(ElasticsearchPathInfo<NodesInfoRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.LowLevelClient.NodesInfoAsync<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesInfoAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.NodesInfoForAllAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesInfoForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesInfo", new [] { "/_nodes", "/_nodes/{node_id}", "/_nodes/{metric}", "/_nodes/{node_id}/{metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> NodesShutdownDispatch<T>(ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesShutdown<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesShutdownForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesShutdown", new [] { "/_shutdown", "/_cluster/nodes/_shutdown", "/_cluster/nodes/{node_id}/_shutdown" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesShutdownDispatchAsync<T>(ElasticsearchPathInfo<NodesShutdownRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesShutdownAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesShutdownForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesShutdown", new [] { "/_shutdown", "/_cluster/nodes/_shutdown", "/_cluster/nodes/{node_id}/_shutdown" }, new [] { PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> NodesStatsDispatch<T>(ElasticsearchPathInfo<NodesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric, pathInfo.IndexMetric))
						return this.LowLevelClient.NodesStats<T>(pathInfo.NodeId,pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.LowLevelClient.NodesStats<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric, pathInfo.IndexMetric))
						return this.LowLevelClient.NodesStatsForAll<T>(pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesStats<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.NodesStatsForAll<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesStatsForAll<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesStats", new [] { "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> NodesStatsDispatchAsync<T>(ElasticsearchPathInfo<NodesStatsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.NodeId, pathInfo.Metric, pathInfo.IndexMetric))
						return this.LowLevelClient.NodesStatsAsync<T>(pathInfo.NodeId,pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId, pathInfo.Metric))
						return this.LowLevelClient.NodesStatsAsync<T>(pathInfo.NodeId,pathInfo.Metric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric, pathInfo.IndexMetric))
						return this.LowLevelClient.NodesStatsForAllAsync<T>(pathInfo.Metric,pathInfo.IndexMetric,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.NodeId))
						return this.LowLevelClient.NodesStatsAsync<T>(pathInfo.NodeId,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Metric))
						return this.LowLevelClient.NodesStatsForAllAsync<T>(pathInfo.Metric,u => pathInfo.RequestParameters);
					return this.LowLevelClient.NodesStatsForAllAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "NodesStats", new [] { "/_nodes/stats", "/_nodes/{node_id}/stats", "/_nodes/stats/{metric}", "/_nodes/{node_id}/stats/{metric}", "/_nodes/stats/{metric}/{index_metric}", "/_nodes/{node_id}/stats/{metric}/{index_metric}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> PercolateDispatch<T>(ElasticsearchPathInfo<PercolateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.PercolateGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.PercolateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.Percolate<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Percolate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.PercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.PercolateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.PercolateAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.PercolateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Percolate", new [] { "/{index}/{type}/_percolate", "/{index}/{type}/{id}/_percolate" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> PingDispatch<T>(ElasticsearchPathInfo<PingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					return this.LowLevelClient.Ping<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Ping", new [] { "/" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal Task<ElasticsearchResponse<T>> PingDispatchAsync<T>(ElasticsearchPathInfo<PingRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.HEAD:
					return this.LowLevelClient.PingAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Ping", new [] { "/" }, new [] { PathInfoHttpMethod.HEAD });
		}
		
		internal ElasticsearchResponse<T> PutScriptDispatch<T>(ElasticsearchPathInfo<PutScriptRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.LowLevelClient.PutScript<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.LowLevelClient.PutScriptPost<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.PutScriptAsync<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Lang, pathInfo.Id))
						return this.LowLevelClient.PutScriptPostAsync<T>(pathInfo.Lang,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.PutTemplate<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Id))
						return this.LowLevelClient.PutTemplatePost<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.PutTemplateAsync<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Id))
						return this.LowLevelClient.PutTemplatePostAsync<T>(pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.ScrollGet<T>(pathInfo.ScrollId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ScrollGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.ScrollId))
						return this.LowLevelClient.Scroll<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Scroll<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Scroll", new [] { "/_search/scroll", "/_search/scroll/{scroll_id}" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> ScrollDispatchAsync<T>(ElasticsearchPathInfo<ScrollRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.ScrollId))
						return this.LowLevelClient.ScrollGetAsync<T>(pathInfo.ScrollId,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ScrollGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.ScrollId))
						return this.LowLevelClient.ScrollAsync<T>(pathInfo.ScrollId,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.ScrollAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Scroll", new [] { "/_search/scroll", "/_search/scroll/{scroll_id}" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SearchDispatch<T>(ElasticsearchPathInfo<SearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Search<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.Search<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Search<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Search", new [] { "/_search", "/{index}/_search", "/{index}/{type}/_search" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchDispatchAsync<T>(ElasticsearchPathInfo<SearchRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Search", new [] { "/_search", "/{index}/_search", "/{index}/{type}/_search" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SearchExistsDispatch<T>(ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchExists<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchExists<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchExists<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchExistsGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchExistsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchExistsGet<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchExists", new [] { "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchExistsDispatchAsync<T>(ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchExistsAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchExistsAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchExistsAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchExistsGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchExistsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchExistsGetAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchExists", new [] { "/_search/exists", "/{index}/_search/exists", "/{index}/{type}/_search/exists" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SearchShardsDispatch<T>(ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchShardsGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchShardsGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchShardsGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchShards<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchShards<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchShards<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchShards", new [] { "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchShardsDispatchAsync<T>(ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchShardsGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchShardsGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchShardsGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchShardsAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchShardsAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchShardsAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchShards", new [] { "/_search_shards", "/{index}/_search_shards", "/{index}/{type}/_search_shards" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SearchTemplateDispatch<T>(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchTemplateGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchTemplateGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchTemplateGet<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchTemplate<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchTemplate<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchTemplate<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchTemplate", new [] { "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal Task<ElasticsearchResponse<T>> SearchTemplateDispatchAsync<T>(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchTemplateGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchTemplateGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchTemplateGetAsync<T>(u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.SearchTemplateAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SearchTemplateAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SearchTemplateAsync<T>(body,u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SearchTemplate", new [] { "/_search/template", "/{index}/_search/template", "/{index}/{type}/_search/template" }, new [] { PathInfoHttpMethod.GET, PathInfoHttpMethod.POST });
		}
		
		internal ElasticsearchResponse<T> SnapshotCreateDispatch<T>(ElasticsearchPathInfo<SnapshotRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.PUT:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.LowLevelClient.SnapshotCreate<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.LowLevelClient.SnapshotCreatePost<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotCreateAsync<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.LowLevelClient.SnapshotCreatePostAsync<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotCreateRepository<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.LowLevelClient.SnapshotCreateRepositoryPost<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotCreateRepositoryAsync<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.LowLevelClient.SnapshotCreateRepositoryPostAsync<T>(pathInfo.Repository,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotDelete<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotDeleteAsync<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotDeleteRepository<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotDeleteRepositoryAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotGet<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotGetAsync<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotGetRepository<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SnapshotGetRepository<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotGetRepository", new [] { "/_snapshot", "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotGetRepositoryDispatchAsync<T>(ElasticsearchPathInfo<GetRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository))
						return this.LowLevelClient.SnapshotGetRepositoryAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SnapshotGetRepositoryAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotGetRepository", new [] { "/_snapshot", "/_snapshot/{repository}" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SnapshotRestoreDispatch<T>(ElasticsearchPathInfo<RestoreRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.LowLevelClient.SnapshotRestore<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotRestoreAsync<T>(pathInfo.Repository,pathInfo.Snapshot,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotStatus<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Repository))
						return this.LowLevelClient.SnapshotStatus<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SnapshotStatus<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotStatus", new [] { "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SnapshotStatusDispatchAsync<T>(ElasticsearchPathInfo<SnapshotStatusRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Repository, pathInfo.Snapshot))
						return this.LowLevelClient.SnapshotStatusAsync<T>(pathInfo.Repository,pathInfo.Snapshot,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Repository))
						return this.LowLevelClient.SnapshotStatusAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SnapshotStatusAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "SnapshotStatus", new [] { "/_snapshot/_status", "/_snapshot/{repository}/_status", "/_snapshot/{repository}/{snapshot}/_status" }, new [] { PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> SnapshotVerifyRepositoryDispatch<T>(ElasticsearchPathInfo<VerifyRepositoryRequestParameters> pathInfo )
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Repository))
						return this.LowLevelClient.SnapshotVerifyRepository<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.SnapshotVerifyRepositoryAsync<T>(pathInfo.Repository,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.Suggest<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.Suggest<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SuggestGet<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SuggestGet<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Suggest", new [] { "/_suggest", "/{index}/_suggest" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal Task<ElasticsearchResponse<T>> SuggestDispatchAsync<T>(ElasticsearchPathInfo<SuggestRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SuggestAsync<T>(pathInfo.Index,body,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SuggestAsync<T>(body,u => pathInfo.RequestParameters);

				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index))
						return this.LowLevelClient.SuggestGetAsync<T>(pathInfo.Index,u => pathInfo.RequestParameters);
					return this.LowLevelClient.SuggestGetAsync<T>(u => pathInfo.RequestParameters);

			}
			throw DispatchException.Create(pathInfo, "Suggest", new [] { "/_suggest", "/{index}/_suggest" }, new [] { PathInfoHttpMethod.POST, PathInfoHttpMethod.GET });
		}
		
		internal ElasticsearchResponse<T> TermvectorDispatch<T>(ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo , object body)
		{
			switch(pathInfo.HttpMethod)
			{
				case PathInfoHttpMethod.GET:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.TermvectorGet<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.TermvectorGet<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.Termvector<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.Termvector<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.TermvectorGetAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.TermvectorGetAsync<T>(pathInfo.Index,pathInfo.Type,u => pathInfo.RequestParameters);
					break;

				case PathInfoHttpMethod.POST:
					if (AllSet(pathInfo.Index, pathInfo.Type, pathInfo.Id))
						return this.LowLevelClient.TermvectorAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					if (AllSet(pathInfo.Index, pathInfo.Type))
						return this.LowLevelClient.TermvectorAsync<T>(pathInfo.Index,pathInfo.Type,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.Update<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
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
						return this.LowLevelClient.UpdateAsync<T>(pathInfo.Index,pathInfo.Type,pathInfo.Id,body,u => pathInfo.RequestParameters);
					break;

			}
			throw DispatchException.Create(pathInfo, "Update", new [] { "/{index}/{type}/{id}/_update" }, new [] { PathInfoHttpMethod.POST });
		}
		
	}	
}
