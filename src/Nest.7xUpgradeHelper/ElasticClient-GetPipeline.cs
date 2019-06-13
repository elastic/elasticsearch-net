using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetPipelineResponse GetPipeline(this IElasticClient client,Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null);

		/// <inheritdoc />
		public static GetPipelineResponse GetPipeline(this IElasticClient client,IGetPipelineRequest request);

		/// <inheritdoc />
		public static Task<GetPipelineResponse> GetPipelineAsync(this IElasticClient client,Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetPipelineResponse> GetPipelineAsync(this IElasticClient client,IGetPipelineRequest request, CancellationToken ct = default);
	}


}
