using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatTemplatesRecord> CatTemplates(this IElasticClient client,Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatTemplatesRecord> CatTemplates(this IElasticClient client,ICatTemplatesRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatTemplatesRecord>> CatTemplatesAsync(this IElasticClient client,
			Func<CatTemplatesDescriptor, ICatTemplatesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatTemplatesRecord>> CatTemplatesAsync(this IElasticClient client,ICatTemplatesRequest request,
			CancellationToken ct = default
		);
	}

}
