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
		GetIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc />
		GetIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest request);

		/// <inheritdoc />
		Task<GetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector) =>
			GetIndexSettings(selector?.Invoke(new GetIndexSettingsDescriptor()));

		/// <inheritdoc />
		public GetIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest request) =>
			DoRequest<IGetIndexSettingsRequest, GetIndexSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetIndexSettingsResponse> GetIndexSettingsAsync(
			Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		) => GetIndexSettingsAsync(selector?.Invoke(new GetIndexSettingsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetIndexSettingsRequest, GetIndexSettingsResponse>(request, request.RequestParameters, ct);
	}
}
