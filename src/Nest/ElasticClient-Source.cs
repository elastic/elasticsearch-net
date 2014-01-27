using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{

	public partial class ElasticClient
	{
		public FieldSelection<T> SourceFields<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class
		{
			return this.Dispatch<GetDescriptor<T>, GetQueryString, GetResponse<T>>(
				getSelector,
				(p, d) => this.RawDispatch.GetDispatch(p)
			).Fields;
		}
		
		public Task<FieldSelection<T>> SourceFieldsAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class
		{
			return this.DispatchAsync<GetDescriptor<T>, GetQueryString, GetResponse<T>, IGetResponse<T>>(
				getSelector,
				(p, d) => this.RawDispatch.GetDispatchAsync(p)
			).ContinueWith(t=>t.Result.Fields);
		}
		
		//TODO replace with actual call to _source in es 1.0 and NEST 1.0
		public T Source<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class
		{
			return this.Dispatch<GetDescriptor<T>, GetQueryString, GetResponse<T>>(
				getSelector,
				(p, d) => this.RawDispatch.GetDispatch(p)
			).Source;
		}
		
		public Task<T> SourceAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class
		{
			return this.DispatchAsync<GetDescriptor<T>, GetQueryString, GetResponse<T>, IGetResponse<T>>(
				getSelector,
				(p, d) => this.RawDispatch.GetDispatchAsync(p)
			).ContinueWith(t=>t.Result.Source);
		}
	
		
	}
}
		
