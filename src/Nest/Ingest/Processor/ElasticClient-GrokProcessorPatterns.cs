using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		///     Retrieving which patterns the grok processor is packaged with, useful as different versions are bundled with different processors.
		///     <para> </para>
		///     https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the endpoint usage operation</param>
		IGrokProcessorPatternsResponse GrokProcessorPatterns(Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null);

		/// <inheritdoc />
		IGrokProcessorPatternsResponse GrokProcessorPatterns(IGrokProcessorPatternsRequest request);

		/// <inheritdoc />
		Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(IGrokProcessorPatternsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGrokProcessorPatternsResponse GrokProcessorPatterns(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null
		) =>
			GrokProcessorPatterns(selector.InvokeOrDefault(new GrokProcessorPatternsDescriptor()));

		/// <inheritdoc />
		public IGrokProcessorPatternsResponse GrokProcessorPatterns(IGrokProcessorPatternsRequest request) =>
			Dispatcher.Dispatch<IGrokProcessorPatternsRequest, GrokProcessorPatternsRequestParameters, GrokProcessorPatternsResponse>(
				request,
				(p, d) => LowLevelDispatch.IngestProcessorGrokDispatch<GrokProcessorPatternsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GrokProcessorPatternsAsync(selector.InvokeOrDefault(new GrokProcessorPatternsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGrokProcessorPatternsResponse> GrokProcessorPatternsAsync(IGrokProcessorPatternsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGrokProcessorPatternsRequest, GrokProcessorPatternsRequestParameters, GrokProcessorPatternsResponse,
					IGrokProcessorPatternsResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.IngestProcessorGrokDispatchAsync<GrokProcessorPatternsResponse>(p, c)
				);
	}
}
