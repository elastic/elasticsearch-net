using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The get settings API allows to retrieve settings of index/indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-settings.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get index settings operation</param>
		IGetIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc/>
		IGetIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest request);

		/// <inheritdoc/>
		Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector) =>
			this.GetIndexSettings(selector?.Invoke(new GetIndexSettingsDescriptor()));

		/// <inheritdoc/>
		public IGetIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest request) =>
			this.Dispatcher.Dispatch<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, GetIndexSettingsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesGetSettingsDispatch<GetIndexSettingsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetIndexSettingsAsync(selector?.Invoke(new GetIndexSettingsDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, GetIndexSettingsResponse, IGetIndexSettingsResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesGetSettingsDispatchAsync<GetIndexSettingsResponse>(p, c)
			);
	}
}
