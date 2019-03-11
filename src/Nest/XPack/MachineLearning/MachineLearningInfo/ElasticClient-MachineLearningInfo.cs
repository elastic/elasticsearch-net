using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Returns defaults and limits used by machine learning.
		/// </summary>
		IMachineLearningInfoResponse MachineLearningInfo(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		IMachineLearningInfoResponse MachineLearningInfo(IMachineLearningInfoRequest request);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
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
				(p, d) => LowLevelDispatch.MlInfoDispatch<MachineLearningInfoResponse>(p)
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
				(p, d, c) => LowLevelDispatch.MlInfoDispatchAsync<MachineLearningInfoResponse>(p, c)
			);
	}
}
