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
		/// The put mapping API allows to register specific mapping definition for a specific type.
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/indices-put-mapping.html
		/// </summary>
		/// <typeparam name="T">The type we want to map in Elasticsearch</typeparam>
		/// <param name="selector">A descriptor to describe the mapping of our type</param>
		PutMappingResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class;

		/// <inheritdoc />
		PutMappingResponse Map(IPutMappingRequest request);

		/// <inheritdoc />
		Task<PutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<PutMappingResponse> MapAsync(IPutMappingRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutMappingResponse Map<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)
			where T : class =>
			Map(selector?.Invoke(new PutMappingDescriptor<T>()));

		/// <inheritdoc />
		public PutMappingResponse Map(IPutMappingRequest request) =>
			DoRequest<IPutMappingRequest, PutMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutMappingResponse> MapAsync<T>(
			Func<PutMappingDescriptor<T>, IPutMappingRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			MapAsync(selector?.Invoke(new PutMappingDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<PutMappingResponse> MapAsync(IPutMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutMappingRequest, PutMappingResponse>(request, request.RequestParameters, ct);
	}
}
