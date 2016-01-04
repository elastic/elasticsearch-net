using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutSearchTemplateResponse PutSearchTemplate(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector);

		/// <inheritdoc/>
		IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector);

		/// <inheritdoc/>
		Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutSearchTemplateResponse PutSearchTemplate(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector) =>
			this.PutSearchTemplate(selector?.Invoke(new PutSearchTemplateDescriptor(id)));

		/// <inheritdoc/>
		public IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IPutSearchTemplateRequest, PutSearchTemplateRequestParameters, PutSearchTemplateResponse>(
				request,
				this.LowLevelDispatch.PutTemplateDispatch<PutSearchTemplateResponse>
			);

		/// <inheritdoc/>
		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector) => 
			this.PutSearchTemplateAsync(selector?.Invoke(new PutSearchTemplateDescriptor(id)));

		/// <inheritdoc/>
		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IPutSearchTemplateRequest, PutSearchTemplateRequestParameters, PutSearchTemplateResponse, IPutSearchTemplateResponse>(
				request,
				this.LowLevelDispatch.PutTemplateDispatchAsync<PutSearchTemplateResponse>
			);
	}
}
