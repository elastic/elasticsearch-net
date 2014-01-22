using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IUnregisterPercolateResponse UnregisterPercolator(Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector)
		{
			return this.Dispatch<UnregisterPercolatorDescriptor, DeleteQueryString, UnregisterPercolateResponse>(
				selector,
				(p, d) => this.RawDispatch.DeleteDispatch(p)
			);
		}
		
		public Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector)
		{
			return this.DispatchAsync<UnregisterPercolatorDescriptor, DeleteQueryString, UnregisterPercolateResponse, IUnregisterPercolateResponse>(
				selector,
				(p, d) => this.RawDispatch.DeleteDispatchAsync(p)
			);
		}
		

		public IRegisterPercolateResponse RegisterPercolator<T>(
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			return this.Dispatch<RegisterPercolatorDescriptor<T>, IndexQueryString, RegisterPercolateResponse>(
				percolatorSelector,
				(p, d) => this.RawDispatch.IndexDispatch(p, d)
			);
		}
	
		public Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class
		{
			return this.DispatchAsync<RegisterPercolatorDescriptor<T>, IndexQueryString, RegisterPercolateResponse, IRegisterPercolateResponse>(
				percolatorSelector,
				(p, d) => this.RawDispatch.IndexDispatchAsync(p, d)
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
