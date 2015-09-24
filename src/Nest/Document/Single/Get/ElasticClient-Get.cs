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
		/// Use the /{index}/{type}/{id} to get the document and its metadata
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="getSelector">A descriptor that describes which document's source to fetch</param>
		IGetResponse<T> Get<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> getSelector = null) where T : class;

		/// <inheritdoc/>
		IGetResponse<T> Get<T>(IGetRequest getRequest) where T : class;

		/// <inheritdoc/>
		Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> getSelector = null) where T : class;

		/// <inheritdoc/>
		Task<IGetResponse<T>> GetAsync<T>(IGetRequest getRequest) where T : class;

	}

	public partial class ElasticClient
	{

		//TODO I Deleted GetExtensions, when we introduced Document as a parameter folks can do 
		//Source(Document.Index("a").Type("x").Id("1"), s=>s)
		//Source(Document.Infer(doc), s=>s)
		//Source(Document.Index<T>().Type<TOptional>().Id(2), s=>s)
		//Source(Document.Id<T>(2), s=>s)

		/// <inheritdoc/>
		public IGetResponse<T> Get<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> getSelector = null) where T : class =>
			this.Get<T>(getSelector.InvokeOrDefault(new GetDescriptor<T>(document)));

		/// <inheritdoc/>
		public IGetResponse<T> Get<T>(IGetRequest getRequest) where T : class => 
			this.Dispatcher.Dispatch<IGetRequest, GetRequestParameters, GetResponse<T>>(
				getRequest,
				(p, d) => this.LowLevelDispatch.GetDispatch<GetResponse<T>>(p)
			);

		/// <inheritdoc/>
		public Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> getSelector = null) where T : class=>
			this.GetAsync<T>(getSelector.InvokeOrDefault(new GetDescriptor<T>(document)));

		/// <inheritdoc/>
		public Task<IGetResponse<T>> GetAsync<T>(IGetRequest getRequest) where T : class => 
			this.Dispatcher.DispatchAsync<IGetRequest, GetRequestParameters, GetResponse<T>, IGetResponse<T>>(
				getRequest,
				(p, d) => this.LowLevelDispatch.GetDispatchAsync<GetResponse<T>>(p)
			);
	}
}