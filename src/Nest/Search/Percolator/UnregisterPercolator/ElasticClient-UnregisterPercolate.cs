using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Unregister a percolator
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <param name="name">The name for the percolator</param>
		/// <param name="selector">An optional descriptor describing the unregister percolator operation further</param>
		IUnregisterPercolateResponse UnregisterPercolator<T>(string name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		IUnregisterPercolateResponse UnregisterPercolator(IUnregisterPercolatorRequest unregisterPercolatorRequest);

		/// <inheritdoc/>
		Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync<T>(string name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest unregisterPercolatorRequest);
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IUnregisterPercolateResponse UnregisterPercolator<T>(string name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class => 
			this.Dispatcher.Dispatch<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse>(
				selector.InvokeOrDefault(new UnregisterPercolatorDescriptor<T>().Name(name)),
				(p, d) => this.LowLevelDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);

		/// <inheritdoc/>
		public IUnregisterPercolateResponse UnregisterPercolator(IUnregisterPercolatorRequest unregisterPercolatorRequest) => 
			this.Dispatcher.Dispatch<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse>(
				unregisterPercolatorRequest,
				(p, d) => this.LowLevelDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync<T>(string name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class => 
			this.Dispatcher.DispatchAsync<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
				selector.InvokeOrDefault(new UnregisterPercolatorDescriptor<T>().Name(name)),
				(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest unregisterPercolatorRequest) => 
			this.Dispatcher.DispatchAsync<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
				unregisterPercolatorRequest,
				(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
			);
	}
}