using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc />
		public IUnregisterPercolateResponse UnregisterPercolator<T>(string name, Func<UnregisterPercolatorDescriptor<T>, UnregisterPercolatorDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.Dispatch<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, UnregisterPercolateResponse>(
				s => selector(s.Name(name)),
				(p, d) => this.RawDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);
		}

		/// <inheritdoc />
		public IUnregisterPercolateResponse UnregisterPercolator(IUnregisterPercolatorRequest unregisterPercolatorRequest)
		{
			return this.Dispatch<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse>(
				unregisterPercolatorRequest,
				(p, d) => this.RawDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync<T>(string name, Func<UnregisterPercolatorDescriptor<T>, UnregisterPercolatorDescriptor<T>> selector = null)
			where T : class
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<UnregisterPercolatorDescriptor<T>, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
					s => selector(s.Name(name)),
					(p, d) => this.RawDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest unregisterPercolatorRequest)
		{
			return this.DispatchAsync<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
					unregisterPercolatorRequest,
					(p, d) => this.RawDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
				);
		}



		/// <inheritdoc />
		public IRegisterPercolateResponse RegisterPercolator<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector)
			where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.Dispatch<RegisterPercolatorDescriptor<T>, IndexRequestParameters, RegisterPercolateResponse>(
				s => percolatorSelector(s.Name(name)),
				(p, d) => this.RawDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest)
		{
			return this.Dispatch<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse>(
				registerPercolatorRequest,
				(p, d) => this.RawDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) 
			where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.DispatchAsync
				<RegisterPercolatorDescriptor<T>, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
					s => percolatorSelector(s.Name(name)),
					(p, d) => this.RawDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
				);
		}

		/// <inheritdoc />
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest) 
		{
			return this.DispatchAsync<IRegisterPercolatorRequest, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
					registerPercolatorRequest,
					(p, d) => this.RawDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
				);
		}


		/// <inheritdoc />
		public IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatch<PercolateDescriptor<T>, PercolateRequestParameters, PercolateResponse>(
				percolateSelector,
				(p, d) => this.RawDispatch.PercolateDispatch<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IPercolateResponse Percolate<T>(IPercolateRequest<T> percolateRequest)
			where T : class
		{
			return this.Dispatch<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse>(
				percolateRequest,
				(p, d) => this.RawDispatch.PercolateDispatch<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateDescriptor<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				percolateSelector,
				(p, d) => this.RawDispatch.PercolateDispatchAsync<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> percolateRequest)
			where T : class
		{
			return this.DispatchAsync<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				percolateRequest,
				(p, d) => this.RawDispatch.PercolateDispatchAsync<PercolateResponse>(p, d)
			);
		}



		/// <inheritdoc />
		public IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector)
			where T : class
		{
			return this.Dispatch<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				percolateSelector,
				(p, d) => this.RawDispatch.CountPercolateDispatch<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IPercolateCountResponse PercolateCount<T>(T @object, Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector = null)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatch<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.CountPercolateDispatch<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class
		{
			return this.Dispatch<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				percolateCountRequest,
				(p, d) => this.RawDispatch.CountPercolateDispatch<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector)
			where T : class
		{
			return this.DispatchAsync<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				percolateSelector,
				(p, d) => this.RawDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(T @object, Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector = null)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class
		{
			return this.DispatchAsync<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				percolateCountRequest,
				(p, d) => this.RawDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}

	}
}