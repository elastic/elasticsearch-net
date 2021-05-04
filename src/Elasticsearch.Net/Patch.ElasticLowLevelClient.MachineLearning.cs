// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;
using static Elasticsearch.Net.HttpMethod;

namespace Elasticsearch.Net.Specification.MachineLearningApi
{
	// Introduced as workaround for the introduction of a body in https://github.com/elastic/elasticsearch/pull/56895
	public partial class LowLevelMachineLearningNamespace
	{
		///<summary>DELETE on /_ml/_delete_expired_data <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/ml-delete-expired-data.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse DeleteExpiredData<TResponse>(DeleteExpiredDataRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(DELETE, "_ml/_delete_expired_data", null, RequestParams(requestParameters));
		///<summary>DELETE on /_ml/_delete_expired_data <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/ml-delete-expired-data.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public Task<TResponse> DeleteExpiredDataAsync<TResponse>(DeleteExpiredDataRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(DELETE, "_ml/_delete_expired_data", ctx, null, RequestParams(requestParameters));
	}
}
