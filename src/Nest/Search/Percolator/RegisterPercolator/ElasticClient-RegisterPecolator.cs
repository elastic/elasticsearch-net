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
		/// <param name="percolatorSelector">An optional descriptor describing the register percolator operation further</param>
		IRegisterPercolateResponse RegisterPercolator<T>(string name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class;

		/// <inheritdoc/>
		IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest);

		/// <inheritdoc/>
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRegisterPercolateResponse RegisterPercolator<T>(string name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class => 
			this.Dispatcher.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse>(
				percolatorSelector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>().Name(name)),
				(p, d) => this.LowLevelDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
			);

		/// <inheritdoc/>
		public IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest) => 
			this.Dispatcher.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse>(
				registerPercolatorRequest,
				(p, d) => this.LowLevelDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class => 
			this.Dispatcher.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
				percolatorSelector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>().Name(name)),
				(p, d) => this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest) => 
			this.Dispatcher.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
				registerPercolatorRequest,
				(p, d) => this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
			);
	}
}