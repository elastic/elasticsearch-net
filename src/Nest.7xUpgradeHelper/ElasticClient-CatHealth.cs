using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatHealthRecord> CatHealth(this IElasticClient client,Func<CatHealthDescriptor, ICatHealthRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatHealthRecord> CatHealth(this IElasticClient client,ICatHealthRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatHealthRecord>> CatHealthAsync(this IElasticClient client,
			Func<CatHealthDescriptor, ICatHealthRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatHealthRecord>> CatHealthAsync(this IElasticClient client,ICatHealthRequest request, CancellationToken ct = default
		);
	}
}
