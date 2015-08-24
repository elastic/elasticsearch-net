using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IUnregisterPercolateResponse UnregisterPercolator<T>(string name, Func<UnregisterPercolatorDescriptor<T>, UnregisterPercolatorDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, UnregisterPercolateResponse>(
				s => selector(s.Name(name)),
				(p, d) => this.LowLevelDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);
		}

		/// <inheritdoc/>
		public IUnregisterPercolateResponse UnregisterPercolator(IUnregisterPercolatorRequest unregisterPercolatorRequest)
		{
			return this.Dispatcher.Dispatch<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse>(
				unregisterPercolatorRequest,
				(p, d) => this.LowLevelDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync<T>(string name, Func<UnregisterPercolatorDescriptor<T>, UnregisterPercolatorDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
					s => selector(s.Name(name)),
					(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
				);
		}

		/// <inheritdoc/>
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest unregisterPercolatorRequest)
		{
			return this.Dispatcher.DispatchAsync<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
					unregisterPercolatorRequest,
					(p, d) => this.LowLevelDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
				);
		}

	}
}