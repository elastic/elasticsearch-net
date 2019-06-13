using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Returns defaults and limits used by machine learning.
		/// </summary>
		public static MachineLearningInfoResponse MachineLearningInfo(this IElasticClient client,Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		public static MachineLearningInfoResponse MachineLearningInfo(this IElasticClient client,IMachineLearningInfoRequest request);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		public static Task<MachineLearningInfoResponse> MachineLearningInfoAsync(this IElasticClient client,Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})"/>
		public static Task<MachineLearningInfoResponse> MachineLearningInfoAsync(this IElasticClient client,IMachineLearningInfoRequest request,
			CancellationToken ct = default
		);
	}

}
