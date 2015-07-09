using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IRegisterPercolateResponse RegisterPercolator<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector)
			where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.Dispatcher.Dispatch<RegisterPercolatorDescriptor<T>, IndexRequestParameters, RegisterPercolateResponse>(
				s => percolatorSelector(s.Name(name)),
				(p, d) => this.LowLevelDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest)
		{
			return this.Dispatcher.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse>(
				registerPercolatorRequest,
				(p, d) => this.LowLevelDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) 
			where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.Dispatcher.DispatchAsync
				<RegisterPercolatorDescriptor<T>, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
					s => percolatorSelector(s.Name(name)),
					(p, d) => this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
				);
		}

		/// <inheritdoc />
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest) 
		{
			return this.Dispatcher.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
					registerPercolatorRequest,
					(p, d) => this.LowLevelDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
				);
		}

	}
}