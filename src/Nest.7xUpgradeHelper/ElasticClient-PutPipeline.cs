using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static PutPipelineResponse PutPipeline(this IElasticClient client,Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector);

		/// <inheritdoc />
		public static PutPipelineResponse PutPipeline(this IElasticClient client,IPutPipelineRequest request);

		/// <inheritdoc />
		public static Task<PutPipelineResponse> PutPipelineAsync(this IElasticClient client,Id id, Func<PutPipelineDescriptor, IPutPipelineRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PutPipelineResponse> PutPipelineAsync(this IElasticClient client,IPutPipelineRequest request, CancellationToken ct = default);
	}

}
