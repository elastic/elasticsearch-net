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
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc />
		GetIndexResponse GetIndex(IGetIndexRequest request);

		/// <inheritdoc />
		Task<GetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetIndexResponse GetIndex(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null) =>
			GetIndex(selector.InvokeOrDefault(new GetIndexDescriptor(indices)));

		/// <inheritdoc />
		public GetIndexResponse GetIndex(IGetIndexRequest request) =>
			DoRequest<IGetIndexRequest, GetIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetIndexResponse> GetIndexAsync(
			Indices indices,
			Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetIndexAsync(selector.InvokeOrDefault(new GetIndexDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<GetIndexResponse> GetIndexAsync(IGetIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetIndexRequest, GetIndexResponse>(request, request.RequestParameters, ct);
	}
}
