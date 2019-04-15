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
		MachineLearningInfoResponse MachineLearningInfo(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		MachineLearningInfoResponse MachineLearningInfo(IMachineLearningInfoRequest request);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		Task<MachineLearningInfoResponse> MachineLearningInfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		Task<MachineLearningInfoResponse> MachineLearningInfoAsync(IMachineLearningInfoRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public MachineLearningInfoResponse MachineLearningInfo(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null) =>
			MachineLearningInfo(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()));

		/// <inheritdoc />
		public MachineLearningInfoResponse MachineLearningInfo(IMachineLearningInfoRequest request) =>
			DoRequest<IMachineLearningInfoRequest, MachineLearningInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<MachineLearningInfoResponse> MachineLearningInfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken ct = default
		) => MachineLearningInfoAsync(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<MachineLearningInfoResponse> MachineLearningInfoAsync(IMachineLearningInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IMachineLearningInfoRequest, MachineLearningInfoResponse>(request, request.RequestParameters, ct);
	}
}
