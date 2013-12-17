using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, BaseQuery> querySelector) 
			where T : class
		{
			var bq = querySelector(new ValidateQueryDescriptor<T>());
			var descriptor = bq as ValidateQueryDescriptor<T>;
			
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesValidateQueryDispatch(pathInfo, bq)
				.Deserialize<ValidateResponse>();
		}

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, BaseQuery> querySelector) where T : class
		{
			var bq = querySelector(new ValidateQueryDescriptor<T>());
			var descriptor = bq as ValidateQueryDescriptor<T>;
			
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesValidateQueryDispatchAsync(pathInfo, bq)
				.ContinueWith(r => r.Result.Deserialize<ValidateResponse>() as IValidateResponse);
		}

	}
}
