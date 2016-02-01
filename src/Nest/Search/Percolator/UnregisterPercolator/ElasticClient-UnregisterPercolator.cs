using System;
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
		IUnregisterPercolatorResponse UnregisterPercolator<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		IUnregisterPercolatorResponse UnregisterPercolator(IUnregisterPercolatorRequest request);

		/// <inheritdoc/>
		Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request);
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IUnregisterPercolatorResponse UnregisterPercolator<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class =>
			this.UnregisterPercolator(selector.InvokeOrDefault(new UnregisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		public IUnregisterPercolatorResponse UnregisterPercolator(IUnregisterPercolatorRequest request) => 
			this.Dispatcher.Dispatch<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolatorResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteDispatch<UnregisterPercolatorResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null)
			where T : class => 
			this.UnregisterPercolatorAsync(selector.InvokeOrDefault(new UnregisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc/>
		public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request) => 
			this.Dispatcher.DispatchAsync<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolatorResponse, IUnregisterPercolatorResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<UnregisterPercolatorResponse>(p)
			);
	}
}