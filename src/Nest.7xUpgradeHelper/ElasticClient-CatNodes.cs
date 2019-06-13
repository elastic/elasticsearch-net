using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatNodesRecord> CatNodes(this IElasticClient client,Func<CatNodesDescriptor, ICatNodesRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatNodesRecord> CatNodes(this IElasticClient client,ICatNodesRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatNodesRecord>> CatNodesAsync(this IElasticClient client,Func<CatNodesDescriptor, ICatNodesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatNodesRecord>> CatNodesAsync(this IElasticClient client,ICatNodesRequest request, CancellationToken ct = default);
	}

}
