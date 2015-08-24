using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, MultiPercolateDescriptor> multiPercolateSelector) => 
			this.Dispatcher.Dispatch<MultiPercolateDescriptor, MultiPercolateRequestParameters, MultiPercolateResponse>(
				multiPercolateSelector, this.LowLevelDispatch.MpercolateDispatch<MultiPercolateResponse>
			);

		/// <inheritdoc/>
		public IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest multiRequest) => 
			this.Dispatcher.Dispatch<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse>(
				multiRequest, this.LowLevelDispatch.MpercolateDispatch<MultiPercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, MultiPercolateDescriptor> multiPercolateSelector) => 
			this.Dispatcher.DispatchAsync<MultiPercolateDescriptor, MultiPercolateRequestParameters, MultiPercolateResponse, IMultiPercolateResponse>(
				multiPercolateSelector, this.LowLevelDispatch.MpercolateDispatchAsync<MultiPercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest multiPercolateRequest) => 
			this.Dispatcher.DispatchAsync<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse, IMultiPercolateResponse>(
				multiPercolateRequest, this.LowLevelDispatch.MpercolateDispatchAsync<MultiPercolateResponse>
			);
	}
}