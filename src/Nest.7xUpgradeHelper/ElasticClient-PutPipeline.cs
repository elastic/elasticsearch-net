using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutPipelineResponse PutPipeline(this IElasticClient client, Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector)
			=> client.Ingest.PutPipeline(id, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutPipelineResponse PutPipeline(this IElasticClient client, IPutPipelineRequest request)
			=> client.Ingest.PutPipeline(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutPipelineResponse> PutPipelineAsync(this IElasticClient client, Id id,
			Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken ct = default
		)
			=> client.Ingest.PutPipelineAsync(id, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutPipelineResponse> PutPipelineAsync(this IElasticClient client, IPutPipelineRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.PutPipelineAsync(request, ct);
	}
}
