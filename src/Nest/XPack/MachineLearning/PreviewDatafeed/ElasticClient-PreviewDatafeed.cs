using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Preview a Machine Learning datafeed.
		/// This preview shows the structure of the data that will be passed to the anomaly detection engine.
		/// </summary>
		IPreviewDatafeedResponse<T> PreviewDatafeed<T>(Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IPreviewDatafeedResponse<T> PreviewDatafeed<T>(IPreviewDatafeedRequest request);

		/// <inheritdoc/>
		Task<IPreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(IPreviewDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPreviewDatafeedResponse<T> PreviewDatafeed<T>(Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null) =>
			this.PreviewDatafeed<T>(selector.InvokeOrDefault(new PreviewDatafeedDescriptor(datafeedId)));

		/// <inheritdoc/>
		public IPreviewDatafeedResponse<T> PreviewDatafeed<T>(IPreviewDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IPreviewDatafeedRequest, PreviewDatafeedRequestParameters, PreviewDatafeedResponse<T>>(
				request,
				this.PreviewDatafeedResponse<T>,
				(p, d) => this.LowLevelDispatch.XpackMlPreviewDatafeedDispatch<PreviewDatafeedResponse<T>>(p)
			);

		/// <inheritdoc/>
		public Task<IPreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PreviewDatafeedAsync<T>(selector.InvokeOrDefault(new PreviewDatafeedDescriptor(datafeedId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(IPreviewDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPreviewDatafeedRequest, PreviewDatafeedRequestParameters, PreviewDatafeedResponse<T>, IPreviewDatafeedResponse<T>>(
				request,
				cancellationToken,
				this.PreviewDatafeedResponse<T>,
				(p, d, c) => this.LowLevelDispatch.XpackMlPreviewDatafeedDispatchAsync<PreviewDatafeedResponse<T>>(p, c)
			);

		private PreviewDatafeedResponse<T> PreviewDatafeedResponse<T>(IApiCallDetails response, Stream stream) => response.Success
			? new PreviewDatafeedResponse<T> { Data = this.ConnectionSettings.Serializer.Deserialize<IReadOnlyCollection<T>>(stream) }
			: new PreviewDatafeedResponse<T>();
	}
}
