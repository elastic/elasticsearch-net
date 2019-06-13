using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatIndicesRecord> CatIndices(this IElasticClient client,Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatIndicesRecord> CatIndices(this IElasticClient client,ICatIndicesRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(this IElasticClient client,
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(this IElasticClient client,ICatIndicesRequest request,
			CancellationToken ct = default
		);
	}
}
