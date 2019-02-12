using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more categories.
		/// </summary>
		IMlInfoResponse MlInfo(Func<MlInfoDescriptor, IMlInfoRequest> selector = null);

		/// <inheritdoc />
		IMlInfoResponse MlInfo(IMlInfoRequest request);

		/// <inheritdoc />
		Task<IMlInfoResponse> MlInfoAsync(Func<MlInfoDescriptor, IMlInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IMlInfoResponse> MlInfoAsync(IMlInfoRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMlInfoResponse MlInfo(Func<MlInfoDescriptor, IMlInfoRequest> selector = null) =>
			MlInfo(selector.InvokeOrDefault(new MlInfoDescriptor()));

		/// <inheritdoc />
		public IMlInfoResponse MlInfo(IMlInfoRequest request) =>
			Dispatcher.Dispatch<IMlInfoRequest, MlInfoRequestParameters, MlInfoResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlInfoDispatch<MlInfoResponse>(p)
			);

		/// <inheritdoc />
		public Task<IMlInfoResponse> MlInfoAsync(Func<MlInfoDescriptor, IMlInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MlInfoAsync(selector.InvokeOrDefault(new MlInfoDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IMlInfoResponse> MlInfoAsync(IMlInfoRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IMlInfoRequest, MlInfoRequestParameters, MlInfoResponse, IMlInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlInfoDispatchAsync<MlInfoResponse>(p, c)
			);
	}
}
