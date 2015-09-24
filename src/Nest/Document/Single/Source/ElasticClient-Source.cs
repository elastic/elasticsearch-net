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
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="getSelector">A descriptor that describes which document's source to fetch</param>
		T Source<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> getSelector = null) where T : class;

		/// <inheritdoc/>
		T Source<T>(ISourceRequest sourceRequest) where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> getSelector = null) where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(ISourceRequest sourceRequest) where T : class;

	}

	//TODO I Deleted SourceExtensions, when we introduced Document as a parameter folks can do 
	//Source(Document.Index("a").Type("x").Id("1"), s=>s)
	//Source(Document.Infer(doc), s=>s)
	//Source(Document.Index<T>().Type<TOptional>().Id(2), s=>s)

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public T Source<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> getSelector = null) where T : class =>
			this.Source<T>(getSelector.InvokeOrDefault(new SourceDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public T Source<T>(ISourceRequest sourceRequest) where T : class
		{
			sourceRequest.RouteValues.Resolve(ConnectionSettings); 
			var response = this.LowLevelDispatch.GetSourceDispatch<T>(sourceRequest);
			return response.Body;
		}

		/// <inheritdoc/>
		public Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> getSelector = null) where T : class =>
			this.SourceAsync<T>(getSelector.InvokeOrDefault(new SourceDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<T> SourceAsync<T>(ISourceRequest sourceRequest) where T : class
		{
			sourceRequest.RouteValues.Resolve(ConnectionSettings); 
			var response = this.LowLevelDispatch.GetSourceDispatchAsync<T>(sourceRequest)
				.ContinueWith(t => t.Result.Body, TaskContinuationOptions.ExecuteSynchronously);
			return response;
		}

	}
}