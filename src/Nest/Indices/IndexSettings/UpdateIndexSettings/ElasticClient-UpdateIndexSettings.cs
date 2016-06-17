using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Change specific index level settings in real time. Note not all index settings CAN be updated.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that strongly types all the updateable settings</param>
		IUpdateIndexSettingsResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector);

		/// <inheritdoc/>
		IUpdateIndexSettingsResponse UpdateIndexSettings(IUpdateIndexSettingsRequest request);

		/// <inheritdoc/>
		Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(
			Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateIndexSettingsResponse UpdateIndexSettings(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector) =>
			this.UpdateIndexSettings(selector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IUpdateIndexSettingsResponse UpdateIndexSettings(IUpdateIndexSettingsRequest request) =>
			this.Dispatcher.Dispatch<IUpdateIndexSettingsRequest, UpdateIndexSettingsRequestParameters, UpdateIndexSettingsResponse>(
				request,
				this.LowLevelDispatch.IndicesPutSettingsDispatch<UpdateIndexSettingsResponse>
			);

		/// <inheritdoc/>
		public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(
			Indices indices,
			Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.UpdateIndexSettingsAsync(selector.InvokeOrDefault(new UpdateIndexSettingsDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IUpdateIndexSettingsRequest, UpdateIndexSettingsRequestParameters, UpdateIndexSettingsResponse, IUpdateIndexSettingsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.IndicesPutSettingsDispatchAsync<UpdateIndexSettingsResponse>
			);
	}
}
