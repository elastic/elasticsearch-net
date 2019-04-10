using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The get settings API allows to retrieve settings of index/indices.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-settings.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get index settings operation</param>
		IGetIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc />
		IGetIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest request);

		/// <inheritdoc />
		Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector) =>
			GetIndexSettings(selector?.Invoke(new GetIndexSettingsDescriptor()));

		/// <inheritdoc />
		public IGetIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest request) =>
			DoRequest<IGetIndexSettingsRequest, GetIndexSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(
			Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		) => GetIndexSettingsAsync(selector?.Invoke(new GetIndexSettingsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetIndexSettingsRequest, IGetIndexSettingsResponse, GetIndexSettingsResponse>(request, request.RequestParameters, ct);
	}
}
