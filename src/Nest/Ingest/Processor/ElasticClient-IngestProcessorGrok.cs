using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using NodesHotThreadConverter = Func<IApiCallDetails, Stream, NodesHotThreadsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieving which patterns which the grok processor is packaged with, useful as different versions are bundled with different processors.
		/// <para> </para>https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the endpoint usage operation</param>
		IIngestProcessorGrokResponse IngestProcessorGrok(Func<IngestProcessorGrokDescriptor, IIngestProcessorGrokRequest> selector = null);

		/// <inheritdoc/>
		IIngestProcessorGrokResponse IngestProcessorGrok(IIngestProcessorGrokRequest request);

		/// <inheritdoc/>
		Task<IIngestProcessorGrokResponse> IngestProcessorGrokAsync(Func<IngestProcessorGrokDescriptor, IIngestProcessorGrokRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IIngestProcessorGrokResponse> IngestProcessorGrokAsync(IIngestProcessorGrokRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIngestProcessorGrokResponse IngestProcessorGrok(Func<IngestProcessorGrokDescriptor, IIngestProcessorGrokRequest> selector = null) =>
			this.IngestProcessorGrok(selector.InvokeOrDefault(new IngestProcessorGrokDescriptor()));

		/// <inheritdoc/>
		public IIngestProcessorGrokResponse IngestProcessorGrok(IIngestProcessorGrokRequest request) =>
			this.Dispatcher.Dispatch<IIngestProcessorGrokRequest, IngestProcessorGrokRequestParameters, IngestProcessorGrokResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IngestProcessorGrokDispatch<IngestProcessorGrokResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIngestProcessorGrokResponse> IngestProcessorGrokAsync(Func<IngestProcessorGrokDescriptor, IIngestProcessorGrokRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.IngestProcessorGrokAsync(selector.InvokeOrDefault(new IngestProcessorGrokDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IIngestProcessorGrokResponse> IngestProcessorGrokAsync(IIngestProcessorGrokRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IIngestProcessorGrokRequest, IngestProcessorGrokRequestParameters, IngestProcessorGrokResponse, IIngestProcessorGrokResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IngestProcessorGrokDispatchAsync<IngestProcessorGrokResponse>(p, c)
			);
	}
}
