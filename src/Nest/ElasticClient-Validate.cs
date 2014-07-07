using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector)
			where T : class
		{
			return this.Dispatch<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, ValidateResponse>(
				querySelector,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatch<ValidateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IValidateResponse> ValidateAsync<T>(
			Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector)
			where T : class
		{
			return this.DispatchAsync<ValidateQueryDescriptor<T>, ValidateQueryRequestParameters, ValidateResponse, IValidateResponse>(
				querySelector,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatchAsync<ValidateResponse>(p, d)
			);
		}
	}
}