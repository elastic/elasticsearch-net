// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using static Elasticsearch.Net.HttpMethod;

// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable once CheckNamespace
// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable RedundantExtendsListEntry
namespace Elasticsearch.Net.Specification.NodesApi
{
	///<summary>
	/// Nodes APIs.
	/// <para>Not intended to be instantiated directly. Use the <see cref = "IElasticLowLevelClient.Nodes"/> property
	/// on <see cref = "IElasticLowLevelClient"/>.
	///</para>
	///</summary>
	public partial class LowLevelNodesNamespace : NamespacedClientProxy
	{
		internal LowLevelNodesNamespace(ElasticLowLevelClient client): base(client)
		{
		}

		///<summary>GET on /_nodes/hot_threads <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-hot-threads.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse HotThreadsForAll<TResponse>(NodesHotThreadsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, "_nodes/hot_threads", null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/hot_threads <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-hot-threads.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.hot_threads", "")]
		public Task<TResponse> HotThreadsForAllAsync<TResponse>(NodesHotThreadsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, "_nodes/hot_threads", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/hot_threads <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-hot-threads.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse HotThreads<TResponse>(string nodeId, NodesHotThreadsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/hot_threads"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/hot_threads <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-hot-threads.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.hot_threads", "node_id")]
		public Task<TResponse> HotThreadsAsync<TResponse>(string nodeId, NodesHotThreadsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/hot_threads"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse InfoForAll<TResponse>(NodesInfoRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, "_nodes", null, RequestParams(requestParameters));
		///<summary>GET on /_nodes <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.info", "")]
		public Task<TResponse> InfoForAllAsync<TResponse>(NodesInfoRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, "_nodes", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Info<TResponse>(string nodeId, NodesInfoRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.info", "node_id")]
		public Task<TResponse> InfoAsync<TResponse>(string nodeId, NodesInfoRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse InfoForAll<TResponse>(string metric, NodesInfoRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{metric:metric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.info", "metric")]
		public Task<TResponse> InfoForAllAsync<TResponse>(string metric, NodesInfoRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{metric:metric}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Info<TResponse>(string nodeId, string metric, NodesInfoRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/{metric:metric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-info.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">A comma-separated list of metrics you wish returned. Leave empty to return all.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.info", "node_id, metric")]
		public Task<TResponse> InfoAsync<TResponse>(string nodeId, string metric, NodesInfoRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/{metric:metric}"), ctx, null, RequestParams(requestParameters));
		///<summary>POST on /_nodes/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "body">An object containing the password for the elasticsearch keystore</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ReloadSecureSettingsForAll<TResponse>(PostData body, ReloadSecureSettingsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, "_nodes/reload_secure_settings", body, RequestParams(requestParameters));
		///<summary>POST on /_nodes/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "body">An object containing the password for the elasticsearch keystore</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.reload_secure_settings", "body")]
		public Task<TResponse> ReloadSecureSettingsForAllAsync<TResponse>(PostData body, ReloadSecureSettingsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, "_nodes/reload_secure_settings", ctx, body, RequestParams(requestParameters));
		///<summary>POST on /_nodes/{node_id}/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs to span the reload/reinit call. Should stay empty because reloading usually involves all cluster nodes.</param>
		///<param name = "body">An object containing the password for the elasticsearch keystore</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ReloadSecureSettings<TResponse>(string nodeId, PostData body, ReloadSecureSettingsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, Url($"_nodes/{nodeId:nodeId}/reload_secure_settings"), body, RequestParams(requestParameters));
		///<summary>POST on /_nodes/{node_id}/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs to span the reload/reinit call. Should stay empty because reloading usually involves all cluster nodes.</param>
		///<param name = "body">An object containing the password for the elasticsearch keystore</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.reload_secure_settings", "node_id, body")]
		public Task<TResponse> ReloadSecureSettingsAsync<TResponse>(string nodeId, PostData body, ReloadSecureSettingsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, Url($"_nodes/{nodeId:nodeId}/reload_secure_settings"), ctx, body, RequestParams(requestParameters));
		///<summary>GET on /_nodes/stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse StatsForAll<TResponse>(NodesStatsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, "_nodes/stats", null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.stats", "")]
		public Task<TResponse> StatsForAllAsync<TResponse>(NodesStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, "_nodes/stats", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Stats<TResponse>(string nodeId, NodesStatsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/stats"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.stats", "node_id")]
		public Task<TResponse> StatsAsync<TResponse>(string nodeId, NodesStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/stats"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/stats/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse StatsForAll<TResponse>(string metric, NodesStatsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/stats/{metric:metric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/stats/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.stats", "metric")]
		public Task<TResponse> StatsForAllAsync<TResponse>(string metric, NodesStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/stats/{metric:metric}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/stats/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Stats<TResponse>(string nodeId, string metric, NodesStatsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/stats/{metric:metric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/stats/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.stats", "node_id, metric")]
		public Task<TResponse> StatsAsync<TResponse>(string nodeId, string metric, NodesStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/stats/{metric:metric}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/stats/{metric}/{index_metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "indexMetric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#x27;t used if `indices` (or `all`) metric isn&#x27;t specified.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse StatsForAll<TResponse>(string metric, string indexMetric, NodesStatsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/stats/{metric:metric}/{indexMetric:indexMetric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/stats/{metric}/{index_metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "indexMetric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#x27;t used if `indices` (or `all`) metric isn&#x27;t specified.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.stats", "metric, index_metric")]
		public Task<TResponse> StatsForAllAsync<TResponse>(string metric, string indexMetric, NodesStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/stats/{metric:metric}/{indexMetric:indexMetric}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/stats/{metric}/{index_metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "indexMetric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#x27;t used if `indices` (or `all`) metric isn&#x27;t specified.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Stats<TResponse>(string nodeId, string metric, string indexMetric, NodesStatsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/stats/{metric:metric}/{indexMetric:indexMetric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/stats/{metric}/{index_metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-stats.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "indexMetric">Limit the information returned for `indices` metric to the specific index metrics. Isn&#x27;t used if `indices` (or `all`) metric isn&#x27;t specified.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.stats", "node_id, metric, index_metric")]
		public Task<TResponse> StatsAsync<TResponse>(string nodeId, string metric, string indexMetric, NodesStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/stats/{metric:metric}/{indexMetric:indexMetric}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/usage <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse UsageForAll<TResponse>(NodesUsageRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, "_nodes/usage", null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/usage <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.usage", "")]
		public Task<TResponse> UsageForAllAsync<TResponse>(NodesUsageRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, "_nodes/usage", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/usage <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Usage<TResponse>(string nodeId, NodesUsageRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/usage"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/usage <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.usage", "node_id")]
		public Task<TResponse> UsageAsync<TResponse>(string nodeId, NodesUsageRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/usage"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/usage/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse UsageForAll<TResponse>(string metric, NodesUsageRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/usage/{metric:metric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/usage/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.usage", "metric")]
		public Task<TResponse> UsageForAllAsync<TResponse>(string metric, NodesUsageRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/usage/{metric:metric}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/usage/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Usage<TResponse>(string nodeId, string metric, NodesUsageRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/usage/{metric:metric}"), null, RequestParams(requestParameters));
		///<summary>GET on /_nodes/{node_id}/usage/{metric} <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs or names to limit the returned information; use `_local` to return information from the node you&#x27;re connecting to, leave empty to get information from all nodes</param>
		///<param name = "metric">Limit the information returned to the specified metrics</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.usage", "node_id, metric")]
		public Task<TResponse> UsageAsync<TResponse>(string nodeId, string metric, NodesUsageRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_nodes/{nodeId:nodeId}/usage/{metric:metric}"), ctx, null, RequestParams(requestParameters));
	}
}