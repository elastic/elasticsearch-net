using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-validate.html
		/// </summary>
		/// <typeparam name="T">The type used to describe the query</typeparam>
		/// <param name="selector">A descriptor that describes the query operation</param>
		ValidateQueryResponse ValidateQuery<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		ValidateQueryResponse ValidateQuery(IValidateQueryRequest request);

		/// <inheritdoc />
		Task<ValidateQueryResponse> ValidateQueryAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<ValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ValidateQueryResponse ValidateQuery<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class =>
			ValidateQuery(selector?.Invoke(new ValidateQueryDescriptor<T>()));

		/// <inheritdoc />
		public ValidateQueryResponse ValidateQuery(IValidateQueryRequest request) =>
			DoRequest<IValidateQueryRequest, ValidateQueryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ValidateQueryResponse> ValidateQueryAsync<T>(
			Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			ValidateQueryAsync(selector?.Invoke(new ValidateQueryDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<ValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IValidateQueryRequest, ValidateQueryResponse, ValidateQueryResponse>(request, request.RequestParameters, ct);
	}
}
