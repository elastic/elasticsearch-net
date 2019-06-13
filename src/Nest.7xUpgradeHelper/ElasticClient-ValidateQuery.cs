using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static ValidateQueryResponse ValidateQuery<T>(this IElasticClient client,Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		public static ValidateQueryResponse ValidateQuery(this IElasticClient client,IValidateQueryRequest request);

		/// <inheritdoc />
		public static Task<ValidateQueryResponse> ValidateQueryAsync<T>(this IElasticClient client,Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<ValidateQueryResponse> ValidateQueryAsync(this IElasticClient client,IValidateQueryRequest request,
			CancellationToken ct = default
		);
	}

}
