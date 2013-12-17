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
		public IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector) 
			where T : class
		{
			querySelector.ThrowIfNull("querySelector");
			var descriptor = querySelector(new ValidateQueryDescriptor<T>());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesValidateQueryDispatch(pathInfo, descriptor)
				.Deserialize<ValidateResponse>();
		}

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector) 
			where T : class
		{
			querySelector.ThrowIfNull("querySelector");
			var descriptor = querySelector(new ValidateQueryDescriptor<T>());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesValidateQueryDispatchAsync(pathInfo, descriptor)
				.ContinueWith(r => r.Result.Deserialize<ValidateResponse>() as IValidateResponse);
		}

	}
}
