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

namespace Elasticsearch.Net
{
	// Introduced for BWC for body introduced in https://github.com/elastic/elasticsearch/pull/57276
	public partial class ElasticLowLevelClient
	{
		///<summary>POST on /_field_caps <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/search-field-caps.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse FieldCapabilities<TResponse>(FieldCapabilitiesRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, "_field_caps", null, RequestParams(requestParameters));
		///<summary>POST on /_field_caps <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/search-field-caps.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public Task<TResponse> FieldCapabilitiesAsync<TResponse>(FieldCapabilitiesRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, "_field_caps", ctx, null, RequestParams(requestParameters));
		///<summary>POST on /{index}/_field_caps <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/search-field-caps.html</para></summary>
		///<param name = "index">A comma-separated list of index names; use the special string `_all` or Indices.All to perform the operation on all indices</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse FieldCapabilities<TResponse>(string index, FieldCapabilitiesRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new() => DoRequest<TResponse>(POST, Url($"{index:index}/_field_caps"), null, RequestParams(requestParameters));
		///<summary>POST on /{index}/_field_caps <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/search-field-caps.html</para></summary>
		///<param name = "index">A comma-separated list of index names; use the special string `_all` or Indices.All to perform the operation on all indices</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public Task<TResponse> FieldCapabilitiesAsync<TResponse>(string index, FieldCapabilitiesRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, IElasticsearchResponse, new() => DoRequestAsync<TResponse>(POST, Url($"{index:index}/_field_caps"), ctx, null, RequestParams(requestParameters));
	}
}
