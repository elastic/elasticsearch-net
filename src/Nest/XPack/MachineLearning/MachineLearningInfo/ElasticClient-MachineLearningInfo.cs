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
			CancellationToken ct = default
		);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(IMachineLearningInfoRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMachineLearningInfoResponse MachineLearningInfo(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null) =>
			MachineLearningInfo(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()));

		/// <inheritdoc />
		public IMachineLearningInfoResponse MachineLearningInfo(IMachineLearningInfoRequest request) =>
			DoRequest<IMachineLearningInfoRequest, MachineLearningInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken ct = default
		) => MachineLearningInfoAsync(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<IMachineLearningInfoResponse> MachineLearningInfoAsync(IMachineLearningInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IMachineLearningInfoRequest, IMachineLearningInfoResponse, MachineLearningInfoResponse>(request, request.RequestParameters, ct);
	}
}
