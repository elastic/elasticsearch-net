using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		IGrokProcessorPatternsResponse GrokProcessorPatterns(Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null);

		/// <inheritdoc />
		IGrokProcessorPatternsResponse GrokProcessorPatterns(IGrokProcessorPatternsRequest request);

		/// <inheritdoc />
		Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(IGrokProcessorPatternsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGrokProcessorPatternsResponse GrokProcessorPatterns(Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null) =>
			GrokProcessorPatterns(selector.InvokeOrDefault(new GrokProcessorPatternsDescriptor()));

		/// <inheritdoc />
		public IGrokProcessorPatternsResponse GrokProcessorPatterns(IGrokProcessorPatternsRequest request) =>
			Dispatch2<IGrokProcessorPatternsRequest, GrokProcessorPatternsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		) => GrokProcessorPatternsAsync(selector.InvokeOrDefault(new GrokProcessorPatternsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(IGrokProcessorPatternsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGrokProcessorPatternsRequest, IGrokProcessorPatternsResponse, GrokProcessorPatternsResponse>(request, request.RequestParameters, ct);
	}
}
