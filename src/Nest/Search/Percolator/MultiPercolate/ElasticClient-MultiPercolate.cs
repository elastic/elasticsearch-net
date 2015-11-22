using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, IMultiPercolateRequest> multiPercolateSelector);

		/// <inheritdoc/>
		IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest multiRequest);

		/// <inheritdoc/>
		Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> multiPercolateSelector);

		/// <inheritdoc/>
		Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest multiPercolateRequest);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, IMultiPercolateRequest> multiPercolateSelector) =>
			this.MultiPercolate(multiPercolateSelector?.Invoke(new MultiPercolateDescriptor()));

		/// <inheritdoc/>
		public IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest multiRequest) => 
			this.Dispatcher.Dispatch<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse>(
				multiRequest, this.LowLevelDispatch.MpercolateDispatch<MultiPercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> multiPercolateSelector) => 
			this.MultiPercolateAsync(multiPercolateSelector?.Invoke(new MultiPercolateDescriptor()));

		/// <inheritdoc/>
		public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest multiPercolateRequest) => 
			this.Dispatcher.DispatchAsync<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse, IMultiPercolateResponse>(
				multiPercolateRequest, this.LowLevelDispatch.MpercolateDispatchAsync<MultiPercolateResponse>
			);
	}
}