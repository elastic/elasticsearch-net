using System;
using System.Collections.Generic;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public IValidateResponse Validate(Action<ValidateQueryPathDescriptor> querySelector)
		{
			var descriptor = new ValidateQueryPathDescriptor();
			querySelector(descriptor);
			var stringQuery = this.Serialize(descriptor);
			var path = this.PathResolver.GetPathForTyped(descriptor, "_validate/query");
			if (descriptor._QueryStringQuery.IsNullOrEmpty())
				return this._Validate(path, stringQuery);
			return this._ValidateQueryString(path);
		}

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it. 
		/// </summary>
		public IValidateResponse Validate<T>(Action<ValidateQueryPathDescriptor<T>> querySelector) where T : class
		{
			var descriptor = new ValidateQueryPathDescriptor<T>();
			querySelector(descriptor);
			var stringQuery = this.Serialize(descriptor);
			var path = this.PathResolver.GetPathForTyped(descriptor, "_validate/query");
			if (descriptor._QueryStringQuery.IsNullOrEmpty())
				return this._Validate(path, stringQuery);
			return this._ValidateQueryString(path);
		}
		private IValidateResponse _Validate(string path, string query)
		{
			var status = this.Connection.PostSync(path, query);
			var r = this.ToParsedResponse<ValidateResponse>(status);
			return r;
		}
		private IValidateResponse _ValidateQueryString(string path)
		{
			var status = this.Connection.GetSync(path);
			var r = this.ToParsedResponse<ValidateResponse>(status);
			return r;
		}
	}
}
