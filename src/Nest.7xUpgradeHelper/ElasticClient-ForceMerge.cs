using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		public static ForceMergeResponse ForceMerge(this IElasticClient client,Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc />
		public static ForceMergeResponse ForceMerge(this IElasticClient client,IForceMergeRequest request);

		/// <inheritdoc />
		public static Task<ForceMergeResponse> ForceMergeAsync(this IElasticClient client,Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ForceMergeResponse> ForceMergeAsync(this IElasticClient client,IForceMergeRequest request, CancellationToken ct = default);
	}

}
