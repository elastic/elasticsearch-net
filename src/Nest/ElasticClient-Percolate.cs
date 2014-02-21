using System;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IUnregisterPercolateResponse UnregisterPercolator(string name, Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<UnregisterPercolatorDescriptor, DeleteQueryString, UnregisterPercolateResponse>(
				s => selector(s.Name(name)),
				(p, d) => this.RawDispatch.DeleteDispatch(p),
				allow404: true
			);
		}
		
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(string name, Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<UnregisterPercolatorDescriptor, DeleteQueryString, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
				s => selector(s.Name(name)),
				(p, d) => this.RawDispatch.DeleteDispatchAsync(p),
				allow404: true
			);
		}
		

		public IRegisterPercolateResponse RegisterPercolator<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) 
			where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.Dispatch<RegisterPercolatorDescriptor<T>, IndexQueryString, RegisterPercolateResponse>(
				s => percolatorSelector(s.Name(name)),
				(p, d) => this.RawDispatch.IndexDispatch(p, d._RequestBody)
			);
		}
	
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			percolatorSelector.ThrowIfNull("percolatorSelector");
			return this.DispatchAsync<RegisterPercolatorDescriptor<T>, IndexQueryString, RegisterPercolateResponse, IRegisterPercolateResponse>(
				s => percolatorSelector(s.Name(name)),
				(p, d) => this.RawDispatch.IndexDispatchAsync(p, d._RequestBody)
			);
			
		}


		public IPercolateResponse Percolate<T>(T @object, Func<PercolateDescriptor<T, T>, PercolateDescriptor<T, T>> percolateSelector = null)
			where T : class
		{
			return this.Percolate<T, T>(@object, percolateSelector);
		}

		public Task<IPercolateResponse> PercolateAsync<T>(T @object, Func<PercolateDescriptor<T, T>, PercolateDescriptor<T, T>> percolateSelector = null)
			where T : class
		{
			return this.PercolateAsync<T, T>(@object, percolateSelector);
		}


		public IPercolateResponse Percolate<T, K>(K @object, Func<PercolateDescriptor<T, K>, PercolateDescriptor<T, K>> percolateSelector = null)
			where T : class
			where K : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatch<PercolateDescriptor<T, K>, PercolateQueryString, PercolateResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.PercolateDispatch(p, d)
			);
		}

		public Task<IPercolateResponse> PercolateAsync<T, K>(K @object, Func<PercolateDescriptor<T, K>, PercolateDescriptor<T, K>> percolateSelector = null)
			where T : class
			where K : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.DispatchAsync<PercolateDescriptor<T, K>, PercolateQueryString, PercolateResponse, IPercolateResponse>(
				s => percolateSelector(s.Object(@object)),
				(p, d) => this.RawDispatch.PercolateDispatchAsync(p, d)
			);	
		}
	}
}
