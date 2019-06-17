using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SimulatePipelineResponse SimulatePipeline(this IElasticClient client,
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector
		)
			=> client.Ingest.SimulatePipeline(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static SimulatePipelineResponse SimulatePipeline(this IElasticClient client, ISimulatePipelineRequest request)
			=> client.Ingest.SimulatePipeline(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SimulatePipelineResponse> SimulatePipelineAsync(this IElasticClient client,
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		)
			=> client.Ingest.SimulatePipelineAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<SimulatePipelineResponse> SimulatePipelineAsync(this IElasticClient client, ISimulatePipelineRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.SimulatePipelineAsync(request, ct);
	}
}
