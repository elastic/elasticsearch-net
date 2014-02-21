using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector) 
			where T : class
		{
			return this.Dispatch<ValidateQueryDescriptor<T>, ValidateQueryQueryString, ValidateResponse>(
				querySelector,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatch(p, d)
			);
		}

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector) 
			where T : class
		{
			return this.DispatchAsync<ValidateQueryDescriptor<T>, ValidateQueryQueryString, ValidateResponse, IValidateResponse>(
				querySelector,
				(p, d) => this.RawDispatch.IndicesValidateQueryDispatchAsync(p, d)
			);
		}

	}
}
