using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetPipelineResponse GetPipeline(this IElasticClient client, Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null)
			=> client.Ingest.GetPipeline(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetPipelineResponse GetPipeline(this IElasticClient client, IGetPipelineRequest request)
			=> client.Ingest.GetPipeline(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetPipelineResponse> GetPipelineAsync(this IElasticClient client,
			Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Ingest.GetPipelineAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetPipelineResponse> GetPipelineAsync(this IElasticClient client, IGetPipelineRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.GetPipelineAsync(request, ct);
	}
}
