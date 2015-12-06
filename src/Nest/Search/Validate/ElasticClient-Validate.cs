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
		/// <param name="selector">A descriptor that describes the query operation</param>
		IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IValidateResponse Validate(IValidateQueryRequest request);

		/// <inheritdoc/>
		Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IValidateResponse> ValidateAsync(IValidateQueryRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class =>
			this.Validate(selector?.Invoke(new ValidateQueryDescriptor<T>()));

		/// <inheritdoc/>
		public IValidateResponse Validate(IValidateQueryRequest request) => 
			this.Dispatcher.Dispatch<IValidateQueryRequest, ValidateQueryRequestParameters, ValidateResponse>(
				request,
				this.LowLevelDispatch.IndicesValidateQueryDispatch<ValidateResponse>
			);

		/// <inheritdoc/>
		public Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)
			where T : class => 
			this.ValidateAsync(selector?.Invoke(new ValidateQueryDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IValidateResponse> ValidateAsync(IValidateQueryRequest request) => 
			this.Dispatcher.DispatchAsync<IValidateQueryRequest, ValidateQueryRequestParameters, ValidateResponse, IValidateResponse>(
				request,
				this.LowLevelDispatch.IndicesValidateQueryDispatchAsync<ValidateResponse>
			);
	}
}