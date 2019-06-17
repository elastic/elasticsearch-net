using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Ingest.SimulatePipeline(), please update this usage.")]
		public static SimulatePipelineResponse SimulatePipeline(this IElasticClient client,
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector
		)
			=> client.Ingest.SimulatePipeline(selector);

		[Obsolete("Moved to client.Ingest.SimulatePipeline(), please update this usage.")]
		public static SimulatePipelineResponse SimulatePipeline(this IElasticClient client, ISimulatePipelineRequest request)
			=> client.Ingest.SimulatePipeline(request);

		[Obsolete("Moved to client.Ingest.SimulatePipelineAsync(), please update this usage.")]
		public static Task<SimulatePipelineResponse> SimulatePipelineAsync(this IElasticClient client,
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		)
			=> client.Ingest.SimulatePipelineAsync(selector, ct);

		[Obsolete("Moved to client.Ingest.SimulatePipelineAsync(), please update this usage.")]
		public static Task<SimulatePipelineResponse> SimulatePipelineAsync(this IElasticClient client, ISimulatePipelineRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.SimulatePipelineAsync(request, ct);
	}
}
