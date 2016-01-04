using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.IO;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Use the /{index}/{type}/{id} to get the document and its metadata
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">A descriptor that describes which document's source to fetch</param>
		IGetResponse<T> Get<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IGetResponse<T> Get<T>(IGetRequest request) where T : class;

		/// <inheritdoc/>
		Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<IGetResponse<T>> GetAsync<T>(IGetRequest request) where T : class;

	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IGetResponse<T> Get<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector = null) where T : class =>
			this.Get<T>(selector.InvokeOrDefault(new GetDescriptor<T>(document)));

		/// <inheritdoc/>
		public IGetResponse<T> Get<T>(IGetRequest request) where T : class =>
			this.Dispatcher.Dispatch<IGetRequest, GetRequestParameters, GetResponse<T>>(
				request,
				(p, d) => this.LowLevelDispatch.GetDispatch<GetResponse<T>>(p)
			);

		/// <inheritdoc/>
		public Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector = null) where T : class =>
			this.GetAsync<T>(selector.InvokeOrDefault(new GetDescriptor<T>(document)));

		/// <inheritdoc/>
		public Task<IGetResponse<T>> GetAsync<T>(IGetRequest request) where T : class =>
			this.Dispatcher.DispatchAsync<IGetRequest, GetRequestParameters, GetResponse<T>, IGetResponse<T>>(
				request,
				(p, d) => this.LowLevelDispatch.GetDispatchAsync<GetResponse<T>>(p)
			);
	}
}