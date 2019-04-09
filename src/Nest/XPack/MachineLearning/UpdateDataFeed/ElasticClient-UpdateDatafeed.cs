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
		IUpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		IUpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request);

		/// <inheritdoc />
		Task<IUpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		Task<IUpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class =>
			UpdateDatafeed(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)));

		/// <inheritdoc />
		public IUpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request) =>
			Dispatch2<IUpdateDatafeedRequest, UpdateDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IUpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			where T : class =>
			UpdateDatafeedAsync(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)), ct);

		/// <inheritdoc />
		public Task<IUpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IUpdateDatafeedRequest, IUpdateDatafeedResponse, UpdateDatafeedResponse>
				(request, request.RequestParameters, ct);
	}
}
