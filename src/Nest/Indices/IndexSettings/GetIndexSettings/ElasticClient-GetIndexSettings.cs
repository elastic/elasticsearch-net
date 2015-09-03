using System;
using System.Linq;
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
		IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc/>
		IIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest getIndexSettingsRequest);

		/// <inheritdoc/>
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc/>
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest getIndexSettingsRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector) =>
			this.GetIndexSettings(selector?.Invoke(new GetIndexSettingsDescriptor()));

		/// <inheritdoc/>
		public IIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest getIndexSettingsRequest) => 
			this.Dispatcher.Dispatch<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, IndexSettingsResponse>(
				getIndexSettingsRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetSettingsDispatch<IndexSettingsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector) =>
			this.GetIndexSettingsAsync(selector?.Invoke(new GetIndexSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest getIndexSettingsRequest) => 
			this.Dispatcher.DispatchAsync<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, IndexSettingsResponse, IIndexSettingsResponse>(
				getIndexSettingsRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetSettingsDispatchAsync<IndexSettingsResponse>(p)
			);
	}
}