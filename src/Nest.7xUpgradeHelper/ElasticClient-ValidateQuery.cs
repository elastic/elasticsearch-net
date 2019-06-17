using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-validate.html
		/// </summary>
		/// <typeparam name="T">The type used to describe the query</typeparam>
		/// <param name="selector">A descriptor that describes the query operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ValidateQueryResponse ValidateQuery<T>(this IElasticClient client,
			Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector
		)
			where T : class
			=> client.Indices.ValidateQuery(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ValidateQueryResponse ValidateQuery(this IElasticClient client, IValidateQueryRequest request)
			=> client.Indices.ValidateQuery(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ValidateQueryResponse> ValidateQueryAsync<T>(this IElasticClient client,
			Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class
			=> client.Indices.ValidateQueryAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ValidateQueryResponse> ValidateQueryAsync(this IElasticClient client, IValidateQueryRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.ValidateQueryAsync(request,ct);
	}
}
