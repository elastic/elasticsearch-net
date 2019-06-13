using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DeletePipelineResponse DeletePipeline(this IElasticClient client,Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null);

		/// <inheritdoc />
		public static DeletePipelineResponse DeletePipeline(this IElasticClient client,IDeletePipelineRequest request);

		/// <inheritdoc />
		public static Task<DeletePipelineResponse> DeletePipelineAsync(this IElasticClient client,Id id, Func<DeletePipelineDescriptor, IDeletePipelineRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeletePipelineResponse> DeletePipelineAsync(this IElasticClient client,IDeletePipelineRequest request,
			CancellationToken ct = default
		);
	}

}
