using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.ValidateQuery(), please update this usage.")]
		public static ValidateQueryResponse ValidateQuery<T>(this IElasticClient client,
			Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector
		)
			where T : class
			=> client.Indices.ValidateQuery(selector);

		[Obsolete("Moved to client.Indices.ValidateQuery(), please update this usage.")]
		public static ValidateQueryResponse ValidateQuery(this IElasticClient client, IValidateQueryRequest request)
			=> client.Indices.ValidateQuery(request);

		[Obsolete("Moved to client.Indices.ValidateQueryAsync(), please update this usage.")]
		public static Task<ValidateQueryResponse> ValidateQueryAsync<T>(this IElasticClient client,
			Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class
			=> client.Indices.ValidateQueryAsync(selector, ct);

		[Obsolete("Moved to client.Indices.ValidateQueryAsync(), please update this usage.")]
		public static Task<ValidateQueryResponse> ValidateQueryAsync(this IElasticClient client, IValidateQueryRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.ValidateQueryAsync(request, ct);
	}
}
