using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatAliasesRecord> CatAliases(this IElasticClient client,
			Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null
		)
			=> client.Cat.Aliases(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatAliasesRecord> CatAliases(this IElasticClient client, ICatAliasesRequest request)
			=> client.Cat.Aliases(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(this IElasticClient client,
			Func<CatAliasesDescriptor, ICatAliasesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.AliasesAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatAliasesRecord>> CatAliasesAsync(this IElasticClient client, ICatAliasesRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.AliasesAsync(request, ct);
	}
}
