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
				s => selector(s.Name(name).RequestConfiguration(r=>r.AllowedStatusCodes(404))),
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
					s => selector(s.Name(name).RequestConfiguration(r=>r.AllowedStatusCodes(404))),
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
				(p, d) => this.RawDispatch.IndexDispatch<RegisterPercolateResponse>(p, d)
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
					(p, d) => this.RawDispatch.IndexDispatchAsync<RegisterPercolateResponse>(p, d)
				);
		}

		/// <inheritdoc />
		public IPercolateResponse Percolate<T>(T @object, Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector = null)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatch<PercolateDescriptor<T>, PercolateRequestParameters, PercolateResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.PercolateDispatch<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IPercolateResponse> PercolateAsync<T>(T @object, Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector = null)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateDescriptor<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.PercolateDispatchAsync<PercolateResponse>(p, d)
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
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(T @object, Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector = null)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}
	}
}