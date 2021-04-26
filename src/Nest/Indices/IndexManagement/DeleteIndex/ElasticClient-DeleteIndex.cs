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
		/// The delete index API allows to delete an existing index.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		DeleteIndexResponse DeleteIndex(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null);

		/// <inheritdoc />
		DeleteIndexResponse DeleteIndex(IDeleteIndexRequest request);

		/// <inheritdoc />
		Task<DeleteIndexResponse> DeleteIndexAsync(
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteIndexResponse DeleteIndex(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null) =>
			DeleteIndex(selector.InvokeOrDefault(new DeleteIndexDescriptor(indices)));

		/// <inheritdoc />
		public DeleteIndexResponse DeleteIndex(IDeleteIndexRequest request) =>
			DoRequest<IDeleteIndexRequest, DeleteIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteIndexResponse> DeleteIndexAsync(
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		) => DeleteIndexAsync(selector.InvokeOrDefault(new DeleteIndexDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<DeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteIndexRequest, DeleteIndexResponse>(request, request.RequestParameters, ct);
	}
}
