using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutSearchTemplateResponse PutSearchTemplate(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutSearchTemplateResponse PutSearchTemplate(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector = null) =>
			this.PutSearchTemplate(selector.InvokeOrDefault(new PutSearchTemplateDescriptor(id)));

		/// <inheritdoc/>
		public IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IPutSearchTemplateRequest, PutSearchTemplateRequestParameters, PutSearchTemplateResponse>(
				request,
				this.LowLevelDispatch.PutTemplateDispatch<PutSearchTemplateResponse>
			);

		/// <inheritdoc/>
		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector = null) => 
			this.PutSearchTemplateAsync(selector.InvokeOrDefault(new PutSearchTemplateDescriptor(id)));

		/// <inheritdoc/>
		public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IPutSearchTemplateRequest, PutSearchTemplateRequestParameters, PutSearchTemplateResponse, IPutSearchTemplateResponse>(
				request,
				this.LowLevelDispatch.PutTemplateDispatchAsync<PutSearchTemplateResponse>
			);
	}
}
