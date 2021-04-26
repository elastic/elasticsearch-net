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

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class NamespacedClientProxy
	{
		private readonly ElasticLowLevelClient _client;

		internal NamespacedClientProxy(ElasticLowLevelClient client) => _client = client;

		protected TResponse DoRequest<TResponse>(HttpMethod post, string url, PostData body, IRequestParameters @params)
			where TResponse : class, IElasticsearchResponse, new() =>
			_client.DoRequest<TResponse>(post, url, body, @params);

		protected Task<TResponse> DoRequestAsync<TResponse>(HttpMethod post, string url, CancellationToken ctx, PostData body, IRequestParameters @params)
			where TResponse : class, IElasticsearchResponse, new() =>
			_client.DoRequestAsync<TResponse>(post, url, ctx, body, @params);

		protected string Url(FormattableString formattable) => _client.Url(formattable);

		protected TRequestParams RequestParams<TRequestParams>(TRequestParams requestParams, string contentType)
			where TRequestParams : class, IRequestParameters, new()
			=> _client.RequestParams(requestParams, contentType ?? ContentType, contentType ?? ContentType);

		protected TRequestParams RequestParams<TRequestParams>(TRequestParams requestParams)
			where TRequestParams : class, IRequestParameters, new()
			=> _client.RequestParams(requestParams, ContentType, ContentType);

		// ReSharper disable once UnassignedGetOnlyAutoProperty intended to be overridden
		protected virtual string ContentType { get; }
	}
}
