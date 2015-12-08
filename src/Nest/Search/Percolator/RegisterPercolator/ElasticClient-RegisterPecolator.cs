using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Register a percolator
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, will also be used to strongly type the query</typeparam>
		/// <param name="name">The name for the percolator</param>
		/// <param name="selector">An optional descriptor describing the register percolator operation further</param>
		IRegisterPercolateResponse RegisterPercolator<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest request);

		/// <inheritdoc/>
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRegisterPercolateResponse RegisterPercolator<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class =>
			this.RegisterPercolator(selector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		public IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest request) => 
			this.Dispatcher.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse>(
				request,
				this.LowLevelDispatch.IndexDispatch<RegisterPercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
			where T : class => 
			this.RegisterPercolatorAsync(selector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request) => 
			this.Dispatcher.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
				request,
				this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolateResponse>
			);
	}
}