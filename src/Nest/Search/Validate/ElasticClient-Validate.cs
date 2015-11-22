using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-validate.html
		/// </summary>
		/// <typeparam name="T">The type used to describe the query</typeparam>
		/// <param name="querySelector">A descriptor that describes the query operation</param>
		IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> querySelector)
			where T : class;

		/// <inheritdoc/>
		IValidateResponse Validate(IValidateQueryRequest validateQueryRequest);

		/// <inheritdoc/>
		Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> querySelector)
			where T : class;

		/// <inheritdoc/>
		Task<IValidateResponse> ValidateAsync(IValidateQueryRequest validateQueryRequest);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> querySelector)
			where T : class =>
			this.Validate(querySelector?.Invoke(new ValidateQueryDescriptor<T>()));

		/// <inheritdoc/>
		public IValidateResponse Validate(IValidateQueryRequest validateQueryRequest) => 
			this.Dispatcher.Dispatch<IValidateQueryRequest, ValidateQueryRequestParameters, ValidateResponse>(
				validateQueryRequest,
				this.LowLevelDispatch.IndicesValidateQueryDispatch<ValidateResponse>
			);

		/// <inheritdoc/>
		public Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> querySelector)
			where T : class => 
			this.ValidateAsync(querySelector?.Invoke(new ValidateQueryDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IValidateResponse> ValidateAsync(IValidateQueryRequest validateQueryRequest) => 
			this.Dispatcher.DispatchAsync<IValidateQueryRequest, ValidateQueryRequestParameters, ValidateResponse, IValidateResponse>(
				validateQueryRequest,
				this.LowLevelDispatch.IndicesValidateQueryDispatchAsync<ValidateResponse>
			);
	}
}