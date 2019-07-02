using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Aliases(), please update this usage.")]
		public static CatResponse<CatAliasesRecord> CatAliases(this IElasticClient client,
			Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null
		)
			=> client.Cat.Aliases(selector);

		[Obsolete("Moved to client.Cat.Aliases(), please update this usage.")]
		public static CatResponse<CatAliasesRecord> CatAliases(this IElasticClient client, ICatAliasesRequest request)
			=> client.Cat.Aliases(request);

		[Obsolete("Moved to client.Cat.AliasesAsync(), please update this usage.")]
		public static Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(this IElasticClient client,
			Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.AliasesAsync(selector, ct);

		[Obsolete("Moved to client.Cat.AliasesAsync(), please update this usage.")]
		public static Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(this IElasticClient client, ICatAliasesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.AliasesAsync(request, ct);
	}
}
