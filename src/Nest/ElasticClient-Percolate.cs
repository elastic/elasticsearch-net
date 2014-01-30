using System;
using System.Linq;
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
		

		public IRegisterPercolateResponse RegisterPercolator<T>(
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			return this.Dispatch<RegisterPercolatorDescriptor<T>, IndexQueryString, RegisterPercolateResponse>(
				percolatorSelector,
				(p, d) => this.RawDispatch.IndexDispatch(p, d._RequestBody)
			);
		}
	
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			return this.DispatchAsync<RegisterPercolatorDescriptor<T>, IndexQueryString, RegisterPercolateResponse, IRegisterPercolateResponse>(
				percolatorSelector,
				(p, d) => this.RawDispatch.IndexDispatchAsync(p, d._RequestBody)
			);
			
		}

		public IPercolateResponse Percolate<T>(
			Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class
		{
			return this.Dispatch<PercolateDescriptor<T>, PercolateQueryString, PercolateResponse>(
				percolateSelector,
				(p, d) => this.RawDispatch.PercolateDispatch(p, d)
			);
		}

		

		public Task<IPercolateResponse> PercolateAsync<T>(
			Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class
		{
			return this.DispatchAsync<PercolateDescriptor<T>, PercolateQueryString, PercolateResponse, IPercolateResponse>(
				percolateSelector,
				(p, d) => this.RawDispatch.PercolateDispatchAsync(p, d)
			);	
		}
	}
}
