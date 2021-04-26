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
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the analyze operation</param>
		AnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector);

		/// <inheritdoc />
		AnalyzeResponse Analyze(IAnalyzeRequest request);

		/// <inheritdoc />
		Task<AnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<AnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public AnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector) =>
			Analyze(selector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc />
		public AnalyzeResponse Analyze(IAnalyzeRequest request) =>
			DoRequest<IAnalyzeRequest, AnalyzeResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<AnalyzeResponse> AnalyzeAsync(
			Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		) => AnalyzeAsync(selector?.Invoke(new AnalyzeDescriptor()), ct);

		/// <inheritdoc />
		public Task<AnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAnalyzeRequest, AnalyzeResponse>(request, request.RequestParameters, ct);
	}
}
