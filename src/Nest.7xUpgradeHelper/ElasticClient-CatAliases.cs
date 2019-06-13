using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatAliasesRecord> CatAliases(this IElasticClient client,Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatAliasesRecord> CatAliases(this IElasticClient client,ICatAliasesRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(this IElasticClient client,Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(this IElasticClient client,ICatAliasesRequest request,
			CancellationToken ct = default
		);
	}
}
