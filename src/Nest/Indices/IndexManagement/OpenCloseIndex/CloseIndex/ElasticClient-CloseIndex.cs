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

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it.
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked
		/// for read/write operations.
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html</a>
		/// </summary>
		/// <param name="selector">A descriptor thata describes the close index operation</param>
		CloseIndexResponse CloseIndex(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null);

		/// <inheritdoc />
		CloseIndexResponse CloseIndex(ICloseIndexRequest request);

		/// <inheritdoc />
		Task<CloseIndexResponse> CloseIndexAsync(
			Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CloseIndexResponse CloseIndex(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null) =>
			CloseIndex(selector.InvokeOrDefault(new CloseIndexDescriptor(indices)));

		/// <inheritdoc />
		public CloseIndexResponse CloseIndex(ICloseIndexRequest request) =>
			DoRequest<ICloseIndexRequest, CloseIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<CloseIndexResponse> CloseIndexAsync(
			Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null,
			CancellationToken ct = default
		) => CloseIndexAsync(selector.InvokeOrDefault(new CloseIndexDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<CloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICloseIndexRequest, CloseIndexResponse>(request, request.RequestParameters, ct);
	}
}
