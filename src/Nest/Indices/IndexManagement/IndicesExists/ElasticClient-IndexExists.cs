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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	using IndexExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Used to check if the index (indices) exists or not.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-exists.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the index exist operation</param>
		ExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null);

		/// <inheritdoc />
		ExistsResponse IndexExists(IIndexExistsRequest request);

		/// <inheritdoc />
		Task<ExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ExistsResponse IndexExists(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null) =>
			IndexExists(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)));

		/// <inheritdoc />
		public ExistsResponse IndexExists(IIndexExistsRequest request) =>
			DoRequest<IIndexExistsRequest, ExistsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ExistsResponse> IndexExistsAsync(
			Indices indices,
			Func<IndexExistsDescriptor, IIndexExistsRequest> selector = null,
			CancellationToken ct = default
		) => IndexExistsAsync(selector.InvokeOrDefault(new IndexExistsDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<ExistsResponse> IndexExistsAsync(IIndexExistsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IIndexExistsRequest, ExistsResponse>(request, request.RequestParameters, ct);
	}
}
