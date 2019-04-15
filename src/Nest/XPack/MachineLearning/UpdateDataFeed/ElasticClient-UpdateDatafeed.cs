using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Update a machine learning datafeed.
		/// </summary>
		UpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		UpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request);

		/// <inheritdoc />
		Task<UpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<UpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class =>
			UpdateDatafeed(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)));

		/// <inheritdoc />
		public UpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request) =>
			DoRequest<IUpdateDatafeedRequest, UpdateDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<UpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			UpdateDatafeedAsync(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)), ct);

		/// <inheritdoc />
		public Task<UpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpdateDatafeedRequest, UpdateDatafeedResponse, UpdateDatafeedResponse>
				(request, request.RequestParameters, ct);
	}
}
