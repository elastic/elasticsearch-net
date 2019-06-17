using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The SQL Translate API accepts SQL in a JSON document and translates it into a native Elasticsearch search
		/// request
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static TranslateSqlResponse TranslateSql(this IElasticClient client, Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null
		)
			=> client.Sql.Translate(selector);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static TranslateSqlResponse TranslateSql(this IElasticClient client, ITranslateSqlRequest request)
			=> client.Sql.Translate(request);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<TranslateSqlResponse> TranslateSqlAsync(this IElasticClient client,
			Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Sql.TranslateAsync(selector, ct);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<TranslateSqlResponse> TranslateSqlAsync(this IElasticClient client, ITranslateSqlRequest request,
			CancellationToken ct = default
		)
			=> client.Sql.TranslateAsync(request, ct);
	}
}
