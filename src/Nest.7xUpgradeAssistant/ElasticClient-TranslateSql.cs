using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Sql.Translate(), please update this usage.")]
		public static TranslateSqlResponse TranslateSql(this IElasticClient client, Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null
		)
			=> client.Sql.Translate(selector);

		[Obsolete("Moved to client.Sql.Translate(), please update this usage.")]
		public static TranslateSqlResponse TranslateSql(this IElasticClient client, ITranslateSqlRequest request)
			=> client.Sql.Translate(request);

		[Obsolete("Moved to client.Sql.TranslateAsync(), please update this usage.")]
		public static Task<TranslateSqlResponse> TranslateSqlAsync(this IElasticClient client,
			Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Sql.TranslateAsync(selector, ct);

		[Obsolete("Moved to client.Sql.TranslateAsync(), please update this usage.")]
		public static Task<TranslateSqlResponse> TranslateSqlAsync(this IElasticClient client, ITranslateSqlRequest request,
			CancellationToken ct = default
		)
			=> client.Sql.TranslateAsync(request, ct);
	}
}
