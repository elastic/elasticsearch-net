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
		IValidateQueryResponse ValidateQuery<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class;

		/// <inheritdoc />
		IValidateQueryResponse ValidateQuery(IValidateQueryRequest request);

		/// <inheritdoc />
		Task<IValidateQueryResponse> ValidateQueryAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<IValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IValidateQueryResponse ValidateQuery<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class =>
			ValidateQuery(selector?.Invoke(new ValidateQueryDescriptor<T>()));

		/// <inheritdoc />
		public IValidateQueryResponse ValidateQuery(IValidateQueryRequest request) =>
			Dispatch2<IValidateQueryRequest, ValidateQueryResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IValidateQueryResponse> ValidateQueryAsync<T>(
			Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			ValidateQueryAsync(selector?.Invoke(new ValidateQueryDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<IValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IValidateQueryRequest, IValidateQueryResponse, ValidateQueryResponse>(request, request.RequestParameters, ct);
	}
}
