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
		/// Retrieving which patterns the grok processor is packaged with, useful as different versions are bundled with different processors.
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the endpoint usage operation</param>
		GrokProcessorPatternsResponse GrokProcessorPatterns(Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null);

		/// <inheritdoc />
		GrokProcessorPatternsResponse GrokProcessorPatterns(IGrokProcessorPatternsRequest request);

		/// <inheritdoc />
		Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(IGrokProcessorPatternsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GrokProcessorPatternsResponse GrokProcessorPatterns(Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null) =>
			GrokProcessorPatterns(selector.InvokeOrDefault(new GrokProcessorPatternsDescriptor()));

		/// <inheritdoc />
		public GrokProcessorPatternsResponse GrokProcessorPatterns(IGrokProcessorPatternsRequest request) =>
			DoRequest<IGrokProcessorPatternsRequest, GrokProcessorPatternsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		) => GrokProcessorPatternsAsync(selector.InvokeOrDefault(new GrokProcessorPatternsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(IGrokProcessorPatternsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGrokProcessorPatternsRequest, GrokProcessorPatternsResponse>(request, request.RequestParameters, ct);
	}
}
