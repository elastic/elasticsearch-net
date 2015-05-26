using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public T Source<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector) where T : class
		{
			var descriptor = getSelector(new SourceDescriptor<T>());
			var pathInfo = ((IPathInfo<SourceRequestParameters>) descriptor).ToPathInfo(_connectionSettings); 
			var response = this.RawDispatch.GetSourceDispatch<T>(pathInfo);
			return response.Response;
		}

		/// <inheritdoc />
		public T Source<T>(ISourceRequest sourceRequest) where T : class
		{
			var pathInfo = sourceRequest.ToPathInfo(_connectionSettings); 
			var response = this.RawDispatch.GetSourceDispatch<T>(pathInfo);
			return response.Response;
		}

		/// <inheritdoc />
		public Task<T> SourceAsync<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector) where T : class
		{
			var descriptor = getSelector(new SourceDescriptor<T>());
			var pathInfo = ((IPathInfo<SourceRequestParameters>) descriptor).ToPathInfo(_connectionSettings);
			var response = this.RawDispatch.GetSourceDispatchAsync<T>(pathInfo)
				.ContinueWith(t =>
				{
					if (t.IsFaulted && t.Exception != null)
					{
						t.Exception.Flatten().InnerException.RethrowKeepingStackTrace();
						return null; //won't be hit
					}
					return t.Result.Response;
				});
			return response;
		}

		/// <inheritdoc />
		public Task<T> SourceAsync<T>(ISourceRequest sourceRequest) where T : class
		{
			var pathInfo = sourceRequest.ToPathInfo(_connectionSettings);
			var response = this.RawDispatch.GetSourceDispatchAsync<T>(pathInfo)
				.ContinueWith(t =>
				{
					if (t.IsFaulted && t.Exception != null) 
					{
						t.Exception.Flatten().InnerException.RethrowKeepingStackTrace();
						return null; //won't be hit
					}
					return t.Result.Response;
				});
			return response;
		}

	}
}