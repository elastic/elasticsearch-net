using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		public IUpdateResponse Update<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class
		{
			return this.Update<T, T>(updateSelector);
		}
		public IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector) 
			where T : class
			where K : class
		{
			var updateDescriptor = updateSelector(new UpdateDescriptor<T, K>());
			var pathInfo = updateDescriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.UpdateDispatch(pathInfo, updateDescriptor)
				.Deserialize<UpdateResponse>();
		}
		
		public Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class
		{
			return this.UpdateAsync<T, T>(updateSelector);
		}
		public Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector) 
			where T : class
			where K : class
		{
			var updateDescriptor = updateSelector(new UpdateDescriptor<T, K>());
			var pathInfo = updateDescriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.UpdateDispatchAsync(pathInfo, updateDescriptor)
				.ContinueWith(r => r.Result.Deserialize<UpdateResponse>() as IUpdateResponse);
		}
	}
}
