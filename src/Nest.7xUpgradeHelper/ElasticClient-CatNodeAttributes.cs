using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatNodeAttributesRecord> CatNodeAttributes(this IElasticClient client,Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatNodeAttributesRecord> CatNodeAttributes(this IElasticClient client,ICatNodeAttributesRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(this IElasticClient client,
			Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(this IElasticClient client,ICatNodeAttributesRequest request,
			CancellationToken ct = default
		);
	}
}
