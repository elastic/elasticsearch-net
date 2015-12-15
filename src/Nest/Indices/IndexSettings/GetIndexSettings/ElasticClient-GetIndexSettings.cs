using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc/>
		Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request);
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
		public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector) =>
			this.GetIndexSettingsAsync(selector?.Invoke(new GetIndexSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request) => 
			this.Dispatcher.DispatchAsync<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, GetIndexSettingsResponse, IGetIndexSettingsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesGetSettingsDispatchAsync<GetIndexSettingsResponse>(p)
			);
	}
}