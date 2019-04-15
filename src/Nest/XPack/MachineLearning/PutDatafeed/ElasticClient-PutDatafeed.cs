using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a machine learning datafeed.
		/// You must create a job before you create a datafeed. You can associate only one datafeed to each job.
		/// </summary>
		PutDatafeedResponse PutDatafeed<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null) where T : class;

		/// <inheritdoc />
		PutDatafeedResponse PutDatafeed(IPutDatafeedRequest request);

		/// <inheritdoc />
		Task<PutDatafeedResponse> PutDatafeedAsync<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default
		) where T : class;

		/// <inheritdoc />
		Task<PutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutDatafeedResponse PutDatafeed<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null)
			where T : class =>
			PutDatafeed(selector.InvokeOrDefault(new PutDatafeedDescriptor<T>(datafeedId)));

		/// <inheritdoc />
		public PutDatafeedResponse PutDatafeed(IPutDatafeedRequest request) =>
			DoRequest<IPutDatafeedRequest, PutDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutDatafeedResponse> PutDatafeedAsync<T>(
			Id datafeedId,
			Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default
		)
			where T : class =>
			PutDatafeedAsync(selector.InvokeOrDefault(new PutDatafeedDescriptor<T>(datafeedId)), cancellationToken);

		/// <inheritdoc />
		public Task<PutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutDatafeedRequest, PutDatafeedResponse, PutDatafeedResponse>(request, request.RequestParameters, ct);
	}
}
