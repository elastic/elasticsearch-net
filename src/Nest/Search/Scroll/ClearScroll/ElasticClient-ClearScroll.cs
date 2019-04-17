using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a registered scroll request on the cluster
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html
		/// </summary>
		/// <param name="selector">Specify the scroll id as well as request specific configuration</param>
		ClearScrollResponse ClearScroll(Func<ClearScrollDescriptor, IClearScrollRequest> selector);

		/// <inheritdoc />
		ClearScrollResponse ClearScroll(IClearScrollRequest request);

		/// <inheritdoc />
		Task<ClearScrollResponse> ClearScrollAsync(Func<ClearScrollDescriptor, IClearScrollRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ClearScrollResponse> ClearScrollAsync(IClearScrollRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ClearScrollResponse ClearScroll(Func<ClearScrollDescriptor, IClearScrollRequest> selector) =>
			ClearScroll(selector.InvokeOrDefault(new ClearScrollDescriptor()));

		/// <inheritdoc />
		public ClearScrollResponse ClearScroll(IClearScrollRequest request) =>
			DoRequest<IClearScrollRequest, ClearScrollResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ClearScrollResponse> ClearScrollAsync(
			Func<ClearScrollDescriptor, IClearScrollRequest> selector,
			CancellationToken ct = default
		) => ClearScrollAsync(selector.InvokeOrDefault(new ClearScrollDescriptor()), ct);

		/// <inheritdoc />
		public Task<ClearScrollResponse> ClearScrollAsync(IClearScrollRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClearScrollRequest, ClearScrollResponse>(request, request.RequestParameters, ct);
	}
}
