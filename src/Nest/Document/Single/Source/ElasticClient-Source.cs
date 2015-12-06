using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document, 
		/// without any additional content around it. 
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		T Source<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector = null) where T : class;

		/// <inheritdoc/>
		T Source<T>(ISourceRequest request) where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(ISourceRequest request) where T : class;

	}

	//TODO I Deleted SourceExtensions, when we introduced Document as a parameter folks can do 
	//Source(Document.Index("a").Type("x").Id("1"), s=>s)
	//Source(Document.Infer(doc), s=>s)
	//Source(Document.Index<T>().Type<TOptional>().Id(2), s=>s)

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public T Source<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector = null) where T : class =>
			this.Source<T>(selector.InvokeOrDefault(new SourceDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public T Source<T>(ISourceRequest request) where T : class
		{
			request.RouteValues.Resolve(ConnectionSettings); 
			var response = this.LowLevelDispatch.GetSourceDispatch<T>(request);
			return response.Body;
		}

		/// <inheritdoc/>
		public Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector = null) where T : class =>
			this.SourceAsync<T>(selector.InvokeOrDefault(new SourceDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<T> SourceAsync<T>(ISourceRequest request) where T : class
		{
			request.RouteValues.Resolve(ConnectionSettings); 
			var response = this.LowLevelDispatch.GetSourceDispatchAsync<T>(request)
				.ContinueWith(t => t.Result.Body, TaskContinuationOptions.ExecuteSynchronously);
			return response;
		}

	}
}