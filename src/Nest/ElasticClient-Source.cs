using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{

	public partial class ElasticClient
	{
		public T Source<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector) where T : class
		{
			var descriptor = getSelector(new SourceDescriptor<T>());
			var pathInfo = ((IPathInfo<SourceQueryString>)descriptor).ToPathInfo(_connectionSettings);
			var response = this.RawDispatch.GetSourceDispatch<T>(pathInfo);
			return response.Response;
		}
		
		public Task<T> SourceAsync<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector) where T : class
		{
			var descriptor = getSelector(new SourceDescriptor<T>());
			var pathInfo = ((IPathInfo<SourceQueryString>)descriptor).ToPathInfo(_connectionSettings);
			var response = this.RawDispatch.GetSourceDispatchAsync<T>(pathInfo)
				.ContinueWith(t=>t.Result.Response);
			return response;
		}
	
		
	}
}
		
