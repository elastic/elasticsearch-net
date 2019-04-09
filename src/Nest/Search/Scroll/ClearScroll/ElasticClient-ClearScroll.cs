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
		IClearScrollResponse ClearScroll(Func<ClearScrollDescriptor, IClearScrollRequest> selector);

		/// <inheritdoc />
		IClearScrollResponse ClearScroll(IClearScrollRequest request);

		/// <inheritdoc />
		Task<IClearScrollResponse> ClearScrollAsync(Func<ClearScrollDescriptor, IClearScrollRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClearScrollResponse ClearScroll(Func<ClearScrollDescriptor, IClearScrollRequest> selector) =>
			ClearScroll(selector.InvokeOrDefault(new ClearScrollDescriptor()));

		/// <inheritdoc />
		public IClearScrollResponse ClearScroll(IClearScrollRequest request) =>
			Dispatch2<IClearScrollRequest, ClearScrollResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IClearScrollResponse> ClearScrollAsync(
			Func<ClearScrollDescriptor, IClearScrollRequest> selector,
			CancellationToken ct = default
		) => ClearScrollAsync(selector.InvokeOrDefault(new ClearScrollDescriptor()), ct);

		/// <inheritdoc />
		public Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IClearScrollRequest, IClearScrollResponse, ClearScrollResponse>(request, request.RequestParameters, ct);
	}
}
