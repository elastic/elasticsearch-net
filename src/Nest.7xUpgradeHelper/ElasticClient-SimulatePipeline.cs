using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static SimulatePipelineResponse SimulatePipeline(this IElasticClient client,Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc />
		public static SimulatePipelineResponse SimulatePipeline(this IElasticClient client,ISimulatePipelineRequest request);

		/// <inheritdoc />
		public static Task<SimulatePipelineResponse> SimulatePipelineAsync(this IElasticClient client,Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<SimulatePipelineResponse> SimulatePipelineAsync(this IElasticClient client,ISimulatePipelineRequest request,
			CancellationToken ct = default
		);
	}

}
