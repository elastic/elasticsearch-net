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
		IAcknowledgedResponse PutSearchTemplate(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse PutSearchTemplate(IPutSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse PutSearchTemplate(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector) =>
			this.PutSearchTemplate(selector?.Invoke(new PutSearchTemplateDescriptor(id)));

		/// <inheritdoc/>
		public IAcknowledgedResponse PutSearchTemplate(IPutSearchTemplateRequest request) => 
			this.Dispatcher.Dispatch<IPutSearchTemplateRequest, PutSearchTemplateRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.PutTemplateDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector) => 
			this.PutSearchTemplateAsync(selector?.Invoke(new PutSearchTemplateDescriptor(id)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request) => 
			this.Dispatcher.DispatchAsync<IPutSearchTemplateRequest, PutSearchTemplateRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.PutTemplateDispatchAsync<AcknowledgedResponse>
			);
	}
}
