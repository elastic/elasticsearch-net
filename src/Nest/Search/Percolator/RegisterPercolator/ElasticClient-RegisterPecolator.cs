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
		IRegisterPercolateResponse RegisterPercolator<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class;

		/// <inheritdoc/>
		IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest);

		/// <inheritdoc/>
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRegisterPercolateResponse RegisterPercolator<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class =>
			this.RegisterPercolator(percolatorSelector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		public IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest) => 
			this.Dispatcher.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse>(
				registerPercolatorRequest,
				this.LowLevelDispatch.IndexDispatch<RegisterPercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> percolatorSelector)
			where T : class => 
			this.RegisterPercolatorAsync(percolatorSelector.InvokeOrDefault(new RegisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest) => 
			this.Dispatcher.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
				registerPercolatorRequest,
				this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolateResponse>
			);
	}
}