/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
