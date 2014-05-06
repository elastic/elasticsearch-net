using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUnregisterPercolateResponse UnregisterPercolator(string name,
			Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<UnregisterPercolatorDescriptor, DeleteRequestParameters, UnregisterPercolateResponse>(
				s => selector(s.Name(name).RequestConfiguration(r=>r.AllowStatusCodes(404))),
				(p, d) => this.RawDispatch.DeleteDispatch<UnregisterPercolateResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(string name,
			Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync
				<UnregisterPercolatorDescriptor, DeleteRequestParameters, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
					s => selector(s.Name(name).RequestConfiguration(r=>r.AllowStatusCodes(404))),
					(p, d) => this.RawDispatch.DeleteDispatchAsync<UnregisterPercolateResponse>(p)
				);
		}

		/// <inheritdoc />
		public IRegisterPercolateResponse RegisterPercolator<T>(string name,
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector)
			where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.Dispatch<RegisterPercolatorDescriptor<T>, IndexRequestParameters, RegisterPercolateResponse>(
				s => percolatorSelector(s.Name(name)),
				(p, d) => this.RawDispatch.IndexDispatch<RegisterPercolateResponse>(p, d._RequestBody)
			);
		}

		/// <inheritdoc />
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name,
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.DispatchAsync
				<RegisterPercolatorDescriptor<T>, IndexRequestParameters, RegisterPercolateResponse, IRegisterPercolateResponse>(
					s => percolatorSelector(s.Name(name)),
					(p, d) => this.RawDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d._RequestBody)
				);
		}

		/// <inheritdoc />
		public IPercolateResponse Percolate<T>(T @object, Func<PercolateDescriptor<T, T>, PercolateDescriptor<T, T>> percolateSelector = null)
			where T : class
		{
			return this.Percolate<T, T>(@object, percolateSelector);
		}

		/// <inheritdoc />
		public Task<IPercolateResponse> PercolateAsync<T>(T @object, Func<PercolateDescriptor<T, T>, PercolateDescriptor<T, T>> percolateSelector = null)
			where T : class
		{
			return this.PercolateAsync<T, T>(@object, percolateSelector);
		}

		/// <inheritdoc />
		public IPercolateResponse Percolate<T, K>(K @object, Func<PercolateDescriptor<T, K>, PercolateDescriptor<T, K>> percolateSelector = null)
			where T : class
			where K : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatch<PercolateDescriptor<T, K>, PercolateRequestParameters, PercolateResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.PercolateDispatch<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateResponse> PercolateAsync<T, K>(K @object, Func<PercolateDescriptor<T, K>, PercolateDescriptor<T, K>> percolateSelector = null)
			where T : class
			where K : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateDescriptor<T, K>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.PercolateDispatchAsync<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IPercolateCountResponse PercolateCount<T>(T @object, Func<PercolateCountDescriptor<T, T>, PercolateCountDescriptor<T, T>> percolateSelector = null)
			where T : class
		{
			return this.PercolateCount<T, T>(@object, percolateSelector);
		}

		/// <inheritdoc />
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(T @object, Func<PercolateCountDescriptor<T, T>, PercolateCountDescriptor<T, T>> percolateSelector = null)
			where T : class
		{
			return this.PercolateCountAsync<T, T>(@object, percolateSelector);
		}
		
		/// <inheritdoc />
		public IPercolateCountResponse PercolateCount<T, K>(K @object, Func<PercolateCountDescriptor<T, K>, PercolateCountDescriptor<T, K>> percolateSelector = null)
			where T : class
			where K : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatch<PercolateCountDescriptor<T, K>, PercolateCountRequestParameters, PercolateCountResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.CountPercolateDispatch<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateCountResponse> PercolateCountAsync<T, K>(K @object, Func<PercolateCountDescriptor<T, K>, PercolateCountDescriptor<T, K>> percolateSelector = null)
			where T : class
			where K : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateCountDescriptor<T, K>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}
	}
}