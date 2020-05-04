// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;
using static Elasticsearch.Net.HttpMethod;

namespace Elasticsearch.Net.Specification.NodesApi
{
	// Introduced as workaround for https://github.com/elastic/elasticsearch-net/pull/4602
	public partial class LowLevelNodesNamespace
	{
		///<summary>POST on /_nodes/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ReloadSecureSettingsForAll<TResponse>(ReloadSecureSettingsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, "_nodes/reload_secure_settings", null, RequestParams(requestParameters));
		///<summary>POST on /_nodes/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.reload_secure_settings", "")]
		public Task<TResponse> ReloadSecureSettingsForAllAsync<TResponse>(ReloadSecureSettingsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, "_nodes/reload_secure_settings", ctx, null, RequestParams(requestParameters));
		///<summary>POST on /_nodes/{node_id}/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs to span the reload/reinit call. Should stay empty because reloading usually involves all cluster nodes.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ReloadSecureSettings<TResponse>(string nodeId, ReloadSecureSettingsRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, Url($"_nodes/{nodeId:nodeId}/reload_secure_settings"), null, RequestParams(requestParameters));
		///<summary>POST on /_nodes/{node_id}/reload_secure_settings <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/secure-settings.html#reloadable-secure-settings</para></summary>
		///<param name = "nodeId">A comma-separated list of node IDs to span the reload/reinit call. Should stay empty because reloading usually involves all cluster nodes.</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("nodes.reload_secure_settings", "node_id")]
		public Task<TResponse> ReloadSecureSettingsAsync<TResponse>(string nodeId, ReloadSecureSettingsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, Url($"_nodes/{nodeId:nodeId}/reload_secure_settings"), ctx, null, RequestParams(requestParameters));
	}
}
