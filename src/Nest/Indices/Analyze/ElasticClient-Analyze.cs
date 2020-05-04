// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
