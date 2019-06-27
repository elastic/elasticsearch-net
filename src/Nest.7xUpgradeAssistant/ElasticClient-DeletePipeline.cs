using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Ingest.DeletePipeline(), please update this usage.")]
		public static DeletePipelineResponse DeletePipeline(this IElasticClient client, Id id,
			Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null
		)
			=> client.Ingest.DeletePipeline(id, selector);

		[Obsolete("Moved to client.Ingest.DeletePipeline(), please update this usage.")]
		public static DeletePipelineResponse DeletePipeline(this IElasticClient client, IDeletePipelineRequest request)
			=> client.Ingest.DeletePipeline(request);

		[Obsolete("Moved to client.Ingest.DeletePipelineAsync(), please update this usage.")]
		public static Task<DeletePipelineResponse> DeletePipelineAsync(this IElasticClient client, Id id,
			Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Ingest.DeletePipelineAsync(id, selector, ct);

		[Obsolete("Moved to client.Ingest.DeletePipelineAsync(), please update this usage.")]
		public static Task<DeletePipelineResponse> DeletePipelineAsync(this IElasticClient client, IDeletePipelineRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.DeletePipelineAsync(request, ct);
	}
}
