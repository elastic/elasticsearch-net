using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatSegmentsRecord> CatSegments(this IElasticClient client,Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatSegmentsRecord> CatSegments(this IElasticClient client,ICatSegmentsRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatSegmentsRecord>> CatSegmentsAsync(this IElasticClient client,
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatSegmentsRecord>> CatSegmentsAsync(this IElasticClient client,ICatSegmentsRequest request,
			CancellationToken ct = default
		);
	}

}
