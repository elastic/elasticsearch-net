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
		IMachineLearningInfoResponse MachineLearningInfo(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null);

		/// <inheritdoc />
		IMachineLearningInfoResponse MachineLearningInfo(IMachineLearningInfoRequest request);

		/// <inheritdoc />
		Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(IMachineLearningInfoRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMachineLearningInfoResponse MachineLearningInfo(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null) =>
			MachineLearningInfo(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()));

		/// <inheritdoc />
		public IMachineLearningInfoResponse MachineLearningInfo(IMachineLearningInfoRequest request) =>
			Dispatcher.Dispatch<IMachineLearningInfoRequest, MachineLearningInfoRequestParameters, MachineLearningInfoResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlInfoDispatch<MachineLearningInfoResponse>(p)
			);

		/// <inheritdoc />
		public Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MachineLearningInfoAsync(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(IMachineLearningInfoRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IMachineLearningInfoRequest, MachineLearningInfoRequestParameters, MachineLearningInfoResponse, IMachineLearningInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlInfoDispatchAsync<MachineLearningInfoResponse>(p, c)
			);
	}
}
